using DocumentFormat.OpenXml.Vml.Spreadsheet;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum CurrentState
        {
            Default,
            Plus,
            Minus,
            Multiply,
            Devine
        };
        CurrentState currentState = CurrentState.Default;
        double CurrentValue = 0;
        Button CurrentOperation;
        public MainWindow()
        {
            InitializeComponent();
            CreateAdditionsButtons();
            ResetValue();
            MainOutput.Content = "0";
        }

        private void ResetValue()
        {
            currentState = CurrentState.Default;
            CurrentValue = 0;
        }

        private void CreateAdditionsButtons()
        {
            Button PlusButton = new Button();
            PlusButton.Content = "+";
            Grid.SetRow(PlusButton, 0);
            Grid.SetColumn(PlusButton, 3);
            NumpadDefinition.Children.Add(PlusButton);
            PlusButton.Click += ChoosedOperation;

            Button MinusButton = new Button();
            MinusButton.Content = "-";
            Grid.SetRow(MinusButton, 1);
            Grid.SetColumn(MinusButton, 3);
            NumpadDefinition.Children.Add(MinusButton);
            MinusButton.Click += ChoosedOperation;

            Button MultiplyButton = new Button();
            MultiplyButton.Content = "*";
            Grid.SetRow(MultiplyButton, 2);
            Grid.SetColumn(MultiplyButton, 3);
            NumpadDefinition.Children.Add(MultiplyButton);
            MultiplyButton.Click += ChoosedOperation;

            Button DivideButton = new Button();
            DivideButton.Content = "/";
            Grid.SetRow(DivideButton, 3);
            Grid.SetColumn(DivideButton, 3);
            NumpadDefinition.Children.Add(DivideButton);
            DivideButton.Click += ChoosedOperation;

        }

        private void ChoosedOperation(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                switch (button.Content.ToString())
                {
                    case "+":
                        currentState = CurrentState.Plus;
                        break;
                    case "-":
                        currentState = CurrentState.Minus; 
                    break;
                    case "*":
                        currentState = CurrentState.Multiply;
                    break;
                    case "/":
                        currentState = CurrentState.Devine;
                    break;
                    default:
                        currentState = CurrentState.Default;
                    break;
                }
                if (CurrentOperation == null)
                {
                    CurrentValue = Double.Parse(MainOutput.Content.ToString());
                }
                else
                {
                    CurrentOperation.Style = (Style)FindResource("DefaultBinderStyle");
                }
                MainOutput.Content = button.Content.ToString();
                button.Style = (Style)FindResource("BorderButtonStyle");
                CurrentOperation = button;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                MainOutput.Content = button.Content;
                switch (currentState)
                {
                    case CurrentState.Plus:
                        CurrentValue += Double.Parse(MainOutput.Content.ToString());
                        MainOutput.Content = CurrentValue.ToString();
                        break;
                    case CurrentState.Minus:
                        CurrentValue -= Double.Parse(MainOutput.Content.ToString());
                        MainOutput.Content = CurrentValue.ToString();
                        break;
                    case CurrentState.Multiply:
                        CurrentValue *= Double.Parse(MainOutput.Content.ToString());
                        MainOutput.Content = CurrentValue.ToString();
                        break;
                    case CurrentState.Devine:
                        CurrentValue /= Double.Parse(MainOutput.Content.ToString());
                        MainOutput.Content = CurrentValue.ToString();
                        
                        break;
                    case CurrentState.Default:
                        MainOutput.Content = button.Content;
                    break;
                }
                
                currentState = CurrentState.Default;
                if (CurrentOperation != null)
                {
                    CurrentOperation.Style = (Style)FindResource("DefaultBinderStyle");
                    CurrentOperation = null;
                }
            }
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            
            var button = sender as Button;
            if (button.Content.ToString() == "=")
            {
                MainOutput.Content = CurrentValue.ToString();
            }
            else
            {
                MainOutput.Content = "0";
            }
            ResetValue();
        }
    }
}
