using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace EmptyTemplate.Classes
{
    class SQLServerConnection
    {
        internal static string GetConnectionStrings()
        {
            string strConString = ConfigurationManager.ConnectionStrings["conString"].ToString();
            return strConString;
        }

        public static string sql;
        public static SqlConnection con = new SqlConnection();
        public static SqlCommand cmd = new SqlCommand("", con);
        public static SqlDataReader rd;
        public static DataTable dt;
        public static SqlDataAdapter da;

        public static void openConnection()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.ConnectionString = GetConnectionStrings();
                    con.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection failed!" + Environment.NewLine +
                                "Description: " + ex.Message.ToString(), "WPF connection to sql", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void Login(string login, string password)
        {
            using (var connection = new SqlConnection(GetConnectionStrings()))
            {
                connection.Open();

                var takeUserCommand = new SqlCommand("Select password, isAdmin from Users where login=@0", connection);
                takeUserCommand.Parameters.AddWithValue("0", login);
                var userReader = takeUserCommand.ExecuteReader();

                if (!userReader.HasRows)
                {
                    throw new ArgumentException($"Пользователь с логином \"{login}\" не найден");
                }

                userReader.Read();
                var userPassword = userReader.GetString(0);
                var isAdmin = userReader.GetBoolean(1);

                if (userPassword != password)
                {
                    throw new ArgumentException("Неверный пароль");
                }

                Global.CurrentUser = new User(login, isAdmin);
            }
        }

        public static void Register(string login, string password, string email, bool isAdmin = false)
        {
            using (var connection = new SqlConnection(GetConnectionStrings()))
            {
                connection.Open();

                var takeUserCommand = new SqlCommand("Select login From Users where login=@0", connection);
                takeUserCommand.Parameters.AddWithValue("0", login);
                var candidateUser = takeUserCommand.ExecuteScalar();

                if (candidateUser != null)
                {
                    throw new ArgumentException($"Пользователь с логином \"{login}\" уже существует");
                }

                var insertUserCommand = new SqlCommand("Insert_User", connection) { CommandType = CommandType.StoredProcedure };
                insertUserCommand.Parameters.AddWithValue("login", login);
                insertUserCommand.Parameters.AddWithValue("password", password);
                insertUserCommand.Parameters.AddWithValue("isAdmin", isAdmin);
                insertUserCommand.Parameters.AddWithValue("email", email);

                insertUserCommand.ExecuteNonQuery();

                MessageBox.Show("Успешная регистрация!");
                Global.CurrentUser = new User(login, isAdmin);
            }
        }
        public static void closeConnection()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Connection failed!" + Environment.NewLine +
                                 "Description: " + ex.Message.ToString(), "WPF connection to sql", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
