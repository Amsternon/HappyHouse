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

public partial class CRUD_OrderWin : Window
{
    private List<order> Orders;
    private order CurrenOrder;
    public CRUD_OrderWin(order currentOrder, List<order> orders)
    {
        InitializeComponent();
        CurrenOrder = currentOrder;
        this.DataContext = currentOrder;
        Orders = orders;
    }

    private MySqlConnection conn;
    string connStr = "server=127.0.0.1;database=abd10_1;port=3306;User Id=root;password=12345";

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var usr = Orders.FirstOrDefault(x => x.ID == CurrenOrder.ID);
        if (usr == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO order VALUES (" + Convert.ToInt32(id.Text) + ", '" + name.Text + "', '" + Login.Text + "', '" + Convert.ToInt32(posyd.Text) + "', '" + kolichestvo.Text + "', '" + Convert.ToInt32(price.Text) + "'), '" + Date.Text + "';";
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
                string upd = "UPDATE order SET id_posyd = " + Convert.ToInt32(posyd.Text) + ", Date = '" +  Date.Text + "', kolichestvo = '" +  kolichestvo.Text + "', Name = '" +  name.Text + "', Login = '" +  Login.Text + "', price = " + Convert.ToInt32(price.Text) + ", WHERE ID = " + Convert.ToInt32(id.Text) + ";";
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
        OrderWin back = new OrderWin();
        this.Close();
        back.Show();
    }
}