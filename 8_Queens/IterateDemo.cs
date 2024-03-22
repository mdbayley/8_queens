namespace _8_Queens
{
    internal class IterateDemo
    {
        private const string DBL_FMT = "0.###################################################################################################################################################################################################################################################################################################################################################";

        internal static void Iterate()
        {
            var total = 0ul;
            for (var i = 0; i < 64; i++)
            {
                var val = (ulong)Math.Pow(2, i);
                total |= val;

                Console.WriteLine(val.ToString(DBL_FMT));
                Console.WriteLine(Convert.ToString((long)val, 2).PadLeft(64, '0'));
                Console.WriteLine(Convert.ToString((long)total, 2).PadLeft(64, '0'));
                Console.WriteLine();
            }

            for (var i = 0; i < 64; i++)
            {
                var val = (ulong)Math.Pow(2, i);
                total &= ~val;

                Console.WriteLine(val.ToString(DBL_FMT));
                Console.WriteLine(Convert.ToString((long)val, 2).PadLeft(64, '0'));
                Console.WriteLine(Convert.ToString((long)total, 2).PadLeft(64, '0'));
                Console.WriteLine();
            }
        }
    }
}
