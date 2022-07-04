using N3.Classes;

namespace N3.Abstractions
{
    public interface IAutoCreator
    {
        List<Wheel> BuildWheels();
        List<Window> CreateWindows();
        Engine CreateEngine();
    }
}
