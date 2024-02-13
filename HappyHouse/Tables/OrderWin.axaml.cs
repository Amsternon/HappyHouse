using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Windows;


namespace HappyHouse.Tables;

public partial class OrderWin : Window
{
    public OrderWin()
    {
        InitializeComponent();
        ShowTable(fullTable);
    }
    string fullTable = "SELECT `order`.ID, worker.Name, client.Login, tableware.ID, `order`.kolichestvo, `order`.price, `order`.Date FROM `order` JOIN worker ON `order`.id_sotryd = worker.ID JOIN client ON `order`.id_client = client.ID JOIN tableware ON `order`.id_posyd = tableware.ID";
    private List<order> orders;
    string connStr = "server=127.0.0.1;database=abd10_1;port=3306;User Id=root;password=12345";
    private MySqlConnection conn;

    public void ShowTable(string sql)
    {
        orders = new List<order>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var Client = new order()
            {
                ID = reader.GetInt32("ID"),
                Name = reader.GetString("Name"),
                Login = reader.GetString("Login"),
                id_posyd = reader.GetInt32("id_posyd"),
                kolichestvo = reader.GetString("kolichestvo"),
                price = reader.GetInt32("price"),
                Date = reader.GetString("Date")
            };
            orders.Add(Client);
        }
        conn.Close();
        DataGrid.ItemsSource = orders;
    }

    private void SearchGoods(object? sender, TextChangedEventArgs e)
    {
        var gds = orders;
        gds = gds.Where(x => x.Name.Contains(Search_Goods.Text)).ToList();
        DataGrid.ItemsSource = gds;
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Top back = new Top();
        Close();
        back.Show();
    }

    private void Reset_OnClick(object? sender, RoutedEventArgs e)
    {
        ShowTable(fullTable);
        Search_Goods.Text = string.Empty;
    }

    private void Del(object? sender, RoutedEventArgs e)
    {
        try
        {
            order usr = DataGrid.SelectedItem as order;
            if (usr == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM order WHERE ID = " + usr.ID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            orders.Remove(usr);
            ShowTable(fullTable);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void AddData(object? sender, RoutedEventArgs e)
    {
        order newOrder = new order();
        CRUD.CRUD_OrderWin add = new CRUD.CRUD_OrderWin(newOrder, orders);
        add.Show();
        this.Close();
    }

    private void Edit(object? sender, RoutedEventArgs e)
    {
        order currenOrder = DataGrid.SelectedItem as order;
        if (currenOrder == null)
            return;
        CRUD.CRUD_OrderWin edit = new  CRUD.CRUD_OrderWin(currenOrder, orders);
        edit.Show();
        this.Close();
    }
}