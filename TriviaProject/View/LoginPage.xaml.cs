using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TriviaProject.ViewModel;

namespace TriviaProject.View
{
    public partial class LoginPage : Page
    {
        private SharedViewModel _sharedViewModel;

        public LoginPage(SharedViewModel sharedViewModel)
        {
            InitializeComponent();
            _sharedViewModel = sharedViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string enteredEmail = UsrTxtBox.Text;
                string enteredPassword = PwdBox.Password;

                if (string.IsNullOrWhiteSpace(enteredEmail) || string.IsNullOrWhiteSpace(enteredPassword))
                {
                    DisplayError("Email and password cannot be empty.");
                    return;
                }

                string connectionString = @"Data Source=C:\Users\User\Downloads\DBAllUsers.db;Version=3;";
                bool matchedUser = false;
                bool isAdmin = false;
                int userID = -1;

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT UserID, IsAdmin FROM Users WHERE Email = @Email AND Password = @Password LIMIT 1";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Email", enteredEmail);
                        command.Parameters.AddWithValue("@Password", enteredPassword);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                matchedUser = true;
                                isAdmin = Convert.ToInt32(reader["IsAdmin"]) == 1;
                                userID = Convert.ToInt32(reader["UserID"]);
                            }
                        }
                    }
                }

                if (matchedUser)
                {
                    if (isAdmin)
                    {
                        // Navigate to the admin page
                        // You need to create AdminPage and pass necessary parameters if needed
                        this.NavigationService.Navigate(new AdminPage(_sharedViewModel));
                    }
                    else
                    {
                        // Navigate to the regular user page
                        this.NavigationService.Navigate(new ChooseCategoryPage(_sharedViewModel));
                    }
                }
                else
                {
                    DisplayError("Invalid email or password. Please try again.");
                }
            }
            catch (Exception ex)
            {
                DisplayError($"An error occurred: {ex.Message}");
            }
        }


        private void DisplayError(string message)
        {
            Error.Play();
            Error.Stop();
            Error.Position = TimeSpan.Zero;
            Error.Play();
            MessageBox.Show(message);
        }

        private void PwdBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PwdBox.Password = "";
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Register(_sharedViewModel));
        }

        private void UsrTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UsrTxtBox.Text = "";
        }

        private void Button_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
