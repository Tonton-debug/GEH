using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameEngineHrundelGUI
{
   public class Command
    {
        private const string EXIT = "exit";
        private const string OPEN_DLL = "open";
        private const string HELP = "help";
        private const string ROTATE_CAMERA = "rotate";
        private readonly string command_string;
        private readonly string argument_string;
        public Command(string text)
        {
            command_string = text.Replace(" ", "").Split(new char[] { '=' })[0];
           if(text.Replace(" ", "").Split(new char[] { '=' }).Length>1)
            argument_string = text.Replace(" ", "").Split(new char[] { '=' })[1];
        }
        public void RunCommand(ref string info_text)
        {
            MainWindow window = (MainWindow)Application.Current.MainWindow;
            switch (command_string)
            {
                case EXIT when argument_string=="":
                    ((ICommand)window).Exit();
                    
                    break;
                case ROTATE_CAMERA when argument_string != "":
                    int rotateY = 0;
                    int rotateX;
                    string[] _array_text = argument_string.Split(new char[] {',' });
                    if (!int.TryParse(_array_text[0], out rotateX) || !int.TryParse(_array_text[1], out rotateY)|| _array_text.Length>2)
                    {
                        info_text = "We cant convert " + _array_text[1] + " or " + _array_text[2] + " to int";
                        break;
                    }
                    ((ICommand)window).RotateCamera(rotateX,rotateY);
                    break;
                case OPEN_DLL when argument_string != "":
                    if (!File.Exists(argument_string))
                    {
                        info_text = "File " + argument_string + " is not exits";
                        break;
                    }
                    ((ICommand)window).RunDLL(argument_string);
                    break;
                case HELP when argument_string=="":
                    
                    break;
                default:
                    info_text = "Unknown command or incorrectly typed command :(";
                    break;
            }
        }
    }
}
