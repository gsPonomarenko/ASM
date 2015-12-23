using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace BaseOperations
{
  [Export(typeof(HomeworkWPF.ICommand))]
  [ExportMetadata("Operation", "add")]
  public class Add : HomeworkWPF.ICommand
  {
    private string defenition = "add";
    public string Defenition()
    {
      return defenition;
    }

    public void Operate(ref HomeworkWPF.Core CurrentStatement, string parametrs)
    {
      if (parametrs != "")
      {
        int comma = parametrs.IndexOf(',');
        string left = parametrs.Substring(0, comma);
        string right = parametrs.Substring(comma + 1);
        right = right.Trim();

        int rightValue = 0;
        int result = 0;
        int ifRightHasCount = right.IndexOfAny(new char[] { '1','2','3','4','5',
                                               '6','7','8','9','0'});
        if (ifRightHasCount == -1)
        {
          if (right.Length == 3 && right[0] == 'e' && right[2] == 'x')
          {
            for (char c = 'a'; c < 'i'; c++)
            {
              if (right[1] == c)
              {
                rightValue = CurrentStatement.GetRegister(right);
                c = 'i';
              }
            }
          }
        }
        else
        {
          rightValue = int.Parse(right);
        }

        if (left.Length == 3 && left[0] == 'e' && left[2] == 'x')
        {
          for (char c = 'a'; c < 'i'; c++)
          {
            if (left[1] == c)
            {
              result = CurrentStatement.GetRegister(left) + rightValue;
              CurrentStatement.SetRegister(left, result);
              return;
            }
          }
        }
      }
      throw new HomeworkWPF.ParseCommandException(CurrentStatement.Commands[CurrentStatement.CurrentCommand]);
    }
  }

  [Export(typeof(HomeworkWPF.ICommand))]
  [ExportMetadata("Operation", "sub")]
  class Sub : HomeworkWPF.ICommand
  {
    private string defenition = "Sub";
    public string Defenition()
    {
      return defenition;
    }
    public void Operate(ref HomeworkWPF.Core CurrentStatement, string parametrs)
    {
      if (parametrs != "")
      {
        int comma = parametrs.IndexOf(',');
        string left = parametrs.Substring(0, comma);
        string right = parametrs.Substring(comma + 1);
        right = right.Trim();

        int rightValue = 0;
        int result = 0;
        int ifRightHasCount = right.IndexOfAny(new char[] { '1','2','3','4','5',
                                               '6','7','8','9','0'});
        if (ifRightHasCount == -1)
        {
          if (right.Length == 3 && right[0] == 'e' && right[2] == 'x')
          {
            for (char c = 'a'; c < 'i'; c++)
            {
              if (right[1] == c)
              {
                rightValue = CurrentStatement.GetRegister(right);
                c = 'i';
              }
            }
          }
        }
        else
        {
          rightValue = int.Parse(right);
        }

        if (left.Length == 3 && left[0] == 'e' && left[2] == 'x')
        {
          for (char c = 'a'; c < 'i'; c++)
          {
            if (left[1] == c)
            {
              result = CurrentStatement.GetRegister(left) - rightValue;
              CurrentStatement.SetRegister(left, result);
              return;
            }
          }
        }
      }
      throw new HomeworkWPF.ParseCommandException(
                CurrentStatement.Commands[CurrentStatement.CurrentCommand]);
    }

  }

  [Export(typeof(HomeworkWPF.ICommand))]
  [ExportMetadata("Operation", "mul")]
  public class Mul : HomeworkWPF.ICommand
  {
    private string defenition = "mul";
    public string Defenition()
    {
      return defenition;
    }
    public void Operate(ref HomeworkWPF.Core CurrentStatement, string parametrs)
    {
      if (parametrs != "")
      {
        int comma = parametrs.IndexOf(',');
        string left = parametrs.Substring(0, comma);
        string right = parametrs.Substring(comma + 1);
        right = right.Trim();

        int rightValue = 0;
        int result = 0;

        int ifRightHasCount = right.IndexOfAny(new char[] { '1','2','3','4','5',
                                               '6','7','8','9','0'});
        if (ifRightHasCount == -1)
        {
          if (right.Length == 3 && right[0] == 'e' && right[2] == 'x')
          {
            for (char c = 'a'; c < 'i'; c++)
            {
              if (right[1] == c)
              {
                rightValue = CurrentStatement.GetRegister(right);
                c = 'i';
              }
            }
          }
        }
        else
        {
          rightValue = int.Parse(right);
        }

        if (left.Length == 3 && left[0] == 'e' && left[2] == 'x')
        {
          for (char c = 'a'; c < 'i'; c++)
          {
            if (left[1] == c)
            {
              result = CurrentStatement.GetRegister(left) * rightValue;
              CurrentStatement.SetRegister(left, result);
              return;
            }
          }
        }
      }
      throw new HomeworkWPF.ParseCommandException(
          CurrentStatement.Commands[CurrentStatement.CurrentCommand]);
    }
  }
  
  [Export(typeof(HomeworkWPF.ICommand))]
  [ExportMetadata("Operation", "div")]
  public class Div : HomeworkWPF.ICommand
  {
    private string defenition = "div";
    public string Defenition()
    {
      return defenition;
    }
    public void Operate(ref HomeworkWPF.Core CurrentStatement, string parametrs)
    {
      if (parametrs != "")
      {
        int comma = parametrs.IndexOf(',');
        string left = parametrs.Substring(0, comma);
        string right = parametrs.Substring(comma + 1);
        right = right.Trim();

        int rightValue = 0;
        int result = 0;

        int ifRightHasCount = right.IndexOfAny(new char[] { '1','2','3','4','5',
                                                '6','7','8','9','0'});
        if (ifRightHasCount == -1)
        {
          if (right.Length == 3 && right[0] == 'e' && right[2] == 'x')
          {
            for (char c = 'a'; c < 'i'; c++)
            {
              if (right[1] == c)
              {
                rightValue = CurrentStatement.GetRegister(right);
                c = 'i';
              }
            }
          }
        }
        else
        {
          rightValue = int.Parse(right);
        }

        if (left.Length == 3 && left[0] == 'e' && left[2] == 'x')
        {
          for (char c = 'a'; c < 'i'; c++)
          {
            if (left[1] == c)
            {
              result = CurrentStatement.GetRegister(left) / rightValue;
              CurrentStatement.SetRegister(left, result);
              return;
            }
          }
        }
      }
      throw new HomeworkWPF.ParseCommandException(
                CurrentStatement.Commands[CurrentStatement.CurrentCommand]);
    }
  }

  [Export(typeof(HomeworkWPF.ICommand))]
  [ExportMetadata("Operation", "in")]
  public class In : HomeworkWPF.ICommand
  {
    private string defenition = "in";
    public string Defenition()
    {
      return defenition;
    }
    public void Operate(ref HomeworkWPF.Core CurrentStatement, string parametrs)
    {
      if (parametrs != "")
      {
        int comma = parametrs.IndexOf(',');
        string left = parametrs.Substring(0, comma);
        string right = parametrs.Substring(comma + 1);
        right = right.Trim();

        int rightValue = 0;
        int result = 0;

        int ifRightHasCount = right.IndexOfAny(new char[] { '1','2','3','4','5',
                                                '6','7','8','9','0'});
        if (ifRightHasCount == -1)
        {
          if (right.Length == 3 && right[0] == 'e' && right[2] == 'x')
          {
            for (char c = 'a'; c < 'i'; c++)
            {
              if (right[1] == c)
              {
                rightValue = CurrentStatement.GetRegister(right);
                c = 'i';
              }
            }
          }
        }
        else
        {
          rightValue = int.Parse(right);
        }

        if (left.Length == 5 && left.Contains("port"))
        {
          for (char c = 'A'; c < 'E'; c++)
          {
            if (left[4] == c)
            {
              result = rightValue;
              CurrentStatement.SetPort(left, result);
              return;
            }
          }
        }
      }
      throw new HomeworkWPF.ParseCommandException(
                CurrentStatement.Commands[CurrentStatement.CurrentCommand]);

    }
  }


  [Export(typeof(HomeworkWPF.ICommand))]
  [ExportMetadata("Operation", "out")]
  public class Out : HomeworkWPF.ICommand
  {
    private string defenition = "out";
    public string Defenition()
    {
      return defenition;
    }
    public void Operate(ref HomeworkWPF.Core CurrentStatement, string parametrs)
    {
      if (parametrs != "")
      {
        int comma = parametrs.IndexOf(',');
        string left = parametrs.Substring(0, comma);
        string right = parametrs.Substring(comma + 1);
        right = right.Trim();

        int rightValue = 0;
        int result = 0;

        int ifRightHasCount = right.IndexOfAny(new char[] { '1','2','3','4','5',
                                                '6','7','8','9','0'});
        if (ifRightHasCount == -1)
        {
          if (right.Length == 5 && right.Contains("port"))
          {
            for (char c = 'A'; c < 'E'; c++)
            {
              if (right[4] == c)
              {
                rightValue = CurrentStatement.GetPort(right);
                c = 'E';
              }
            }
          }
        }
        else
        {
          throw new HomeworkWPF.ParseCommandException(
                    CurrentStatement.Commands[CurrentStatement.CurrentCommand]);
        }

        if (left.Length == 3 && left[0] == 'e' && left[2] == 'x')
        {
          for (char c = 'a'; c < 'i'; c++)
          {
            if (left[1] == c)
            {
              result = rightValue;
              CurrentStatement.SetRegister(left, result);
              return;
            }
          }
        }
      }
      throw new HomeworkWPF.ParseCommandException(
                CurrentStatement.Commands[CurrentStatement.CurrentCommand]);
    }
  }

  [Export(typeof(HomeworkWPF.ICommand))]
  [ExportMetadata("Operation", "mov")]
  public class Mov : HomeworkWPF.ICommand
  {
    private string defenition = "mov";
    public string Defenition()
    {
      return defenition;
    }
    public void Operate(ref HomeworkWPF.Core CurrentStatement, string parametrs)
    {
      if (parametrs != "")
      {
        int comma = parametrs.IndexOf(',');
        string left = parametrs.Substring(0, comma);
        string right = parametrs.Substring(comma + 1);
        right = right.Trim();

        int rightValue = 0;
        int result = 0;
        int ifRightHasCount = right.IndexOfAny(new char[] { '1','2','3','4','5',
                                                '6','7','8','9','0'});
        if (ifRightHasCount == -1)
        {
          if (right.Length == 3 && right[0] == 'e' && right[2] == 'x')
          {
            for (char c = 'a'; c < 'i'; c++)
            {
              if (right[1] == c)
              {
                rightValue = CurrentStatement.GetRegister(right);
                c = 'i';
              }
            }
          }
        }
        else
        {
          rightValue = int.Parse(right);
        }

        if (left.Length == 3 && left[0] == 'e' && left[2] == 'x')
        {
          for (char c = 'a'; c < 'i'; c++)
          {
            if (left[1] == c)
            {
              result = rightValue;
              CurrentStatement.SetRegister(left, result);
              return;
            }
          }
        }
      }
      throw new HomeworkWPF.ParseCommandException(CurrentStatement.Commands[CurrentStatement.CurrentCommand]);
    }
  }
  
  [Export(typeof(HomeworkWPF.ICommand))]
  [ExportMetadata("Operation", "call")]
  public class Call : HomeworkWPF.ICommand
  {
    private string defenition = "Call";
    public string Defenition()
    {
      return defenition;
    }
    public void Operate(ref HomeworkWPF.Core CurrentStatement, string parametrs)
    {
      if (parametrs != "")
      {
        string destination = parametrs.Trim();
        int address = 0;

        int count = 0;

        if (destination[destination.Count() - 1] == 'h')
        {
          address = Int32.Parse(destination);
        }
        else
        {
          foreach (HomeworkWPF.CommandItem i in CurrentStatement.Commands)
          {
            if (i.Command.Contains(parametrs + ":") == true)
            {
              address = i.Address;
            }
          }
        }

        foreach (HomeworkWPF.CommandItem i in CurrentStatement.Commands)
        {
          if (i.Address == address)
          {
            CurrentStatement.Push(new HomeworkWPF.StackItem
            {
              Data = CurrentStatement.Commands[CurrentStatement.CurrentCommand].Address,
              Address = true
            });
            CurrentStatement.CurrentCommand = count - 1;
            return;
          }
          count++;
        }

      }
      throw new HomeworkWPF.ParseCommandException(CurrentStatement.Commands[CurrentStatement.CurrentCommand]);
    }
  }

  [Export(typeof(HomeworkWPF.ICommand))]
  [ExportMetadata("Operation", "ret")]
  public class Ret : HomeworkWPF.ICommand
  {
    private string defenition = "Return";
    public string Defenition()
    {
      return defenition;
    }
    public void Operate(ref HomeworkWPF.Core CurrentStatement, string parametrs)
    {
      if (parametrs == "")
      {
        int address = 0;

        HomeworkWPF.StackItem item = null;

        int count = 0;

        while (address == 0 && CurrentStatement.MicroStack.Count > 0)
        {
          item = CurrentStatement.Pop();
          if (item.Address == true)
          {
            address = item.Data;
          }
        }

        foreach (HomeworkWPF.CommandItem i in CurrentStatement.Commands)
        {
          if (i.Address == address)
          {
            CurrentStatement.CurrentCommand = count;
            return;
          }
          count++;
        }
      }
      throw new HomeworkWPF.ParseCommandException(CurrentStatement.Commands[CurrentStatement.CurrentCommand]);
    }
  }

  [Export(typeof(HomeworkWPF.ICommand))]
  [ExportMetadata("Operation", "loop")]
  public class Loop : HomeworkWPF.ICommand
  {
    private string defenition = "Loop";
    public string Defenition()
    {
      return defenition;
    }
    public void Operate(ref HomeworkWPF.Core CurrentStatement, string parametrs)
    {
      if (parametrs != "")
      {
        CurrentStatement.Ecx = CurrentStatement.Ecx - 1;
        if (CurrentStatement.Ecx > 0)
        {
          string destination = parametrs.Trim();
          int address = 0;

          int count = 0;

          if (destination[destination.Count() - 1] == 'h')
          {
            address = Int32.Parse(destination);
          }
          else
          {
            foreach (HomeworkWPF.CommandItem i in CurrentStatement.Commands)
            {
              if (i.Command.Contains(destination + ":") == true)
              {
                address = i.Address;
              }
            }
          }

          foreach (HomeworkWPF.CommandItem i in CurrentStatement.Commands)
          {
            if (i.Address == address)
            {
              CurrentStatement.CurrentCommand = count - 1;
              return;
            }
            count++;
          }
        }
        else
        {
          return;
        }
      }
      throw new HomeworkWPF.ParseCommandException(CurrentStatement.Commands[CurrentStatement.CurrentCommand]);
    }
  }

  [Export(typeof(HomeworkWPF.ICommand))]
  [ExportMetadata("Operation", "jmp")]
  public class Jamp : HomeworkWPF.ICommand
  {
    private string defenition = "Jamp";
    public string Defenition()
    {
      return defenition;
    }

    public void Operate(ref HomeworkWPF.Core CurrentStatement, string parametrs)
    {
      if (parametrs != "")
      {
        string destination = parametrs.Trim();
        int address = 0;

        int count = 0;

        if (destination[destination.Count() - 1] == 'h')
        {
          address = Int32.Parse(destination);
        }
        else
        {
          foreach (HomeworkWPF.CommandItem i in CurrentStatement.Commands)
          {
            if (i.Command.Contains(parametrs + ":") == true)
            {
              address = i.Address;
            }
          }
        }

        foreach (HomeworkWPF.CommandItem i in CurrentStatement.Commands)
        {
          if (i.Address == address)
          {
            CurrentStatement.CurrentCommand = count - 1;
            return;
          }
          count++;
        }

      }
      throw new HomeworkWPF.ParseCommandException(CurrentStatement.Commands[CurrentStatement.CurrentCommand]);
    }
  }

  [Export(typeof(HomeworkWPF.ICommand))]
  [ExportMetadata("Operation", "jz")]
  public class Jz : HomeworkWPF.ICommand
  {
    private string defenition = "Jamp if Zero Flag is 0";
    public string Defenition()
    {
      return defenition;
    }

    public void Operate(ref HomeworkWPF.Core CurrentStatement, string parametrs)
    {
      if (parametrs != "")
      {
        if (CurrentStatement.ZF == 0)
        {
          string destination = parametrs.Trim();
          int address = 0;

          int count = 0;

          if (destination[destination.Count() - 1] == 'h')
          {
            address = Int32.Parse(destination);
          }
          else
          {
            foreach (HomeworkWPF.CommandItem i in CurrentStatement.Commands)
            {
              if (i.Command.Contains(parametrs + ":") == true)
              {
                address = i.Address;
                break;
              }
            }
          }

          foreach (HomeworkWPF.CommandItem i in CurrentStatement.Commands)
          {
            if (i.Address == address)
            {
              CurrentStatement.CurrentCommand = count - 1;
              return;
            }
            count++;
          }

        }
        else
        {
          return;
        }
      }
      throw new HomeworkWPF.ParseCommandException(CurrentStatement.Commands[CurrentStatement.CurrentCommand]);
    }
  }

  [Export(typeof(HomeworkWPF.ICommand))]
  [ExportMetadata("Operation", "cmp")]
  public class Cmp : HomeworkWPF.ICommand
  {
    private string defenition = "Compare. Signature \"cmp <left>, <right>\". " +
                         "If <left> >= <right>, ZF = 1.";
    public string Defenition()
    {
      return defenition;
    }

    public void Operate(ref HomeworkWPF.Core CurrentStatement, string parametrs)
    {
      if (parametrs != "")
      {
        int comma = parametrs.IndexOf(',');
        string left = parametrs.Substring(0, comma);
        string right = parametrs.Substring(comma + 1);
        right = right.Trim();

        int leftValue = 0;
        int rightValue = 0;

        int ifRightHasCount = right.IndexOfAny(new char[] { '1','2','3','4','5',
                                               '6','7','8','9','0'});

        if (ifRightHasCount == -1)
        {
          if (right.Length == 3 && right[0] == 'e' && right[2] == 'x')
          {
            for (char c = 'a'; c < 'i'; c++)
            {
              if (right[1] == c)
              {
                rightValue = CurrentStatement.GetRegister(right);
                c = 'i';
              }
            }
          }
        }
        else
        {
          rightValue = int.Parse(right);
        }

        int ifLeftHasCount = left.IndexOfAny(new char[] { '1','2','3','4','5',
                                               '6','7','8','9','0'});

        if (ifLeftHasCount == -1)
        {
          if (left.Length == 3 && left[0] == 'e' && left[2] == 'x')
          {
            for (char c = 'a'; c < 'i'; c++)
            {
              if (left[1] == c)
              {
                leftValue = CurrentStatement.GetRegister(left);
                c = 'i';
              }
            }
          }
        }
        else
        {
          leftValue = int.Parse(left);
        }

        if (leftValue >= rightValue)
        {
          CurrentStatement.ZF = 1;
        }
        else
        {
          CurrentStatement.ZF = 0;
        }
        return;
      }
      throw new HomeworkWPF.ParseCommandException(CurrentStatement.Commands[CurrentStatement.CurrentCommand]);
    }
  }
} 