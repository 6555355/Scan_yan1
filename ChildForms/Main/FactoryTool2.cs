using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using PrinterStubC.Utility;

namespace BYHXPrinterManager.Main
{
    public partial class FactoryTool2 : Form
    {
        private const byte Cmd = 0xF0;
        /// <summary>
        /// 查询IO状态返回解析时用
        /// </summary>
        //bit0~bit4 j32_5~j32_9
        Dictionary<int,string> armDictionary=new Dictionary<int, string>()
        {
            {0,"j32_5"},
            {1,"j32_6"},
            {2,"j32_7"},
            {3,"j32_8"},
            {4,"j32_9"},
        };
        /// <summary>
        /// 查询IO状态返回解析时用
        /// </summary>
        //bit0~bit13 J23 J24 J16 J17 J15 J22 J20 HB j21 j18 j38 j31 j34 j35
        Dictionary<int, string> dspDictionary = new Dictionary<int, string>()
        {
            {0,"J23"},
            {1,"J24"},
            {2,"J16"},
            {3,"J17"},
            {4,"J15"},
            {5,"J22"},
            {6,"J20"},
            {7,"HB"},
            {8,"J21"},
            {9,"J18"},
            {10,"J38"},
            {11,"J31"},
            {12,"J34"},
            {13,"J35"},
        };

        private bool _autoGetAxisIoState;
        private bool _autoGetAxisIoCoor;
        private int _currentMoveAxis = 0;
        private ExecuteType _executeType = ExecuteType.Null;
        public FactoryTool2()
        {
            InitializeComponent();
        }

        private void AutoGetAxisIoStateRefreshView(string json)
        {
            try
            {
                if (json.Contains("GetAxisIo"))
                {
                    bool isSuccess;
                    GetAxisIoRoot root = null;
                    try
                    {
                        root = JsonConvert.DeserializeObject<GetAxisIoRoot>(json);
                        isSuccess = true;
                    }
                    catch
                    {
                        isSuccess = false;
                    }
                    if (isSuccess)
                    {
                        for (int i = 0; i < 32; i++)
                        {
                            dataGridView_AxisIoState.Rows[i].Cells[0].Value = (root.GetAxisIo.Arm >> i & 1) == 1
                                ? armDictionary.ContainsKey(i) ? armDictionary[i] : "bit" + i
                                : "";
                            dataGridView_AxisIoState.Rows[i].Cells[1].Value = (root.GetAxisIo.Dsp >> i & 1) == 1
                                ? dspDictionary.ContainsKey(i) ? dspDictionary[i] : "bit" + i
                                : "";
                            dataGridView_AxisIoState.Rows[i].Cells[2].Value =
                                (root.GetAxisIo.ZBoard >> i & 1) == 1 ? "bit" + i : "";
                        }
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        private void AutoGetAxisIoCoorRefreshView(string json)
        {
            try
            {
                if (json.Contains("GetAxisCoor"))
                {
                    bool isSuccess;
                    GetAxisCoorRoot root = null;
                    try
                    {
                        root = JsonConvert.DeserializeObject<GetAxisCoorRoot>(json);
                        isSuccess = true;
                    }
                    catch
                    {
                        isSuccess = false;
                    }
                    if (isSuccess)
                    {
                        for (int i = 0; i < root.GetAxisCoor.Count; i++)
                        {
                            dataGridView_AxisIoCoor.Rows[i].Cells[0].Value = "AXIS" + (i + 1);
                            dataGridView_AxisIoCoor.Rows[i].Cells[1].Value = root.GetAxisCoor[i];
                        }
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        public void CallBack(byte[] buffer)
        {
            try
            {
                string json = Encoding.ASCII.GetString(buffer);
                LogWriter.WriteLog(json,true);
                if (json.Contains("\0"))
                {
                    json = json.Substring(0, json.IndexOf("\0", StringComparison.Ordinal));
                }
                if (_autoGetAxisIoState)
                {
                    AutoGetAxisIoStateRefreshView(json);
                }
                if (_autoGetAxisIoCoor)
                {
                    AutoGetAxisIoCoorRefreshView(json);
                }
                switch (_executeType)
                {
                    case ExecuteType.Null:
                        break;
                    case ExecuteType.Move:
                        {
                            //MessageBox.Show(json);
                            break;
                        }
                    case ExecuteType.GetAxisIoState:
                        {
                            GetAxisIoRoot root = JsonConvert.DeserializeObject<GetAxisIoRoot>(json);
                            for (int i = 0; i < 32; i++)
                            {
                                dataGridView_AxisIoState.Rows[i].Cells[0].Value = (root.GetAxisIo.Arm >> i & 1) == 1
                                    ? armDictionary.ContainsKey(i) ? armDictionary[i] : "bit" + i
                                    : "";
                                dataGridView_AxisIoState.Rows[i].Cells[1].Value = (root.GetAxisIo.Dsp >> i & 1) == 1
                                    ? dspDictionary.ContainsKey(i) ? dspDictionary[i] : "bit" + i
                                    : "";
                                dataGridView_AxisIoState.Rows[i].Cells[2].Value =
                                    (root.GetAxisIo.ZBoard >> i & 1) == 1 ? "bit" + i : "";
                            }
                            break;
                        }
                    case ExecuteType.GetAxisCoordinate:
                        {
                            GetAxisCoorRoot root = JsonConvert.DeserializeObject<GetAxisCoorRoot>(json);
                            for (int i = 0; i < root.GetAxisCoor.Count; i++)
                            {
                                dataGridView_AxisIoCoor.Rows[i].Cells[0].Value = "AXIS" + (i + 1);
                                dataGridView_AxisIoCoor.Rows[i].Cells[1].Value = root.GetAxisCoor[i];
                            }
                            break;
                        }
                    case ExecuteType.GetAxisMtParam:
                        {
                            GetAxisMtParamRoot root = JsonConvert.DeserializeObject<GetAxisMtParamRoot>(json);
                            for (int i = 0; i < root.GetAxisMtParam.Count; i++)
                            {
                                GetAxisMtParam param = root.GetAxisMtParam[i];
                                if (param.device == "Dsp")
                                {
                                    dataGridView_AxisMtParam.Rows.Add(
                                        "AXIS" + (i + 1),
                                        param.device,
                                        GetDspIoEnumString(param.limitOriSen),
                                        GetDspIoEnumString(param.limitTerSen),
                                        GetDspIoEnumString(param.initOriSen),
                                        GetDspIoEnumString(param.initTerSen),
                                        GetDspIoEnumString(param.jerkSen));
                                }
                                else
                                {
                                    dataGridView_AxisMtParam.Rows.Add(
                                        "AXIS" + (i + 1),
                                        param.device,
                                        param.limitOriSen,
                                        param.limitTerSen,
                                        param.initOriSen,
                                        param.initTerSen,
                                        param.jerkSen);
                                }
                            }
                            break;
                        }
                }
            }
            catch
            {
                // ignored
            }
            finally
            {
                EnableControl(true);
                _executeType = ExecuteType.Null;
            }
        }

        private string GetDspIoEnumString(int index)
        {
            if (index < (int)EDspIOIndex.Jack_Max && index > 0)
            {
                return Enum.GetName(typeof(EDspIOIndex), index);
            }
            return index.ToString();
        }

        private void EnableControl(bool enable)
        {
            try
            {
                foreach (Control control in Controls)
                {
                    control.Enabled = enable;
                }
                menuStrip1.Enabled = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void button_MoveLenAxis_Run_Click(object sender, EventArgs e)
        {
            try
            {
                _currentMoveAxis = (int) numericUpDown_MoveLenAxis_Axis.Value;
                _executeType = ExecuteType.Move;
                EnableControl(false);
                MoveLenAxis moveLenAxis = new MoveLenAxis()
                {
                    Axis = (int) numericUpDown_MoveLenAxis_Axis.Value,
                    Len = (int) numericUpDown_MoveLenAxis_Len.Value,
                    Speed = (int) numericUpDown_MoveLenAxis_Speed.Value,
                };
                MoveLenAxisRoot root = new MoveLenAxisRoot()
                {
                    MoveLenAxis = moveLenAxis,
                };
                string json = JsonConvert.SerializeObject(root);
                byte[] buffer = Encoding.ASCII.GetBytes(json);
                uint bufferSize = (uint) buffer.Length;
                int ret = CoreInterface.SetEpsonEP0Cmd(Cmd, buffer, ref bufferSize, (ushort)bufferSize, 0);
                if (ret == 0)
                {
                    MessageBox.Show(ResString.GetResString("SendCmdFailed"));
                    EnableControl(true);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                EnableControl(true);
            }
        }

        private void button_MoveToAxis_Run_Click(object sender, EventArgs e)
        {
            try
            {
                _currentMoveAxis = (int)numericUpDown_MoveToAxis_Axis.Value;
                _executeType = ExecuteType.Move;
                EnableControl(false);
                MoveToAxis moveToAxis = new MoveToAxis()
                {
                    Axis = (int) numericUpDown_MoveToAxis_Axis.Value,
                    To = (int) numericUpDown_MoveToAxis_To.Value,
                    Speed = (int) numericUpDown_MoveToAxis_Speed.Value,
                };
                MoveToAxisRoot root = new MoveToAxisRoot()
                {
                    MoveToAxis = moveToAxis,
                };
                string json = JsonConvert.SerializeObject(root);
                byte[] buffer = Encoding.ASCII.GetBytes(json);
                uint bufferSize = (uint) buffer.Length;
                int ret = CoreInterface.SetEpsonEP0Cmd(Cmd, buffer, ref bufferSize, (ushort)bufferSize, 0);
                if (ret == 0)
                {
                    MessageBox.Show(ResString.GetResString("SendCmdFailed"));
                    EnableControl(true);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                EnableControl(true);
            }
        }

        private void button_IOOutput_Run_Click(object sender, EventArgs e)
        {
            try
            {
                _currentMoveAxis = (int)numericUpDown_IOOutput_Axis.Value;
                _executeType = ExecuteType.Move;
                EnableControl(false);
                MoveLenAxis moveLenAxis = new MoveLenAxis()
                {
                    Axis = (int) numericUpDown_IOOutput_Axis.Value,
                    Len = (int) numericUpDown_IOOutput_Len.Value,
                    Speed = (int) numericUpDown_IOOutput_Speed.Value,
                };
                IoOutput root = new IoOutput()
                {
                    Io = moveLenAxis,
                };
                string json = JsonConvert.SerializeObject(root);
                byte[] buffer = Encoding.ASCII.GetBytes(json);
                uint bufferSize = (uint) buffer.Length;
                int ret = CoreInterface.SetEpsonEP0Cmd(Cmd, buffer, ref bufferSize, (ushort)bufferSize, 0);
                if (ret == 0)
                {
                    MessageBox.Show(ResString.GetResString("SendCmdFailed"));
                    EnableControl(true);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                EnableControl(true);
            }
        }

        private void button_GetAxisIoState_Click(object sender, EventArgs e)
        {
            try
            {
                _executeType = ExecuteType.GetAxisIoState;
                EnableControl(false);
                string json = "{\"GetAxis\":\"Io\"}";
                byte[] buffer = Encoding.ASCII.GetBytes(json);
                uint bufferSize = (uint)buffer.Length;
                int ret = CoreInterface.SetEpsonEP0Cmd(Cmd, buffer, ref bufferSize, (ushort)bufferSize, 0);
                if (ret == 0)
                {
                    MessageBox.Show(ResString.GetResString("SendCmdFailed"));
                    EnableControl(true);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                EnableControl(true);
            }
            //GetAxisIoRoot root = new GetAxisIoRoot()
            //{
            //    GetAxisIo = new GetAxisIo()
            //    {
            //        Arm = 11,
            //        Dsp = 11,
            //        ZBoard = 11,
            //    }
            //};
            //json = JsonConvert.SerializeObject(root);
            //CallBack(Encoding.ASCII.GetBytes(json));
        }

        private void button_GetAxisIoCoor_Click(object sender, EventArgs e)
        {
            try
            {
                _executeType = ExecuteType.GetAxisCoordinate;
                EnableControl(false);
                string json = "{\"GetAxis\":\"Coor\"}";
                byte[] buffer = Encoding.ASCII.GetBytes(json);
                uint bufferSize = (uint)buffer.Length;
                int ret = CoreInterface.SetEpsonEP0Cmd(Cmd, buffer, ref bufferSize, (ushort)bufferSize, 0);
                if (ret == 0)
                {
                    MessageBox.Show(ResString.GetResString("SendCmdFailed"));
                    EnableControl(true);
                }
                //GetAxisCoorRoot root = new GetAxisCoorRoot()
                //{
                //    GetAxisCoor = new List<int>()
                //    {
                //        1000,200,300,400,500,600
                //    }
                //};
                //json = JsonConvert.SerializeObject(root);
                //CallBack(Encoding.ASCII.GetBytes(json));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                EnableControl(true);
            }
        }

        private void button_GetAxisMtParam_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView_AxisMtParam.Rows.Clear();
                _executeType = ExecuteType.GetAxisMtParam;
                EnableControl(false);
                string json = "{\"GetAxis\":\"MtParam\"}";
                byte[] buffer = Encoding.ASCII.GetBytes(json);
                uint bufferSize = (uint)buffer.Length;
                int ret = CoreInterface.SetEpsonEP0Cmd(Cmd, buffer, ref bufferSize, (ushort)bufferSize, 0);
                if (ret == 0)
                {
                    MessageBox.Show(ResString.GetResString("SendCmdFailed"));
                    EnableControl(true);
                }
                //GetAxisMtParamRoot root = new GetAxisMtParamRoot()
                //{
                //    GetAxisMtParam = new List<GetAxisMtParam>()
                //    {
                //        new GetAxisMtParam()
                //        {
                //            device = "Dsp",
                //            initOriSen = 1,
                //            initTerSen = 0,
                //            jerkSen = 0,
                //            limitOriSen = 0,
                //            limitTerSen = 0,
                //        },
                //        new GetAxisMtParam()
                //        {
                //            device = "Dsp",
                //            initOriSen = 0,
                //            initTerSen = 0,
                //            jerkSen = 0,
                //            limitOriSen = 0,
                //            limitTerSen = 0,
                //        }
                //    }
                //};
                //json = JsonConvert.SerializeObject(root);
                //CallBack(Encoding.ASCII.GetBytes(json));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                EnableControl(true);
            }
        }

        private void toolStripMenuItem_Stop_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentMoveAxis > 0)
                {
                    string json = "{\"StopAxis\":{\"Axis\":" + _currentMoveAxis + "}}";
                    byte[] buffer = Encoding.ASCII.GetBytes(json);
                    uint bufferSize = (uint)buffer.Length;
                    int ret = CoreInterface.SetEpsonEP0Cmd(Cmd, buffer, ref bufferSize, (ushort)bufferSize, 0);
                    if (ret == 0)
                    {
                        MessageBox.Show(ResString.GetResString("SendCmdFailed"));
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void checkBox_AutoGetAxisIoState_CheckedChanged(object sender, EventArgs e)
        {
            _executeType = ExecuteType.Null;
            button_GetAxisIoState.Enabled = !checkBox_AutoGetAxisIoState.Checked;
            _autoGetAxisIoState = checkBox_AutoGetAxisIoState.Checked;
            if (checkBox_AutoGetAxisIoState.Checked)
            {
                Thread thread = new Thread(AutoGetAxisIoStateThread)
                {
                    IsBackground = true,
                };
                thread.Start();
            }
        }

        private void AutoGetAxisIoStateThread()
        {
            try
            {
                while (_autoGetAxisIoState)
                {
                    string json = "{\"GetAxis\":\"Io\"}";
                    byte[] buffer = Encoding.ASCII.GetBytes(json);
                    uint bufferSize = (uint)buffer.Length;
                    int ret = CoreInterface.SetEpsonEP0Cmd(Cmd, buffer, ref bufferSize, (ushort)bufferSize, 0);
                    if (ret == 0)
                    {
                        MessageBox.Show(ResString.GetResString("SendCmdFailed"));
                        break;
                    }
                    Thread.Sleep(1000);
                }
                Invoke(new Action(() => checkBox_AutoGetAxisIoState.Checked = false));
            }
            catch
            {
                // ignored
            }
        }

        private void checkBox_AutoGetAxisIoCoor_CheckedChanged(object sender, EventArgs e)
        {
            _executeType = ExecuteType.Null;
            button_GetAxisIoCoor.Enabled = !checkBox_AutoGetAxisIoCoor.Checked;
            _autoGetAxisIoCoor = checkBox_AutoGetAxisIoCoor.Checked;
            if (checkBox_AutoGetAxisIoCoor.Checked)
            {
                Thread thread = new Thread(AutoGetAxisIoCoorThread)
                {
                    IsBackground = true,
                };
                thread.Start();
            }
        }

        private void AutoGetAxisIoCoorThread()
        {
            try
            {
                while (_autoGetAxisIoCoor)
                {
                    string json = "{\"GetAxis\":\"Coor\"}";
                    byte[] buffer = Encoding.ASCII.GetBytes(json);
                    uint bufferSize = (uint)buffer.Length;
                    int ret = CoreInterface.SetEpsonEP0Cmd(Cmd, buffer, ref bufferSize, (ushort)bufferSize, 0);
                    if (ret == 0)
                    {
                        break;
                    }
                    Thread.Sleep(1000);
                }
                Invoke(new Action(() => checkBox_AutoGetAxisIoCoor.Checked = false));
            }
            catch
            {
                // ignored
            }
        }

        private void FactoryTool2_FormClosing(object sender, FormClosingEventArgs e)
        {
            _autoGetAxisIoState = false;
            _autoGetAxisIoCoor = false;
        }

        private void FactoryTool2_Load(object sender, EventArgs e)
        {
            dataGridView_AxisIoState.Rows.Clear();
            for (int i = 0; i < 32; i++)
            {
                dataGridView_AxisIoState.Rows.Add();
            } 
            dataGridView_AxisIoCoor.Rows.Clear();
            for (int i = 0; i < 32; i++)
            {
                dataGridView_AxisIoCoor.Rows.Add();
            }
        }

    }


    /// <summary>
    /// 执行类型，为了区分板卡返回数据的解析方式
    /// </summary>
    public enum ExecuteType
    {
        Null,
        Move,
        GetAxisIoState,
        GetAxisCoordinate,
        GetAxisMtParam,
    }

    /// <summary>
    /// Dsp对应的IO枚举
    /// </summary>
    public enum EDspIOIndex
    {
        J23 = 1,	//!<	默认X轴原点
        J24 = 2,	//!<	默认Y轴原点
        J16 = 3,	//!<	默认Z轴原点
        J17 = 4,	//!<	默认X轴终点
        J15 = 5,	//!<	默认Y轴终点
        J22 = 6,	//!<	默认Z轴终点
        J20 = 7,	//!<	默认急停
        HB = 8,		//!<	头板插座
        J21 = 9,
        J18 = 10,
        J38 = 11,
        J31 = 12,
        J34 = 13,
        J35 = 14,
        Jack_Max
    };

    /// <summary>
    /// 查询轴对应的传感器
    /// </summary>
    public class GetAxisMtParamRoot
    {
        public List<GetAxisMtParam> GetAxisMtParam { get; set; }
    }

    public class GetAxisMtParam
    {
        public string device { get; set; }
        public int limitOriSen { get; set; }
        public int limitTerSen { get; set; }
        public int initOriSen { get; set; }
        public int initTerSen { get; set; }
        public int jerkSen { get; set; }
        //device:Dsp,//Dsp,Arm,ZBoard
        //limitOriSen:1,//原点限位编号
        //limitTerSen:1,//终点限位编号
        //initOriSen:1,//初始化原点传感器编号
        //initTerSen:1,//初始化终点传感器编号
        //jerkSen:1//急停传感器编号
    }

    /// <summary>
    /// 查询坐标
    /// </summary>
    public class GetAxisCoorRoot
    {
        public List<int> GetAxisCoor { get; set; }
    }

    /// <summary>
    /// 查询IO状态
    /// </summary>
    public class GetAxisIoRoot
    {
        public GetAxisIo GetAxisIo { get; set; }
    }

    public class GetAxisIo
    {
        public uint Arm { get; set; }    //bit0~bit4 j32_5~j32_9
        public uint Dsp { get; set; }    //bit0~bit13 J23 J24 J16 J17 J15 J22 J20 HB j21 j18 j38 j31 j34 j35
        public uint ZBoard { get; set; } //目前没有固定座号
    }

    /// <summary>
    /// 控制IO输出
    /// </summary>
    public class IoOutput
    {
        public MoveLenAxis Io { get; set; }
    }

    /// <summary>
    /// 移动轴指令
    /// 定长移动
    /// </summary>
    public class MoveToAxisRoot
    {
        public MoveToAxis MoveToAxis { get; set; }
    }

    public class MoveToAxis
    {
        public int Axis { get; set; }  //轴编号（1~X）
        public int To { get; set; }    //移动到此坐标（脉冲单位）
        public int Speed { get; set; } //脉冲/s
    }

    /// <summary>
    /// 移动轴指令
    /// 定点移动
    /// </summary>
    public class MoveLenAxisRoot
    {
        public MoveLenAxis MoveLenAxis { get; set; }
    }

    public class MoveLenAxis
    {
        public int Axis { get; set; }  //轴编号（1~X）
        public int Len { get; set; }   //移动距离（脉冲单位）正负控制方向
        public int Speed { get; set; } //脉冲/s
    }
}
