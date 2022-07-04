using N4.Abstractions;
using N4.Classes;

namespace N4
{
    class Program
    {
        static void Main(string[] args)
        {
            Component country = new Country("Ukraine");
            Component odessObl = new AdminPart("Odessa Oblast");
            Component odessa = new City("Odessa");
            Component chernomorsk = new City("Chernomorsk");
            odessObl.Add(odessa);
            odessObl.Add(chernomorsk);
            country.Add(odessObl);
            Component zpObl = new AdminPart("Zaporizhzhia Oblast");
            Component zp = new City("Zaporizhzhia");
            zpObl.Add(zp);
            country.Add(zpObl);
            country.Execute();
        }
    }
}