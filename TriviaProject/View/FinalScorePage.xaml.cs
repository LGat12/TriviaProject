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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TriviaProject.ViewModel;

namespace TriviaProject.View
{
    /// <summary>
    /// Interaction logic for FinalScorePage.xaml
    /// </summary>
    public partial class FinalScorePage : Page
    {
        SharedViewModel _sharedViewModel;
        public FinalScorePage(double grade, SharedViewModel sharedViewModel)
        {
            InitializeComponent();
            Score.Play();
            GradeTextBlock.Text = $"Your grade: {grade}%";
            _sharedViewModel = sharedViewModel;
        }

        public void ReturnToCategorySelection_Click(object sender, RoutedEventArgs e)
        {
            ChooseCategoryPage chooseCatagoryPage = new ChooseCategoryPage(_sharedViewModel);
            this.NavigationService.Navigate(chooseCatagoryPage);

        }

    }
}
