using System;
using System.Collections.Generic;
using System.IO;
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

namespace HotelSys
{
    /// <summary>
    /// Логика взаимодействия для SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        public SignUpWindow()
        {
            InitializeComponent();
        }

        private const string dataPath = @"Data";

        public static string userName = "";
        public static string userPassword = "";


        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(dataPath + "\\Users"))
            {
                Directory.CreateDirectory(dataPath + "\\Users");
            }

            userName = txtLogin.Text;

            if (userName.Length == 0) // Перевірка на пусте поле
            {
                MessageBox.Show("Логін не може бути пустий");
                return;
            }

            userPassword = txtPassword.Password;

            if (userPassword.Length == 0) // Перевірка на пусте поле
            {
                MessageBox.Show("Пароль не може бути пустий");
                return;
            }


            if (Directory.Exists(dataPath + "\\Users\\" + userName))
            {
                MessageBox.Show("Користувач вже інсує");
                return;
            }

            Directory.CreateDirectory(dataPath + "\\Users\\" + userName);

            string hashOfPassword = BCrypt.Net.BCrypt.HashPassword(userPassword);

            File.WriteAllText(dataPath + "\\Users\\" + userName + "\\Info.txt", hashOfPassword + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n");
            MessageBox.Show("Реєстрація успішна!");

            MainWindow signInWindow = new MainWindow();
            signInWindow.Show();
            this.Close();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow signInWindow = new MainWindow();
            signInWindow.Show();
            this.Close();
        }
    }
}
