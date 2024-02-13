namespace HappyHouse;

public class client
{
    public int ID { get; set; }
    public string Surname { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Telephone { get; set; }
    public string email { get; set; }
}

public class tableware
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Namer { get; set; }
    public string Named { get; set; }
    public int price { get; set; }
}

public class materials
{
    public int ID { get; set; }
    public string Namer { get; set; }
}

public class colorss
{
    public int ID { get; set; }
    public string Named { get; set; }
}
public class worker
{
    public int ID { get; set; }
    public string Surname { get; set; }
    public string Name { get; set; }
    public string Otchestvo { get; set; }
    public string Names { get; set; }
    public string Telephone { get; set; }
    public string email { get; set; }
}

public class jobs
{
    public int ID { get; set; }
    public string Names { get; set; }
    public string zarplata { get; set; }
}

public class order
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public int id_posyd { get; set; }
    public string kolichestvo { get; set; }
    public int price { get; set; }
    public string Date { get; set; }
}