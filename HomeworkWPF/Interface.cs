using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace HomeworkWPF
{

  public interface IAssembler
  {
    void PerformSteps();
    void LoadCoreStatement(ref Core Statement);
  }

  public interface ICommand
  {
    string Defenition();
    void Operate(ref Core CurrentStatement, string parametrs);
  }

  public interface ICommandData
  {
    String Operation { get; }
  }

  [Export(typeof(IAssembler))]
  class MyTranslator : IAssembler
  {
    [ImportMany]
    IEnumerable<Lazy<ICommand, ICommandData>> operations;

    public Core CurrentStatement;

    public void PerformSteps()
    {
        CommandItem tempCommand = CurrentStatement.Commands[CurrentStatement.CurrentCommand];
        
        int space = tempCommand.Command.IndexOf(' ');
        
        /*Поиск метки*/
        int colon = tempCommand.Command.IndexOf(':');
        string command = "";

        string nameCommand = "";
        string parametrs = "";


          if (colon != -1)
          {
            command = tempCommand.Command.Substring(colon+1);
            command = command.Trim();
            space = command.IndexOf(' ');
          }
          else
          {
            command = tempCommand.Command;
            command = command.Trim();
            space = command.IndexOf(' ');
          }

          if (space != -1)
          {
            nameCommand = command.Substring(0, space);
            parametrs = command.Substring(space + 1);
          }
          else
          {
            nameCommand = command;
            parametrs = "";
          }
          foreach (Lazy<ICommand, ICommandData> i in operations)
          {
            if (String.Equals(nameCommand, i.Metadata.Operation, StringComparison.OrdinalIgnoreCase))
            {
              i.Value.Operate(ref CurrentStatement, parametrs);
              break;
            }
          }
    }

    public void LoadCoreStatement(ref Core Statement)
    {
      CurrentStatement = Statement;
      LoadHelp();
    }

    private void LoadHelp()
    {
      string faq = "FAQ\nSupported commands:\n";
      string defenition;
      foreach (Lazy<ICommand, ICommandData> i in operations)
      {
        defenition = i.Value.Defenition();
        faq += defenition + "\n";
      }
      faq += "Copyright by Grigory Ponomarenko (IU8-34 BMSTU)\n";
      CurrentStatement.Help = faq;
    }
  }


  public class Program
  {
    private CompositionContainer _container;

    [Import(typeof(IAssembler))]
    public IAssembler assembler;

    public Program(string path)
    {
      //An aggregate catalog that combines multiple catalogs
      var catalog = new AggregateCatalog();
      //Adds all the parts found in the same assembly as the Program class
      catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
      catalog.Catalogs.Add(new DirectoryCatalog(path));

      //Create the CompositionContainer with the parts in the catalog
      _container = new CompositionContainer(catalog);

      //Fill the imports of this object
      try
      {
        this._container.ComposeParts(this);
      }
      catch (CompositionException compositionException)
      {
        Console.WriteLine(compositionException.ToString());
      }
      
    }
  }

}
