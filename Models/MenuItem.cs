using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VRSAPI.ModelsP
{
    [Table(menu_items)]
    public class MenuItem
    {
        [Key]
        [Column("item_id")]
        public int ItemId { get; set; }

        [Required]
        [Column("item_name")]
        [MaxLength(45)]
        public string ItemName { get; set; } = string.Empty;

    }

}