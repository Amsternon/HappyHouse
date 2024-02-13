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

public partial class CRUD_Goods : Window
{
    private List<tableware> Goods;
    private tableware CurrenGoods;
    public CRUD_Goods(tableware currenGoods, List<tableware> goods)
    {
        InitializeComponent();
        CurrenGoods = currenGoods;
        this.DataContext = currenGoods;
        Goods = goods;
    }

    private MySqlConnection conn;
    string connStr = "server=127.0.0.1;database=abd10_1;port=3306;User Id=root;password=12345";

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var usr = Goods.FirstOrDefault(x => x.ID == CurrenGoods.ID);
        if (usr == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO tableware VALUES (" + Convert.ToInt32(id.Text) + ", '" + name.Text + "', '" + namer.Text + "','" + Named.Text + "','" + Convert.ToInt32(price.Text) + "');";
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
                string upd = "UPDATE tableware SET Named = '" + Named.Text + "', price = '" +  Convert.ToInt32(price.Text) + "', Namer = " + namer.Text + ", Name = " + name.Text + " WHERE id = " + Convert.ToInt32(id.Text) + ";";
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
        GoodsWin back = new GoodsWin();
        this.Close();
        back.Show();
    }

}