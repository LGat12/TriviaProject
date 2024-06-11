using System;
using System.Windows;
using System.Windows.Controls;
using TriviaProject.ViewModel;

namespace TriviaProject.View
{
    public partial class ChooseCategoryPage : Page
    {
        private SharedViewModel _sharedViewModel;

        public ChooseCategoryPage(SharedViewModel sharedViewModel)
        {
            InitializeComponent();
            _sharedViewModel = sharedViewModel;
        }

        private void Geography_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigateToQuizPage("Geography");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            NavigateToQuizPage("History");
        }

        private void Sports_Click(object sender, RoutedEventArgs e)
        {
            NavigateToQuizPage("Sports");
        }

        private void NavigateToQuizPage(string category)
        {
            // Create a new instance of the quiz page and pass the selected category
            try
            {
                // Create a new instance of the quiz page and pass the selected category
                QuizPage quizPage = new QuizPage(_sharedViewModel, category);

                // Navigate to the quiz page
                this.NavigationService.Navigate(quizPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
