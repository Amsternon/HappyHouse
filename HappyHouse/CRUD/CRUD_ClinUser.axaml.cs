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
using HappyHouse.Tables;
using MySql.Data.MySqlClient;

namespace HappyHouse.CRUD;

public partial class CRUD_ClinUser : Window
{
    private List<client> Users;
    private client CurrentUsers;
    public CRUD_ClinUser(client currentUser, List<client> users)
    {
        InitializeComponent();
        CurrentUsers = currentUser;
        this.DataContext = currentUser;
        Users = users;
    }
    private MySqlConnection conn;
    string connStr = "server=127.0.0.1;database=abd10_1;port=3306;User Id=root;password=12345";

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var usr = Users.FirstOrDefault(x => x.ID == CurrentUsers.ID);
        if (usr == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO client VALUES (" + Convert.ToInt32(ID.Text) + ", '" + surname.Text + "', '" + name.Text + "', '" + login.Text + "', '" + password.Text + "', '" + telephone.Text + "', '" + email.Text + "');";
                MySqlCommand cmd = new MySqlCommand(add, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception);
            }
        }
        else
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE client SET Surname = '" + surname.Text + "', Name = '" +  name.Text + "', Login = '" + login.Text + "', Password = '" + password.Text + "', Telephone = '" + telephone.Text + "', email = '" + email.Text + "' WHERE id = " + Convert.ToInt32(ID.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(upd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.Write("Error" + exception);
            }
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        ClinUser back = new ClinUser();
        this.Close();
        back.Show();
    }
}