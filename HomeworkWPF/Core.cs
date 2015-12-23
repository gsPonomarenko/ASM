using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace HomeworkWPF
{
  [Serializable]
  public class Core: IPorts, IRegisters, IStack, IFlags
  {
    public Core()
    {
      Commands = new List<CommandItem>();
      MicroStack = new Stack<StackItem>();
      CurrentCoreEvent = new CoreEvents();

      InitByZero();

      isDebugging = false;
      currentCommand = 0;
    }

    public List<CommandItem> Commands;
    private int currentCommand;

    public Stack<StackItem> MicroStack;

    public string Help;

    [NonSerialized]
    public CoreEvents CurrentCoreEvent;

    /*Состояние: запущена ли отладка?*/
    public bool isDebugging;

    public void LoadCommandsFile(string filepath)
    {
      string str;
      Int32 TempAddress = 0xAAA0;

      // Прочитать документ
      using (StreamReader swIn = File.OpenText(filepath))
      {
        while ((str = swIn.ReadLine()) != null)
        {

          Commands.Add(new CommandItem { Address = TempAddress, Command = str });
          TempAddress++;
        } // while
      } // using
    }

    public int CurrentCommand
    {
      get
      {
        return currentCommand;
      }
      set
      {
        currentCommand = value;
        CurrentCoreEvent.SayCoreCurItemChanged();
      }
    }

    public void Push(StackItem item)
    {
      if (MicroStack.Count < 500)
      {
        MicroStack.Push(item);
        CurrentCoreEvent.SayPushStack();
      }
      else
      {
        throw new Exception("Stack overeflow.");
      }
    }

    public StackItem Pop()
    {
      if (MicroStack.Count > 0)
      {
        CurrentCoreEvent.SayPopStack();
        return MicroStack.Pop();
      }
      else
      {
        throw new Exception("Stack is less.");
      }
    }

    public Int32 Eax
    { 
      get { return eax; }
      set 
      {
        eax = value;
        CurrentCoreEvent.SayRegisterChanged("eax");
      }
    }

    public Int32 Ebx 
    {
      get { return ebx; }
      set
      {
        ebx = value;
        CurrentCoreEvent.SayRegisterChanged("ebx");
      }
    }
    
    public Int32 Ecx
    {
      get { return ecx; }
      set
      {
        ecx = value;
        CurrentCoreEvent.SayRegisterChanged("ecx");
      }
    }
    public Int32 Edx
    {
      get { return edx; }
      set
      {
        edx = value;
        CurrentCoreEvent.SayRegisterChanged("edx");
      }
    }

    public Int32 Eex
    {
      get { return eex; }
      set
      {
        eex = value;
        CurrentCoreEvent.SayRegisterChanged("eex");
      }
    }
    public Int32 Efx
    {
      get { return efx; }
      set
      {
        efx = value;
        CurrentCoreEvent.SayRegisterChanged("efx");
      }
    }
    public Int32 Egx
    {
      get { return egx; }
      set
      {
        egx = value;
        CurrentCoreEvent.SayRegisterChanged("egx");
      }
    }
    public Int32 Ehx
    {
      get { return ehx; }
      set
      {
        ehx = value;
        CurrentCoreEvent.SayRegisterChanged("ehx");
      }
    }

    public Int32 PortA
    { 
      get { return portA; }
      set
      {
        portA = value;
        CurrentCoreEvent.SayPortChanged("portA");
      }
    }
    public Int32 PortB
    {
      get { return portB; }
      set
      {
        portB = value;
        CurrentCoreEvent.SayPortChanged("portB");
      }
    }
    public Int32 PortC
    {
      get { return portC; }
      set
      {
        portC = value;
        CurrentCoreEvent.SayPortChanged("portC");
      }
    }
    public Int32 PortD
    { 
      get { return portD; }
      set
      {
        portD = value;
        CurrentCoreEvent.SayPortChanged("portD");
      }
    }

    public Int32 ZF
    {
      get { return zf; }
      set
      {
        zf = value;
        CurrentCoreEvent.SayZeroFlagChanged();
      }
    }

    public int GetRegister(string reg)
    {
      switch (reg)
      {
        case "eax": return Eax;
        case "ebx": return Ebx;
        case "ecx": return Ecx;
        case "edx": return Edx;
        case "eex": return Eex;
        case "efx": return Efx;
        case "egx": return Egx;
        case "ehx": return Ehx;
      }
      throw new Exception("Can't find register");
    }

    public int SetRegister(string reg, int value)
    {
      switch (reg)
      {
        case "eax": Eax=value;
                    return 0;
        case "ebx": Ebx = value;
                    return 0;
        case "ecx": Ecx = value;
                    return 0;
        case "edx": Edx = value;
                    return 0;
        case "eex": Eex = value;
                    return 0;
        case "efx": Efx = value;
                    return 0;
        case "egx": Egx = value;
                    return 0;
        case "ehx": Ehx = value;
                    return 0;
      }
      throw new Exception("Can't find register.");
    }

    public int GetPort(string port)
    {
      switch (port)
      {
        case "portA": return PortA;
        case "portB": return PortB;
        case "portC": return PortC;
        case "portD": return PortD;
      }
      throw new Exception("Can't find port.");
    }

    public int SetPort(string port, int value)
    {
      switch (port)
      {
        case "portA": PortA = value;
          return 0;
        case "portB": PortB = value;
          return 0;
        case "portC": PortC = value;
          return 0;
        case "portD": PortD = value;
          return 0;
      }
      throw new Exception("Can't find port.");
    }

    private Int32 eax;
    private Int32 ebx;
    private Int32 ecx;
    private Int32 edx;

    private Int32 eex;
    private Int32 efx;
    private Int32 egx;
    private Int32 ehx;

    private Int32 portA;
    private Int32 portB;
    private Int32 portC;
    private Int32 portD;

    private Int32 zf;

    public void InitByZero()
    {
      Eax = 0;
      Ebx = 0;
      Ecx = 0;
      Edx = 0;

      Eex = 0;
      Efx = 0;
      Egx = 0;
      Ehx = 0;

      PortA = 0;
      PortB = 0;
      PortC = 0;
      PortD = 0;

      ZF = 0;

      MicroStack.Clear();
      CurrentCoreEvent.SayReloadStack();
    }

  }

  public class CoreEvents
  {
    public delegate void CoreRegisterChanged(string register);
    public delegate void CorePortChanged(string register);
    public delegate void CoreStackChanged();
    public delegate void CoreFlagChanged();
    public delegate void CoreCurItemChanged();

    public event CoreRegisterChanged RegisterChanged;
    public void SayRegisterChanged(string register)
    {
      if (IfEventsLoaded == true)
      {
        RegisterChanged(register);
      }
    }

    public event CorePortChanged PortChanged;
    public void SayPortChanged(string port)
    {
      if (IfEventsLoaded == true)
      {
        PortChanged(port);
      }
    }

    public event CoreStackChanged PushStack;
    public void SayPushStack()
    {
      if (IfEventsLoaded == true)
      {
        PushStack();
      }
    }

    public event CoreStackChanged PopStack;
    public void SayPopStack()
    {
      if (IfEventsLoaded == true)
      {
        PopStack();
      }
    }

    public event CoreStackChanged ReloadStack;
    public void SayReloadStack()
    {
      if (IfEventsLoaded == true)
      {
        ReloadStack();
      }
    }

    public event CoreFlagChanged ZeroFlagChanged;
    public void SayZeroFlagChanged()
    {
      if (IfEventsLoaded == true)
      {
        ZeroFlagChanged();
      }
    }

    public event CoreCurItemChanged CurItemChanged;
    public void SayCoreCurItemChanged()
    {
      if (IfEventsLoaded == true)
      {
        CurItemChanged();
      }
    }

    public bool IfEventsLoaded = false;
  }

  public interface IRegisters
  {
    Int32 Eax { get; set; }
    Int32 Ebx { get; set; }
    Int32 Ecx { get; set; }
    Int32 Edx { get; set; }

    Int32 Eex { get; set; }
    Int32 Efx { get; set; }
    Int32 Egx { get; set; }
    Int32 Ehx { get; set; }
  }


  public interface IPorts
  {
    Int32 PortA { get; set; }
    Int32 PortB { get; set; }
    Int32 PortC { get; set; }
    Int32 PortD { get; set; }
  }

  public interface IFlags
  {
    Int32 ZF { get; set; }
  }

  public interface IStack
  {
    void Push(StackItem item);
    StackItem Pop();
  }

  [Serializable]
  public class CommandItem
  {
    public int Address { get; set; }
    public string Command { get; set; }
  }

  [Serializable]
  public class StackItem
  {
    public int Data { get; set; }
    public bool Address { get; set; }
  }

}

