using N5.Abstractions;

namespace N5.Classes
{
    public class Game
    {
        Memento State { get; set; }
        public Memento SaveState() { throw new NotImplementedException(); }
        public void Restore(Memento state) { }
        public void PlaceX() { }
        public void PlaceO() { }
    }
}
