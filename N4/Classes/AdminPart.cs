using N4.Abstractions;

namespace N4.Classes
{
    public class AdminPart : Component
    {
        private List<Component> cities = new List<Component>();

        public AdminPart(string name)
            : base(name)
        {
        }

        public override void Add(Component component)
        {
            cities.Add(component);
        }

        public override void Remove(Component component)
        {
            cities.Remove(component);
        }

        public override void Execute()
        {
            Console.WriteLine("Адмiнiстративнi частинa: " + Name);
            Console.WriteLine("Мiста: ");
            for (int i = 0; i < cities.Count; i++)
            {
                cities[i].Execute();
            }
        }
    }
}
