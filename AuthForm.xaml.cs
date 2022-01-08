//
using EmptyTemplate.Classes;
using System;
using System.Windows;
using System.Windows.Media;

namespace EmptyTemplate
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// exec sp_who на Azure посмотреть подключения
    /// Connect to Azure:
    /// Server=tcp:sqlwpfserver.database.windows.net,1433;Initial Catalog=todaywpf;Persist Security Info=False;User ID=Nikolai;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
    /// Connect to SqlServer:
    ///Data Source = DESKTOP - TUGTERO\SQLSERVERLOCALDB;Initial Catalog = TodayWPF; Integrated Security = True
    public partial class AuthForm : Window
    {
        public AuthForm()
        {
            InitializeComponent();
        }



        private void Register_Btn_Click(object sender, RoutedEventArgs e)
        {
            var login = Form_Login.Text.Trim();
            var password = Form_password.Password.Trim();
            var re_password = Form_re_password.Password.Trim();
            var email = Form_Email.Text.Trim().ToLower();

            try
            {
                SQLServerConnection.Register(login, password, email);
                Hide();
                WorkSpace workspace = new WorkSpace();
                workspace.Show();
            }
            catch (ArgumentException ex)
            {
                _ = MessageBox.Show(ex.Message);
            }
        }

        private void Login_Btn_Click(object sender, RoutedEventArgs e)
        {
            var login = User_Login.Text;
            var password = User_password.Password;

            try
            {
                SQLServerConnection.Login(login, password);
                var workspace = new WorkSpace();
                workspace.Show();
                Close();

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Reg_Pnl_Click(object sender, RoutedEventArgs e)
        {
            Reg_Panel.Visibility = Visibility.Visible;
            Log_Panel.Visibility = Visibility.Collapsed;
        }

        private void Log_Panl_Click(object sender, RoutedEventArgs e)
        {
            Log_Panel.Visibility = Visibility.Visible;
            Reg_Panel.Visibility = Visibility.Collapsed;
        }

        private void Pas_Changed(object sender, RoutedEventArgs e)
        {

            if (Form_password.Password.Length < 5)
            {
                Form_password.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B00020"));
                Pas_stat.Text = "Password too short";
            }
            else
            {
                Pas_stat.Text = string.Empty;
                Form_password.ClearValue(BorderBrushProperty);
            }

        }

        private void Rep_Pas_Changed(object sender, RoutedEventArgs e)
        {
            if (Form_re_password.Password == Form_password.Password)
            {
                Rep_pas_stat.Text = string.Empty;
                Form_re_password.ClearValue(BorderBrushProperty);
            }
            else
            {
                Form_re_password.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B00020"));
                Rep_pas_stat.Text = "Password doesn't match";
            }
        }
    }
}
