using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Windows;
using TriviaProject.Model;

namespace TriviaProject.ViewModel
{
    public class AdminViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<User> Users { get; private set; }
        public ObservableCollection<Question> Questions { get; private set; }
        public ObservableCollection<Answer> Answers { get; private set; }

        private string connectionString = @"Data Source=C:\Users\User\Downloads\DBAllUsers.db;Version=3;";

        public AdminViewModel()
        {
            Users = new ObservableCollection<User>();
            Questions = new ObservableCollection<Question>();
            Answers = new ObservableCollection<Answer>();

            LoadData();
        }

        private void LoadData()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Fetch data from the Users table
                string userSql = "SELECT * FROM Users";
               

                using (SQLiteCommand userCommand = new SQLiteCommand(userSql, connection))
                {
                    using (SQLiteDataReader userReader = userCommand.ExecuteReader())
                    {
                        while (userReader.Read())
                        {
                            Users.Add(new User
                            {
                                UserId = userReader.GetInt32(userReader.GetOrdinal("UserID")),
                                FirstName = userReader.GetString(userReader.GetOrdinal("FirstName")),
                                LastName = userReader.GetString(userReader.GetOrdinal("LastName")),
                                Email = userReader.GetString(userReader.GetOrdinal("Email")),
                                Password = userReader.GetString(userReader.GetOrdinal("Password")),
                                // Add other properties as needed
                            });
                        }
                    }
                }

                // Fetch data from the Questions table
                string questionSql = "SELECT * FROM Questions";
                

                using (SQLiteCommand questionCommand = new SQLiteCommand(questionSql, connection))
                {
                    using (SQLiteDataReader questionReader = questionCommand.ExecuteReader())
                    {
                        while (questionReader.Read())
                        {
                            Questions.Add(new Question
                            {
                                QuestionId = questionReader.GetInt32(questionReader.GetOrdinal("QuestionID")),
                                QuestionText = questionReader.GetString(questionReader.GetOrdinal("Question")),
                                Category = questionReader.GetString(questionReader.GetOrdinal("Category")),
                                // Add other properties as needed
                            });
                        }
                    }
                }

                // Fetch data from the Answers table
                string answerSql = "SELECT * FROM Answers";
                

                using (SQLiteCommand answerCommand = new SQLiteCommand(answerSql, connection))
                {
                    using (SQLiteDataReader answerReader = answerCommand.ExecuteReader())
                    {
                        while (answerReader.Read())
                        {
                            Answers.Add(new Answer
                            {
                                AnswerId = answerReader.GetInt32(answerReader.GetOrdinal("AnswerID")),
                                AnswerText = answerReader.GetString(answerReader.GetOrdinal("Answer")),
                                QuestionId = answerReader.GetInt32(answerReader.GetOrdinal("QuestionID")),
                                IsCorrect = answerReader.GetBoolean(answerReader.GetOrdinal("IsCorrect")),
                                // Add other properties as needed
                            });
                        }
                    }
                }
            }
        }

        public void SaveChanges()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Update Users
                        foreach (User user in Users)
                        {
                            string updateUserSql = @"UPDATE Users 
                                                 SET FirstName = @FirstName, 
                                                     LastName = @LastName, 
                                                     Email = @Email, 
                                                     Password = @Password
                                                 WHERE UserId = @UserId";

                            using (SQLiteCommand command = new SQLiteCommand(updateUserSql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                                command.Parameters.AddWithValue("@LastName", user.LastName);
                                command.Parameters.AddWithValue("@Email", user.Email);
                                command.Parameters.AddWithValue("@Password", user.Password);
                                command.Parameters.AddWithValue("@UserId", user.UserId);

                                command.ExecuteNonQuery();
                            }
                        }

                        // Update Questions
                        foreach (Question question in Questions)
                        {
                            string updateQuestionSql = @"UPDATE Questions 
                                                 SET Question = @QuestionText,
                                                     Category = @Category
                                                 WHERE QuestionID = @QuestionId";

                            using (SQLiteCommand command = new SQLiteCommand(updateQuestionSql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@QuestionText", question.QuestionText);
                                command.Parameters.AddWithValue("@Category", question.Category);
                                command.Parameters.AddWithValue("@QuestionId", question.QuestionId);

                                command.ExecuteNonQuery();
                            }
                        }

                        // Update Answers
                        foreach (Answer answer in Answers)
                        {
                            string updateAnswerSql = @"UPDATE Answers 
                                               SET Answer = @AnswerText,
                                                   QuestionID = @QuestionId,
                                                   IsCorrect = @IsCorrect
                                               WHERE AnswerID = @AnswerId";

                            using (SQLiteCommand command = new SQLiteCommand(updateAnswerSql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@AnswerText", answer.AnswerText);
                                command.Parameters.AddWithValue("@QuestionId", answer.QuestionId);
                                command.Parameters.AddWithValue("@IsCorrect", answer.IsCorrect);
                                command.Parameters.AddWithValue("@AnswerId", answer.AnswerId);

                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while saving changes: {ex.Message}");
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
