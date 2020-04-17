// -----------------------------------------------------------------------
// <copyright file="TcpIpCmdEnum.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TcpIpBase.Byhx
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public enum TcpIpCmdByhx : byte
    {
        /// <summary>
        /// 结束包
        /// </summary>
        EndPackage = 0xff,
        /// <summary>
        /// 回复反馈
        /// </summary>
        ResponsePackage = 0xfe,
        /// <summary>
        /// 向服务端获取信息
        /// </summary>
        GetInfo = 0x01,
        /// <summary>
        /// 打印工具栏（操作命令）
        /// </summary>
        OprationCmd = 0x02,
        /// <summary>
        /// 打印参数栏
        /// </summary>
        PrintParameter=0x03,
        /// <summary>
        /// 设置-打印机-打印设置
        /// </summary>
        PrinterSetting = 0x04,
        /// <summary>
        /// 设置-打印机-彩条
        /// </summary>
        ColorBarSetting = 0x05,
        /// <summary>
        /// 设置-打印机-闪喷设置
        /// </summary>
        SpraySetting = 0x06,
        /// <summary>
        /// 设置-打印机-打印方向
        /// </summary>
        PrintDir = 0x07,
        /// <summary>
        /// 设置-个性设置
        /// </summary>
        PreferenceSetting=0x08,
        /// <summary>
        /// 设置-校准-水平
        /// </summary>
        CaribrationHor = 0x09,
        /// <summary>
        /// 设置-校准-步进
        /// </summary>
        CaribrationStep = 0x0a,
        /// <summary>
        /// 系统设置
        /// </summary>
        SystemSetting = 0x0b,
        ///// <summary>
        ///// 设置-校准-垂直
        ///// </summary>
        //CaribrationVer = 0x0b,
        ///// <summary>
        ///// 设置-校准-重叠
        ///// </summary>
        //CaribrationOverLap=0x0c,
        ///// <summary>
        ///// 工厂调试模式
        ///// </summary>
        //FactoryDebug=0x0d,
        //Error =  -1,
        //StatusDirty = 0,
        //ReportSettings,
        //UpdateSettings,
        //SetSettings,
        //SendFile,
        //RipFile,
        //CancelRipFile,
        //StartPrint,
        //StopPrint
    }

    //public enum SystemSettingSubCmd : byte
    //{
    //    Exit = 1,//1：关闭程序	1
    //    FormMinimized = 2,//2：程序最小化到托盘	1
    //}

    //public enum GetInfoSubCmd : byte
    //{
    //    CurPos = 1,//1：当前X、Y轴坐标值	1
    //    Status = 2,//2：设备状态	1
    //    ErrorCode = 3,//错误代码
    //    JobSize = 4,//4：图像尺寸（单位：cm）	格式：(width,height)
    //    JobRes = 5,//5：图像分辨率（单位：DPI）	格式：(width,height)
    //    PrintMode = 6,// 6：打印模式	暂时设值为0
    //    PrintPass = 7,//7：每Pass的扫描次数	数值
    //    ScanMode = 8, //8：扫描模式	单向值为1，双向值为2
    //    JobPath = 9, //9：任务地址	文件路径
    //    CariStepPass =10,//10：校准-步进Pass数	数值  ?????????// 
    //    CariStep = 11,//11：校准-步进	数值              ?????????//
    //    ToolbarStepAdjust = 12,//12：打印栏-步进	数值
    //    ToolbarPass = 13,//13：打印栏-Pass数	数值
    //    ToolbarSpeed = 14,//14：打印栏-喷车速度	高中低速（值为1-3）
    //    PlatformDistanceY = 15,//15：平台距离Y	数值
    //    JobList = 16,//16：当前作业列表
    //    PrintPercent = 17,//17：打印进度百分比

    //    StatusEx = 0x20,//2：(设备状态,错误代码,打印或升级进度)
    //}

    //public enum OprationCmdSubCmd : byte
    //{
    //    AddJob = 1,
    //    DelJob =2,
    //    PrintJob = 3,
    //    PauseResume = 4,
    //    StopPrint = 5,
    //    SetOriginX = 6,
    //    SetOriginY = 7,
    //    Back2HomeX = 8,
    //    Back2HomeY = 9,
    //    //PrintCaribration = 10,
    //    StartMoveTest = 10,
    //    StopMoveTest = 11,
    //    AutoPausePerPage = 12,//层间暂停
    //    LayingSand =13,// 铺沙
    //    StartMove = 14, // 参数带方向
    //    StopMove = 15,

    //    PrintJobEx = 0x40, //打印作业,带平台选择等参数
    //    OpenSettingForm = 0x41, //打开设置窗口
    //    OpenVolTempForm = 0x42, //打开实时设置窗口
    //    OpenCaribrationWizard = 0x43, //打开校准向导窗口
    //    OpenAboutForm = 0x44, //打开关于窗口
    //    Exit = 0x45, // 退出程序
    //    OpenUpdateForm = 0x46, //打开关于窗口
    //    StopPrintEx = 0x47, // 取消某个台板的打印
    //    OpenPasswordForm=0x48,//打开密码窗口
    //    OpenFactoryDebugForm =0x49,//打开工厂调试窗口
    //    AutoClean= 0x50, //自动清洗
    //    Spray = 0x51, // 闪喷
    //}

    //public enum PrintParameterSubCmd : byte
    //{
    //    AutoCenterPrint = 1,
    //    OriginX,
    //    OriginY,
    //    StepAdjust=4,
    //    PrintPass,
    //    PrintSpeed = 6,
    //    PrintDir = 7,
    //    PlatformDistanceY =8,
    //}

    //public enum PrinterSettingSubCmd : byte
    //{
    //    AutoJumpWhite =1,
    //    JumpWhiteTime = 2,//跳白时间（单位：s）
    //    JobSpace = 3,// 作业间距（单位：cm）
    //    FeatherType = 4,//4：羽化类型	渐变、均匀、波浪、高级、UV（值为1-5）
    //    FeatherPeacent = 5,//5：羽化值	关、弱、中、强、自定义（值为1-4、自定义）
    //    MultiInk = 6,//6：墨量	默认、两倍、多倍（值为1-3）
    //}

    //public enum ColorBarSettingSubCmd : byte
    //{
    //    Space = 1, //距离（单位：cm）
    //    Width = 2,//宽度（单位：cm）
    //    Position = 3,//3：位置	两边、左、右、无(值为1-4)
    //    Normally = 4,//4：普通	值为0(假)，值为1(真)
    //    ColorOverlap = 5,//5：彩色重叠	值为0(假)，值为1(真)
    //    SameHeightWithImage = 6,//6：和图像同高	值为0(假)，值为1(真)
    //}

    //public enum SpraySettingSubCmd : byte
    //{
    //    AutoSpray = 1,//1：自动闪喷（单位：pass）	数值
    //    SrayPeriod = 2,//2：闪喷周期（单位：ms）	数值
    //    SprayTimeBeforPrint = 3,//3：打印前闪喷持续时间（单位：ms）
    //    IdleSpray = 4,//4：空闲闪喷	值为0(假)，值为1(真)
    //    SprayBeforPrint = 5,//5：打印前闪喷	值为0(假)，值为1(真)
    //}

    //public enum PrintDirSubCmd : byte
    //{
    //    MirrorPrint = 1,//镜像打印
    //}

    //public enum PreferenceSettingSubCmd : byte
    //{
    //    HotForlder = 1,//1：使用热文件位置	值为0则不可用，合法路径则可用。
    //    DelJobAfterPrint = 2,//2：打印完成后删除作业	值为0(假)，值为1(真)
    //    DelFileAfterPrint =3,//3：打印完成后删除文件	值为0(假)，值为1(真)
    //    LeftRightSwap = 4,//4：左右运动反向	值为0(假)，值为1(真)
    //    DisplayUnit = 5,//5：单位	英寸、英尺、毫米、厘米、米（值为1-5）
    //    DisplayLang = 6,//显示语言
    //}

    //public enum CaribrationHorSubCmd : byte
    //{
    //    SpeedDpiIndex =1,//1：速度、DPI设置	 (高600、中600、低600、高400、中400、低400、高300、中300、低300、) （值为1-9）
    //    CopyTo,
    //    CariMode = 3,//3：校准模式	快速、颜色、全校准(值为1-3)
    //    BiDri = 4,//4：双向	数值
    //    LineWidth = 5,//线宽
    //    LeftCari = 6,//6：左头	（k,c,m,y）或（k,c,m,y ,k,c,m,y）
    //    RightCari = 7,//7：右头	（k,c,m,y）或（k,c,m,y ,k,c,m,y）
    //}

    //public enum CaribrationStepSubCmd : byte
    //{
    //    PassNum =1,
    //    AdjustValue,
    //    Step,
    //    BaseStep,
    //}

    //public enum CaribrationVerSubCmd : byte
    //{
    //    AdjustValue =1,
    //}

    //public enum CaribrationOverLapSubCmd : byte
    //{
    //    AdjustValue = 1,
    //}

    //public enum FactoryDebugSubCmd : byte
    //{
    //    //AdjustValue = 1,
    //}
}
