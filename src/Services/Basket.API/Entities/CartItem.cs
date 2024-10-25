using System.ComponentModel.DataAnnotations;

namespace Basket.API.Entities;

public class CartItem
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }
    [Required]
    [Range(0.1, double.MaxValue, ErrorMessage = "Price must be at least 0.1")]
    public decimal ItemPrice { get; set; }
    public string ItemNo { get; set; }
    public string ItemName { get; set; }
}