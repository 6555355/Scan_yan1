using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultimediaLayout.Utilities
{
    public class CoreConst
    {
        /// <summary>
        /// WPF 窗口以及其中的所有元素都使用与设备无关的单位进行度量。一个与设备无关的单位被定义为1/96 英寸。
        /// </summary>
        public const double WPFUnit = 1d / 96d;

        /// <summary>
        /// 第一个Paper Canvas的宽度，其他Paper Canvas都相对此计算
        /// </summary>
        public const double BasePaperWidth = 500;

    }
}
