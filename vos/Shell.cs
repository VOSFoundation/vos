namespace Vos
{
    public class Shell
    {
        public System.IO.TextWriter Out { get; set; }
        public System.IO.TextReader In { get; set; }

        public Shell()
        {
        }
        public void WriteLine(string v)
        {
            this.Out.WriteLine(v);
        }
        public string ReadLine()
        {
            return this.In.ReadLine();
        }
        public void Write(string v)
        {
            this.Out.Write(v);
        }
    }
}