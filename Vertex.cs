#pragma warning disable
using System;
using System.IO;
using System.Collections.Generic;
namespace Vertex
{
    public class Vertex
    {
        public static string cToken = "";
        public static string csToken = "";
        public static string clToken = "";
        public static string coToken = "";
        public static bool inIfStatement = false;
        public static bool inIfStatement2 = false;
        public static string[] commands = 
        {
            "import",
            "print",
            "int",
            "flt",
            "str",
            "if",
            "end",
            "inp",
            "exit",
            "goto",
            "end",
            "finish",
        };
        public static string[] specialCharacters = 
        {
            "[ns]",
            "[nl]",
        };
        public static Dictionary<string,string> Str = new Dictionary<string,string>();
        public static Dictionary<string,int> Int = new Dictionary<string,int>();
        public static Dictionary<string,float> Flt = new Dictionary<string,float>();
        public static Dictionary<int,Dictionary<int,string>> If = new Dictionary<int,Dictionary<int,string>>();
        public static Dictionary<int,Dictionary<int,string>> Functions = new Dictionary<int,Dictionary<int,string>>();
        public static Dictionary<int,string> allCommands = new Dictionary<int,string>();
        public static void Parse(string line,StreamReader fileReader)
        {
            if (line == null)
            {
                return;
            }
            foreach (var token in line.Split(' '))
            {
                foreach (var command in commands)
                {
                    if (token.Contains(command) && cToken != token)
                    {
                        if (token == "end")
                        {
                            if (inIfStatement == true && inIfStatement2 == false)
                            {
                                inIfStatement = false;
                            }
                            if (inIfStatement == true && inIfStatement2 == true)
                            {
                                inIfStatement2 = false;
                            }
                            string newLine = fileReader.ReadLine();
                            newLine = newLine.Replace("\t","");
                            if (newLine != null)
                            {
                                cToken = null;
                                csToken = null;
                                Parse(newLine,fileReader);
                                break;
                            }
                            else
                            {
                                return;
                            }
                        }
                        if (token == "finish")
                        {
                            return;
                        }
                        else
                        {
                            cToken = token;
                        }
                        break;
                    }
                    else
                    {
                        if ((token != null || token != "") && (token != cToken || token != csToken))
                        {
                            if (!inIfStatement)
                            {
                                if (cToken == "print")
                                {
                                    if (Str.ContainsKey(token))
                                    {
                                        Console.WriteLine(Str[token]);
                                        string newLine = fileReader.ReadLine();
                                        newLine = newLine.Replace("\t","");
                                        if (newLine != null)
                                        {
                                            cToken = null;
                                            csToken = null;
                                            Parse(newLine,fileReader);
                                            break;
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                    if (Int.ContainsKey(token))
                                    {
                                        Console.WriteLine(Int[token]);
                                        string newLine = fileReader.ReadLine();
                                        newLine = newLine.Replace("\t","");
                                        if (newLine != null)
                                        {
                                            cToken = null;
                                            csToken = null;
                                            Parse(newLine,fileReader);
                                            break;
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                    if (Flt.ContainsKey(token))
                                    {
                                        Console.WriteLine(Flt[token]);
                                        string newLine = fileReader.ReadLine();
                                        newLine = newLine.Replace("\t","");
                                        if (newLine != null)
                                        {
                                            cToken = null;
                                            csToken = null;
                                            Parse(newLine,fileReader);
                                            break;
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        string modifiedToken = token.Replace("[ns]"," ");
                                        modifiedToken = modifiedToken.Replace("[nl]","\n");
                                        Console.WriteLine(modifiedToken);
                                        string newLine = fileReader.ReadLine();
                                        newLine = newLine.Replace("\t","");
                                        if (newLine != null)
                                        {
                                            cToken = null;
                                            csToken = null;
                                            Parse(newLine,fileReader);
                                            break;
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                }
                                if (cToken == "inp")
                                {
                                    if (Str.ContainsKey(token))
                                    {
                                        Str[token] = Console.ReadLine();
                                        string newLine = fileReader.ReadLine();
                                        newLine = newLine.Replace("\t","");
                                        if (newLine != null)
                                        {
                                            cToken = null;
                                            csToken = null;
                                            Parse(newLine,fileReader);
                                            break;
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                }
                                if (cToken == "int" && csToken == null)
                                {
                                    foreach (var specialCharacter in specialCharacters)
                                    {
                                        if (token.Contains(specialCharacter))
                                        {
                                            Console.WriteLine("VT-002: Special characters are not allowed in a variable name");
                                            break;
                                        }
                                        else
                                        {
                                            csToken = token;
                                            break;
                                        }
                                    }
                                }
                                if (cToken == "int" && csToken != null && token != csToken)
                                {
                                    int value;
                                    if (int.TryParse(token,out value))
                                    {
                                        Int.Add(csToken,value);
                                        string newLine = fileReader.ReadLine();
                                        newLine = newLine.Replace("\t","");
                                        if (newLine != null)
                                        {
                                            cToken = null;
                                            csToken = null;
                                            Parse(newLine,fileReader);
                                            break;
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("VT-003: Invalid intenger type : " + token);
                                        break;
                                    }
                                }
                                if (cToken == "str" && csToken == null)
                                {
                                    foreach (var specialCharacter in specialCharacters)
                                    {
                                        if (token.Contains(specialCharacter))
                                        {
                                            Console.WriteLine("VT-002: Special characters are not allowed in a variable name");
                                            break;
                                        }
                                        else
                                        {
                                            csToken = token;
                                            break;
                                        }
                                    }
                                }
                                if (cToken == "str" && csToken != null)
                                {
                                    string value = token.Replace("[ns]"," ");
                                    value = value.Replace("[nl]","\n");
                                    Str.Add(csToken,value);
                                    string newLine = fileReader.ReadLine();
                                    newLine = newLine.Replace("\t","");
                                    if (newLine != null)
                                    {
                                        cToken = null;
                                        csToken = null;
                                        Parse(newLine,fileReader);
                                        break;
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                                if (cToken == "flt" && csToken == null)
                                {
                                    foreach (var specialCharacter in specialCharacters)
                                    {
                                        if (token.Contains(specialCharacter))
                                        {
                                            Console.WriteLine("VT-002: Special characters are not allowed in a variable name");
                                            break;
                                        }
                                        else
                                        {
                                            csToken = token;
                                            break;
                                        }
                                    }
                                }
                                if (cToken == "flt" && (csToken != "" || csToken == null))
                                {
                                    float value;
                                    if (float.TryParse(token,out value))
                                    {
                                        Flt.Add(csToken,value);
                                        string newLine = fileReader.ReadLine();
                                        newLine = newLine.Replace("\t","");
                                        if (newLine != null)
                                        {
                                            cToken = null;
                                            csToken = null;
                                            Parse(newLine,fileReader);
                                            break;
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("VT-003: Invalid float type : " + token);
                                        break;
                                    }
                                }
                                if (cToken == "if" && (csToken == null && coToken == null && clToken == null))
                                {
                                    foreach (var specialCharacter in specialCharacters)
                                    {
                                        if (token.Contains(specialCharacter))
                                        {
                                            Console.WriteLine("VT-002: Special characters are not allowed in a variable name");
                                            break;
                                        }
                                        else
                                        {
                                            csToken = token;
                                            break;
                                        }
                                    }
                                }
                                if (cToken == "if" && (csToken != null && coToken == null && clToken == null))
                                {
                                    foreach (var specialCharacter in specialCharacters)
                                    {
                                        if (token.Contains(specialCharacter))
                                        {
                                            Console.WriteLine("VT-002: Special characters are not allowed in a variable name");
                                            break;
                                        }
                                        else
                                        {
                                            coToken = token;
                                            break;
                                        }
                                    }
                                }
                                if (cToken == "if" && (csToken != null && coToken != null && clToken == null))
                                {
                                    foreach (var specialCharacter in specialCharacters)
                                    {
                                        if (token.Contains(specialCharacter))
                                        {
                                            Console.WriteLine("VT-002: Special characters are not allowed in a variable name");
                                            break;
                                        }
                                        else
                                        {
                                            clToken = token;
                                            break;
                                        }
                                    }
                                }
                                if (cToken == "if" && (csToken!= null && coToken != null && clToken != null))
                                {
                                    if (Str.ContainsKey(csToken))
                                    {
                                        if (Str.ContainsKey(clToken))
                                        {
                                            if (coToken == "==")
                                            {
                                                if (Str[csToken] == Str[clToken])
                                                {
                                                    if (inIfStatement == false)
                                                    {
                                                        inIfStatement = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        inIfStatement2 = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                            if (coToken == "!=")
                                            {
                                                if (Str[csToken] != Str[clToken])
                                                {
                                                    if (inIfStatement == false)
                                                    {
                                                        inIfStatement = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        inIfStatement2 = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                            if (coToken == ">")
                                            {
                                                if (Str[csToken].Length > Str[clToken].Length)
                                                {
                                                    if (inIfStatement == false)
                                                    {
                                                        inIfStatement = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        inIfStatement2 = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                            if (coToken == "<")
                                            {
                                                if (Str[csToken].Length < Str[clToken].Length)
                                                {
                                                    if (inIfStatement == false)
                                                    {
                                                        inIfStatement = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        inIfStatement2 = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("VT-004: Cannot compare other variable type with string");
                                        }
                                    }
                                    if (Int.ContainsKey(csToken))
                                    {
                                        if (Int.ContainsKey(clToken))
                                        {
                                            if (coToken == "==")
                                            {
                                                if (Int[csToken] == Int[clToken])
                                                {
                                                    if (inIfStatement == false)
                                                    {
                                                        inIfStatement = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        inIfStatement2 = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                            if (coToken == "!=")
                                            {
                                                if (Int[csToken] != Int[clToken])
                                                {
                                                    if (inIfStatement == false)
                                                    {
                                                        inIfStatement = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        inIfStatement2 = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                            if (coToken == ">")
                                            {
                                                if (Int[csToken] > Int[clToken])
                                                {
                                                    if (inIfStatement == false)
                                                    {
                                                        inIfStatement = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        inIfStatement2 = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                            if (coToken == "<")
                                            {
                                                if (Int[csToken] < Int[clToken])
                                                {
                                                    if (inIfStatement == false)
                                                    {
                                                        inIfStatement = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        inIfStatement2 = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("VT-005: Cannot compare other variable type with intenger");
                                        }
                                    }
                                    if (Flt.ContainsKey(csToken))
                                    {
                                        if (Flt.ContainsKey(clToken))
                                        {
                                            if (coToken == "==")
                                            {
                                                if (Flt[csToken] == Flt[clToken])
                                                {
                                                    if (inIfStatement == false)
                                                    {
                                                        inIfStatement = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        inIfStatement2 = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                            if (coToken == "!=")
                                            {
                                                if (Flt[csToken] != Flt[clToken])
                                                {
                                                    if (inIfStatement == false)
                                                    {
                                                        inIfStatement = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        inIfStatement2 = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                            if (coToken == ">")
                                            {
                                                if (Flt[csToken] > Flt[clToken])
                                                {
                                                    if (inIfStatement == false)
                                                    {
                                                        inIfStatement = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        inIfStatement2 = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                            if (coToken == "<")
                                            {
                                                if (Flt[csToken] < Flt[clToken])
                                                {
                                                    if (inIfStatement == false)
                                                    {
                                                        inIfStatement = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        inIfStatement2 = true;
                                                        string newLine = fileReader.ReadLine();
                                                        newLine = newLine.Replace("\t","");
                                                        if (newLine != null)
                                                        {
                                                            cToken = null;
                                                            csToken = null;
                                                            Parse(newLine,fileReader);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("VT-005: Cannot compare other variable type with float");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void ReadIfStatement(Dictionary<int,Dictionary<int,string>> IfStatement,StreamReader fileReader)
        {
            foreach (var line in IfStatement.Keys)
            {
                if (IfStatement.ContainsKey(line))
                {
                    if (IfStatement.ContainsValue(IfStatement[line]))
                    {
                        for (int cLine = 0; cLine < IfStatement[line].Keys.Count; cLine++)
                        {
                            Parse(IfStatement[line][cLine],fileReader);
                        }
                        return;
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            string cArg = "";
            if (args.Length == 0)
            {
                Console.WriteLine("Vertex\nUsage: Vertex <options>\nOptions:\n--r <file>.vt (Run)\n--v (Version)\n--l (License)");
                return;
            }
            foreach (var arg in args)
            {
                if (arg == "--r")
                {
                    cArg = arg;
                }
                if (arg == "--v")
                {
                    Console.WriteLine("0.3.0");
                }
                if (arg == "--l")
                {
                    Console.WriteLine("MIT License");
                }
                if (arg != null && cArg == "--r")
                {
                    if (arg.EndsWith(".vt"))
                    {
                        Console.Clear();
                        StreamReader fileReader = new StreamReader(arg);
                        String line;
                        line = fileReader.ReadLine();
                        line = line.Replace("\t","");
                        if  (line != null)
                        {
                            Parse(line,fileReader);
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("VT-001: Invalid file type");
                    }
                }
            }
        }
    }
}