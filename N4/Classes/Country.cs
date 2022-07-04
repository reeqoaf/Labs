using N4.Abstractions;

namespace N4.Classes
{
    public class Country : Component
    {
        private List<Component> parts = new List<Component>();

        public Country(string name)
            : base(name)
        {
        }

        public override void Add(Component component)
        {
            parts.Add(component);
        }

        public override void Remove(Component component)
        {
            parts.Remove(component);
        }

        public override void Execute()
        {
            Console.WriteLine("Країна: " + Name);
            Console.WriteLine("Адмiнiстративнi частини:");
            for (int i = 0; i < parts.Count; i++)
            {
                parts[i].Execute();
            }
        }
    }
}
