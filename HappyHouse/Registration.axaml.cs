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

public partial class Registration : Window
{
    public Registration()
    {
        InitializeComponent();
    }
    private MySqlConnection conn;
    private string connStr = "server=127.0.0.1;database=abd10_1;port=3306;User Id=root;password=12345";
    private void Reg(object? sender, RoutedEventArgs e)
    {
        conn = new MySqlConnection(connStr);
        conn.Open();
        string regist = "INSERT INTO client VALUES (" + Convert.ToInt32(id.Text) + ", '" + Surname.Text + "', '" + Name.Text + "', '" + Login.Text + "', '" + Password.Text + "', '" + Telephone.Text + "', '" + email.Text + "');";
        MySqlCommand cmd = new MySqlCommand(regist, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        MainWindow auth = new MainWindow();
        this.Close();
        auth.Show();
    }
}