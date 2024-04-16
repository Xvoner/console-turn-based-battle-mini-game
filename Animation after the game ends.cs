
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME
{
    class whowin
    { 
        public static async Task playerwin()               //用异步的原因是，此时gamebegin里的gamemode为null，while进行了else循环防止主程序结束
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Green;
            for (int i = 0; i <= 50; i++)
            {
                for (int j = 0; j <= 30; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write("*");

                    Console.SetCursorPosition(100 - i, j);
                    Console.Write("*");
                }
                await Task.Delay(50);
            }
            await Task.Delay(2000);
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor= ConsoleColor.Black;
            Console.ForegroundColor= ConsoleColor.Green;
            Console.Clear();
            gamebegin.choosemodel(data.gamemodels.gamewhosuccess);
        }

        public static async Task bosswin()
        {
            Console.SetCursorPosition(5, 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(data.playerhp);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(data.playerpos, 15);
            Console.Write(" O");
            Console.SetCursorPosition(data.playerpos, 16);
            Console.Write("/|\\");
            Console.SetCursorPosition(data.playerpos, 17);
            Console.Write("/ \\");
            Console.SetCursorPosition(data.bosslifepos, 1);        
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(data.bosshp);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(data.bosspos, 15);
            Console.Write(" O");
            Console.SetCursorPosition(data.bosspos, 16);
            Console.Write("/|\\");
            Console.SetCursorPosition(data.bosspos, 17);
            Console.Write("/ \\");
            Console.SetCursorPosition(data.bosspos, 18);
            Console.Write("/   \\");

            Thread.Sleep(2000);
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor= ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Red;
            for(int i=0;i<50;i++)
            {
                Console.WriteLine("************************************************************************************************************************************************************************************************");
                await Task.Delay(50);
            }
            Console.SetCursorPosition(0, 0);
            Console.Clear();

            await Task.Delay(2000);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            gamebegin.choosemodel(data.gamemodels.gamewhosuccess);
        }
    }
}
