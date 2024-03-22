
using System.Runtime.Serialization;

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

        public int Index; // this is likely redundant...

        public bool HasQueen;

        public int Row;
        public int Col;
        public int Dfs;
        public int Dbs;

        public override string ToString()
        {
            return $"Index={Index}, HasQueen={HasQueen}, Row={Row}, Col={Col}, DFS={Dfs}, DBS={Dbs}";
        }

        public string ToString(ulong[] rows, ulong[] cols, ulong[] dfss, ulong[] dbss)
        {
            return $"Index={Index}, " +
                   $"HasQueen={HasQueen}, " +
                   $"Row={Row}[{Convert.ToString((long)rows[Row], 2)}], " +
                   $"Col={Col}[{Convert.ToString((long)cols[Col], 2)}]," +
                   $" DFS={Dfs}[{Convert.ToString((long)dfss[Dfs], 2)}], " +
                   $"DBS={Dbs}[{Convert.ToString((long)dbss[Dbs], 2)}]";
        }
    }
}
