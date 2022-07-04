namespace N5.Classes
{
    public class Memento
    {
        public Game State { get; set; }

        public Memento(Game state)
        {
            State = state;
        }
        public void Restore() { }
    }
}
