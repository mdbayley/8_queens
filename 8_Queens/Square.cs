
namespace _8_Queens
{
    internal struct Square
    {
        public Square(int index, int row, int col, int dfs, int dbs)
        {
            Index = index;
            HasQueen = false;
            Row = row;
            Col = col;
            Dfs = dfs;
            Dbs = dbs;
        }

        public int Index;
        public bool HasQueen;
        public int Row;
        public int Col;
        public int Dfs;
        public int Dbs;
    }
}
