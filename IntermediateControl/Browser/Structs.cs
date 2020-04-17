using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SHQUERYRBINFO
{
    public uint cbSize;
    public ulong i64Size;
    public ulong i64NumItems;
};