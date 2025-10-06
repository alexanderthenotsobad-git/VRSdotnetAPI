// File: /home/alex/nvrs-dotnet-api/VRSAPI/DTOs/MenuItemDto.cs

namespace VRSAPI.DTOs
{
    /// <summary>
    /// DTO for returning menu item data to frontend
    /// Includes the most recent image ID
    /// </summary>
    public class MenuItemDto
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string? ItemDesc { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public string? ItemType { get; set; }
        public int? ImageId { get; set; }  // Most recent image ID for this item
    }

    /// <summary>
    /// DTO for creating a new menu item
    /// All fields required except ItemDesc
    /// </summary>
    public class CreateMenuItemDto
    {
        public string ItemName { get; set; } = string.Empty;
        public string? ItemDesc { get; set; }
        public decimal Price { get; set; }
        public string ItemType { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO for updating an existing menu item
    /// All fields optional - only provided fields will be updated
    /// </summary>
    public class UpdateMenuItemDto
    {
        public string? ItemName { get; set; }
        public string? ItemDesc { get; set; }
        public decimal? Price { get; set; }
        public string? ItemType { get; set; }
    }
}