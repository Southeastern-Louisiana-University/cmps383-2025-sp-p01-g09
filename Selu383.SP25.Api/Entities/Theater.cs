using System;

public class Theater
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int SeatCount { get; set; }

}

public class TheaterGetDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public int SeatCount { get; set; }

}

public class TheaterCreateDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public int SeatCount { get; set; }

}
public class TheaterUpdateDto
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public int SeatCount { get; set; }

}
