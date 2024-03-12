using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace LAB2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Binding();

        }

        private void Binding()
        {
            CommandBinding saveCommand_Save = new CommandBinding(ApplicationCommands.Save, execute_Save, canExecute_Save);
            CommandBinding saveCommand_Open = new CommandBinding(ApplicationCommands.Open, execute_Open);
            CommandBinding saveCommand_Copy = new CommandBinding(ApplicationCommands.Copy, execute_Copy, canExecute_Copy);
            CommandBinding saveCommand_Paste = new CommandBinding(ApplicationCommands.Paste, execute_Paste);
            CommandBinding saveCommand_Erase = new CommandBinding(ApplicationCommands.Undo, execute_Erase);

            CommandBindings.Add(saveCommand_Save);
            CommandBindings.Add(saveCommand_Open);
            CommandBindings.Add(saveCommand_Copy);
            CommandBindings.Add(saveCommand_Paste);
            CommandBindings.Add(saveCommand_Erase);
        }

        private void execute_Erase(object sender, ExecutedRoutedEventArgs e)
        {
            inputTextBox.Undo();
            MessageBox.Show("The file was Erased!");
        }

        private void execute_Paste(object sender, ExecutedRoutedEventArgs e)
        {
            inputTextBox.Paste();
            MessageBox.Show("The file was Pasted!");
        }

        private void canExecute_Copy(object sender, CanExecuteRoutedEventArgs e)
        {
            if (inputTextBox.Text.Trim().Length > 0) e.CanExecute = true; else e.CanExecute = false;
        }

        private void execute_Copy(object sender, ExecutedRoutedEventArgs e)
        {
            inputTextBox.Copy();
            MessageBox.Show("The file was Copied!");
        }

        void execute_Open(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    string text = File.ReadAllText(filePath);
                    inputTextBox.Text = text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
            MessageBox.Show("The file was opened!");
        }

        void canExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            if (inputTextBox.Text.Trim().Length > 0) e.CanExecute = true; else e.CanExecute = false;
        }
        void execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            System.IO.File.WriteAllText("myFile.txt", inputTextBox.Text);
            MessageBox.Show("The file was saved!");
        }



    }
}
