namespace PetalCrashSharpLib
{
    public class Spawner
    {
        public Block _block;
        public int _progress;


        public void SetProgress(int prog)
        {
            _progress = prog;
        }
        public Spawner()
        {
            _block = new Block();
            _progress = 0;
        }
        public Spawner(int blockInt)
        {
            _block = new Block(blockInt);
            _progress = 0;
        }


        public override string ToString()
        {
            return _progress.ToString() + "," + _block.TypusString();
        }
    }
}