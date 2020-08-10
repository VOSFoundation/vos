namespace Vos
{
    public abstract class Storage
    {
        public abstract System.Collections.Generic.IEnumerable<string> EnumerateEntries(string v);

        public abstract string ReadAllText(string v);

        public abstract byte[] ReadAllByte(string v);
    }
}