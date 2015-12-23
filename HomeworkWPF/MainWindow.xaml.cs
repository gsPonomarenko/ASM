using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.ComponentModel;

namespace HomeworkWPF
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  /// 

  public partial class MainWindow : Window
  {
    public Core CurrentStation;
    public Program CoreProgram;



    public MainWindow()
    {
      InitializeComponent();
      InitializeStack();
      InitializeRegisters();
      InitializePorts();

      CurrentStation = new Core();

      LoadLibraries();
    }

    private void LoadLibraries()
    {
      string path = Environment.CurrentDirectory;
      CoreProgram = new Program(path);
      CoreProgram.assembler.LoadCoreStatement(ref CurrentStation);

      InitializeEvents();

      CancelButton.Visibility = System.Windows.Visibility.Collapsed;
      NextStepButton.Visibility = System.Windows.Visibility.Collapsed;
    }

    private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
    {
      // Конфигурирование диалогового окна
      if (CurrentStation.Commands.Count != 0)
      {
        if (MessageBox.Show("Do you want to close current project?", "Open file",
                             MessageBoxButton.YesNo) == MessageBoxResult.No)
        {
          return;
        }
        CurrentStation.Commands.Clear();
      }
      Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
      dlg.FileName = "example"; // Имя по умолчанию
      dlg.DefaultExt = ".txt"; // Расширение по умолчанию
      dlg.Filter = "Text documents (.txt)|*.txt"; // Фильтрация по расширению

      // Показать диалоговое окно
      Nullable<bool> result = dlg.ShowDialog();

      if (result == true)
      {
        string filepath = dlg.FileName;
        CurrentStation.LoadCommandsFile(filepath);
      } //if
      ReloadCommands();
    }

    private void MenuItemSaveDump_Click(object sender, RoutedEventArgs e)
    {
      BinaryFormatter binFormat = new BinaryFormatter();
      
      // Конфигурирование диалогового окна
      Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
      dlg.FileName = "data"; // Имя по умолчанию
      dlg.DefaultExt = ".dat"; // Расширение по умолчанию
      dlg.Filter = "Saved Dump Data (.dat)|*.dat"; // Фильтрация по расширению

      // Показать диалоговое окно
      Nullable<bool> result = dlg.ShowDialog();


      if (result == true)
      {
        // Прочитать документ
        string filepath = dlg.FileName;
        using (Stream fStream = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
          binFormat.Serialize(fStream, CurrentStation);
        } 
      } 

      MessageBox.Show("Dump saved");
    }

    private void MenuHelp_Click(object sender, RoutedEventArgs e)
    {
      
      MessageBox.Show(CurrentStation.Help);
    }
    private void MenuItemOpenDump_Click(object sender, RoutedEventArgs e)
    {
      BinaryFormatter binFormat = new BinaryFormatter();
      
      // Конфигурирование диалогового окна
      Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
      dlg.FileName = "data"; // Имя по умолчанию
      dlg.DefaultExt = ".dat"; // Расширение по умолчанию
      dlg.Filter = "Saved Dump Data (.dat)|*.dat"; // Фильтрация по расширению

      // Показать диалоговое окно
      Nullable<bool> result = dlg.ShowDialog();


      if (result == true)
      {
        // Прочитать документ
        string filepath = dlg.FileName;
        using (Stream fStream = File.OpenRead(filepath))
        {
          CurrentStation = (Core)binFormat.Deserialize(fStream);
        } 
      } 


      CurrentStation.CurrentCoreEvent = new CoreEvents();
      InitializeEvents();
      CoreProgram.assembler.LoadCoreStatement(ref CurrentStation);
      ViewAllCoreInfo();
    }

    private void InitializeStack()
    {
      /*Инициализация стека*/
      StackView.Items.Clear();
    }

    private void InitializeRegisters()
    {
      /*Инициализация регистров*/
      RegistersView.Items.Clear();

      char c = 'a';
      while (c != 'i')
      {
        RegistersView.Items.Add(new RegisterItem { Name = "e" + c + "x", Value = 0 });
        c++;
      }

      InitializeFlags();
    }

    private void InitializePorts()
    {
      /*Инициализация регистров*/
      PortsView.Items.Clear();

      char c = 'A';
      while (c != 'E')
      {
        PortsView.Items.Add(new PortItem { Name = "port" + c, Value = 0 });
        c++;
      }
    }

    private void InitializeFlags()
    {
      RegistersView.Items.Add(new RegisterItem { Name = "ZF", Value = 0 });
    }

    private void InitializeEvents()
    {
      CurrentStation.CurrentCoreEvent.RegisterChanged += RefreshRegister;
      CurrentStation.CurrentCoreEvent.PortChanged += RefreshPort;
      CurrentStation.CurrentCoreEvent.PushStack += AddToStackList;
      CurrentStation.CurrentCoreEvent.PopStack += RemoveFromStackList;
      CurrentStation.CurrentCoreEvent.ReloadStack += ReloadStack;
      CurrentStation.CurrentCoreEvent.ZeroFlagChanged += RefreshZeroFlag;
      CurrentStation.CurrentCoreEvent.CurItemChanged += SelectItem;
      CurrentStation.CurrentCoreEvent.IfEventsLoaded = true;
    }

    private void ReloadCommands()
    {
      CommandsList.Items.Clear();

      foreach (HomeworkWPF.CommandItem item in CurrentStation.Commands)
      {
        CommandsList.Items.Add(new HomeworkWPF.MainWindow.CommandItem
        {
          Address = item.Address,
          Command = item.Command
        });
      }
    }

    private void ReloadStack()
    {
      StackView.Items.Clear();
      foreach (StackItem i in CurrentStation.MicroStack)
      {
        if (i.Address == false)
        {
          StackView.Items.Add(new StackListItem { Data = i.Data.ToString()});
        }
        else
        {
          StackView.Items.Add(new StackListItem { Data = i.Data.ToString("X") + "h"});
        }
      }
    }

    private void ReloadRegisters()
    {
      char c = 'a';
      while (c != 'i')
      {
        RefreshRegister("e" + c + "x");
        c++;
      }
    }

    private void ReloadPorts()
    {
      char c = 'A';
      while (c != 'E')
      {
        RefreshPort("port" + c);
        c++;
      }
    }

    private void RefreshRegister(string register)
    {
      switch (register)
      {
        case "eax":
          RegistersView.Items[0] = new RegisterItem { Name = "eax", Value = CurrentStation.Eax };
          break;
        case "ebx":
          RegistersView.Items[1] = new RegisterItem { Name = "ebx", Value = CurrentStation.Ebx };
          break;
        case "ecx":
          RegistersView.Items[2] = new RegisterItem { Name = "ecx", Value = CurrentStation.Ecx };
          break;
        case "edx":
          RegistersView.Items[3] = new RegisterItem { Name = "edx", Value = CurrentStation.Edx };
          break;

        case "eex":
          RegistersView.Items[4] = new RegisterItem { Name = "eex", Value = CurrentStation.Eex };
          break;
        case "efx":
          RegistersView.Items[5] = new RegisterItem { Name = "efx", Value = CurrentStation.Efx };
          break;
        case "egx":
          RegistersView.Items[6] = new RegisterItem { Name = "egx", Value = CurrentStation.Egx };
          break;
        case "ehx":
          RegistersView.Items[7] = new RegisterItem { Name = "ehx", Value = CurrentStation.Ehx };
          break;
      }
    }

    private void RefreshPort(string port)
    {
      switch (port)
      {
        case "portA":
          PortsView.Items[0] = new PortItem { Name = "portA", Value = CurrentStation.PortA };
          break;
        case "portB":
          PortsView.Items[1] = new PortItem { Name = "portB", Value = CurrentStation.PortB };
          break;
        case "portC":
          PortsView.Items[2] = new PortItem { Name = "portC", Value = CurrentStation.PortC };
          break;
        case "portD":
          PortsView.Items[3] = new PortItem { Name = "portD", Value = CurrentStation.PortD };
          break;
      }
    }

    private void AddToStackList()
    {
      HomeworkWPF.StackItem item = CurrentStation.MicroStack.First();
      
      if (item.Address == false)
      {
        StackView.Items.Add(new StackListItem { Data = item.Data.ToString() });
      }
      else
      {
        StackView.Items.Add(new StackListItem { Data = item.Data.ToString("X") + "h" });
      }
    }

    private void RemoveFromStackList()
    {
      int i = StackView.Items.Count - 1;
      StackView.Items.RemoveAt(i);
    }

    private void RefreshZeroFlag() 
    {
      RegistersView.Items[8] = new RegisterItem { Name = "ZF", Value = CurrentStation.ZF };
    }

    private void ViewAllCoreInfo()
    {
      ReloadCommands();
      ReloadStack();
      ReloadRegisters();
      ReloadPorts();
      RefreshZeroFlag();

      if (CurrentStation.isDebugging == true)
      {
        StateView.Text = "Debugging";

        StartDebugButton.Visibility = System.Windows.Visibility.Collapsed;
        CancelButton.Visibility = System.Windows.Visibility.Visible;
        NextStepButton.Visibility = System.Windows.Visibility.Visible;
      }
    }

    private void SelectItem()
    {
      if (CommandsList.Items.Count > CurrentStation.CurrentCommand)
      {
        CommandsList.SelectedIndex = CurrentStation.CurrentCommand;
      }
    }


    public class CommandItem
    {
      public int Address { get; set; }
      public string Command { get; set; }
    }

    public class PortItem
    {
      public string Name { get; set; }
      public int Value { get; set; }
    }

    public class RegisterItem
    {
      public string Name { get; set; }
      public int Value { get; set; }
    }

    public class StackListItem
    {
      public string Data { get; set; }
    }


    private void RunButton_Click(object sender, RoutedEventArgs e)
    {
      if (CurrentStation.isDebugging == false)
      {
        CancelButton_Click(sender, e);
        CommandsList.SelectedIndex = 0;
      }
      else
      {
        CurrentStation.isDebugging = false;
      }

      StateView.Text = "Running";

      while (CurrentStation.CurrentCommand < CurrentStation.Commands.Count)
      {
        if (CurrentStation.Commands[CurrentStation.CurrentCommand].Command.Length == 0)
        {
          break;
        }
        try
        {
          CoreProgram.assembler.PerformSteps();
        }
        catch (ParseCommandException ex)
        {
          MessageBox.Show(ex.Message);
          CancelButton_Click(sender, e);
          return;
        }
        CurrentStation.CurrentCommand++;
      }

      CommandsList.SelectedIndex = CurrentStation.CurrentCommand;
      CurrentStation.CurrentCommand = 0;
      StateView.Text = "";

      CancelButton.Visibility = System.Windows.Visibility.Collapsed;
      NextStepButton.Visibility = System.Windows.Visibility.Collapsed;
      StartDebugButton.Visibility = System.Windows.Visibility.Visible;
    }

    private void StartDebugButton_Click(object sender, RoutedEventArgs e)
    {
      if (CurrentStation.Commands.Count > 0)
      {
        CancelButton_Click(sender, e);
        CurrentStation.CurrentCommand = 0;
        CurrentStation.InitByZero();

        CommandsList.SelectedIndex = 0;
        CurrentStation.isDebugging = true;

        StateView.Text = "Debugging";

        StartDebugButton.Visibility = System.Windows.Visibility.Collapsed;
        CancelButton.Visibility = System.Windows.Visibility.Visible;
        NextStepButton.Visibility = System.Windows.Visibility.Visible;
      }
    }

    private void NextStepButton_Click(object sender, RoutedEventArgs e)
    {
      if (CurrentStation.Commands.Count > 0)
      {
        try
        {
          CoreProgram.assembler.PerformSteps();
        }
        catch (ParseCommandException ex)
        {
          MessageBox.Show(ex.Message);
          CancelButton_Click(sender, e);
          return;
        }
        CurrentStation.CurrentCommand++;
        if (CurrentStation.Commands[CurrentStation.CurrentCommand].Command.Length == 0)
        {
          CancelButton_Click(sender, e);
        }
      }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      CurrentStation.CurrentCommand = 0;
      CommandsList.SelectedIndex = -1;
      CurrentStation.InitByZero();
      CurrentStation.isDebugging = false;

      StateView.Text = "";

      CancelButton.Visibility = System.Windows.Visibility.Collapsed;
      NextStepButton.Visibility = System.Windows.Visibility.Collapsed;
      StartDebugButton.Visibility = System.Windows.Visibility.Visible;
    }

    private void MenuItemExit_Click(object sender, RoutedEventArgs e)
    {
      Application.Current.Shutdown();
    }
  }
}