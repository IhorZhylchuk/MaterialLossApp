namespace MaterialLossApp.Models
{
    public class Ingredient:Default
    {
        public override int Id { get; set; }
        public int MaterialNumber { get; set; }
        public override string Name { get; set; } = string.Empty;
        public string SectionName { get; set; } = string.Empty;
        public string Use { get; set; } = string.Empty;
        public double Capacity { get; set; }

    }
}
