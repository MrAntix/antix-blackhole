namespace Antix.Blackhole
{
    internal class FindResult
    {
        public string Found { get; internal set; }
        public int Index { get; internal set; }

        public static implicit operator int(FindResult r)
        {
            return r == null ? -1 : r.Index;
        }
    }
}