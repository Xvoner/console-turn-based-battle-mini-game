using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME
{
    interface updateenvent  //让若干场景都继承这个接口，因为这几个场景都要用到update选项
    {
        void update();
    }
}
