
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME
{
    class gamestart:updateenvent
    {
        public gamestart()
        {
            data.target = 1;

            Console.SetCursorPosition(60, 5);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("游戏开始");
            redchose();
        }
        private void redchose() //选择让某个字幕变红
        {

                Console.SetCursorPosition(60, 15);
                Console.ForegroundColor = data.target == 1 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("游戏开始");

                Console.SetCursorPosition(60, 21);
                Console.ForegroundColor = data.target == 2 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("退出游戏");

                Console.SetCursorPosition(60, 27);
                Console.ForegroundColor = data.target == 3 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("查看游戏介绍");
            
        }

        private void changemode()
        {
            switch(data.target)
            {
                case 1:
                    gamebegin.choosemodel(data.gamemodels.gameing);
                    break;
                case 2:
                    Console.Clear();
                    Environment.Exit(0);
                    break;
                case 3:
                    gamebegin.choosemodel(data.gamemodels.gametips);
                    break;
            }
        }
        public void update()
        {
            if(Console.KeyAvailable) //加这个的原因是，只有在按下按键的时候才能触发以下方法，防止卡在switch (Console.ReadKey(true).KeyChar)中会一直等待键盘输入才执行，这样会造成当切换到下一个场景的时候，这个方法会继续等待键盘输入事件，按下ws键后还会跳出那些字幕
            {
                switch (Console.ReadKey(true).KeyChar)
                {
                    case 'W':
                        data.target--;
                        if (data.target < 1)
                        {
                            data.target = 1;
                        }
                        redchose();
                        break;
                    case 'S':
                        data.target++;
                        if (data.target > 3)
                        {
                            data.target = 3;
                        }
                        redchose();
                        break;
                    case 'Z':
                        changemode();
                        break;
                }
            }
                
        }
    }
}
