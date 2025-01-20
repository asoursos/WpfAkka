﻿namespace WpfAkka.Models;

public class User
{
    public Guid UserId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public override string ToString()
    {
        return $"id:{UserId} ({FirstName} {LastName})";
    }
}
