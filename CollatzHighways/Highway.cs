using System;
using Edge.NumbersMagic;
using Edge.RecursiveQuerier;

namespace CollatzHighways
{
    public enum HighwayType
    {
        Init=0,
        Even=5,
        Odd=1,
        Terminal=3
    }
    public class HighWay
    {
        private static readonly DivisibilityQuerier Querier = new DivisibilityQuerier(2);
        public HighWay(int initial)
        {
            this.initial = initial;
        }
        public int initial { get; }
        public int this[int index]
        {
            get
            {
                return initial * Querier.PowQuerier[index];
            }
        }
        public HighwayType Type
        {
            get
            {
                if (initial == 1)
                    return HighwayType.Init;
                switch (initial % 6)
                {
                    case 1:
                        return HighwayType.Even;
                    case 3:
                        return HighwayType.Terminal;
                    case 5:
                        return HighwayType.Odd;
                    default:
                        throw new Exception("initial must be odd");
                }
            }
        }
        public int exitIndex(int num)
        {
            switch (Type)
            {
                case HighwayType.Init:
                    return 4 + 2 * num;
                case HighwayType.Odd:
                    return 1 + 2 * num;
                case HighwayType.Even:
                    return 2 + 2 * num;
                default:
                    throw new Exception("no exits");
            }
        }
        public int exitValue(int num)
        {
            return this[exitIndex(num)];
        }
        public HighWay exit(int num)
        {
            return new HighWay((exitValue(num) - 1) / 3);
        }
        public int getnum(int exitIndex)
        {
            switch (Type)
            {
                case HighwayType.Init:
                    return (exitIndex - 4) / 2;
                case HighwayType.Odd:
                    return (exitIndex - 1) / 2;
                case HighwayType.Even:
                    return (exitIndex - 2) / 2;
                default:
                    throw new Exception("no exits");
            }
        }
        public HighWay source(out int num)
        {
            int origin = initial * 3 + 1;
            int s;
            var exitIndex = Querier.Divisibility(origin, out s);
            var ret = new HighWay(s);
            num = ret.getnum(exitIndex);
            return ret;
        }
        public static HighWay getFromValue(int n, out int valueIndex)
        {
            int init;
            valueIndex = Querier.Divisibility(n, out init);
            return new HighWay(init);
        }
    }

}
