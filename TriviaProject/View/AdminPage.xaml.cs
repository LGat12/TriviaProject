using System;
using System.Windows;
using System.Windows.Controls;
using TriviaProject.ViewModel;

namespace TriviaProject.View
{
    public partial class AdminPage : Page
    {
        private AdminViewModel _adminViewModel;
        private SharedViewModel _sharedViewModel;
        public AdminPage(SharedViewModel sharedViewModel)
        {
            InitializeComponent();
            _sharedViewModel = sharedViewModel;
            _adminViewModel = new AdminViewModel();
            DataContext = _adminViewModel;
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            _adminViewModel.SaveChanges();
        }

        private void ShowUsersButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataGrid.Visibility = Visibility.Visible;
            QuestionsDataGrid.Visibility = Visibility.Collapsed;
            AnswerDataGrid.Visibility = Visibility.Collapsed;
        }

        private void ShowQuestionsButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataGrid.Visibility = Visibility.Collapsed;
            QuestionsDataGrid.Visibility = Visibility.Visible;
            AnswerDataGrid.Visibility = Visibility.Collapsed;
        }

        private void ShowAnswersButton_Click(object sender, RoutedEventArgs e)
        {
            UserDataGrid.Visibility = Visibility.Collapsed;
            QuestionsDataGrid.Visibility = Visibility.Collapsed;
            AnswerDataGrid.Visibility = Visibility.Visible;
        }

        private void Play_Quiz(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ChooseCategoryPage(_sharedViewModel));

        }
    }
}
