using System;
using System.Windows;

namespace EmptyTemplate
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class WorkSpace : Window
    {
        public WorkSpace()
        {
            InitializeComponent();
            ShowAdminControls();
            if (!Classes.Global.CurrentUser.IsAdmin)
            {
                HideAdminControls();
            }
            User_data.Text = Classes.Global.CurrentUser.Login;
        }

        private void ShowAdminControls()
        {
            Admin.Visibility = Visibility.Visible;
        }

        private void HideAdminControls()
        {
            User.Visibility = Visibility.Visible;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            AuthForm auth = new AuthForm();
            auth.Show();
            Classes.Global.CurrentUser = default;
            Close();

        }
    }
}
