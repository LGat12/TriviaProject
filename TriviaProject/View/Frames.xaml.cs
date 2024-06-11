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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TriviaProject.View;
using TriviaProject.ViewModel;

namespace TriviaProject.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Frames : Window
    {
        private SharedViewModel _sharedViewModel;
        private UserViewModel _userViewModel;
        public Frames()
        {
            InitializeComponent();

            _sharedViewModel = new SharedViewModel();
            _userViewModel = new UserViewModel(_sharedViewModel);
            DataContext = _userViewModel;
            

            mainFrame.Navigate(new LoginPage(_sharedViewModel));
            
        }


        private void ClickClose(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
            this.Close();
        }

        private void ClickLogout(object sender, RoutedEventArgs e)
        {


            mainFrame.Navigate(new LoginPage(_sharedViewModel));
           
        }
    }
}
