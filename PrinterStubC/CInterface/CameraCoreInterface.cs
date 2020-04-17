using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using BYHXPrinterManager;

namespace PrinterStubC.CInterface
{
    public class CameraCoreInterface
    {
        public const string CameraDllNam = "Camera.dll";

/// <summary>
/// 启动相机,左右相机的ip地址,dll从配置文件读取.
/// 传递俩个picturebox句柄
/// </summary>
/// <param name="leftPicturebox">显示左相机图像的控件句柄</param>
        /// <param name="rightPicturebox">显示右相机图像的控件句柄</param>
/// <param name="width">图像宽度</param>
/// <param name="height">图像高度</param>
/// <param name="isCariMode">是否是标定模式</param>
/// <returns></returns>
        [DllImport(CameraDllNam)]
        public static extern int CameraStart(IntPtr leftPicturebox, IntPtr rightPicturebox, int width, int height,int isCariMode=0);


        /// <summary>
        /// 关闭相机.
        /// </summary>
        [DllImport(CameraDllNam)]
        public static extern int CameraClose();
        /// <summary>
        /// 设置相机参数
        /// </summary>
        /// <param name="cameraName">相机名称</param>
        /// <param name="type">功能id</param>
        /// <param name="param">参数设置值</param>
                [DllImport(CameraDllNam)]
        public static extern void SetCameraParam(string cameraName, GX_FEATURE_ID type, double param);
        /// <summary>
        /// 获取相机参数
        /// </summary>
                /// <param name="cameraName">相机名称</param>
                /// <param name="type">功能id</param>
                /// <param name="param">参数设置值</param>
                [DllImport(CameraDllNam)]
                public static extern void GetCameraParam(string cameraName, GX_FEATURE_ID type, double param);
        ///// <summary>
        ///// 导出指定相机的配置文件到指定文件.
        ///// </summary>
        ///// <param name="filepath">配置文件保存路径</param>
        ///// <param name="cameraName">相机名称</param>
        //[DllImport(CameraDllNam)]
        //public static extern void ExportConfigs(string filepath, string cameraName);

        ///// <summary>
        ///// 导入指定相机的配置文件到指定文件.
        ///// </summary>
        ///// <param name="filepath">配置文件保存路径</param>
        ///// <param name="cameraName">相机名称</param>
        //[DllImport(CameraDllNam)]
        //public static extern void ImportConfigs(string filepath, string cameraName);


        /// <summary>
        /// 图像识别算法参数修改.
        /// </summary>
        /// <param name="param">参数结构体</param>
        [DllImport(CameraDllNam)]
        public static extern void SetImageProcessParam(ImageProcessParam param);

        /// <summary>
        /// 枚举相机.
        /// </summary>
        /// <param name="infos"></param>
        [DllImport(CameraDllNam)]
        public static extern int EnumCamera(byte[] infos);

        public static bool GetCameraStatus()
        {
            return true;
        }
        /// <summary>
        /// 设置标定参数
        /// </summary>
        /// <param name="name">相机名称</param>
        /// <param name="thred_high">阀值最大值</param>
        /// <param name="thred_low">阀值最小值</param>
        /// <param name="doutCnt">单个轮廓包括的最少点数</param>
        /// <param name="maxError">单个轮廓内所有点到圆心距离允许的最大平均误差</param>
        [DllImport(CameraDllNam)]
        public static extern void SetCameraCaribrationParam(string name, int thred_high, int thred_low, int doutCnt, double maxError);
        /// <summary>
        /// 设置当前标定步骤
        /// </summary>
        /// <param name="step">索引值0:初始模式(调整光照);1:</param>
        [DllImport(CameraDllNam)]
        public static extern void SetCameraCaribrationStep(int step);
        /// <summary>
        /// 步进结束后,通知相机拍照
        /// </summary>
        /// <param name="stepDistance">步进距离[英寸]</param>
        /// <param name="pointDistance">圆心Y间距[英寸]</param>
        /// <param name="delayTime">识别延迟次数[拍照周期]</param>
        /// <param name="bJobStart">是否为第一次</param>
        [DllImport(CameraDllNam)]
        public static extern void OnStepFinished(double stepDistance, double pointDistance, int delayTime,byte bJobStart);
    }

    [Serializable]
    public struct ImageProcessParam
    {
        public float distance;
        public float pen_width1;
        public float radius1;
        public float pen_width2;
        public float radius2;
    }

    [Serializable]
    public struct CameraInfos
    {
        public int Num;//相机数量
        [MarshalAs(UnmanagedType.ByValArray,SizeConst = 16)]
        public byte[] IpAdresses; //没四个字节表示一个ip地址
    }
}
