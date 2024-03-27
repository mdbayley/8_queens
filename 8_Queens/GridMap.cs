
namespace _8_Queens
{
    /// <summary>
    /// GridMap is a representation of up to 256 squares (dimension = 16) tracking the squares' row, coulumn and diagonals
    /// TODO: Consider making this an array (or span) of arbitrary length
    /// </summary>
    internal struct GridMap
    {
        private ulong _bits0; // bits 0 - 63
        private ulong _bits1; // bits 64 - 127
        private ulong _bits2; // bits 128 - 191
        private ulong _bits3; // bits 192 - 255

        public void Set(int index)
        {
            ulong bit;

            switch (index)
            {
                case < 64:
                    bit = (ulong)Math.Pow(2, index);
                    _bits0 |= bit;
                    return;

                case < 128:
                    bit = (ulong)Math.Pow(2, index - 64);
                    _bits1 |= bit;
                    return;

                case < 192:
                    bit = (ulong)Math.Pow(2, index - 128);
                    _bits2 |= bit;
                    return;

                case < 256:
                    bit = (ulong)Math.Pow(2, index - 192);
                    _bits2 |= bit;
                    return;

                default:
                    throw new ArgumentOutOfRangeException(nameof(index), $"{index} is invalid, index must be less than 256");
            }
        }

        public void Reset(int index)
        {
            ulong bit;

            switch (index)
            {
                case < 64:
                    bit = (ulong)Math.Pow(2, index);
                    _bits0 &= ~bit;
                    return;

                case < 128:
                    bit = (ulong)Math.Pow(2, index - 64);
                    _bits1 &= ~bit;
                    return;

                case < 192:
                    bit = (ulong)Math.Pow(2, index - 128);
                    _bits2 &= ~bit;
                    return;

                case < 256:
                    bit = (ulong)Math.Pow(2, index - 192);
                    _bits3 &= ~bit;
                    return;

                default:
                    throw new ArgumentOutOfRangeException(nameof(index), $"{index} is invalid, index must be less than 256");
            }
        }

        public void Clear()
        {
            _bits0 = 0;
            _bits1 = 0;
            _bits2 = 0;
            _bits3 = 0;
        }

        public bool HasAny()
        {
            return _bits0 > 0 || _bits1 > 0 || _bits2 > 0 || _bits3 > 0;
        }

        public bool IsClear()
        {
            return _bits0 == 0 && _bits1 == 0 && _bits2 == 0 && _bits3 == 0;
        }
    }
}
