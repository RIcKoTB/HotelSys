using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
using static HotelSys.BusyNumbersWindow;

namespace HotelSys
{
    /// <summary>
    /// Логика взаимодействия для MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {

        List<FreeNumbers> data = new List<FreeNumbers>();

        private const string freeNumbersPath = @"Data\\Numbers.txt";

        public MainMenuWindow()
        {
            InitializeComponent();
            readFile();
        }

        public void readFile()
        {
            string[] lines = File.ReadAllLines(freeNumbersPath);

            foreach (string line in lines)
            {
                string[] values = line.Split('|');

                if (values.Length >= 5) // Переконатися, що масив містить принаймні 6 елементів
                {
                    // Додавання нового рядка до DataGrid
                    data.Add(new FreeNumbers(values[0], values[1], values[2], values[3], values[4]));
                }
                else
                {
                    Console.WriteLine("Рядок не містить достатньо елементів");
                }
            }

            dgTable.ItemsSource = data;
        }


        public class FreeNumbers
        {
            public string IdNumbers { get; set; }
            public string Status { get; set; }
            public string Type { get; set; }
            public string DateArrivale { get; set; }
            public string DateDepartue { get; set; }

            public FreeNumbers(string idNumbers, string status, string type, string dateArrivale, string dateDepartue)
            {
                this.IdNumbers = idNumbers;
                this.Status = status;
                this.Type = type;
                this.DateArrivale = dateArrivale;
                this.DateDepartue = dateDepartue;
            }
        }

        private void btnBusyNumbers_Click(object sender, RoutedEventArgs e)
        {
            BusyNumbersWindow busyNumbersWindow = new BusyNumbersWindow();
            busyNumbersWindow.Show();
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
            ChangeReservWindow changeReservWindow = new ChangeReservWindow();
            changeReservWindow.Show();
            this.Close();
        }
    }
}
