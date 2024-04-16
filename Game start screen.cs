
namespace GAME
{
    class gamebegin
    {
        public static updateenvent? gamemode;
        //游戏初始化，设置控制台的宽高, 设置缓冲区
        public gamebegin()
        {
            Console.SetWindowSize(130,35);
            Console.SetBufferSize(130,35);
            Console.CursorVisible = false;
        }
        //从主程序入口调用游戏开始指令，加一个线程
        public void start()
        {
            Thread t = new Thread(update);
            t.Start();
            choosemodel(data.gamemodels.gamestart);   //选择游戏模式为开始菜单界面
            update();   //执行游戏循环主体
        }
        public static void choosemodel(data.gamemodels gamemodel)
        {
            Console.Clear();
            switch(gamemodel)  //把游戏模式实例赋给gamemode，以便让目前的实例场景调用update函数来循环
            {
                case data.gamemodels.gamestart:
                    gamemode = new gamestart();
                    break;
                case data.gamemodels.gametips:
                    gamemode = new introduct();
                    break;
                case data.gamemodels.gameing:
                    gamemode = new gameing();
                    break;
                case data.gamemodels.gamewhosuccess:
                    gamemode = new gamewhosuccess();
                    break;
                case data.gamemodels.gameend:
                    gamemode = new gameend();
                    break;
                case data.gamemodels.gameknifdate:
                    gamemode = new gameknifdate();
                    break;
                
            }
        }
        private void update()
        {
            while (true)
            {
                if(gamemode!=null)
                {
                    Thread.Sleep((int)data.updatestop);  //设置隔多久循环以此，减轻cpu压力
                    if (gamemode != null)                 //设置两层检测空类型的原因是防止gamemode在判断语句里类型还会变化
                    {
                        gamemode.update();
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                } 
            }
        }
    }
}