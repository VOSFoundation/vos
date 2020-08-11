using System;

namespace Vos
{
    public class VOS
    {
        public System.Collections.Generic.List<Process> Processes = new System.Collections.Generic.List<Process>();
        public Shell Shell { get; }
        public FileSystem FileSystem { get; }

        public VOS()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            this.Shell = new Shell()
            {
                In = System.Console.In,
                Out = System.Console.Out,
            };
            this.FileSystem = new FileSystem();
        }

        private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetAssembly(typeof(VOS));
            if (args.Name.StartsWith("vos, "))
            {
                return asm;
            }
            return null;
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
        private void Run(string v)
        {
            string[] args, line = v.Trim().Split(' ');
            string app = line[0];
            args = new string[line.Length - 1];
            Array.Copy(line, 1, args, 0, args.Length);
            Process p = this.CreateProcess(app);
            p.Shell.In = this.Shell.In;
            p.Shell.Out = this.Shell.Out;
            p.Start(args);
        }
        [System.STAThread]
        public static void Main(string[] args)
        {
            VOS os = new VOS();
            os.FileSystem.Mount("/", new MirrorStorage(args[1]));
            string init = os.FileSystem.ReadAllText("/etc/init");
            string[] lines = init.Split("\r\n".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
            os.Run(lines[0]);
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run();
        }
    }
}