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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelSys
{
    /// <summary>
    /// Логика взаимодействия для BusyNumbersWindow.xaml
    /// </summary>
    public partial class BusyNumbersWindow : Window
    {

        List<BusyNumbers> data = new List<BusyNumbers>();

        private const string busyNumbersPath = @"Data\\BusyNumbers.txt";

        public BusyNumbersWindow()
        {
            InitializeComponent();
            readFile();
        }

        public void readFile()
        {
            string[] lines = File.ReadAllLines(busyNumbersPath);

            foreach (string line in lines)
            {
                string[] values = line.Split('|');

                if (values.Length >= 6) // Переконатися, що масив містить принаймні 6 елементів
                {
                    // Додавання нового рядка до DataGrid
                    data.Add(new BusyNumbers(values[0], values[1], values[2], values[3], values[4], values[5]));
                }
                else
                {
                    Console.WriteLine("Рядок не містить достатньо елементів");
                }
            }

            dgTable.ItemsSource = data;
        }


        public class BusyNumbers
        {
            public string IdNumbers { get; set; }
            public string Status { get; set; }
            public string PIB { get; set; }
            public string Type { get; set; }
            public string DateArrivale { get; set; }
            public string DateDepartue { get; set; }

            public BusyNumbers(string idNumbers, string status, string pib, string type, string dateArrivale, string dateDepartue)
            {
                this.IdNumbers = idNumbers;
                this.Status = status;
                this.PIB = pib;
                this.Type = type;
                this.DateArrivale = dateArrivale;
                this.DateDepartue = dateDepartue;
            }
        }

        private void btnFreeNumbers_Click(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainMenuWindow = new MainMenuWindow();
            mainMenuWindow.Show();
            this.Close();
        }

        private void btnReserv_Click(object sender, RoutedEventArgs e)
        {
            ReservNumberWindow reservNumberWindow = new ReservNumberWindow();
            reservNumberWindow.Show();
            this.Close();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            ReservNumberWindow reservNumberWindow = new ReservNumberWindow();
            reservNumberWindow.Show();
            this.Close();
        }
    }
}
