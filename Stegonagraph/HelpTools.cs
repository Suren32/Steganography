using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stegonagraph
{
    //класс вспомогательных функций
    static public class HelpTools
    {
        //автоматическое дополнение байта нулями (8,16,32 ...)
        static public String AutoAddByte(String bfStr, int count)
        {
            while (bfStr.Length < count)
                bfStr = "0" + bfStr;

            return bfStr;
        }
    }
}
