using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Guess_the_number_WpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int secretNumber;
        private int attempts;

        public MainWindow()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            secretNumber = new Random().Next(1,100);
            attempts = 0;

            HintTextBlock.Inlines.Clear();
            HintTextBlock.Inlines.Add(new Run("Введите число от 1 до 100"));

            AttemptsTextBlock.Text = "Попыток: 0";
            GuessTextBox.Text = "";
            GuessTextBox.IsEnabled = true;
            CheckButton.IsEnabled = true;
            NewGameButton.Visibility = Visibility.Hidden;
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(GuessTextBox.Text, out int userGuess))
            {
                MessageBox.Show("Пожалуйста, введите целое число.", "Ошибка"); return;
            }

            if (userGuess < 1 || userGuess > 100)
            {
                MessageBox.Show("Число должно быть от 1 до 100.", "Ошибка"); return;
            }

            attempts++;
            AttemptsTextBlock.Text = $"Попыток: {attempts}";

            if (userGuess == secretNumber)
            {
                HintTextBlock.Inlines.Clear();
                HintTextBlock.Inlines.Add(new Run($"Поздравляю! Вы угадали число {secretNumber} за {attempts} попыток.")
                {
                    Foreground = Brushes.Black
                });
                GuessTextBox.IsEnabled = false;
                CheckButton.IsEnabled = false;
                NewGameButton.Visibility = Visibility.Visible;
            }
            else if (userGuess < secretNumber) // >
            {
                HintTextBlock.Inlines.Clear();
                HintTextBlock.Inlines.Add(new Run("Загаданное число ") { Foreground = Brushes.Black });
                HintTextBlock.Inlines.Add(new Run("больше") { Foreground = Brushes.Red });
                HintTextBlock.Inlines.Add(new Run(".") { Foreground = Brushes.Black });
            }
            else // <
            {
                HintTextBlock.Inlines.Clear();
                HintTextBlock.Inlines.Add(new Run("Загаданное число ") { Foreground = Brushes.Black });
                HintTextBlock.Inlines.Add(new Run("меньше") { Foreground = Brushes.Green });
                HintTextBlock.Inlines.Add(new Run(".") { Foreground = Brushes.Black });
            }
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }
    }
}
