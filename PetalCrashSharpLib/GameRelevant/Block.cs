namespace PetalCrashSharpLib
{
    public class Block
    {
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public enum State
        {
            Nothing,
            Moved,
            Glow,
            UsedGlow
        }


        public enum Typus
        {
            Empty,
            Wall,
            Red,
            Blue,
            Orange,
            Green,
            Purple,
            Garbage
        }

        public State _state;

        public Typus _typus;
        public bool movable;
        public Block()
        {
            _typus = Typus.Empty;
            _state = State.Nothing;
            Direction1 = null;
            movable = false;
        }

        public Block(Typus color)
        {
            movable = true;
            if (color == Typus.Empty || color == Typus.Garbage || color == Typus.Wall)
            { movable = false; }

            _typus = color;
        }

        // for puzzle format
        public Block(int i)
        {
            movable = true;
            switch (i)
            {
                case 0:
                    _typus = Typus.Empty;
                    movable = false;
                    break;
                case 1:
                    _typus = Typus.Wall;
                    movable = false;

                    break;
                case 2:
                    _typus = Typus.Red;
                    break;
                case 3:
                    _typus = Typus.Blue;
                    break;
                case 4:
                    _typus = Typus.Orange;
                    break;
                case 5:
                    _typus = Typus.Green;
                    break;
                case 6:
                    _typus = Typus.Purple;
                    break;
                case 7:
                    _typus = Typus.Garbage;
                    movable = false;

                    break;
            }
            _state = 0;
            Direction1 = null;
        }

        public string TypusString()
        {

            return _typus switch
            {
                Typus.Empty => "0",
                Typus.Wall => "1",
                Typus.Red => "2",
                Typus.Blue => "3",
                Typus.Orange => "4",
                Typus.Green => "5",
                Typus.Purple => "6",
                Typus.Garbage => "7",
                _ => "0",
            };
        }

        public string DirectionString()
        {
            return Direction1 switch
            {
                Direction.Up => "u",
                Direction.Down => "d",
                Direction.Left => "l",
                Direction.Right => "r",
                _ => " ",
            };
        }

        public string StateString()
        {
            return _state switch
            {
                State.Glow => "G",
                State.Moved => "M",
                State.Nothing => " ",
                State.UsedGlow => "U",
                _ => " ",

            };

        }

        public override string ToString()
        {

            return TypusString() + StateString() + DirectionString();
        }
        internal Direction? Direction1 { get; set; }
    }
}