namespace N1.Entities
{
    public class Restaurant
    {
        public string Name { get; set; }
        public List<Menu> Menus { get; set; }
        public List<Meal> Meals { get; set; }
        public string CreateDate { get; set; }
        public Menu ActualMenu { get => Menus.LastOrDefault(); }
    }
}
