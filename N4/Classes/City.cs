using N4.Abstractions;

namespace N4.Classes
{
    public class City : Component
    {
        public City(string name) : base(name)
        {

        }
        public override void Execute()
        {
            Console.WriteLine(Name);
        }
    }
}
