using System;
using System.Collections.Generic;

namespace CustomersMVC.Data;

public partial class Customer
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
