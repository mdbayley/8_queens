
namespace _8_Queens
{
    // DFD = Diagonal Forward (like forward-slash)
    // DBK = Diagonal Back (like backslash)

    internal struct Square
    {
        public Square(int index, int row, int col, int dfd, int dbk)
        {
            Index = index;
            HasQueen = false;
            Row = row;
            Col = col;
            Dfd = dfd;
            Dbk = dbk;
        }

        public int Index;
        public bool HasQueen;
        public int Row;
        public int Col;
        public int Dfd;
        public int Dbk;
    }
}
