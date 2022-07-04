using N3.Abstractions;

namespace N3.Classes
{
    public class CarCreator : IAutoCreator
    {
        public List<Wheel> BuildWheels()
        {
            throw new NotImplementedException();
        }

        public Engine CreateEngine()
        {
            throw new NotImplementedException();
        }

        public List<Window> CreateWindows()
        {
            throw new NotImplementedException();
        }
        public Car Getresult()
        {
            throw new NotImplementedException();
        }
    }
}
