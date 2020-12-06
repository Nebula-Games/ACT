using System;

namespace ACT_Core
{
    class Program
    {
        static void Main(string[] args)
        {
            void MenuClicking(string Name, string ID, string[] OtherData)
            {
                if (Name == "SampleDLLGenericCallback")
                {
                    Console.Clear();
                    Console.WriteLine("In SampleDLLGenericCallback - Press Key");
                    Console.ReadKey();
                }
            }
            var Evnt = new ACT.Core.Delegates.OnMenuItemClick(MenuClicking);

            ACT.Core.Windows.Console.ACTMenuSystem.InitMenuSystem(new ACT.Core.Windows.Console.ACTMenuSystemInitializationSettings()
            {
                AutoRunMenu = false,
                Columns = 100,
                Rows = 50,
                RequireEnterToSelect_Override = true,
                ForegroundColor = ConsoleColor.White,
                BackgroundColor = ConsoleColor.Black
            }, Evnt, true);

            
            ACT.Core.Windows.Console.ACTMenuSystem.RunMenu();


            //Console.WriteLine("Hello World!");

            /*
             * memory
             Time - Strength of the Memory creates the sense of the passage of time

            */
        }


    }
}
