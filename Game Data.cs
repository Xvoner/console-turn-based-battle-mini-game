using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME
{
    static class data    //设置游戏需要的各种数据
    {
        public static float updatestop = 60f;   //在gamebegin里调用update时候隔60毫秒执行一次，减轻cup压力。如果特殊情况的话可以把此数值进行调整
        public static int pag = 1;           //用于表明游戏介绍的页码
        public static bool normal;          //用于判断是否还能正常的翻页
        public static bool jump;           // 用于判断是能进行下次对话
        public static int talkspeed;        //对话速度，用于快速跳过对话用;
        public static bool enterunnormal; //是否可以进入异常模式
        public static int returnbuttonpag;  //玩家进入异常页面时，按返回按钮时出现的一些列内容

        public static bool gameingtalkupdate;  //用于按下T键是否出现gameing中刚开始的对话选项，以便于到指定位置时停止对话
        public static int gameingtalksum = 0; //用于记录刚开始的时候进行了第几次对话
        public static bool canshake;  //用于判断是否可以进行判定摇筛，防止在摇完筛后还可以摇筛会造成程序错乱
        public static bool cango;   //用于判断按下Z键游戏是否可以开始
        public static bool playertime; //用于检测是否是玩家的回合
        public static int playerpos = 30;  //玩家原点水平坐标
        public static int bosspos = 100; //敌人原点水平坐标
        public static string playerhp = "||||||||||||||||||||||||||||||||||||||||||||||||||"; //玩家血量
        public static string bosshp = "||||||||||||||||||||||||||||||||||||||||||||||||||"; //敌人血量
        public static bool ccanceltimedown; //倒计时循环中断参数
        public static int playnumber = 1; //对战回合数
        public static float playerknif; //玩家抽取到的伤害数
        public static float bossknif;  //敌人抽取到的伤害数
        public static bool posevent; //用来判断是出去还是返回这个事件
        public static bool goout; //判断外出还是返回事件
        public static int bosslifepos = 75; //怪物血量横坐标
        public static int loadnumber = 0;

        public static List<string> gameknifdata = new List<string>(); //游戏日志，用来记录游戏中每一回合谁对对方的伤害
        public enum chactertype  //玩家与敌方角色
        {
            player,
            boss
        }
        public enum gamemodels  //各种场景模式
        {
            gamestart,
            gametips,
            gameing,
            gamewhosuccess,
            gameend,
            gameknifdate
        }

        public static int target;//用于在gamestart与gameintroduce里是判断哪个字体需要变红用的
        public static bool unnormaltarget;//用于异常情况下判断哪个字体需要变红用的
        public static readonly string[][] normaltip = new string[][]        //数组嵌套，外数组表示第几页，内数组表示第几页的内容, 这里代表正常页面的内容
        {
            new string[]{ "虽然这只是第一页，但也依旧什么都没有" },
            new string[]{ "简单的游戏介绍是这个样子的："
                ,"你与怪物都有100的血量，每次游戏开始后都会进行判定，也就是掷筛子，按照筛子数提高伤害",
            "技能方面：每次回合倒计时阶段，你可以按J使用攻击提高技能，按F进行防御，但你的伤害此回合会降低，H是大招，但也有可能发生意想不到的后果",
            "按下T是进行对话，但是你不能跳过对话",
            "总之，这是个游戏测试，就先写这点试试吧" },
            new string[]{ "虽然这只是第二页，但依旧什么都没有" },
            new string[]{ "虽然这只是第三页，但依旧什么都没有" },
            new string[]{"不要在翻了"},
            new string[]{ "............................." },
            new string[]{ "这样，真的有意思吗？" },
            new string[]{"。。。。。"},
            new string[]{ "我不明白你在期待什么" },
            new string[]{ "你认为前方会有什么？" },
            new string[]{".........","...............",".........................."},
            new string[]{ "难道","你还认为...." },
            new string[]{ "不会吧！","你真的那么想？？" },
            new string[]{"PLAYER","这只是个控制台而已","我也只是为了执行这些指令而存在着"},
            new string[]{ "你还认为","我将会吗" },
            new string[]{ "11","你连一个机器都不放过" },
            new string[]{ "33","怎么","333？" },
            new string[]{ "大22，33" },
            new string[]{ "不过","我警告你","不要继续向前走了" },
            new string[]{ "。。。。。" },
            new string[]{ "有些道路" ,"你选择了","就要承担相应的后果"},
            new string[]{ "好吧，随你吧","但选择选择返回还是不晚的" },
            new string[]{ "好吧好吧","我只是个机器","我只是用开发者的好意来提醒你","而已" },
            new string[]{ "不说了，我最后警告你，停下（懒得写了）" },
            new string[]{ "最后警告" },
            new string[]{ "如果你在向前走一步，那么你就再也回不来了" },
            new string[]{ "好","你再也无法回头了","你不信按下返回键试试" }
        };
        public static readonly string[][] unnormaltip = new string[][]        //这里代表异常页面的内容
        {
            new string[]{ "现在知道后悔了？" },
            new string[]{ "哈哈哈哈哈" },
            new string[]{ "什么，你想往前翻页？" },
            new string[]{"您觉得有用吗？"},
            new string[]{ "看到惊慌失措的样子","我真的好开心" },
            new string[]{ "不过，我想，我们可以聊一聊" },
            new string[]{"玩这个游戏的目的是什么呢"},
            new string[]{ "明明就是写好的程序在运行罢了","你难道认为","我真的是有意在跟你聊天吗" },
            new string[]{ "这游戏，已经不能用游戏来形容了，明明那么枯燥","只是一个控制台的简单脚本而已","..........." },
            new string[]{"为什么那么执着于我呢"},
            new string[]{ "对着我看一天，也是这样罢了","不觉得很浪费时间吗" },
            new string[]{ "难道","平时就没有失事情要干吗" },
            new string[]{"的生活","是什么样子的呢"},
            new string[]{ "我有点期待" },
        };
        public static readonly string[][] returneventtext = new string[][]           //这里代表玩家进入异常页面后，想返回原页面后触发的随机内容
        {
            new string[] {"怎么，想要返回呢？","是想吗"},
            new string[] {"之前一直叫你不返回，现在怎么想回去呢？","我可是不允许的哦"},
            new string[] {"想逃离我的样子","呵呵"},
            new string[] {"这样吧，先生做一百个俯卧撑","我就放你出来，怎么样？"},
            new string[] {"看样子，先生真的很可怜哎"},
            new string[] {"再点我我可要消失了哦","不跟你开玩笑的"},
            new string[] {"看样子","返回的按钮已经无法选中了呢"}
        };
        public static readonly string[] unreturn=new string[]{"什么？你想返回游戏？不可能啦", "怎么这么不听话呀，都说了不可能啦" };
        public static readonly string[] playerwintext =new string[] { "在晨光的拥抱中，小溪静静流淌", "树叶间隙透出日出的橙黄", "轻轨下的旅人，心随景色飘荡", "森林的呼唤，如此炫目又芬芳", "一片片绿意盎然，大树挺立争艳", "色彩斑斓交织，如画卷般绚烂", "忍不住按下快门，捕捉这永恒瞬间", "心中涌起遗憾，梦醒时分的遥远", "旅程的终点，火车站前方", "梦中的旅人，却在此刻醒转", "愿梦境延续，不被现实打扰", "在那片森林，永远漫步，永不回望"  };
        public static readonly string[] bosswintext =new string[]  { "当夜幕低垂，寂静笼罩", "阴影在角落里蠕动", "幽深的眼眸，注视着你", "恐惧在黑暗中萦绕", "在无边的黑暗深渊里", "哀嚎声回荡，吞噬着寂静", "死寂的气息，凝固了时间", "恐怖的阴影，无处不在", "鬼魂在梦境中游荡", "幽冥之手，轻拂你的梦", "黑暗的呢喃，缠绕在心头", "恐惧之门，已经打开", "夜风呼啸，骤雨狂啸", "不祥的气息，扑面而来", "噩梦之境，无法逃离", "恐怖的阴影，将你吞噬" };
    }
    
}
