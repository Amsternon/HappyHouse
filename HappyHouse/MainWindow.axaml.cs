using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace HappyHouse;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    string connectionString = "server=127.0.0.1;database=abd10_1;port=3306;User Id=root;password=12345";

    public void Authorization(object? sender, RoutedEventArgs e)
    {
        string username = Login.Text;
        string password = Password.Text;
        if (IsUserValid(username, password))
        {
            Top choise = new Top();
            Hide();
            choise.Show();
        }
        else
        {
            Console.Write("Неверный логин или пароль");
        }
    }

    private bool IsUserValid(string username, string password) //проверка пользователей по БД
    {
        bool isValid = false;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT COUNT(1) FROM client WHERE Login = @Username AND Password = @Password";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();

                object result = command.ExecuteScalar();
                int count = Convert.ToInt32(result);

                if (count == 1)
                {
                    isValid = true;
                }
            }
        }
        return isValid;
    }

    public void Exit_PR(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }

    private void Registration(object? sender, RoutedEventArgs e)
    {
        Registration reg = new Registration();
        Hide();
        reg.Show();
    }
}