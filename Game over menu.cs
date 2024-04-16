using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME
{
     class gameend:updateenvent
    {
        Random loadnumber = new Random();
        public gameend()
        {
            data.target = 1;

            redchose();
        }
        private void redchose()
        {
            Console.SetCursorPosition(60, 15);
            Console.ForegroundColor = data.target == 1 ? ConsoleColor.Red : ConsoleColor.Blue;
            Console.Write("再来一局");

            Console.SetCursorPosition(60, 21);
            Console.ForegroundColor = data.target == 2 ? ConsoleColor.Red : ConsoleColor.Blue;
            if(data.playertime)
            {
                Console.Write("查看游戏回放");
            }
            else
            {
                Console.Write("查看死亡回放");
            }
            Console.SetCursorPosition(60, 27);
            Console.ForegroundColor = data.target == 3 ? ConsoleColor.Red : ConsoleColor.Blue;
            Console.Write("退出游戏");
        }
        public void update()
        {
            if(Console.KeyAvailable)
            {
                switch(Console.ReadKey(true).KeyChar)
                {
                    case 'W':
                        data.target--;
                        if(data.target<=1)
                        {
                            data.target = 1;
                        }
                        redchose();
                        break;
                    case 'S':
                        data.target++;
                        if(data.target>=3)
                        {
                            data.target = 3;
                        }
                        redchose();
                        break;
                    case 'Z':
                        changgemode();
                        break;
                }
            }
        }
        private void changgemode()
        {
            switch(data.target)
            {
                case 1:
                    onloadannimation();           //确定后执行加载动画，然后游戏重新开始
                    onloadnumber();
                    gamebegin.gamemode = null;
                    break;
                case 2:
                    gamebegin.choosemodel(data.gamemodels.gameknifdate);
                    break;
                case 3:
                    Console.Clear();
                    Environment.Exit(0);
                    break;
            }
        }
        private async Task onloadannimation()               //图像动画
        {
            Console.Clear();
            Console.Write("正在加载虚拟机");

            for (int i = 0; i < 100;i++)
            {
                Console.SetCursorPosition(i,(int)i/4);
                Console.Write("-");
                await Task.Delay(loadnumber.Next(5, 100));
            }
        }

        private async Task onloadnumber()              //加载百分比
        {
            Console.SetCursorPosition(60, 30);
            Console.Write("        ");
            Console.SetCursorPosition(60, 30);
            Console.Write(data.loadnumber+"%");
            data.loadnumber++;
            await Task.Delay(loadnumber.Next(10, 300));
            if (data.loadnumber<=100)
            {
                onloadnumber();
            }
            else
            {
                gamebegin.choosemodel(data.gamemodels.gamestart);
            }
        }
    }
}
