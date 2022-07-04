namespace N1.Entities
{
    public class Meal
    {
        public string Name { get; set; }
        public List<(Product, string)> Products { get; set; }
        public string Price { get; set; }
    }
}
