using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;
using HappyHouse.Tables;

namespace HappyHouse.CRUD;

public partial class CRUD_Sotryd : Window
{
    private List<worker> Sotryd;
    private worker CurrenSotryd;
    public CRUD_Sotryd(worker currentSotryd, List<worker> sotryd)
    {
        InitializeComponent();
        CurrenSotryd = currentSotryd;
        this.DataContext = currentSotryd;
        Sotryd = sotryd;
    }

    private MySqlConnection conn;
    string connStr = "server=127.0.0.1;database=abd10_1;port=3306;User Id=root;password=12345";
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var usr = Sotryd.FirstOrDefault(x => x.ID == CurrenSotryd.ID);
        if (usr == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO worker VALUES (" + Convert.ToInt32(Id.Text) + ", '" + Surname.Text + "', '" + Name.Text + "', '" + Otchestvo.Text + "','" + Convert.ToInt32(Jobs.Text) + "', '" + Telephone.Text + "', '" + Email.Text + "');";
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
                string upd = "UPDATE worker SET Dolzhnost = " + Convert.ToInt32(Jobs.Text) + ", Surname = '" +  Surname.Text + "', Name = " + Name.Text + ", Otchestvo = " + Otchestvo.Text + ", Telephone = " + Telephone.Text + ", email = " + Email.Text + " WHERE ID = " + Convert.ToInt32(Id.Text) + ";";
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
        sotrydnik back = new sotrydnik();
        this.Close();
        back.Show();
    }
}