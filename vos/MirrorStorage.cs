namespace Vos
{
    internal class MirrorStorage : Storage
    {
        private string VV;

        public MirrorStorage(string v)
        {
            this.VV = System.IO.Path.GetFullPath(v);
        }
        public override System.Collections.Generic.IEnumerable<string> EnumerateEntries(string v)
        {
            foreach (string entry in System.IO.Directory.EnumerateFileSystemEntries(this.VV + "/" + v))
            {
                string[] p = entry.Split("\\/".ToCharArray(), System.StringSplitOptions.None);
                yield return p[p.Length - 1];
            }
        }
        public override string ReadAllText(string v)
        {
            return System.IO.File.ReadAllText(this.VV + "/" + v);
        }
        public override byte[] ReadAllByte(string v)
        {
            return System.IO.File.ReadAllBytes(this.VV + "/" + v);
        }
    }
}