using System.Windows;
using CommunityHub.Application.Database.Repositories;

namespace CommunityHub.Ui.Views;

public partial class LogInForm : Window
{
    private readonly UserDbRepository _userRepository;

    public LogInForm()
    {
        InitializeComponent();
        _userRepository = new UserDbRepository();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        string username = UsernameTextBox.Text;
        string password = PasswordBox.Password;

        long userId = _userRepository.GetIdByCredentials(username, password);

        if (userId != -1)
        {
            HomeWindow homeWindow = new HomeWindow(userId);
            homeWindow.Show();
            this.Close();
        }
        else
        {
            ErrorMessageTextBlock.Text = "Neispravno korisničko ime ili lozinka.";
            ErrorMessageTextBlock.Visibility = Visibility.Visible;
        }
    }
}