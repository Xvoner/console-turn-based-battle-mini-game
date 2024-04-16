
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME
{
    class gamewhosuccess:updateenvent
    {
        public gamewhosuccess()
        {
            if (data.playertime)         //此时是玩家回合说明是玩家胜利，那么显示玩家胜利台词
            {
                Console.SetCursorPosition(60, 3);
                for (int i=0;i<data.playerwintext.Length;i++)
                {
                    Console.SetCursorPosition(60, Console.CursorTop);
                    Console.WriteLine(data.playerwintext[i]);
                    Console.WriteLine();                     //隔行进行文字输出
                }
            }
            else
            {
                for (int i = 0; i < data.bosswintext.Length; i++)
                {
                    Console.SetCursorPosition(60, Console.CursorTop);
                    Console.WriteLine(data.bosswintext[i]);
                    Console.WriteLine();
                }
            }
            timedown();

        }
        private async Task timedown()
        {
            for(int i=0;i<5;i++)
            {
                await Task.Delay(1000);
            }
            Console.SetCursorPosition(13, 33);
            Console.Write("继续 继续 继续 继续 继续 继续 继续 继续 继续 继续 继续 继续 继续 继续 继续 继续 继续 继续 继续 继续");
        }
        public void update()
        {
            if(Console.KeyAvailable)
            {
                if(Console.ReadKey(true).KeyChar=='Z')
                {
                    gamebegin.choosemodel(data.gamemodels.gameend);
                }
            }
        }
    }
}
