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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelSys
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private const string dataPath = @"Data";

        public static string userName = ""; 
        public static string userPassword = ""; 

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(dataPath + "\\Users"))
            {
                Directory.CreateDirectory(dataPath + "\\Users");
            }

            userName = txtLogin.Text;

            if (userName.Length == 0)
            {
                MessageBox.Show("Логін не може бути пустий");
                return;
            }

            userPassword = txtPassword.Password;

            if (userPassword.Length == 0)
            {
                MessageBox.Show("Пароль не може бути пустий");
                return;
            }

            try
            {
                string[] tmpStringArray = File.ReadAllLines(dataPath + "\\Users\\" + userName + "\\" + "Info.txt");

                string hashOfPassword = BCrypt.Net.BCrypt.HashPassword(userPassword);

                if (BCrypt.Net.BCrypt.Verify(userPassword, tmpStringArray[0]) == true)
                {
                    MessageBox.Show("Авторизація успішна");
                    MainMenuWindow mainMenuWindow = new MainMenuWindow();
                    mainMenuWindow.Show();
                    this.Close();
                   
                }
                else
                {
                    MessageBox.Show("Не вірний логін або пароль");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Проблеми з автризацією");
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.Show();
            this.Close(); 
        }
    }
}
