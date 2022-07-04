using N4.Abstractions;

namespace N4.Classes
{
    public class Container : Component
    {
        public List<Component> Childrens { get; set; }
        public void Add(Component component) { }
        public void Remove(Component component) { }
        public void GetChildren(Component component) { }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
