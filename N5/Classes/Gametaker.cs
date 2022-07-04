using N5.Abstractions;

namespace N5.Classes
{
    public class Gametaker
    {
        Game Game { get; set; }
        public List<Memento> History { get; set; }
    }
}
