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
    /// Логика взаимодействия для ReservNumberWindow.xaml
    /// </summary>
    public partial class ReservNumberWindow : Window
    {
        public ReservNumberWindow()
        {
            InitializeComponent();
            readFile(); 
        }

        private const string freeNumbersPath = @"Data\\Numbers.txt";
        private const string busyNumbersPath = @"Data\\BusyNumbers.txt";


        public void readFile()
        {
            string[] lines2 = File.ReadAllLines(freeNumbersPath);

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

        private void btnBusyNumbers_Click(object sender, RoutedEventArgs e)
        {
            BusyNumbersWindow busyNumbersWindow = new BusyNumbersWindow();
            busyNumbersWindow.Show();
            this.Close();
        }

        private void btnFreeNumbers_Click(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainMenuWindow = new MainMenuWindow();
            mainMenuWindow.Show();
            this.Close();
        }

        private void btnReserv_Click(object sender, RoutedEventArgs e)
        {
            string[] lines = File.ReadAllLines(freeNumbersPath);
            List<string> updatedLines = new List<string>();

            string targetRoom = cmbFirst.Text;
            string date = dtpMain.Text;
            DateTime currentDateTime = DateTime.Now;

            bool foundRoom = false;

            // Знаходимо рядок з цільовою кімнатою в першому файлі та міняємо його значення
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('|');

                if (parts.Length > 2 && parts[0].Equals(targetRoom))
                {
                    lines[i] = $"{targetRoom}|{MainWindow.userName}|{parts[2]}|{parts[3]}|{currentDateTime}|{date}";
                    foundRoom = true;
                }

                updatedLines.Add(lines[i]);
            }

            if (foundRoom)
            {
                MessageBox.Show("Ви зарезервували номер");

                // Записуємо оновлені рядки в цільовий файл
               File.WriteAllLines(busyNumbersPath, lines);
            }

            string[] lines2 = File.ReadAllLines(freeNumbersPath);

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
                List<string> updatedLines2 = new List<string>(lines2);
                updatedLines2.RemoveAt(lineIndexToRemove);
                lines2 = updatedLines2.ToArray();

                File.WriteAllLines(freeNumbersPath, lines2);

                MessageBox.Show("Рядок був успішно видалений з файлу.");
            }

            else
            {
                MessageBox.Show($"Рядок з кімнатою '{targetRoom}' не знайдено");
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            ChangeReservWindow changeReservWindow = new ChangeReservWindow();
            changeReservWindow.Show();
            this.Close();
        }

        private void cmbFirst_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }
    }
}
