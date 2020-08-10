namespace Vos
{
    public class VOS
    {
        public System.Collections.Generic.List<Process> Processes = new System.Collections.Generic.List<Process>();
        public Shell Shell { get; }
        public FileSystem FileSystem { get; }

        public VOS()
        {
            this.Shell = new Shell()
            {
                In = System.Console.In,
                Out = System.Console.Out,
            };
            this.FileSystem = new FileSystem();
        }
        public Process CreateProcess(string v)
        {
            Process result = new Process(this);
            byte[] data = this.FileSystem.ReadAllByte(v);
            System.Reflection.Assembly asm = System.Reflection.Assembly.Load(data);
            System.Type[] tt = asm.GetTypes();
            foreach (System.Type t in tt)
            {
                if (typeof(Application).IsAssignableFrom(t))
                {
                    result.Application = (Application)System.Activator.CreateInstance(t, result);
                    this.Processes.Add(result);
                    return result;
                }
            }
            return null;
        }
        [System.STAThread]
        static void Main(string[] args)
        {
            VOS os = new VOS();
            os.FileSystem.Mount("/", new MirrorStorage("../../rootfs"));
            Process p = os.CreateProcess("/bin/vosbox");
            p.Shell.In = os.Shell.In;
            p.Shell.Out = os.Shell.Out;
            p.Start(null);
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run();
        }
    }
}