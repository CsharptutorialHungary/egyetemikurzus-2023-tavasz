using DU0038.Controller;

namespace DU0038
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ProgramController programController = new ProgramController();
            programController.StartProgramLoop();
        }
    }
}
