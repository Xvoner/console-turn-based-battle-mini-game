using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME
{
    class gameing:updateenvent
    {
        Random number = new Random();
        public gameing()
        {
            boolupdate();            //判断数据更新，总之就是判断数据，没有解释！！
            numberupdate();         //游戏数据更新，像是敌方的位置啊，血量位置啊什么的

            oneulupdate();           //界面ul更新
        }
        private void numberupdate()
        {
            data.gameknifdata = new List<string>();
            data.gameingtalksum = 0;
            data.bosspos = 100;
            data.playerpos = 30;
            data.playerhp = "||||||||||||||||||||||||||||||||||||||||||||||||||"; //玩家血量
            data.bosshp = "||||||||||||||||||||||||||||||||||||||||||||||||||"; //敌人血量
            data.bosslifepos = 75;
            data.playnumber = 1;
        }
        private void boolupdate()
        {
            data.goout = true;
            data.playertime = true;
            data.gameingtalkupdate = true;
            data.canshake = false;
            data.cango = false;
            data.ccanceltimedown = false;
        }
        private void oneulupdate()
        {
            Console.Clear();
            //玩家的血量表示
            Console.SetCursorPosition(5, 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(data.playerhp);
            //玩家的人物像
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(data.playerpos, 15);
            Console.Write(" O");
            Console.SetCursorPosition(data.playerpos, 16);
            Console.Write("/|\\");
            Console.SetCursorPosition(data.playerpos, 17);
            Console.Write("/ \\");

            //敌人的血量
            Console.SetCursorPosition(data.bosslifepos, 1);        //为了让敌人的血量从左到右消失用的
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(data.bosshp);
            //敌人的画像
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(data.bosspos, 15);
            Console.Write(" O");
            Console.SetCursorPosition(data.bosspos, 16);
            Console.Write("/|\\");
            Console.SetCursorPosition(data.bosspos, 17);
            Console.Write("/ \\");
            Console.SetCursorPosition(data.bosspos, 18);
            Console.Write("/   \\");
        }
        private void begintalk()
        {
            cleartalk();
            var thistalk = gameingtalk[data.gameingtalksum];
            if(thistalk.chacter==data.chactertype.boss)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(85, 13);
                Console.WriteLine(thistalk.talkdata);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(15, 13);
                Console.WriteLine(thistalk.talkdata);
            }
            data.gameingtalksum++;
            if(data.gameingtalksum>7)
            {
                data.gameingtalkupdate = false;

                Thread.Sleep(2000);

                cleartalk();

                Console.SetCursorPosition(50, 13);
                Console.ForegroundColor = ConsoleColor.Yellow;
                data.cango = true;
                Console.WriteLine("按下Z键准备开始");
            }
        }
        private void cleartalk()
        {
            Console.SetCursorPosition(15, 13);
            Console.Write("                                               ");
            Console.SetCursorPosition(85, 13);
            Console.Write("                                               ");
        }
        public void update()
        {
            if(Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).KeyChar)
                {
                    case 'T':
                        if (data.gameingtalkupdate)
                        {
                            begintalk();
                        }
                        break;
                    case 'Z':
                        if (data.cango)
                        {
                            data.cango = false; //按下开始后无法继续按下开始按钮
                            Console.SetCursorPosition(50, 13);
                            Console.Write("                   ");
                            timedown(); //利用异步方法进行倒计时而不影响主线程的update
                            data.canshake = true; //游戏开始后，可以进行摇筛
                        }
                        break;
                    case 'H':
                        if (data.canshake)
                        {
                            data.canshake = false; //摇筛后无法继续摇
                            data.ccanceltimedown = true;  //如果按下摇筛，则倒计时循环中断

                            Console.SetCursorPosition(30, 13);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(specialtalksplayer[number.Next(0,specialtalksplayer.Count-1)].talkdata);//每次角色发起进攻的时候会有对话

                            Thread.Sleep(1000);
                            oneulupdate();

                            wholeprocess(); //开始执行游戏全过程
                        }
                        break;
                }
            }
        }

        private void wholeprocess() //游戏的主要过程
        {
            if(data.playertime)       //判断是否是玩家的回合
            {
                buffvisable();
                Thread.Sleep(1000);
            }
            cleartalk();//清空所有聊天对话或者人物头顶上的内容

            moveandreturn(moveevent(),goorreturn());

            data.playnumber++;//游戏回合增加1
        }
        private bool moveevent()
        {
            if(data.playertime)
            {
                if(data.goout)
                {
                    return data.playerpos <= 90;
                }
                else
                {
                    return data.playerpos >= 30;
                }
            }
            else
            {
                if(data.goout)
                {
                    return data.bosspos >= 40;
                }
                else
                {
                    return data.bosspos <= 100;
                }
            }
        }
        private int goorreturn()
        {
            if (data.playertime)
            {
                if (data.goout)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (data.goout)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        }
        private async void newbegin()      //生命体运动到对方位置又回来的时候，进行新一轮的开始
        {
            if(data.playertime)
            {
                data.goout = true;
                data.canshake = true;
                data.ccanceltimedown = false;
                timedown();
            }
            else
            {
                data.goout = true;
                bossspecialtk();    //敌方说话
                //await Task.Delay(1000); //这个地方如果继续延时的话，会导致敌方说话后停一秒，感觉有点奇怪，索性去掉吧
                moveandreturn(moveevent(), goorreturn());
            }
        }
        private async Task moveandreturn(bool posevent,int whoint) //利用异步方法，防止程序卡死，生命体移动出去，并且最后触发掉血效果, 然后再回到原点
        {
            if(data.playertime && posevent)
            {
                data.playerpos += whoint;
                oneulupdate();
                await Task.Delay(40);
                moveandreturn(moveevent(), goorreturn()); //只要坐标符合要求，那么会再次调用这个判断里的方法，下同
            }
            else if(!data.playertime && posevent)
            {
                data.bosspos += whoint;
                oneulupdate();
                await Task.Delay(40);
                moveandreturn(moveevent(), goorreturn());
            }
            else
            {
                if(data.goout)                //如果是进攻模，说明此时生物体已经运动到了对面，那么进攻模式设为false防止返回时还会调用hpchange()
                {
                    data.goout = false;
                    await Task.Delay(1000);
                    hpchange();
                }
                else                         //如果不是进攻模式，说明此时生物体已经返回到了原点，玩家或者敌方的模式反过来，进行新一轮newbegiin()
                {
                    data.playertime = !data.playertime;
                    newbegin();
                }
            }
        }
        private void buffvisable() //显示玩家筛到的伤害加成，只提供给玩家显示，对boss隐藏
        {
            data.playerknif = number.Next(1, 20);
            Console.SetCursorPosition(30, 13);
            Console.Write("+" + (int)data.playerknif);
        }
        private void hpchange()
        {
            data.bossknif = number.Next(1, 20);
             if(data.playertime)
            {
                Console.SetCursorPosition(100, 13);
                Console.Write("-" + (int)data.playerknif);        //先显示敌方掉血量，下同
                Thread.Sleep(500);
                if(!(data.bosshp.Length-data.playerknif<=0))  //提前预判生命体是否会死亡，下同
                {
                    moveandreturn(moveevent(), goorreturn());          //在血量减少期间，使用异步方法让人物先回去，不会等着血条消失完后才回去，下同
                    for (int i = 0; i < data.playerknif; i++)  //去除扣掉的血量，下同，由于上面的异步方法被调用了，因此不用刷新ul了
                    {
                        data.bosshp = data.bosshp.Remove(0, 1);
                        data.bosslifepos++;
                        Thread.Sleep((int)2000/(int)data.playerknif);         //为了让血条减少的更加可观，下同，但敌方的血条位于屏幕右侧，因此要让血条从左向右消失
                    }
                }
                else
                {
                    for (int i = 0; data.bosshp.Length>0; i++)  //如果预判到生命体死亡，那么让血量掉完后执行胜利场景
                    {
                        data.bosshp = data.bosshp.Remove(0, 1);
                        data.bosslifepos++;
                        oneulupdate();
                        Thread.Sleep(50);
                    }
                    Thread.Sleep(2000);
                    Console.Clear();

                    whowin.playerwin();                         //玩家胜利
                    gamebegin.gamemode = null;
                }
                data.gameknifdata.Add("玩家对敌方造成了" + data.playerknif + "的伤害");  //玩家对敌方的伤害被记录在日志中
            }
             else
            {
                Console.SetCursorPosition(30, 13);
                Console.Write("-" + (int)data.bossknif);
                Thread.Sleep(500);
                if(!(data.playerhp.Length-data.bossknif<=0))
                {
                    moveandreturn(moveevent(), goorreturn());
                    for (int i = 0; i < data.bossknif; i++)
                    {
                        data.playerhp = data.playerhp.Remove(0, 1);             //玩家的血条从右向左消失
                        Thread.Sleep((int)2000 / (int)data.bossknif);
                    }
                }
                else
                {
                    for (int i = 0; data.playerhp.Length>0; i++)
                    {
                        data.playerhp = data.playerhp.Remove(0, 1);
                        oneulupdate();
                        Thread.Sleep(50);
                    }
                    Thread.Sleep(2000);
                    Console.Clear();

                    whowin.bosswin();
                    gamebegin.gamemode = null;
                }
                data.gameknifdata.Add("敌方对玩家造成了" + data.bossknif + "的伤害");  //玩家对敌方的伤害被记录在日志中
            }
        }
        private async Task timedown() //5秒倒计时，如果在倒计时结束前没有摇筛子，那么默认敌方先开始，反之你先开始
        {
            for(int i=number.Next(2,8);i>=0;i--)
            {
                Console.SetCursorPosition(68, 10);
                Console.WriteLine("   ");
                Console.SetCursorPosition(68, 10); //上面两个方法的原因是为了让倒计时数字清空，然后再次出现，防止数字相互堆叠
                Console.WriteLine(i);
                await Task.Delay(1000); //确保计数器一秒闪烁一个数字
                if(data.ccanceltimedown)  //如果按下H按钮，那么for循环中断
                {
                    break;
                }
                else if(i==0)              //倒计时结束会发生什么呢？
                {
                    data.playertime = false;//敌方先手
                    data.canshake = false;//无法再按下摇筛按键
                    if(data.playnumber==1)    //如果是第一回合
                    {
                        oneulupdate();

                        firstevent();
                        await Task.Delay(1000);
                        wholeprocess();    //角色说完话了，然后开始执行游戏主程序
                    }
                    else
                    {
                        oneulupdate();

                        bossspecialtk();
                        await Task.Delay(1000);
                        wholeprocess();    //角色说完话了，然后开始执行游戏主程序
                    }
                }
            }
        }
        private void firstevent()                   //敌方先手的第一次对话
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(90, 13);
            Console.Write(specialtalksboss[0].talkdata);

            Thread.Sleep(1000);
            cleartalk();
        }
        private void bossspecialtk()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(90, 13);
            Console.Write(specialtalksboss[number.Next(1,specialtalksboss.Count-1)].talkdata);    //从第二个对话数据到最后的随机内容，因为第一个对话是开局用的，就用一次

            Thread.Sleep(1000);
            cleartalk();
        }
        private List<gameingtalkdata> gameingtalk = new List<gameingtalkdata>() //角色对话数据
        {
            new gameingtalkdata()
            {
                chacter=data.chactertype.boss,
                talkdata="300年了，今天，我终于出来了"
            },
            new gameingtalkdata()
            {
                chacter=data.chactertype.player,
                talkdata="这几年，你一定很痛苦吧"
            },
            new gameingtalkdata()
            {
                chacter=data.chactertype.boss,
                talkdata="痛苦并快乐着（难受）"
            },
            new gameingtalkdata()
            {
                chacter=data.chactertype.player,
                talkdata="看到你的样子，我就忍不住想笑出来"
            },
            new gameingtalkdata()
            {
                chacter=data.chactertype.boss,
                talkdata="为什么，是因为我没洗脸吗"
            },
            new gameingtalkdata()
            {
                chacter=data.chactertype.player,
                talkdata="不是的，我只是单纯的想笑"
            },
            new gameingtalkdata()
            {
                chacter=data.chactertype.boss,
                talkdata="你这啥臭毛病，300年了，你还是没改一贯的作风"
            },
            new gameingtalkdata()
            {
                chacter=data.chactertype.boss,
                talkdata="啥也别说了，迎接我的怒火吧"
            }
        };

        private List<specialtalk> specialtalksboss = new List<specialtalk>() //敌方特殊对话数据
        {
            new specialtalk()
            {
                talkdata="时间到了，多谢承让，那我先来"
            },
            new specialtalk()
            {
                talkdata="你马上就很痛了"
            },
            new specialtalk()
            {
                talkdata="真的废物，再见了"
            },
            new specialtalk()
            {
                talkdata="几次了，死性不改啊，这都反应不过来"
            },
            new specialtalk()
            {
                talkdata="一句话，傻逼"
            }
        };
        private List<specialtalk> specialtalksplayer = new List<specialtalk>() //我方特殊对话数据
        {
            new specialtalk()
            {
                talkdata="看来你还得睡个300年"
            },
            new specialtalk()
            {
                talkdata="就这啊，傻逼"
            },
            new specialtalk()
            {
                talkdata="不是，还没开始就结束了？"
            },
            new specialtalk()
            {
                talkdata="我操你妈"
            },
            new specialtalk()
            {
                talkdata="你妈妈被我操了，懂么"
            }
        };
    }
    class gameingtalkdata //角色对话类
    {
        public data.chactertype chacter;
        public string talkdata;
    }

    class specialtalk //特殊对话类
    {
        public string talkdata;
    }
}
