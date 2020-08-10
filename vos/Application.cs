namespace Vos
{
    public abstract class Application
    {
        public Process Process { get; }

        public Application(Process process)
        {
            this.Process = process;
        }
        public abstract int Main(string[] args);
    }
}