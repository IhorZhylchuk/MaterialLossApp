using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MaterialLossApp.Models
{
    public class RealizedOrders
    {
        [Key]
        public int RealizedOrderId { get; set; }
        public int RealizedOrderNumber { get; set; }
        public string RecipeName { get; set; } = string.Empty;
        public int Count { get; set; }
        public ICollection<WasteIngredients> WasteIngredients { get; set; } = new List<WasteIngredients>();

    }
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int WasteIngredientId { get; set; }
        public int RealizedOrderId { get; set; }

    }
}
