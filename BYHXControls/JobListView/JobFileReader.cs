//#define CASHEN_RIP_HEADER //彩神用的rip格式
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PrinterStubC.Utility;

namespace BYHXPrinterManager.JobListView
{
    /// <summary>
    /// 将单个文件读取和双四色俩个文件合并读取,封装FileStream的相应方法。
    /// </summary>
    public class JobFileReader
    {
        private UIJob _workjob;
        private long _curPos;
        private bool _isDouble4CJob;
        private FileStream fileStream1;
        private FileStream fileStream2;
#if CASHEN_RIP_HEADER
        public const int HEADERSIZE = 280;
        public const bool IsCsPrt = true;
#else
        public const int HEADERSIZE = 84;
        public const bool IsCsPrt = false;
#endif

        public JobFileReader(UIJob job,bool isAwbMode)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            _workjob = job;
            _curPos = 0;
            _isDouble4CJob = job.sJobSetting.bIsDouble4CJob && isAwbMode;
            if (File.Exists(job.FileLocation))
            {
                fileStream1 = new FileStream(job.FileLocation, FileMode.Open, FileAccess.Read, FileShare.Read);
                this.ReadInfoFromHeader();
                fileStream1.Seek(0, SeekOrigin.Begin);
            }
            if (_isDouble4CJob && File.Exists(job.FileLocation2))
            {
                fileStream2 = new FileStream(job.FileLocation2, FileMode.Open, FileAccess.Read, FileShare.Read);
                fileStream2.Seek(HEADERSIZE, SeekOrigin.Begin);
            }
        }

        public long Seek(long offset, SeekOrigin origin)
        {
            try
            {
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        {
                            _curPos = offset;
                            break;
                        }
                    case SeekOrigin.Current:
                        {
                            _curPos += offset;
                            break;
                        }
                    case SeekOrigin.End:
                        {
                            _curPos -= offset;
                            break;
                        }
                }
                if (_isDouble4CJob)
                {
                    long maxstep1 = 0;
                    long maxstep2 = 0;
                    long maxStep = GetMaxStep(ref maxstep1, ref maxstep2);
                    if (_curPos > HEADERSIZE)
                    {
                        bool bReadFile1 = ((_curPos - HEADERSIZE) % maxStep) < maxstep1;
                        int stepNum = (int) ((_curPos - HEADERSIZE)/maxStep);
                        if (bReadFile1)
                        {
                            FileStream1.Seek((long)stepNum * (long)maxstep1 + (long)(_curPos - HEADERSIZE) % stepNum + HEADERSIZE, SeekOrigin.Begin);
                            FileStream2.Seek((long)stepNum * (long)maxstep2 + HEADERSIZE, SeekOrigin.Begin);
                        }
                        else
                        {
                            FileStream1.Seek((long)(stepNum + 1) * (long)maxstep1 + HEADERSIZE, SeekOrigin.Begin);
                            FileStream2.Seek((long)stepNum * (long)maxstep2 + ((long)(_curPos - HEADERSIZE) % stepNum - maxstep1) + HEADERSIZE, SeekOrigin.Begin);
                        }
                    }
                    else
                    {
                        FileStream1.Seek(_curPos, SeekOrigin.Begin);
                        FileStream2.Seek(HEADERSIZE, SeekOrigin.Begin);
                    }
                }
                else
                {
                    FileStream1.Seek(_curPos, origin);
                }
                return _curPos;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return _curPos;
            }
        }

        public int Read(byte[] array, int offset, int count)
        {
            int ret = 0;
            if (_isDouble4CJob)
            {
                long maxstep1 = 0;
                long maxstep2 = 0;
                long maxStep = GetMaxStep(ref maxstep1, ref maxstep2);
                int readedCount = 0;
                int suboffset = 0;
                int allcount = count;
                while (allcount > readedCount)
                {
                    if (_curPos == 0 && count >= HEADERSIZE)
                    {
                        ReadHeader(array);
                        _curPos += HEADERSIZE;
                        count -= HEADERSIZE;
                        readedCount += HEADERSIZE;
                        suboffset += HEADERSIZE;
                        FileStream1.Seek(HEADERSIZE, SeekOrigin.Begin);
                    }
                    int len = 0;
                    bool bReadFile1 = ((_curPos - HEADERSIZE)%maxStep) < maxstep1;
                    if (bReadFile1)
                    {
                        int readlen = (int) Math.Min(maxstep1 - (_curPos - HEADERSIZE)%maxStep, allcount - readedCount);
                        len = FileStream1.Read(array, suboffset, readlen);
                        LogWriter.SaveOptionLog(string.Format("FileStream1.Read pos={0},readlen={1},retlen={2}", FileStream1.Position, readlen, len));
                        if (len != readlen && FileStream1.Position != FileStream1.Length)
                        {
                            MessageBox.Show("Read file error!!");
                        }
                    }
                    else
                    {
                        int readlen =
                            (int)
                                Math.Min(maxstep2 - ((_curPos - HEADERSIZE)%maxStep - maxstep1), allcount - readedCount);
                        len = FileStream2.Read(array, suboffset, readlen);
                        LogWriter.SaveOptionLog(string.Format("FileStream2.Read pos={0},readlen={1},retlen={2}", FileStream2.Position, readlen, len));
                        if (len != readlen && FileStream1.Position != FileStream1.Length)
                        {
                            MessageBox.Show("Read file error!!");
                        }
                    }
                    if (len > 0)
                    {
                        readedCount += len;
                        suboffset += len;
                        _curPos += len;
                    }
                    if (fileStream1.Position == fileStream1.Length
                        || fileStream2.Position == fileStream2.Length)
                    {
                        break;
                    }
                    //bReadFile1 = !bReadFile1;
                }
                ret = readedCount;
            }
            else
            {
                ret = FileStream1.Read(array, offset, count);
                LogWriter.SaveOptionLog(string.Format("FileStream1.Read pos={0},readlen={1},retlen={2}", FileStream1.Position, count, ret));
                if (ret != count && FileStream1.Position != FileStream1.Length)
                {
                    MessageBox.Show("Read file error!!");
                }
            }
            return ret;
        }
        /// <summary>
        /// 获取每次读取最大值[每次读一行]
        /// </summary>
        /// <param name="maxStep1">prt文件1的每次读取最大值</param>
        /// <param name="maxStep2">prt文件2的每次读取最大值</param>
        /// <returns>合并后文件的每次读取最大值</returns>
        private long GetMaxStep(ref long maxStep1, ref long maxStep2)
        {
            maxStep1 = header1.nBytePerLine*header1.nImageColorNum;
            if (_isDouble4CJob)
            {
                maxStep2 = header2.nBytePerLine*header2.nImageColorNum;
            }
            return (long) BytePerLine*ColorNum;
        }

        public void Close()
        {
            if (_isDouble4CJob)
            {
                FileStream1.Close();
                FileStream2.Close();
            }
            else
            {
                FileStream1.Close();
            }
        }

        public int ReadInfoFromHeader()
        {
            byte[] buf = new byte[HEADERSIZE];
            return ReadHeader(buf);
        }


        LiyuRipHEADER header1;
        LiyuRipHEADER header2;
        public int ReadHeader(byte[] buffer)
        {
            SrcWidth = SrcHeight = 0;
            LiyuRipHEADER headerTemp;
            CoreInterface.GetLiyuRipHEADER(_workjob.FileLocation, out headerTemp, IsCsPrt);
            CoreInterface.GetLiyuRipHEADER(_workjob.FileLocation, out header1, IsCsPrt);
            SrcWidth = header1.nImageWidth;
            SrcHeight = header1.nImageHeight;
            if (_isDouble4CJob)
            {
                CoreInterface.GetLiyuRipHEADER(_workjob.FileLocation2, out header2, IsCsPrt);
                ColorNum = header1.nImageColorNum + header2.nImageColorNum;
                headerTemp.nAChannel = (byte)header1.nImageColorNum;
                headerTemp.nBChannel = (byte)header2.nImageColorNum;
                headerTemp.nImageColorNum = ColorNum;
            }
            else
            {
                ColorNum = header1.nImageColorNum;
            }
            BytePerLine = header1.nBytePerLine;
            byte[] buftemp = SerializationUnit.StructToBytes(headerTemp);
            if (IsCsPrt)
            {
                int readSize = Marshal.SizeOf(typeof(CAISHEN_HEADER));
                FileStream stream = new FileStream(_workjob.FileLocation, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);
                buftemp = reader.ReadBytes(readSize);
                if (_isDouble4CJob)
                {
                    CAISHEN_HEADER csHeader = (CAISHEN_HEADER)SerializationUnit.BytesToStruct(buftemp, typeof(CAISHEN_HEADER));
                    csHeader.uColors = ColorNum;
                    buftemp = SerializationUnit.StructToBytes(csHeader);
                }
            }
            Buffer.BlockCopy(buftemp, 0, buffer, 0, buftemp.Length);
            return buftemp.Length;
        }

        public int BytePerLine { get; set; }

        public int ColorNum { get; set; }

        public int SrcHeight { get; set; }

        public int SrcWidth { get; set; }

        public FileStream FileStream1
        {
            get { return fileStream1; }
        }

        public FileStream FileStream2
        {
            get { return fileStream2; }
        }

        public double Position
        {
            get { return _curPos; }
        }

        //private int ReadInt(byte[] buffer, int offset)
        //{
        //    int ret = buffer[offset] + (buffer[offset + 1] << 8) + (buffer[offset + 2] << 16) + (buffer[offset + 3] << 24);
        //    return ret;
        //}
        //private void WriteInt(byte[] buffer, int offset, int value)
        //{
        //    buffer[offset] = (byte)(value & 0xff);
        //    buffer[offset + 1] = (byte)((value >> 8) & 0xff);
        //    buffer[offset + 2] = (byte)((value >> 16) & 0xff);
        //    buffer[offset + 3] = (byte)((value >> 24) & 0xff);
        //}
    }
}
