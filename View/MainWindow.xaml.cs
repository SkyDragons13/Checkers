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
using System.IO;
using Checkers.ViewModel;
using Checkers.Services;

namespace Checkers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string CheckboxFilePath = "Checkbox.txt";
        public MainWindow()
        {
            InitializeComponent();
            DataContext= new MainWindowViewModel();
            MainFrame.NavigationService.Navigating += NavigationService_Navigating;
            Closing += Window_Closing;
            Checkbox.IsChecked = Helper.LoadCheckboxState(CheckboxFilePath);
        }
        private void NavigationService_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            // Hide the main window's content when navigating
            if (e.Content != null)
            {
                Content.Visibility = Visibility.Collapsed;
            }
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                ((MainWindowViewModel)DataContext).IsMultipleJumpsEnabled = checkBox.IsChecked ?? false;
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Save checkbox state when the window is closing
            File.WriteAllText(CheckboxFilePath, ((MainWindowViewModel)DataContext).IsMultipleJumpsEnabled ? "1" : "0");
        }

    }
}
