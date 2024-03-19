using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Calculator_v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Collection<string> computationHistory;

        public interface ICalculatorCommand
        {
            void Execute();
        }

        public class AdditionCommand : ICalculatorCommand
        {
            public void Execute()
            {
                Calculator.ExecuteOperation(Calculator.CalculatorOperationType.Addition);
            }
        }

        public class SubtractionCommand : ICalculatorCommand
        {
            public void Execute()
            {
                Calculator.ExecuteOperation(Calculator.CalculatorOperationType.Substraction);
            }
        }

        public class MultiplicationCommand : ICalculatorCommand
        {
            public void Execute()
            {
                Calculator.ExecuteOperation(Calculator.CalculatorOperationType.Multiplying);
            }
        }

        public class DivisionCommand : ICalculatorCommand
        {
            public void Execute()
            {
                Calculator.ExecuteOperation(Calculator.CalculatorOperationType.Dividing);
            }
        }

        public class EnterNumberCommand : ICalculatorCommand
        {
            private int number;

            public EnterNumberCommand(int number)
            {
                this.number = number;
            }

            public void Execute()
            {
                Calculator.EnterNumber(number);
            }
        }

        public class EnterDotCommand : ICalculatorCommand
        {
            public void Execute()
            {
                Calculator.EnterDot();
            }
        }

        public class ClearCommand : ICalculatorCommand
        {
            public void Execute()
            {
                Calculator.Clear();
            }
        }


        public class ClearEntryCommand : ICalculatorCommand
        {
            public void Execute()
            {
                Calculator.ClearEntry();
            }
        }

        public class EraseLastCommand : ICalculatorCommand
        {
            public void Execute()
            {
                Calculator.EraseLast();
            }
        }

        public class EqualCommand : ICalculatorCommand
        {
            public void Execute()
            {
                Calculator.Equal();
            }
        }

        public class EnterPiCommand : ICalculatorCommand
        {
            public void Execute()
            {
                Calculator.EnterPi();
            }
        }

        public class SquareRootCommand : ICalculatorCommand
        {
            public void Execute()
            {
                Calculator.SquareRoot();
            }
        }

        public class ToTheSquareCommand : ICalculatorCommand
        {
            public void Execute()
            {
                Calculator.ToTheSquare();
            }
        }

        public class FactorialCommand : ICalculatorCommand
        {
            public void Execute()
            {
                Calculator.Factorial();
            }
        }

        public class ChangeSignCommand : ICalculatorCommand
        {
            public void Execute()
            {
                Calculator.ChangeSign();
            }
        }



        public MainWindow()
        {
            InitializeComponent();
            computationHistory = new Collection<string>();
            Calculator.InputRestriction = 12;
            Calculator.ComputationEnded += (s, b) =>
            {
                computationHistory.Add(Calculator.getTheLastComputation());
            };

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (PiButton.Visibility == Visibility.Visible ||
        PlusMinusButton.Visibility == Visibility.Visible ||
        SquareRootButton.Visibility == Visibility.Visible ||
        SquareButton.Visibility == Visibility.Visible ||
        FactorialButton.Visibility == Visibility.Visible)
            {
                // Якщо хоча б одна кнопка видима, змінюємо розміри колонок у вкладеному Grid
                InnerGrid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                InnerGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                InnerGrid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
                InnerGrid.ColumnDefinitions[3].Width = new GridLength(1, GridUnitType.Star);
                InnerGrid.ColumnDefinitions[4].Width = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                // Якщо всі кнопки приховані, залишаємо розміри колонок за замовчуванням
                InnerGrid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                InnerGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                InnerGrid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
                InnerGrid.ColumnDefinitions[3].Width = new GridLength(1, GridUnitType.Star);
                InnerGrid.ColumnDefinitions[4].Width = new GridLength(0, GridUnitType.Star);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string content = button.Content.ToString();

            ICalculatorCommand command = null;

            switch (content)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                    command = new EnterNumberCommand(Int32.Parse(content));
                    break;
                case ".":
                    command = new EnterDotCommand();
                    break;
                case "C":
                    command = new ClearCommand();
                    break;
                case "CE":
                    command = new ClearEntryCommand();
                    break;
                case "⌫":
                    command = new EraseLastCommand();
                    break;
                case "+":
                    command = new AdditionCommand();
                    break;
                case "-":
                    command = new SubtractionCommand();
                    break;
                case "x":
                    command = new MultiplicationCommand();
                    break;
                case "/":
                    command = new DivisionCommand();
                    break;  
                case "=":
                    command = new EqualCommand();
                    break;
                case "π":
                    command = new EnterPiCommand();
                    break;
                case "√":
                    command = new SquareRootCommand();
                    break;
                case "x²":
                    command = new ToTheSquareCommand();
                    break;
                case "n!":
                    command = new FactorialCommand();
                    break;
                case "±":
                    command = new ChangeSignCommand();
                    break;
                case "➥":
                    if (PiButton.Visibility == Visibility.Collapsed)
                    {
                        PiButton.Visibility = Visibility.Visible;
                        PlusMinusButton.Visibility = Visibility.Visible;
                        SquareRootButton.Visibility = Visibility.Visible;
                        SquareButton.Visibility = Visibility.Visible;
                        FactorialButton.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        PiButton.Visibility = Visibility.Collapsed;
                        PlusMinusButton.Visibility = Visibility.Collapsed;
                        SquareRootButton.Visibility = Visibility.Collapsed;
                        SquareButton.Visibility = Visibility.Collapsed;
                        FactorialButton.Visibility = Visibility.Collapsed;
                    }
                    if (PiButton.Visibility == Visibility.Visible ||
                        PlusMinusButton.Visibility == Visibility.Visible ||
                        SquareRootButton.Visibility == Visibility.Visible ||
                        SquareButton.Visibility == Visibility.Visible ||
                        FactorialButton.Visibility == Visibility.Visible)
                    {
                        InnerGrid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                        InnerGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                        InnerGrid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
                        InnerGrid.ColumnDefinitions[3].Width = new GridLength(1, GridUnitType.Star);
                        InnerGrid.ColumnDefinitions[4].Width = new GridLength(1, GridUnitType.Star);
                    }
                    else
                    {
                        InnerGrid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                        InnerGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                        InnerGrid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
                        InnerGrid.ColumnDefinitions[3].Width = new GridLength(1, GridUnitType.Star);
                        InnerGrid.ColumnDefinitions[4].Width = new GridLength(0, GridUnitType.Star);
                    }
                    break;
                default:
                    break;
            }

            command?.Execute();
        }

        private void Copy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Clipboard.SetText(tbOut.Text);
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (computationHistory.Count != 0) e.CanExecute = true;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string logFilePath = Directory.GetCurrentDirectory() + @"\" + "Session_" + DateTime.Now.ToString("dd.MM.yyyy") + 'T' + DateTime.Now.ToString("hh.mm.ss") + ".txt";
            using (StreamWriter streamWriter = new StreamWriter(logFilePath))
            {
                int i = 0;
                foreach (string computation in computationHistory)
                {
                    i++;
                    streamWriter.WriteLine(i.ToString() + ") " + computation);
                }
                MessageBox.Show("History of computations has been saved in the file: " + logFilePath);

            }
        }
    }
}
