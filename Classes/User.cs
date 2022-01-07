namespace EmptyTemplate.Classes
{

    public class User : ObservableObject
    {
        private string _login;
        private string _password;
        public string Login
        {
            get => _login;
            set => OnPropertyChanged(ref _login, value);
        }

        public string Password
        {
            get => _password;
            set => OnPropertyChanged(ref _password, value);
        }

        public bool IsAdmin { get; private set; }
        public User() { }
        public User(string login, bool isAdmin)
        {
            Login = login;
            IsAdmin = isAdmin;
        }
    }
}
