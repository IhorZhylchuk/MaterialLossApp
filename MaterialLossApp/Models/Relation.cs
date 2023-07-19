namespace MaterialLossApp.Models
{
    public class Relation
    {
        public int Id { get; set; }
        public int IngredientsId { get; set; }
        public int RecipeId { get; set; }
        public double Amount { get; set; }

    }
}
