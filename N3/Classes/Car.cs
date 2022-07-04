using N3.Abstractions;

namespace N3.Classes
{
    public class Car
    {
        public IAutoCreator _autoCreator;
        public List<Wheel> Wheels { get; set; }
        public List<Window> Windows { get; set; }
        public Engine Engine { get; set; }
    }
}
