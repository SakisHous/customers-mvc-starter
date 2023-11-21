using System;
using System.Collections.Generic;

namespace CustomersMVC.Data;

public partial class Order
{
    public int Orderid { get; set; }

    public int Customerid { get; set; }

    public int Productid { get; set; }

    public int Productqty { get; set; }

    public DateTime? Orderdate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
