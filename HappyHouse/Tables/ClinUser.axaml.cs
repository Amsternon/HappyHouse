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
using HappyHouse.CRUD;
using MySql.Data.MySqlClient;

namespace HappyHouse.Tables;

public partial class ClinUser : Window
{
    public ClinUser()
    {
        InitializeComponent();
        string fullTable = "SELECT * FROM client";
        ShowTable(fullTable);
    }
    private List<client> users;
    string connStr = "server=127.0.0.1;database=abd10_1;port=3306;User Id=root;password=12345";
    private MySqlConnection conn;

    public void ShowTable(string sql)
    {
        users = new List<client>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var Client = new client()
            {
                ID = reader.GetInt32("ID"),
                Surname  = reader.GetString("Surname"),
                Name = reader.GetString("Name"),
                Login = reader.GetString("Login"),
                Password = reader.GetString("Password"),
                Telephone = reader.GetString("Telephone"),
                email  = reader.GetString("email")
            };
            users.Add(Client);
        }
        conn.Close();
        DataGrid.ItemsSource = users;
    }

    private void SearchLogin(object? sender, TextChangedEventArgs e)
    {
        var login = users;
        login = login.Where(x => x.Surname.Contains(Search_Login.Text)).ToList();
        DataGrid.ItemsSource = login;
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Top back = new Top();
        Close();
        back.Show();
    }

    private void Reset_OnClick(object? sender, RoutedEventArgs e)
    {
        string fullTable = "SELECT * FROM client";
        ShowTable(fullTable);
        Search_Login.Text = string.Empty;
    }

    private void Del(object? sender, RoutedEventArgs e)
    {
        try
        {
            client usr = DataGrid.SelectedItem as client;
            if (usr == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM client WHERE ID = " + usr.ID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            users.Remove(usr);
            ShowTable("SELECT * FROM client");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void AddData(object? sender, RoutedEventArgs e)
    {
        client newUser = new client();
        CRUD_ClinUser add = new CRUD_ClinUser(newUser, users);
        add.Show();
        this.Close();
    }

    private void Edit(object? sender, RoutedEventArgs e)
    {
        client currentUser = DataGrid.SelectedItem as client;
        if (currentUser == null)
            return;
        CRUD_ClinUser edit = new  CRUD_ClinUser(currentUser, users);
        edit.Show();
        this.Close();
    }
}