using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GAME
{
    class introduct:updateenvent
    {
        Random number=new Random();
        public introduct()
        {
            data.returnbuttonpag = -1;
            data.target = 3;
            data.pag = 1;      //刚开始看到第二页的内容
            data.jump = false;
            data.normal = true;
            data.enterunnormal = false;
            tonormalpag(1);
        }
        private void cursion()           //将此时选定的行文字标记为红色
        {

            Console.SetCursorPosition(30,30);
            Console.ForegroundColor = data.target == 1 ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine("<<<");

            Console.SetCursorPosition(90,30);
            Console.ForegroundColor = data.target == 3 ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine(">>>");

            Console.SetCursorPosition(60,30);
            Console.ForegroundColor = data.target == 2 ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine("返回");
        }
        private void unnormalcursion()
        {
            Console.SetCursorPosition(30, 30);
            Console.ForegroundColor = data.unnormaltarget ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine("<<<<<<<<<<<<<<");

            Console.SetCursorPosition(90, 30);
            Console.ForegroundColor = data.unnormaltarget  ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(">>>>>>>>>>>>>>");
        }
        private void changemode(data.gamemodels gamemode)
        {
            gamebegin.choosemodel(gamemode);
        }
        private void lookorreturn()
        {
            if (data.normal)
            {
                switch (data.target)
                {
                    case 1:
                        data.pag--;
                        if (data.pag <= 0)
                        {
                            data.pag = 0;
                        }
                        tonormalpag(data.pag);    //到指定正常的页码
                        break;
                    case 3:
                        data.pag++;
                        if (data.pag >= data.normaltip.Length - 1)
                        {
                            tonormalpag(data.pag);    //最后一次到指定正常的页码
                            data.jump = true;     // 不然的话就无法继续对话了
                            data.normal = false;      //开始进入异常的页码
                            data.pag = 0;
                        }
                        else
                        {
                            tonormalpag(data.pag);    //到指定正常的页码
                        }
                        break;
                    default:
                        Console.Clear();
                        changemode(data.gamemodels.gamestart);      //返回初始菜单界面
                        break;
                }
            }
            else
            {
                if(data.returnbuttonpag >= data.returneventtext.Length - 1)             //因为此时下面的返回按钮已经消失了，因此无论按左按钮还是右按钮都是翻页
                {
                    wellunnormalevent();
                }
                else
                {
                    switch (data.target)
                    {
                        case 2:
                            data.returnbuttonpag++;
                            data.enterunnormal = true;
                            playerwanttoreturnevent(data.returnbuttonpag);
                            break;
                        case 1:
                            data.enterunnormal = true;    //只有按下向左的返回键才能正式进入异常页面事件
                            wellunnormalevent();
                            break;
                        default:
                            wellunnormalevent();
                            break;
                    }
                }
            }
        }
        private async Task pagelook()           //显示当前的页码
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(60, 33);
            Console.Write(data.pag+1);
        }
        private async Task playerwanttoreturnevent(int returnpag)     //这些按下返回按钮出现的内容，先按顺序，在随机
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(23, 20);
            foreach (string i in data.returneventtext[returnpag])
            {
                Console.SetCursorPosition(23, Console.CursorTop);
                foreach (char j in i)                          //让字符一个一个缓慢的显示出来
                {
                    Console.Write(j);
                    await Task.Delay(200);
                }
                Console.CursorTop++;
            }
            while (Console.KeyAvailable) { Console.ReadKey(true); } //丢弃缓冲区已经读的的待处理按键事件
            if(data.returnbuttonpag>=data.returneventtext.Length-1)
            {
                unnormalcursion();
            }
            else
            {
                cursion();
            }
            data.jump = true;
        }
        private async Task wellunnormalevent()
        {
            if (data.enterunnormal || data.returnbuttonpag >= data.returneventtext.Length - 1)
            {
                data.pag++;
                tounnromalpag(data.pag);
            }
            else
            {
                data.pag = 0;
                data.jump = true;
            }
        }
        private async Task tonormalpag(int pag)              //因为异常页码与正常页码事件类似，因此最好再写一个类，来判断执行父类还是子类的方法，但我懒得写了，就这样吧
        {
            Console.Clear();
            pagelook();
            Console.ForegroundColor= ConsoleColor.White;
            Console.SetCursorPosition(23, 10);
            foreach (string i in data.normaltip[pag])
            {
                data.talkspeed = 40;
                Console.SetCursorPosition(23, Console.CursorTop);
                foreach (char j in i)                          //让字符一个一个缓慢的显示出来
                {
                    Console.Write(j);
                    await Task.Delay(data.talkspeed);
                }
                Console.CursorTop++;
            }
            while (Console.KeyAvailable) { Console.ReadKey(true); } //丢弃缓冲区已经读的的待处理按键事件
            if (data.returnbuttonpag >= data.returneventtext.Length - 1)
            {
                unnormalcursion();
            }
            else
            {
                cursion();
            }
            data.jump = true;
        }
        private async Task tounnromalpag(int pag)                        //这是异常页码的事件
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(30, 10);
            foreach (string i in data.unnormaltip[pag])
            {
                Console.SetCursorPosition(30, Console.CursorTop);
                foreach (char j in i)                          
                {
                    Console.Write(j);
                    await Task.Delay(100);               //异常的对话界面不能跳过
                }
                Console.CursorTop++;
            }
            while (Console.KeyAvailable) { Console.ReadKey(true); }          //清空按键事件缓冲区内容
            if (data.returnbuttonpag >= data.returneventtext.Length - 1)
            {
                unnormalcursion();
            }
            else
            {
                cursion();
            }
            data.jump = true;
        }
        public void update()
        {
            if (data.jump)
            {
                if(Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).KeyChar)
                    {
                        case 'A':
                            data.target--;
                            data.unnormaltarget = true;
                            if (data.target < 1)
                            {
                                data.target = 1;
                            }
                            if (data.returnbuttonpag >= data.returneventtext.Length - 1)
                            {
                                unnormalcursion();
                            }
                            else
                            {
                                cursion();
                            }
                            break;
                        case 'D':
                            data.target++;
                            data.unnormaltarget = false;
                            if (data.target > 3)
                            {
                                data.target = 3;
                            }
                            if (data.returnbuttonpag >= data.returneventtext.Length - 1)
                            {
                                unnormalcursion();
                            }
                            else
                            {
                                cursion(); 
                            }
                            break;
                        case 'Z':
                            data.jump = false;
                            lookorreturn();
                            break;
                    }
                }  
            }
            else
            {
                if (Console.ReadKey(true).KeyChar == 'Z')      //按下z键快速跳过对话
                {
                    data.talkspeed = 0;
                }
            } 
        }
    }
}
