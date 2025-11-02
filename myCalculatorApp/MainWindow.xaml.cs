using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace myCalculatorApp
{
    /// <summary>
    /// A simple calculator class that is able to perform + - X / and some more functionalities
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        private double _memory = 0;
        private double _firstNumber = 0;
        private string _operation = "";
        private bool _isResultDisplayed = false;
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Number Input
        /// <summary>
        /// Updates display when a number button is clicked. 
        /// If result has been displayed to user -> resets display
        /// </summary>
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isResultDisplayed)
            {
                Display.Text = "";
                _isResultDisplayed = false;
            }
            Button button = (Button)sender;
            Display.Text += button.Content.ToString();   
        }
        #endregion

        #region Add Decimal
        /// <summary>
        /// Adds decimal to the displayed number
        /// If result has been displayed -> resets display
        /// </summary>
        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isResultDisplayed) 
            {
                Display.Text = "";
                _isResultDisplayed = false;
            }

            if (!Display.Text.Contains("."))
            {
                Display.Text += ".";
            }
        }
        #endregion

        #region Clear Display
        /// <summary>
        /// Clears display entirely
        /// </summary>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Display.Text = "";
            _firstNumber = 0;
            _operation = "";
        }
        #endregion

        #region Back Space
        /// <summary>
        /// Clears the last number on the display
        /// </summary>
        private void BackSpaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text.Length > 0)
            {
                Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
            }
        }
        #endregion

        #region Toggle Sign
        /// <summary>
        /// Toggles the sign of the number on the display
        /// </summary>
        private void ToggleSignButton_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text.Length == 0 || Display.Text == "0")
            {
                return;
            }
            
            if (Display.Text.StartsWith("-"))
            {
                Display.Text = Display.Text.Substring(1);
            }
            else
            {
             Display.Text = "-" + Display.Text;
            }
           
        }
        #endregion

        #region Operand Input
        /// <summary>
        /// Saves the displayed number when an operand is pushed then resets display
        /// </summary>
        private void OperandButton_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text.Length == 0) {
                return;
            }
            Button operand = (Button)sender;
            double.TryParse(Display.Text.ToString(), out _firstNumber);
            _operation = operand.Content.ToString();
            Display.Text = "";
        }
        #endregion

        #region Calculate
        /// <summary>
        /// Performs calculation
        /// </summary>
        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text.Length == 0 || _operation == "")
            // if dispaly is empty or there is no operand return
            {

                return;
            }

            double.TryParse(Display.Text, out double secondNumber);
            double result = 0;

           switch(_operation)
            {
                case "+":
                    result = _firstNumber + secondNumber;
                    break;
                case "-":
                    result = _firstNumber - secondNumber;
                    break;
                case "×":
                    result = _firstNumber * secondNumber;
                    break;
                case "÷":
                    if (secondNumber == 0)
                    // if second number is 0 display error
                    {
                        Display.Text = "Error";
                        _operation = "";
                        _isResultDisplayed = true;
                        return;
                    }

                    result = _firstNumber / secondNumber;
                    break;

                default:
                    return;
            }
            Display.Text = result.ToString();
            _firstNumber = result;
            _operation = "";
            _isResultDisplayed = true;
        }
        #endregion

        #region Memory functions
        /// <summary>
        /// Clears Memory
        /// </summary>
        private void McButton_Click(object sender, RoutedEventArgs e)
        {
            _memory = 0;
        }

        /// <summary>
        /// Reads memory to display
        /// </summary>
        private void MrButton_Click(object sender, RoutedEventArgs e)
        {
            Display.Text = _memory.ToString();
        }

        /// <summary>
        /// Adds the displayed number to the memory
        /// </summary>
        private void MplusButton_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text.Length > 0)
            {
                double.TryParse(Display.Text, out double displayedNumber); 
                _memory += displayedNumber;
            }
        }

        /// <summary>
        /// Subtracts the displayed number from the memory
        /// </summary>
        private void MminusButton_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text.Length > 0)
            {
                double.TryParse(Display.Text, out double displayedNumber);
                _memory -= displayedNumber;
            }
        }
        #endregion
    }
}