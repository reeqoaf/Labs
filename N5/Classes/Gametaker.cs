namespace N5.Classes
{
    public class Gametaker
    {
        public Memento Backup { get; set; }
        public void MakeBackup(Game game)
        {
            Memento memento = new(game);
            Backup = memento;
        }
        public void RestoreBackup()
        {
            if (Backup != null) Backup.Restore();
        }

    }
}
