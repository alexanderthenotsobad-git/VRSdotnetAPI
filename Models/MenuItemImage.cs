using System;
using System.ComponentModel.DataAnnotations; // For data annotations
using System.ComponentModel.DataAnnotations.Schema;

namespace VRSAPI.Models
{
    [Table("menu_item_images")]
    public class MenuItemImage
    {
        [Key]
        [Column("image_id")]
        public int ImageId { get; set; }

        [Required]
        [Column("menu_item_id")]
        public int MenuItemId { get; set; } //Foreign Key to menu_items

        [Required]
        [Column("image_data")]
        public byte[] ImageData { get; set; } = Array.Empty<byte>();

        [Column("image_type")]
        [MaxLength(20)]
        public string? ImageType { get; set; } // e.g., "image/png", "image/jpeg"

        [Column("upload_date")]
        [DataType(DataType.Date)]
        public DateTime UploadDate { get; set; }

        // Navigation property for related entities (e.g., a product might have many reviews)
        // public ICollection<Review> Reviews { get; set; } 
    }
}