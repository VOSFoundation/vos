namespace Vos
{
    public class Process
    {
        public VOS OS { get; }
        public Application Application { get; set; }
        public System.Threading.Thread MainThread { get; private set; }
        public int ReturnCode { get; private set; }
        public Shell Shell { get; internal set; }

        public Process(VOS os)
        {
            this.OS = os;
            this.Shell = new Shell();
            this.ReturnCode = -1;
        }
        public void Start(string[] args)
        {
            this.MainThread = new System.Threading.Thread(new System.Threading.ThreadStart(() => { this.ReturnCode = this.Application.Main(args); }));
            this.MainThread.Start();
        }
    }
}