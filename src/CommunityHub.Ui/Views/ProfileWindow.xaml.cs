using System.Windows;
using CommunityHub.Application.Database.Repositories;
using CommunityHub.Application.Domain;

namespace CommunityHub.Ui.Views;

public partial class ProfileWindow : Window
{
    private readonly long _userId;
    private readonly UserDbRepository _userRepository;
    private User _user;

    public ProfileWindow(long userId)
    {
        InitializeComponent();
        _userId = userId;
        _userRepository = new UserDbRepository();
        LoadUser();
    }

    private void LoadUser()
    {
        _user = _userRepository.GetWithPosts(_userId);
        UserInfoTextBlock.Text = $"{_user.Name} {_user.Surname} ({_user.BirthDay.ToString("dd.MM.yyyy.")})";

        if (_user.Posts != null)
        {
            PostsItemsControl.ItemsSource = _user.Posts;
        }
    }

    private void HomeButton_Click(object sender, RoutedEventArgs e)
    {
        HomeWindow homeWindow = new HomeWindow(_userId);
        homeWindow.Show();
        this.Close();
    }

    private void LogoutButton_Click(object sender, RoutedEventArgs e)
    {
        LogInForm loginForm = new LogInForm();
        loginForm.Show();
        this.Close();
    }
}
