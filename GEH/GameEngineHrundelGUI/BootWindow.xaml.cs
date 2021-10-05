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
using System.Windows.Shapes;

namespace GameEngineHrundelGUI
{
    /// <summary>
    /// Логика взаимодействия для BootWindow.xaml
    /// </summary>
    public partial class BootWindow : Window
    {
        public BootWindow()
        {
            InitializeComponent();
        }
        public void SetInfoText(string info)
        {
            LogText.Text = info;
        }
        public void ChangeValueLoadBar(int value)
        {
            LoadingBar.Value = value;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(
                  "If the window is closed, the download will stop. Close? ", "Warning",
                  MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No)
            {
               
                e.Cancel = true;
            }
                
        }
    }
}
