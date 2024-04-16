
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME
{
    class  gameknifdate:updateenvent
    {
        public gameknifdate()
        {
            Console.SetCursorPosition(0, 1);
            Console.ForegroundColor= ConsoleColor.White;
            foreach(string data in data.gameknifdata)
            {
                Console.SetCursorPosition(60, Console.CursorTop);
                Console.WriteLine(data);
                Console.CursorTop++;
            }
        }
        public void update()
        {
            if(Console.KeyAvailable)
            {
                switch(Console.ReadKey(true).KeyChar)
                {
                    case 'Z':
                        gamebegin.choosemodel(data.gamemodels.gameend);
                        break;
                }
            }
        }
    }
}
