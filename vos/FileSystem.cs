namespace Vos
{
    public class FileSystem
    {

        System.Collections.Generic.Dictionary<string, Storage> _Mount = new System.Collections.Generic.Dictionary<string, Storage>();

        public FileSystem()
        {
        }
        internal void Mount(string mountPoint, Storage storage)
        {
            this._Mount.Add(mountPoint, storage);
        }
        public System.Collections.Generic.IEnumerable<string> EnumerateEntries(string currentDirectory)
        {
            string key = this.FindStorage(currentDirectory);
            Storage storage = this._Mount[key];
            return storage.EnumerateEntries(currentDirectory.Substring(key.Length));
        }
        private string FindStorage(string currentDirectory)
        {
            string mnt = "";
            int length = 0;
            foreach (string k in _Mount.Keys)
            {
                if (currentDirectory.StartsWith(k))
                {
                    if (k.Length > length)
                    {
                        mnt = k;
                        length = k.Length;
                    }
                }
            }
            if (length == 0) throw new System.Exception();
            else return mnt;
        }

        public byte[] ReadAllByte(string v)
        {
            v = v.Replace("//", "/");
            string key = this.FindStorage(v);
            Storage storage = this._Mount[key];
            return storage.ReadAllByte(v.Substring(key.Length));
        }

        public string ReadAllText(string v)
        {
            v = v.Replace("//", "/");
            string key = this.FindStorage(v);
            Storage storage = this._Mount[key];
            return storage.ReadAllText(v.Substring(key.Length));
        }
    }
}