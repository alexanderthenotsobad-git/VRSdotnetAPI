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

        [Display(Name = "Upload Date")] // Custom display name for UI
        [DataType(DataType.Date)]
        public DateTime UploadDate { get; set; }

        public IFormFile ImageData {get; set; }
        
        // Navigation property for related entities (e.g., a product might have many reviews)
        // public ICollection<Review> Reviews { get; set; } 
    }
}