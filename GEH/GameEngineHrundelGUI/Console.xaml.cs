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

namespace GameEngineHrundelGUI
{
    /// <summary>
    /// Логика взаимодействия для Console.xaml
    /// </summary>
    public partial class Console : UserControl
    {
        private int _selectedIdTextBox;
        private List<string> _allEnteredWordsList = new List<string>();
        public Console()
        {
            InitializeComponent();
        }
        private string GetTextFromList()
        {
            return _allEnteredWordsList.Count>0&&_selectedIdTextBox!= _allEnteredWordsList.Count ?_allEnteredWordsList[_selectedIdTextBox]:ConsoleInputTextBox.Text;
        }
           
        private void ConsoleTextBlock_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter) 
            {
                TrailingLinesStackPanel.Children.Add(new ResultTextInConsole(">" + ConsoleInputTextBox.Text));
                string log=default;
                new Command(ConsoleInputTextBox.Text).RunCommand(ref log);
                if(log!="")
                    TrailingLinesStackPanel.Children.Add(new ResultTextInConsole(log));
                if (!_allEnteredWordsList.Contains(ConsoleInputTextBox.Text))
                    _allEnteredWordsList.Add(ConsoleInputTextBox.Text);
                ConsoleInputTextBox.Text = "";
               
                _selectedIdTextBox = _allEnteredWordsList.Count;
            }
            if (e.Key == Key.Up|| e.Key == Key.Down)
            {
                _selectedIdTextBox-= _selectedIdTextBox-1>= 0&& e.Key == Key.Up ? 1:0;
                _selectedIdTextBox += _selectedIdTextBox + 1 < _allEnteredWordsList.Count && e.Key == Key.Down ? 1 : 0;
                ConsoleInputTextBox.Text = GetTextFromList().Replace(">","");
            }
            
        }
    }
}
