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
    /// Логика взаимодействия для ChangeReservWindow.xaml
    /// </summary>
    public partial class ChangeReservWindow : Window
    {
        public ChangeReservWindow()
        {
            InitializeComponent();
            readFile();
        }


        private const string freeNumbersPath = @"Data\\Numbers.txt";
        private const string busyNumbersPath = @"Data\\BusyNumbers.txt";


        public void readFile()
        {
            string[] lines2 = File.ReadAllLines(busyNumbersPath);

            List<string> firstElements = new List<string>();

            foreach (string line in lines2)
            {
                string[] elements = line.Split('|');
                if (elements.Length > 0)
                {
                    firstElements.Add(elements[0]);
                }
            }

            foreach (string element in firstElements)
            {
                cmbFirst.Items.Add(element);
            }

        }


        private void btnReserv_Click(object sender, RoutedEventArgs e)
        {
            string[] lines = File.ReadAllLines(busyNumbersPath);
            List<string> updatedLines = new List<string>();

            string targetRoom = cmbFirst.Text;

            bool foundRoom = false;

            // Знаходимо рядок з цільовою кімнатою в першому файлі та міняємо його значення
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('|');

                if (parts.Length > 2 && parts[0].Equals(targetRoom))
                {
                    lines[i] = $"{targetRoom}|Вільно|{parts[2]}|Дата прибуття|Дата відбуття";
                    foundRoom = true;
                }

                updatedLines.Add(lines[i]);
            }

            if (foundRoom)
            {
                MessageBox.Show("Ви зняли резерв номера");

                // Записуємо оновлені рядки в цільовий файл
                File.WriteAllLines(freeNumbersPath, updatedLines);
            }

            string[] lines2 = File.ReadAllLines(busyNumbersPath);

            int lineIndexToRemove = -1;

            // Знаходимо індекс рядка зі змінною targetRoom
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines2[i].Contains(targetRoom))
                {
                    lineIndexToRemove = i;
                    break;
                }
            }

            if (lineIndexToRemove != -1)
            {
                List<string> updatedLines2 = new List<string>(lines);
                updatedLines2.RemoveAt(lineIndexToRemove);
                lines2 = updatedLines2.ToArray();

                File.WriteAllLines(busyNumbersPath, lines2);

                MessageBox.Show("Рядок був успішно видалений з файлу.");
            }

            else
            {
                MessageBox.Show($"Рядок з кімнатою '{targetRoom}' не знайдено");
            }
        }

        private void btnFreeNumbers_Click(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainMenu = new MainMenuWindow();
            mainMenu.Show();
            this.Close();
        }

        private void btnBusyNumbers_Click(object sender, RoutedEventArgs e)
        {
            BusyNumbersWindow busyNumbersWindow = new BusyNumbersWindow();
            busyNumbersWindow.Show();
            this.Close();
        }

        private void btnReservNumber_Click(object sender, RoutedEventArgs e)
        {
            ReservNumberWindow reservNumberWindow = new ReservNumberWindow();
            reservNumberWindow.Show();
            this.Close();
        }

       
    }
}
