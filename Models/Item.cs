using System;

namespace BlogApi.Models
{
  public class Item
  {
    //need id, SKU/bar code, name, short desrip, numberinstock, price, dateordered
    public int Id { get; set; }
    public int SKU { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int NumberInStock { get; set; }
    public int Price { get; set; }
    public DateTime DateOrdered { get; set; } = DateTime.Now;
  }
}
