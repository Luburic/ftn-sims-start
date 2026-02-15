namespace CommunityHub.Application.Domain;

public class User
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDay { get; set; }

    public User(long id, string username, string password, string name, string surname, DateTime birthDay)
    {
        Id = id;
        Username = username;
        Password = password;
        Name = name;
        Surname = surname;
        BirthDay = birthDay;
    }
}
