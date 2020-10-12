#nullable enable


namespace PetalCrashSharpLib
{
    public class Field
    {
        public string? AuthorName, PuzzleName;
        public int[]? AvailableMoves;
        public bool[]? ClearConditions;

        public CField FieldData = null!;

        public int? PuzzleVersion;

        // width of the field in blocks-1
        public int x_size;

        // length of the field in blocks-1
        public int y_size;
        public bool NotDoingStuff;
        public Field(bool isPuzzle,
                     string seed)
        {
            NotDoingStuff = true;
            if (isPuzzle)
            {
                var puzzle = Puzzle.Decode(seed);
                FieldData = puzzle.Field;
                AuthorName = puzzle.AuthorName;
                PuzzleName = puzzle.Name;
                ClearConditions = puzzle.ClearConditions;
                AvailableMoves = puzzle.AvailableMoves;
                PuzzleVersion = puzzle.Version;
                x_size = 7;
                y_size = 7;
            }



        }

        public Field()
        {
            FieldData = new CField();
            x_size = 7;
            y_size = 7;
            NotDoingStuff = true;
        }


        //save original location for movement


        public class CField
        {
            public Block[,] BlockArray;
            public Spawner[,] SpawnerArray;

            public CField(Spawner[,] spawnerArray,
                          Block[,] blockArray)
            {
                this.BlockArray = blockArray;
                this.SpawnerArray = spawnerArray;
            }

            public CField()
            {
                BlockArray = new Block[8, 8];
                SpawnerArray = new Spawner[8, 8];
            }

            public override string ToString()
            {
                string output = "";
                foreach (var b in BlockArray) output += b;
                foreach (var b in SpawnerArray) output += b;
                return output;
            }
        }
    }
}