#pragma warning disable
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;
namespace Vertex
{
    public class Vertex
    {
        public static Form apl = new Form();
        public static void InitializeApl()
        {
            apl.Text = "Sub-kernel: Vertex [Application]";
            apl.Size = new Size(640,480);
            apl.WindowState = FormWindowState.Normal;
            apl.FormBorderStyle = FormBorderStyle.None;
            apl.Bounds = Screen.PrimaryScreen.Bounds;
            apl.BackgroundImage = Image.FromFile("~/Pictures/Background.png");
            apl.BackgroundImageLayout = ImageLayout.Stretch;
            Application.Run(apl);
        }
        public static string cUSR = "";
        public static string cPWD = "";
        public static string cDT1 = "";
        public static string cDT2 = "";
        public static string cDT3 = "";
        public static string cDT4 = "";
        public static string cDT5 = "";
        private static bool tmn_running = false;
        private static bool slt_running = false;
        private static bool pcm_running = false;
        private static bool chr_running = false;
        public static string cToken = "";
        public static string csToken = "";
        public static string clToken = "";
        public static string coToken = "";
        public static bool inIfStatement = false;
        public static bool inIfStatement2 = false;
        public static bool inLoop = false;
        public static string[] commands = 
        {
            "import",
            "print",
            "int",
            "flt",
            "str",
            "if",
            "loop",
            "end",
            "inp",
            "finish",
            "html",
            "vt/web",
            "vtsos/pwd",
            "lgl",
            "db",
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
        public static Dictionary<string,bool> extensions = new Dictionary<string,bool>()
        {
            {"lgl",false},
            {"db",false},
            {"html",false},
            {"io",false},
            {"prc",false},
            {"vtsos",false},
        };
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
                        if (token == "html")
                        {
                            if (extensions["html"] == true)
                            {
                                cToken = token;
                                break;
                            }
                            else
                            {
                                Console.Write("VT-009: Unknown token : " + token);
                                Console.Write("\n");
                            }
                        }
                        if (token == "lgl")
                        {
                            if (extensions["lgl"] == true)
                            {
                                cToken = token;
                                string newLine = fileReader.ReadLine();
                                newLine = newLine.Replace("\t","");
                                if (newLine != null)
                                {
                                    cToken = null;
                                    csToken = null;
                                    Parse(newLine,fileReader);
                                    break;
                                }
                            }
                            else
                            {
                                Console.Write("VT-009: Unknown token : " + token);
                                Console.Write("\n");
                            }
                        }
                        if (token == "db")
                        {
                            if (extensions["db"] == true)
                            {
                                cToken = token;
                                string newLine = fileReader.ReadLine();
                                newLine = newLine.Replace("\t","");
                                if (newLine != null)
                                {
                                    cToken = null;
                                    csToken = null;
                                    Parse(newLine,fileReader);
                                    break;
                                }
                            }
                            else
                            {
                                Console.Write("VT-009: Unknown token : " + token);
                                Console.Write("\n");
                            }
                        }
                        if (token == "io/read" || token == "io/Console.Write")
                        {
                            if (extensions["io"] == true)
                            {
                                cToken = token;
                            }
                            else
                            {
                                Console.Write("VT-009: Unknown token : " + token);
                                Console.Write("\n");
                            }
                        }
                        if (token == "vt/web")
                        {
                            if (extensions["vt"] == true)
                            {
                                cToken = token;
                            }
                            else
                            {
                                Console.Write("VT-009: Unknown token : " + token);
                                Console.Write("\n");
                            }
                        }
                        if (token == "vtsos/pwd")
                        {
                            string pwd = Console.ReadLine();
                            if (pwd == cPWD)
                            {
                                Console.Write("Password: ");
                                string newLine = fileReader.ReadLine();
                                Console.Write("\n");
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
                                Console.Write("vTSOS: Invalid Password");
                                Console.Write("\n");
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
                            if (inIfStatement == false || inIfStatement2 == false)
                            {
                                if (cToken == "print")
                                {
                                    if (Str.ContainsKey(token))
                                    {
                                        Console.Write(Str[token]);
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
                                        Console.Write(Int[token]);
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
                                        Console.Write(Flt[token]);
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
                                        Console.Write(modifiedToken);
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
                                            Console.Write("VT-002: Special characters are not allowed in a variable name");
                                            Console.Write("\n");
                                            break;
                                        }
                                        else
                                        {
                                            csToken = token;
                                            break;
                                        }
                                    }
                                }
                                if (cToken == "int" && (csToken != null && token != csToken))
                                {
                                    int value;
                                    if (int.TryParse(token,out value))
                                    {
                                        Int.Add(csToken,value);
                                        Int[csToken] = value;
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
                                        Console.Write("VT-003: Invalid intenger type : " + token);
                                        Console.Write("\n");
                                        break;
                                    }
                                }
                                if (cToken == "str" && csToken == null)
                                {
                                    foreach (var specialCharacter in specialCharacters)
                                    {
                                        if (token.Contains(specialCharacter))
                                        {
                                            Console.Write("VT-002: Special characters are not allowed in a variable name");
                                            Console.Write("\n");
                                            break;
                                        }
                                        else
                                        {
                                            csToken = token;
                                            break;
                                        }
                                    }
                                }
                                if (cToken == "str" && (csToken != null && token != csToken))
                                {
                                    string value = token.Replace("[ns]"," ");
                                    value = value.Replace("[nl]","\n");
                                    Str.Add(csToken,value);
                                    Str[csToken] = value;
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
                                            Console.Write("VT-002: Special characters are not allowed in a variable name");
                                            Console.Write("\n");
                                            break;
                                        }
                                        else
                                        {
                                            csToken = token;
                                            break;
                                        }
                                    }
                                }
                                if (cToken == "flt" && (csToken != "" && csToken == null))
                                {
                                    float value;
                                    if (float.TryParse(token,out value))
                                    {
                                        Flt.Add(csToken,value);
                                        Flt[csToken] = value;
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
                                        Console.Write("VT-003: Invalid float type : " + token);
                                        Console.Write("\n");
                                        break;
                                    }
                                }
                                if (cToken == "if" && (csToken == null && coToken == null && clToken == null))
                                {
                                    foreach (var specialCharacter in specialCharacters)
                                    {
                                        if (token.Contains(specialCharacter))
                                        {
                                            Console.Write("VT-002: Special characters are not allowed in a variable name");
                                            Console.Write("\n");
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
                                            Console.Write("VT-002: Special characters are not allowed in a variable name");
                                            Console.Write("\n");
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
                                            Console.Write("VT-002: Special characters are not allowed in a variable name");
                                            Console.Write("\n");
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
                                            Console.Write("VT-004: Cannot compare other variable type with string");
                                            Console.Write("\n");
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
                                            Console.Write("VT-006: Cannot compare other variable type with intenger");
                                            Console.Write("\n");
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
                                            Console.Write("VT-007: Cannot compare other variable type with float");
                                            Console.Write("\n");
                                        }
                                    }
                                }
                                if (cToken == "import")
                                {
                                    if (cToken == "import" && token == "lgl")
                                    {
                                        extensions["lgl"] = true;
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
                                    if (cToken == "import" && token == "db")
                                    {
                                        extensions["db"] = true;
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
                                    if (cToken == "import" && token == "html")
                                    {
                                        extensions["html"] = true;
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
                                    if (cToken == "import" && token == "io")
                                    {
                                        extensions["io"] = true;
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
                                    if (cToken == "import" && token == "vtsos")
                                    {
                                        extensions["vtsos"] = true;
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
                                        Console.Write("VT-008: Invalid extension");
                                        Console.Write("\n");
                                    }
                                }
                                if (cToken == "set" && csToken == null)
                                {
                                    foreach (var specialCharacter in specialCharacters)
                                    {
                                        if (token.Contains(specialCharacter))
                                        {
                                            Console.Write("VT-002: Special characters are not allowed in a variable name");
                                            Console.Write("\n");
                                            break;
                                        }
                                        else
                                        {
                                            csToken = token;
                                            break;
                                        }
                                    }
                                }
                                if (cToken == "set" && (csToken != null && token != csToken))
                                {
                                    if (Str.ContainsKey(csToken))
                                    {
                                        string value = token.Replace("[ns]"," ");
                                        value = value.Replace("[nl]","\n");
                                        Str[csToken] = value;
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
                                    if (Int.ContainsKey(csToken))
                                    {
                                        int value;
                                        if (int.TryParse(token,out value))
                                        {
                                            Int[csToken] = value;
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
                                            Console.Write("VT-003: Invalid intenger type : " + token);
                                            Console.Write("\n");
                                            break;
                                        }
                                    }
                                    if (Flt.ContainsKey(csToken))
                                    {
                                        float value;
                                        if (float.TryParse(token,out value))
                                        {
                                            Flt[csToken] = value;
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
                                            Console.Write("VT-004: Invalid float type : " + token);
                                            Console.Write("\n");
                                            break;
                                        }
                                    }
                                }
                                if (cToken == "io/read" && csToken == null)
                                {
                                    foreach (var specialCharacter in specialCharacters)
                                    {
                                        if (token.Contains(specialCharacter))
                                        {
                                            Console.Write("VT-002: Special characters are not allowed in a variable name");
                                            Console.Write("\n");
                                            break;
                                        }
                                        else
                                        {
                                            csToken = token;
                                            break;
                                        }
                                    }
                                }
                                if (cToken == "io/read" && (csToken != null && token != csToken))
                                {
                                    if (Str.ContainsKey(csToken) && !token.StartsWith("https://www."))
                                    {
                                        StreamReader reader = new StreamReader(token);
                                        string file = reader.ReadToEnd();
                                        Str[csToken] = file;
                                        reader.Close();
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
                                    if (Str.ContainsKey(csToken) && token.StartsWith("https://www."))
                                    {
                                        WebClient reader = new WebClient();
                                        string file = reader.DownloadString(token);
                                        Str[csToken] = file;
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
                                if (cToken == "io/Console.Write" && csToken == null)
                                {
                                    foreach (var specialCharacter in specialCharacters)
                                    {
                                        if (token.Contains(specialCharacter))
                                        {
                                            Console.Write("VT-002: Special characters are not allowed in a variable name");
                                            Console.Write("\n");
                                            break;
                                        }
                                        else
                                        {
                                            csToken = token;
                                            break;
                                        }
                                    }
                                }
                                if (cToken == "io/Console.Write" && (csToken != null && token != csToken))
                                {
                                    if (csToken.EndsWith(".vt"))
                                    {
                                        if (token.StartsWith("https://www."))
                                        {
                                            WebClient client = new WebClient();
                                            client.DownloadFile(token,csToken);
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
                                            File.WriteAllText(csToken,modifiedToken);
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
                                if (cToken == "html")
                                {
                                    if (Str.ContainsKey(token))
                                    {
                                        if (Str[token].StartsWith("p:") && Str[token].EndsWith(":p"))
                                        {
                                            string instance = Str[token].Replace("p:","<p>");
                                            string id = instance.Substring(instance.IndexOf("["),instance.IndexOf("]") - instance.IndexOf("[")).Replace("[","");
                                            instance = instance.Replace("[" + id,"");
                                            instance = instance.Replace("[","");
                                            instance = instance.Replace("]","");
                                            instance = instance.Replace("<p>","<p id='" + id + "'>");
                                            instance = instance.Replace(":p","</p>");
                                            instance = instance.Replace("[ns]"," ");
                                            instance = instance.Replace("[nl]","\n");
                                            Str[token] = instance;
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
                                        if (Str[token].StartsWith("h1:") && Str[token].EndsWith(":h1"))
                                        {
                                            string instance = Str[token].Replace("h1:","<h1>");
                                            string id = instance.Substring(instance.IndexOf("["),instance.IndexOf("]") - instance.IndexOf("[")).Replace("[","");
                                            instance = instance.Replace("[" + id,"");
                                            instance = instance.Replace("[","");
                                            instance = instance.Replace("]","");
                                            instance = instance.Replace("h1:","<h1 id='" + id + "'>");
                                            instance = instance.Replace(":h1","</h1>");
                                            instance = instance.Replace("[ns]"," ");
                                            instance = instance.Replace("[nl]","\n");
                                            Str[token] = instance;
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
                                        if (Str[token].StartsWith("h2:") && Str[token].EndsWith(":h2"))
                                        {
                                            string instance = Str[token].Replace("h2:","<h2>");
                                            string id = instance.Substring(instance.IndexOf("["),instance.IndexOf("]") - instance.IndexOf("[")).Replace("[","");
                                            instance = instance.Replace("[" + id,"");
                                            instance = instance.Replace("[","");
                                            instance = instance.Replace("]","");
                                            instance = instance.Replace("h2:","<h2 id='" + id + "'>");
                                            instance = instance.Replace(":h2","</h2>");
                                            instance = instance.Replace("[ns]"," ");
                                            instance = instance.Replace("[nl]","\n");
                                            Str[token] = instance;
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
                                        if (Str[token].StartsWith("h3:") && Str[token].EndsWith(":h3"))
                                        {
                                            string instance = Str[token].Replace("h3:","<h3>");
                                            string id = instance.Substring(instance.IndexOf("["),instance.IndexOf("]") - instance.IndexOf("[")).Replace("[","");
                                            instance = instance.Replace("[" + id,"");
                                            instance = instance.Replace("[","");
                                            instance = instance.Replace("]","");
                                            instance = instance.Replace("h3:","<h3 id='" + id + "'>");
                                            instance = instance.Replace(":h3","</h3>");
                                            instance = instance.Replace("[ns]"," ");
                                            instance = instance.Replace("[nl]","\n");
                                            Str[token] = instance;
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
                                        if (Str[token].StartsWith("h4:") && Str[token].EndsWith(":h4"))
                                        {
                                            string instance = Str[token].Replace("h4:","<h4>");
                                            string id = instance.Substring(instance.IndexOf("["),instance.IndexOf("]") - instance.IndexOf("[")).Replace("[","");
                                            instance = instance.Replace("[" + id,"");
                                            instance = instance.Replace("[","");
                                            instance = instance.Replace("]","");
                                            instance = instance.Replace("h4:","<h4 id='" + id + "'>");
                                            instance = instance.Replace(":h4","</h4>");
                                            instance = instance.Replace("[ns]"," ");
                                            instance = instance.Replace("[nl]","\n");
                                            Str[token] = instance;
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
                                        if (Str[token].StartsWith("h5:") && Str[token].EndsWith(":h5"))
                                        {
                                            string instance = Str[token].Replace("h5:","<h5>");
                                            string id = instance.Substring(instance.IndexOf("["),instance.IndexOf("]") - instance.IndexOf("[")).Replace("[","");
                                            instance = instance.Replace("[" + id,"");
                                            instance = instance.Replace("[","");
                                            instance = instance.Replace("]","");
                                            instance = instance.Replace("h5:","<h5 id='" + id + "'>");
                                            instance = instance.Replace(":h5","</h5>");
                                            instance = instance.Replace("[ns]"," ");
                                            instance = instance.Replace("[nl]","\n");
                                            Str[token] = instance;
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
                                        if (Str[token].StartsWith("h6:") && Str[token].EndsWith(":h6"))
                                        {
                                            string instance = Str[token].Replace("h6:","<h6>");
                                            string id = instance.Substring(instance.IndexOf("["),instance.IndexOf("]") - instance.IndexOf("[")).Replace("[","");
                                            instance = instance.Replace("[" + id,"");
                                            instance = instance.Replace("[","");
                                            instance = instance.Replace("]","");
                                            instance = instance.Replace("h6:","<h6 id='" + id + "'>");
                                            instance = instance.Replace(":h6","</h6>");
                                            instance = instance.Replace("[ns]"," ");
                                            instance = instance.Replace("[nl]","\n");
                                            Str[token] = instance;
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
                                        if (Str[token].StartsWith("a:") && Str[token].EndsWith(":a"))
                                        {
                                            string instance = Str[token];
                                            string link = instance.Substring(instance.IndexOf("{"),instance.IndexOf("}") - instance.IndexOf("{"));
                                            string id = instance.Substring(instance.IndexOf("["),instance.IndexOf("]") - instance.IndexOf("[")).Replace("[","");
                                            instance = instance.Replace("[" + id,"");
                                            instance = instance.Replace(link,"");
                                            instance = instance.Replace("[","");
                                            instance = instance.Replace("]","");
                                            instance = instance.Replace("a:","<a id='" + id + "' href='" + link + "'>");
                                            instance = instance.Replace("[ns]"," ");
                                            instance = instance.Replace("[nl]","\n");
                                            instance = instance.Replace("{","");
                                            instance = instance.Replace("}","");
                                            instance = instance.Replace(":a","</a>");
                                            Str[token] = instance;
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
                                        if (Str[token].StartsWith("img:") && Str[token].EndsWith(":img"))
                                        {
                                            string instance = Str[token];
                                            string link = instance.Substring(instance.IndexOf("{"),instance.IndexOf("}") - instance.IndexOf("{"));
                                            string id = instance.Substring(instance.IndexOf("["),instance.IndexOf("]") - instance.IndexOf("[")).Replace("[","");
                                            instance = instance.Replace("[" + id,"");
                                            instance = instance.Replace(link,"");
                                            instance = instance.Replace("[","");
                                            instance = instance.Replace("]","");
                                            instance = instance.Replace("img:","<img id='" + id + "' src='" + link + "'/>");
                                            instance = instance.Replace(":img","");
                                            instance = instance.Replace("{","");
                                            instance = instance.Replace("}","");
                                            instance = instance.Replace("[ns]"," ");
                                            instance = instance.Replace("[nl]","\n");
                                            Str[token] = instance;
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
                        }
                    }
                }
            }
        }
        public static void Apl()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            InitializeApl();
        }
        public static void vTSOS()
        {
            Console.Clear();
            Console.Write("vTSOS v0.9.2\n");
            StreamReader dataReader = new StreamReader("data.mem");
            string dLine = "";
            dLine = dataReader.ReadLine();
            while (dLine != null)
            {
                if (dLine.StartsWith("usr:"))
                {
                    cUSR = dLine.Replace("usr:","");
                }
                if (dLine.StartsWith("pwd:"))
                {
                    cPWD = dLine.Replace("pwd:","");
                }
                if (dLine.StartsWith("dt1:"))
                {
                    cDT1 = dLine.Replace("dt1:","");
                }
                if (dLine.StartsWith("dt2:"))
                {
                    cDT2 = dLine.Replace("dt2:","");
                }
                if (dLine.StartsWith("dt3:"))
                {
                    cDT3 = dLine.Replace("dt3:","");
                }
                if (dLine.StartsWith("dt4:"))
                {
                    cDT4 = dLine.Replace("dt4:","");
                }
                if (dLine.StartsWith("dt5:"))
                {
                    cDT5 = dLine.Replace("dt5:","");
                }
                dLine = dataReader.ReadLine();
            }
            dataReader.Close();
            string line = "";
            string cData = "";
            string csData = "";
            string[] datas = 
            {
                "*system",
                "*install",
                "*version",
                "*help",
                "*cns",
                "*apl",
                "*dt1",
                "*dt2",
                "*dt3",
                "*dt4",
                "*dt5",
                "*fetch",
                "*license",
                "*run",
                "*remove",
                "*quit",
            };
            Console.Write("Commands\n");
            foreach (var data in datas)
            {
                Console.Write(data);
                Console.Write("\n");
            }
            Console.Write("\n");
            while (true)
            {
                if (cUSR == "")
                {
                    Console.Write("[vtsos@administrator] > ");
                }
                else
                {
                    Console.Write("[vtsos@" + cUSR + "] > ");
                }
                line = Console.ReadLine();
                Console.Write("\n");
                if (line == "*system")
                {
                    Console.Write("vTSOS\n");
                }
                if (line == "*install")
                {
                    Console.Write("Username: ");
                    string usr = Console.ReadLine();
                    Console.Write("\n");
                    Console.Write("Password: ");
                    string pwd = Console.ReadLine();
                    Console.Write("\n");
                    Console.Write("Confirm Password: ");
                    string cfp = Console.ReadLine();
                    Console.Write("\n");
                    if (cfp != pwd)
                    {
                        Console.Write("vTSOS: Passwords are not the same");
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write("Data 1 (*.vt): ");
                        string dt1  = Console.ReadLine();
                        Console.Write("\n");
                        Console.Write("Data 2 (*.vt): ");
                        string dt2  = Console.ReadLine();
                        Console.Write("\n");
                        Console.Write("Data 3 (*.vt): ");
                        string dt3  = Console.ReadLine();
                        Console.Write("\n");
                        Console.Write("Data 4 (*.vt): ");
                        string dt4  = Console.ReadLine();
                        Console.Write("\n");
                        Console.Write("Data 5 (*.vt): ");
                        string dt5  = Console.ReadLine();
                        Console.Write("\n");
                        try
                        {
                            File.WriteAllText("data.mem","usr:" + usr + "\npwd:" + pwd + "\ndt1:" + dt1 + "\ndt2:" + dt2 + "\ndt3:" + dt3 + "\ndt4:" + dt4 + "\ndt5:" + dt5);
                            Console.Write("Installed vTSOS\n");
                            cUSR = usr;
                            cPWD = pwd;
                            cDT1 = dt1;
                            cDT2 = dt2;
                            cDT3 = dt3;
                            cDT4 = dt4;
                            cDT5 = dt5;
                        }
                        catch (System.Exception)
                        {
                            Console.Write("vTSOS: Unable to override data.mem");
                            Console.Write("\n");
                        }
                    }
                }
                if (line == "*delete")
                {
                    File.WriteAllText("data.mem","usr:null\npwd:null\ndt1:hello-world.vt\ndt2:q-a.vt\ndt3:page.vt\ndt4:javascript.vt\ndt5:index.vt");
                    Console.Write("Cleared data of data.mem");
                    Console.Write("\n");
                }
                if (line == "*cns")
                {
                    vTSOS();
                }
                if (line == "*apl")
                {
                    Apl();
                    Console.ResetColor();
                    return;
                }
                if (line == "*version")
                {
                    Console.Write("0.9.2");
                    Console.Write("\n");
                }
                if (line == "*help")
                {
                    Console.Write("Commands\n");
                    foreach (var data in datas)
                    {
                        Console.Write(data);
                        Console.Write("\n");
                    }
                    Console.Write("\n");
                }
                if (line == "*fetch")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(@"  /$/ \$\  /$$$$\  /$$$$\  /$$$$$\  /$$$$\           //  ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("vtsos@" + cUSR + "\n");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(@" /$/  /$/   /$/    /$/     /$/ /$/  /$/             //   ");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("********************************\n");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(@"/$\ /$/    /$/    \$$$$\  /$/ /$/  \$$$$\          //    ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Open-source sub-operating system\n");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(@"\$ $/     /$/       \$/  /$/ /$/     \$/          //     ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("MIT License\n");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(@" \$/     \$/    \$$$$/  \$$$$$/  \$$$$/          //      ");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Rise LLC\n");
                    Console.ResetColor();
                }
                if (line == "*dt1")
                {
                    if (cDT1.EndsWith(".vt"))
                    {
                        Console.Clear();
                        try
                        {
                            StreamReader fileReader = new StreamReader(cDT1);
                            string cLine = fileReader.ReadLine();
                            Parse(cLine,fileReader);
                        }
                        catch (System.Exception exception)
                        {
                            Console.Write("vTSOS: " + exception);
                            Console.Write("\n");
                        }
                    }
                    else
                    {
                        Console.Write("VT-001: Invalid file type");
                        Console.Write("\n");
                    }
                }
                if (line == "*dt2")
                {
                    if (cDT2.EndsWith(".vt"))
                    {
                        Console.Clear();
                        try
                        {
                            StreamReader fileReader = new StreamReader(cDT2);
                            string cLine = fileReader.ReadLine();
                            Parse(cLine,fileReader);
                        }
                        catch (System.Exception exception)
                        {
                            Console.Write("vTSOS: " + exception);
                            Console.Write("\n");
                        }
                    }
                    else
                    {
                        Console.Write("VT-001: Invalid file type");
                        Console.Write("\n");
                    }
                }
                if (line == "*dt3")
                {
                    if (cDT3.EndsWith(".vt"))
                    {
                        Console.Clear();
                        try
                        {
                            StreamReader fileReader = new StreamReader(cDT3);
                            string cLine = fileReader.ReadLine();
                            Parse(cLine,fileReader);
                        }
                        catch (System.Exception exception)
                        {
                            Console.Write("vTSOS: " + exception);
                            Console.Write("\n");
                        }
                    }
                    else
                    {
                        Console.Write("VT-001: Invalid file type");
                        Console.Write("\n");
                    }
                }
                if (line == "*dt4")
                {
                    if (cDT4.EndsWith(".vt"))
                    {
                        Console.Clear();
                        try
                        {
                            StreamReader fileReader = new StreamReader(cDT4);
                            string cLine = fileReader.ReadLine();
                            Parse(cLine,fileReader);
                        }
                        catch (System.Exception exception)
                        {
                            Console.Write("vTSOS: " + exception);
                            Console.Write("\n");
                        }
                    }
                    else
                    {
                        Console.Write("VT-001: Invalid file type");
                        Console.Write("\n");
                    }
                }
                if (line == "*dt5")
                {
                    if (cDT5.EndsWith(".vt"))
                    {
                        Console.Clear();
                        try
                        {
                            StreamReader fileReader = new StreamReader(cDT5);
                            string cLine = fileReader.ReadLine();
                            Parse(cLine,fileReader);
                        }
                        catch (System.Exception exception)
                        {
                            Console.Write("vTSOS: " + exception);
                            Console.Write("\n");
                        }
                    }
                    else
                    {
                        Console.Write("VT-001: Invalid file type");
                        Console.Write("\n");
                    }
                }
                if (line == "*license")
                {
                    Console.Write("MIT License");
                }
                if (line == "*run")
                {
                    string sLine = Console.ReadLine();
                    if (sLine.EndsWith(".vt"))
                    {
                        Console.Clear();
                        try
                        {
                            StreamReader fileReader = new StreamReader(sLine);
                            string cLine = fileReader.ReadLine();
                            Parse(cLine,fileReader);
                        }
                        catch (System.Exception exception)
                        {
                            Console.Write("vTSOS: " + exception);
                            Console.Write("\n");
                        }
                    }
                    else
                    {
                        Console.Write("VT-001: Invalid file type");
                        Console.Write("\n");
                    }
                }
                if (line == "*quit")
                {
                    break;
                }
            }
        }
        static void Main(string[] args)
        {
            Console.Title = "Sub-kernel: Vertex v0.9.2";
            Console.CursorVisible = false;
            string cArg = "";
            if (args.Length == 0)
            {
                Console.Write("Vertex\nUsage: Vertex <options>\nOptions:\n--r <file>.vt (Run)\n--v (Version)\n--l (License)\n--s (Sub Operating System)");
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
                    Console.Write("0.9.2");
                    return;
                }
                if (arg == "--l")
                {
                    Console.Write("MIT License");
                    return;
                }
                if (arg != cArg && cArg == "--r")
                {
                    if (arg.EndsWith(".vt"))
                    {
                        Console.Clear();
                        try
                        {
                            StreamReader fileReader = new StreamReader(arg);
                            string cLine = fileReader.ReadLine();
                            cLine = cLine.Replace("\t","");
                            Parse(cLine,fileReader);
                        }
                        catch (System.Exception exception)
                        {
                            Console.Write("VT-010: " + exception);
                            Console.Write("\n");
                        }
                    }
                    else
                    {
                        Console.Write("VT-001: Invalid file type");
                        Console.Write("\n");
                    }
                }
                if (arg == "--s")
                {
                    vTSOS();
                    return;
                }
            }
        }
    }
}