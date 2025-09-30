using Xunit;
using VRSAPI.ModelsP;

namespace VRSAPI.Models.Tests
{
    public class MenuItemTest
    {
        [Fact]
        public void ItemName_DefaultValue_IsEmptyString()
        {
            // Arrange
            var menuItem = new MenuItem();

            // Act
            var itemName = menuItem.ItemName;

            // Assert
            Assert.Equal(string.Empty, itemName);
        }

        [Fact]
        public void ItemName_SetValue_ReturnsSetValue()
        {
            // Arrange
            var menuItem = new MenuItem();
            var expectedName = "Pizza";

            // Act
            menuItem.ItemName = expectedName;

            // Assert
            Assert.Equal(expectedName, menuItem.ItemName);
        }
    }
}

// We recommend installing an extension to run csharp tests.