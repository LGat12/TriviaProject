using System.Windows;
using System.Windows.Controls;
using TriviaProject.ViewModel;
using TriviaProject.Model;
using System;

namespace TriviaProject.View
{
    public partial class QuizPage : Page
    {
        private SharedViewModel _sharedViewModel;
        private GameLogic gameLogic;
        private string category;

        public QuizPage(SharedViewModel sharedViewModel, string category1)
        {
            InitializeComponent();
            QuizMusic.Play();
            _sharedViewModel = sharedViewModel;
            category = category1;
            gameLogic = new GameLogic(category);
            InitializeUI();
        }

        private void InitializeUI()
        {
            DisplayNextQuestion();
        }

        private void DisplayNextQuestion()
        {
            var questionData = gameLogic.GetNextQuestion();
            if (questionData != null)
            {
                Questions.Text = questionData.QuestionText; // Use QuestionText instead of Question

                // Set answer buttons content
                if (questionData.PossibleAnswers.Count >= 4)
                {
                    ans1.Content = questionData.PossibleAnswers[0].AnswerText; // Use AnswerText instead of Answer
                    ans2.Content = questionData.PossibleAnswers[1].AnswerText; // Use AnswerText instead of Answer
                    ans3.Content = questionData.PossibleAnswers[2].AnswerText; // Use AnswerText instead of Answer
                    ans4.Content = questionData.PossibleAnswers[3].AnswerText; // Use AnswerText instead of Answer
                }
                else
                {
                    MessageBox.Show("Not enough answers fetched from the database.");
                }
            }
            else
            {
                DisplayGrade();
            }
        }


        private void AnswerButtonClick(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            string selectedAnswer = clickedButton.Content.ToString();

            if (gameLogic.IsCorrectAnswer(selectedAnswer))
            {
                Correct.Play();
                Correct.Stop();
                Correct.Position = TimeSpan.Zero;
                Correct.Play();
                MessageBox.Show("Correct answer!");

            }
            else
            {

                Incorrect.Play();
                Incorrect.Stop();
                Incorrect.Position = TimeSpan.Zero;
                Incorrect.Play();
                MessageBox.Show("Incorrect answer!");

            }

            // Fetch a new question and answers for the next round
            DisplayNextQuestion();
        }


        private void DisplayGrade()
        {
            int correctAnswers = gameLogic.GetCorrectAnswersCount();
            int totalQuestions = gameLogic.GetTotalQuestions();
            double grade = ((double)correctAnswers / totalQuestions) * 100;

            FinalScorePage finalScorePage = new FinalScorePage(grade, _sharedViewModel);
            NavigationService.Navigate(finalScorePage);
        }
    }
}