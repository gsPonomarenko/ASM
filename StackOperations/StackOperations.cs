using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace StackOperations
{
  [Export(typeof(HomeworkWPF.ICommand))]
  [ExportMetadata("Operation", "push")]
  public class Push : HomeworkWPF.ICommand
  {
    private string defenition = "Push value into stack";
    public string Defenition()
    {
      return defenition;
    }

    public void Operate(ref HomeworkWPF.Core CurrentStatement, string parametrs)
    {
      if (parametrs != "")
      {

        string source = parametrs.Trim();

        int value = 0;
        int ifSourceHasCount = source.IndexOfAny(new char[] { '1','2','3','4','5',
                                                '6','7','8','9','0'});
        if (ifSourceHasCount == -1)
        {
          if (source.Length == 3 && source[0] == 'e' && source[2] == 'x')
          {
            for (char c = 'a'; c < 'i'; c++)
            {
              if (source[1] == c)
              {
                value = CurrentStatement.GetRegister(source);
                c = 'i';
              }
              else if (c == 'h')
              {
                throw new HomeworkWPF.ParseCommandException(CurrentStatement.Commands[CurrentStatement.CurrentCommand]);
              }
            }
          }
        }
        else
        {
          value = int.Parse(source);
        }

        CurrentStatement.Push(new HomeworkWPF.StackItem { Data = value, Address = false });
      }
      throw new HomeworkWPF.ParseCommandException(CurrentStatement.Commands[CurrentStatement.CurrentCommand]);

    }
  }

  [Export(typeof(HomeworkWPF.ICommand))]
  [ExportMetadata("Operation", "pop")]
  public class Pop : HomeworkWPF.ICommand
  {
    private string defenition = "Pop value from stack";
    public string Defenition()
    {
      return defenition;
    }

    public void Operate(ref HomeworkWPF.Core CurrentStatement, string parametrs)
    {
      if (parametrs != "")
      {
        string source = parametrs.Trim();

        HomeworkWPF.StackItem item = CurrentStatement.Pop();
        int value = item.Data;

        int ifSourceHasCount = source.IndexOfAny(new char[] { '1','2','3','4','5',
                                                '6','7','8','9','0'});
        if (ifSourceHasCount == -1)
        {
          if (source.Length == 3 && source[0] == 'e' && source[2] == 'x')
          {
            for (char c = 'a'; c < 'i'; c++)
            {
              if (source[1] == c)
              {
                CurrentStatement.SetRegister(source, value);
                return;
              }
            }
          }
        }
      }
      else
      {
        throw new HomeworkWPF.ParseCommandException(CurrentStatement.Commands[CurrentStatement.CurrentCommand]);
      }
    }
  }

}
