using System.ComponentModel.DataAnnotations;

namespace MaterialLossApp.Models
{
    public class WasteIngredients
    {
        [Key]
        public int WasteIngredientId { get; set; }
        public int RealizedOrderId { get; set; }
        public int IngredientNumber { get; set; } = new int();
        public string IngredientName { get; set; } = string.Empty;
        public float Count { get; set; }
        public string Waste { get; set; } = string.Empty;
        public int CommentId { get; set; }
    }
}
