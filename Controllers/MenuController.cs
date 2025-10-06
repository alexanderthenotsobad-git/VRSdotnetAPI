// File: /home/alex/nvrs-dotnet-api/VRSAPI/Controllers/MenuController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VRSAPI.Data;
using VRSAPI.DTOs;
using VRSAPI.Models;

namespace VRSAPI.Controllers
{
    [ApiController]
    [Route("/")]
    public class MenuController : ControllerBase
    {
        private readonly VRSDbContext _context;
        private readonly ILogger<MenuController> _logger;

        public MenuController(VRSDbContext context, ILogger<MenuController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET / - Get all menu items with most recent image ID
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetAllMenuItems()
        {
            try
            {
                var menuItems = await _context.MenuItems
                    .Include(m => m.Images)
                    .Select(m => new MenuItemDto
                    {
                        ItemId = m.ItemId,
                        ItemName = m.ItemName,
                        ItemDesc = m.ItemDesc,
                        Price = m.Price,
                        ItemType = m.ItemType,
                        ImageId = m.Images
                            .OrderByDescending(i => i.UploadDate)
                            .Select(i => (int?)i.ImageId)
                            .FirstOrDefault()
                    })
                    .ToListAsync();

                return Ok(menuItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving menu items");
                return StatusCode(500, new { message = "Server error occurred" });
            }
        }

        // POST /createMenuItem - Create new menu item
        [HttpPost("createMenuItem")]
        public async Task<ActionResult<MenuItem>> CreateMenuItem([FromBody] CreateMenuItemDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.ItemName))
                {
                    return BadRequest(new { message = "Item name is required" });
                }

                var menuItem = new MenuItem
                {
                    ItemName = dto.ItemName,
                    ItemDesc = dto.ItemDesc,
                    Price = dto.Price,
                    ItemType = dto.ItemType
                };

                _context.MenuItems.Add(menuItem);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Created menu item: {menuItem.ItemName} (ID: {menuItem.ItemId})");

                return CreatedAtAction(nameof(GetAllMenuItems), new { id = menuItem.ItemId }, menuItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating menu item");
                return StatusCode(500, new { message = "Server error occurred" });
            }
        }

        // PUT /menu/{id} - Update menu item
        [HttpPut("menu/{id}")]
        public async Task<ActionResult> UpdateMenuItem(int id, [FromBody] UpdateMenuItemDto dto)
        {
            try
            {
                var menuItem = await _context.MenuItems.FindAsync(id);

                if (menuItem == null)
                {
                    return NotFound(new { message = "Menu item not found" });
                }

                // Update only provided fields
                if (!string.IsNullOrWhiteSpace(dto.ItemName))
                    menuItem.ItemName = dto.ItemName;

                if (dto.ItemDesc != null)
                    menuItem.ItemDesc = dto.ItemDesc;

                if (dto.Price.HasValue)
                    menuItem.Price = dto.Price.Value;

                if (!string.IsNullOrWhiteSpace(dto.ItemType))
                    menuItem.ItemType = dto.ItemType;

                await _context.SaveChangesAsync();

                _logger.LogInformation($"Updated menu item ID: {id}");

                return Ok(new
                {
                    message = "Menu item updated successfully",
                    itemId = id,
                    affectedRows = 1
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating menu item ID: {id}");
                return StatusCode(500, new { message = "Server error occurred" });
            }
        }

        // DELETE /menu/{id} - Delete menu item
        [HttpDelete("menu/{id}")]
        public async Task<ActionResult> DeleteMenuItem(int id)
        {
            try
            {
                var menuItem = await _context.MenuItems
                    .Include(m => m.Images)
                    .FirstOrDefaultAsync(m => m.ItemId == id);

                if (menuItem == null)
                {
                    return NotFound(new { message = "Menu item not found" });
                }

                _context.MenuItems.Remove(menuItem);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Deleted menu item ID: {id}");

                return Ok(new { message = "Menu item deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting menu item ID: {id}");
                return StatusCode(500, new { message = "Server error occurred" });
            }
        }
    }
}