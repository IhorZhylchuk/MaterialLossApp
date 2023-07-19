namespace MaterialLossApp.Models

{
    public class Item
    {
        public int Id { get; set; }
        public int NrZlecenia { get; set; }
        public string RecipesName { get; set; } = string.Empty;
        public int RecipeId { get; set; }
        public int Count { get; set; }
        public string Opakowanie { get; set; } = string.Empty;
        public string PokrywaNekrętka { get; set; } = string.Empty;
        public string Naklejka { get; set; } = string.Empty;
        public int IlośćOpakowań { get; set; }
        public int IlośćPokrywNekrętek { get; set; }
        public int IlośćNaklejek { get; set; }
    }
    public class ItemsCount
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int IngredientId { get; set; }
        public double IngredientCount { get; set; }
        
    }
}
