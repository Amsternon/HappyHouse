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

public partial class sotrydnik : Window
{
    public sotrydnik()
    {
        InitializeComponent();
        ShowTable(fullTable);
        FillStatus();
    }
    string fullTable = "SELECT worker.ID, worker.Surname, worker.Name, worker.Otchestvo, worker.Telephone, worker.email, jobs.Names FROM worker JOIN jobs ON worker.Dolzhnost = jobs.ID";

    private List<worker> sotryd;
    private List<jobs> job;
    string connStr = "server=127.0.0.1;database=abd10_1;port=3306;User Id=root;password=12345";
    private MySqlConnection conn;

    public void ShowTable(string sql)
    {
        sotryd = new List<worker>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var Client = new worker()
            {
                ID = reader.GetInt32("ID"),
                Surname = reader.GetString("Surname"),
                Name = reader.GetString("Name"),
                Otchestvo = reader.GetString("Otchestvo"),
                Names = reader.GetString("Names"),
                Telephone = reader.GetString("Telephone"),
                email = reader.GetString("email")
            };
            sotryd.Add(Client);
        }
        conn.Close();
        DataGrid.ItemsSource = sotryd;
    }

    private void SearchGoods(object? sender, TextChangedEventArgs e)
    {
        var gds = sotryd;
        gds = gds.Where(x => x.Surname.Contains(Search_Goods.Text)).ToList();
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
            worker usr = DataGrid.SelectedItem as worker;
            if (usr == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM worker WHERE ID = " + usr.ID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            sotryd.Remove(usr);
            ShowTable("SELECT worker.ID, worker.Surname, worker.Name, worker.Otchestvo, worker.Telephone, worker.email, jobs.Names FROM worker JOIN jobs ON worker.Dolzhnost = jobs.ID");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void AddData(object? sender, RoutedEventArgs e)
    {
        worker newOrder = new worker();
        CRUD.CRUD_Sotryd add = new CRUD.CRUD_Sotryd(newOrder, sotryd);
        add.Show();
        this.Close();
    }

    private void Edit(object? sender, RoutedEventArgs e)
    {
        worker currenOrder = DataGrid.SelectedItem as worker;
        if (currenOrder == null)
            return;
        CRUD.CRUD_Sotryd edit = new  CRUD.CRUD_Sotryd(currenOrder, sotryd);
        edit.Show();
        this.Close();
    }

    private void CmbJobs(object? sender, SelectionChangedEventArgs e)
    {
        var genderComboBox = (ComboBox)sender;
        var currentGender = genderComboBox.SelectedItem as jobs;
        var filteredUsers = sotryd
            .Where(x => x.Names == currentGender.Names)
            .ToList();
        DataGrid.ItemsSource = filteredUsers;
    }

    public void FillStatus()
    {
        job = new List<jobs>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("select * from jobs", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentGender = new jobs()
            {
                ID = reader.GetInt32("ID"),
                Names = reader.GetString("Names"),
                zarplata = reader.GetString("zarplata")
            };
            job.Add(currentGender);
        }
        conn.Close();
        var genderComboBox = this.Find<ComboBox>("CmbGender");
        genderComboBox.ItemsSource = job;
    }
}