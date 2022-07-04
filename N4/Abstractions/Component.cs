namespace N4.Abstractions
{
    public abstract class Component
    {
        public string Name { get; set; }

        protected Component(string name)
        {
            Name = name;
        }
        public virtual void Add(Component component) { }

        public virtual void Remove(Component component) { }

        public virtual void Execute() { Console.WriteLine(Name); }
    }
}
