using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VRSAPI.Models
{
    [Table("menu_items")]
    public class MenuItem
    {
        [Key]
        [Column("item_id")]
        public int ItemId { get; set; }

        [Required]
        [Column("item_name")]
        [MaxLength(45)]
        public string ItemName { get; set; } = string.Empty;

        [Column("item_desc")]
        public string? ItemDesc { get; set; }

        [Required]
        [Column("price")]
        [Range(0.01, 999.99)]
        public decimal Price { get; set; }

        [Column("item_type")]
        [MaxLength(45)]
        public string? ItemType { get; set; }

        public virtual ICollection<MenuItemImage> Images { get; set; } = new List<MenuItemImage>();
    }

}