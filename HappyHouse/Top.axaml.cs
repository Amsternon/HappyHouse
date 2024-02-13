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

namespace HappyHouse;

public partial class Top : Window
{
    public Top()
    {
        InitializeComponent();
    }

    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }

    private void Users(object? sender, RoutedEventArgs e)
    {
        ClinUser usr = new ClinUser();
        this.Close();
        usr.Show();
    }

    private void Goods(object? sender, RoutedEventArgs e)
    {
        GoodsWin gds = new GoodsWin();
        this.Close();
        gds.Show();
    }

    private void Sotryd(object? sender, RoutedEventArgs e)
    {
        sotrydnik rab = new sotrydnik();
        this.Close();
        rab.Show();
    }

    private void Orders(object? sender, RoutedEventArgs e)
    {
        OrderWin ord = new OrderWin();
        this.Close();
        ord.Show();
    }
}