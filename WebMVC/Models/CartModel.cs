using Entity.Concrete;
using System.Collections.Generic;

public class CartModel
{
    public List<Product> CartItems { get; set; } = new List<Product>();
}