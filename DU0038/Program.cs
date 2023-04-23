using DU0038.Controller;
using DU0038.Service;

namespace DU0038
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ProgramController programController = new ProgramController();
            programController.StartProgramLoop();

            //Kategória hozzáadása
            //Egyenleg lekérése
            //Bevétel Kategóriánként
            //Költségek kategóriánként
            //Tranzakciók listázása
            //Tranzakciók kategóriánként
            //Tranzakció hozzáadása
        }
    }
}
