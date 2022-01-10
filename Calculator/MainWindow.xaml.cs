using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.Sqlite;
using System.IO;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly History history = new History();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string expression = TextBox.Text;
                string value = Logic.Calc.Calculate(expression);
                TextBlock.Text = value;
                TextBlock time = new TextBlock();
                time.Text = DateTime.Now.ToString();
                TextBlock expr = new TextBlock();
                history.HistPanel.Children.Add(time);
                history.HistPanel.Children.Add(expr);
                using (StreamWriter sw = new StreamWriter("Calc.txt", true, Encoding.Default))
                {
                    sw.WriteLine("{0}:", DateTime.Now);
                    sw.WriteLine(expr);
                }
                using (SqliteConnection connection = new SqliteConnection("Data Source=Calc.db"))
                {
                    connection.Open();
                    SqliteCommand command = new SqliteCommand("CREATE TABLE if not exists Calc(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Expression TEXT NOT NULL, Time TEXT NOT NULL)", connection);
                    command.ExecuteNonQuery();
                    command.CommandText = $"INSERT INTO Calc (Expression, Time) VALUES ('{expr}', '{DateTime.Now}')";
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Невозможно выполнить!");
            }
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            TextBox.Text = string.Empty;
            TextBlock.Text = string.Empty;
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            history.Show();
        }
    }
}
