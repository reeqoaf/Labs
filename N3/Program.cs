using N3.Classes;

namespace N3
{
    class Program
    {
        static void Main(string[] args)
        {
            Director director = new();

            CarCreator carCreator = new();
            director.MakeSportCar(carCreator);
            Car car = carCreator.Getresult();
        }
    }
}