using System;
using System.Text;
using System.IO;
using System.Xml;
using System.ComponentModel;
using System.Collections;

namespace BYHXPrinterManager
{
    public enum AREA_Type
    { 
        none,
        map1,
        map2,
        map3,
        Reserve,
    }

    public enum Head_Type
    {
        Head_9 = 9,
        Head_12 = 12,
        Reserve,
    }

    public struct AREA
    {
        public string Name;
        public AREA_Type type;
        public short ADDR;
        public ArrayList Items;
        public HEADER_S AreaHeader;

        public AREA(object obj)
        {
            Name = string.Empty;
            type = AREA_Type.Reserve;
            ADDR = 0;
            Items = new ArrayList();
            AreaHeader = new HEADER_S(null);
        }

       
    }

    public struct HEADER_S
    {
        public Int16 Checksum1;
        public Int16 Checksum2;
        public Int16 Size;
        public byte TableNumber;
        public byte Reserve; //保留

        public HEADER_S(object obj)
        {
            Checksum1 = 0;
            Checksum2 = 0;
            Size = 0;
            TableNumber = 0;
            Reserve = 0;
        }

        public byte[] GetAreaHeader()
        {
            byte[] mapheader = new byte[8];
            int i = 0; 

            byte[] checksum1 = BitConverter.GetBytes(Checksum1);
            for (int j = 0; j < checksum1.Length; j++)
            {
                mapheader[i++] = checksum1[j];
            }

            byte[] checksum2 = BitConverter.GetBytes(this.Checksum2);
            for (int j = 0; j < checksum2.Length; j++)
            {
                mapheader[i++] = checksum2[j];
            }

            byte[] size = BitConverter.GetBytes(this.Size);
            for (int j = 0; j < size.Length; j++)
            {
                mapheader[i++] = size[j];
            }

            mapheader[i++] = this.TableNumber;
            mapheader[i++] = this.Reserve;

            return mapheader;
        }
    }

    public struct CommondItem
    {
        public string Name;
        public int Oder;
        public TypeCode ItemType;
        public short len;
        public string[] Data;
        //public short Offset;
        public short realLen;
        //
        public MyStruct downWave;			


        public CommondItem(object obj)
        {
            Name = string.Empty;
            Oder = 0;
            ItemType = TypeCode.Byte;
            len = 0;
            Data = null;
            realLen = 0;
            //Offset = 0;
        }
    }

    public struct MAP_1_Item
    {
        public CommondItem CommondItem;
        public byte ColorNumber;
        public byte Reserve0;
        public byte Reserve1;
        public byte Reserve2;

        public MAP_1_Item(object obj)
        {
            CommondItem = new CommondItem(null);
            ColorNumber = 0;
            Reserve0 = Reserve1 = Reserve2 = 0;
        }

        public byte[] GetMapHeader(int MAP_1_MARK, short mapheadsSize)
        {
            byte[] mapheader = new byte[10];
            int i = 0;
            byte[] mark = BitConverter.GetBytes((short)MAP_1_MARK);
            for (int j = 0; j < mark.Length; j++)
            {
                mapheader[i++] = mark[j];
            }

            byte[] Offset = BitConverter.GetBytes((short)mapheadsSize);//(this.CommondItem.Offset);
            for (int j = 0; j < Offset.Length; j++)
            {
                mapheader[i++] = Offset[j];
            }

            byte[] realLen = BitConverter.GetBytes(this.CommondItem.realLen);
            for (int j = 0; j < realLen.Length; j++)
            {
                mapheader[i++] = realLen[j];
            }

            mapheader[i++] = this.ColorNumber;

            mapheader[i++] = this.Reserve0;
            mapheader[i++] = this.Reserve1;
            mapheader[i++] = this.Reserve2;

            return mapheader;
        }
    }

    public struct MAP_2_Item
    {
        public CommondItem CommondItem;
        public byte headType;
        public byte inkType;
        public byte Speed;
        public byte Reserve0;

        public MAP_2_Item(object obj)
        {
            CommondItem = new CommondItem(null);
            headType = 0;
            inkType = 0;
            Speed = 0;
            Reserve0 = 0;
        }

        public byte[] GetMapHeader(int MAP_2_MARK,short mapheadsSize)
        {
            byte[] mapheader = new byte[10];
            int i = 0;
            byte[] mark = BitConverter.GetBytes((short)MAP_2_MARK);
            for (int j = 0; j < mark.Length; j++)
            {
                mapheader[i++] = mark[j];
            }

            byte[] Offset = BitConverter.GetBytes((short)mapheadsSize);//(this.CommondItem.Offset);
            for (int j = 0; j < Offset.Length; j++)
            {
                mapheader[i++] = Offset[j];
            }

            byte[] realLen = BitConverter.GetBytes(this.CommondItem.realLen);
            for (int j = 0; j < realLen.Length; j++)
            {
                mapheader[i++] = realLen[j];
            }

            mapheader[i++] = this.headType;
            mapheader[i++] = this.inkType;
            mapheader[i++] = this.Speed;
            mapheader[i++] = this.Reserve0;

            return mapheader;
        }
    }

    public class MyXmlReader
    {
        private const int MAP_1_MARK = 0xDDDD;
        private const int MAP_2_MARK = 0xEEEE;
        private const int MAP_HEADER_SIZE = 10;
        private const char SPLITERCHAR = ',';

        private ArrayList AREAsFromXml = new ArrayList();

        public ArrayList AREAsFromXml1
        {
            get { return AREAsFromXml; }
            set { AREAsFromXml = value; }
        }

        public void ReadXml(string filename)
        {
            if (!File.Exists(filename))
            {
                return;
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            XmlNode root = doc.FirstChild;
            foreach (XmlNode subnode in doc.ChildNodes)
            {
                if (subnode.Name.ToLower() == "headboardeepromdata")
                    root = subnode;
            }

            AREAsFromXml.Clear();
            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.Name.ToLower() == "area")
                {
                    AREA sub = new AREA(null);
                    foreach (XmlAttribute attr in node.Attributes)
                    {
                        if (attr.Name.ToLower() == "name")
                            sub.Name = attr.Value;
                        else if (attr.Name.ToLower() == "type")
                            sub.type = Get_AREA_Type(attr.Value.ToLower());
                        else if (attr.Name.ToLower() == "addr")
                            sub.ADDR = (short)GetValueByStr(attr.Value);
                    }

                    int checkOder = 0;
                    short AddrOffset = sub.ADDR;
                    foreach (XmlNode item in node.ChildNodes)
                    {
                        if (item.Name.ToLower() != "item")
                            continue;
                        switch (sub.type)
                        {
                            case AREA_Type.none:
                                sub.Items.Add(this.Get_CommondItem(item, checkOder));//, ref AddrOffset));
                                break;
                            case AREA_Type.map1:
                                sub.Items.Add(this.Get_MAP_1_Item(item, checkOder));//, ref AddrOffset));
                                break;
                            case AREA_Type.map2:
                                sub.Items.Add(this.Get_MAP_2_Item(item, checkOder));//, ref AddrOffset));
                                break;
                            case AREA_Type.map3:
                                sub.Items.Add(this.Get_MAP_3_Item(item, checkOder));//, ref AddrOffset));
                                break;
                            default:
                                {
                                    System.Diagnostics.Debug.Assert(false);
                                    break;
                                }
                        }
                        checkOder++;
                    }
                    this.CalculatedAreaHeader(ref sub); 

                    this.AREAsFromXml.Add(sub);
                }
            }
        }

        private AREA_Type Get_AREA_Type(string type)
        {
            switch (type)
            { 
                case "":
                    return AREA_Type.none;
                case "map1":
                    return AREA_Type.map1;
                case "map2":
                    return AREA_Type.map2;
                case "map3":
                    return AREA_Type.map3;
                default:
                    return AREA_Type.Reserve;//System.Diagnostics.Debug.Assert(false);
            }
        }

        unsafe private CommondItem Get_CommondItem(XmlNode item, int checkOder)//,ref short AddrOffset)
        {
            CommondItem ret = new CommondItem(null);
            foreach (XmlAttribute attr in item.Attributes)
            {
                switch (attr.Name.ToLower())
                {
                    case "name":
                        {
                            ret.Name = attr.Value;
                        }
                        break;
                    case "order":
                        {
                            ret.downWave.Index = (char)Convert.ToChar(attr.Value);
                        }
                        break;
                    case "len":
                        {
                            ret.downWave.len = Convert.ToInt16(attr.Value);
                        }
                        break;
                    case "inktype":
                        {
                            ret.downWave.Inktype = (INK)Convert.ToByte(attr.Value);
                        }
                        break;
                    case "speed":
                        {
                            ret.downWave.Speed = (SPEED)Convert.ToByte(attr.Value);
                        }
                        break;
                    case "volume":
                        {
                            ret.downWave.Volume = Convert.ToChar(attr.Value);
                        }
                        break;
                    case "headlist":
                        {
                            string[] strData = attr.Value.Split(",");
                            ret.downWave.HeadList = new char[6];
                            for (int i = 0; i < strData.Length; i++)
                            {
                                ret.downWave.HeadList[i] = Convert.ToChar(strData[i]);
                            }
                        }
                        break;
                    case "colororder":
                        {
                            string[] strData = attr.Value.Split(",");
                            ret.downWave.HeadList = new char[16];
                            if (strData.Length <= 16)
                                for (int i = 0; i < strData.Length; i++)
                                {
                                    ret.downWave.HeadList[i] = Convert.ToChar(strData[i]);
                                }
                        }
                        break;
                    case "frequency":
                        {
                            ret.downWave.FireFreq = Convert.ToInt32(attr.Value);
                        }
                        break;
                    case "pulse":
                        {
                            ret.downWave.PulseWidth = Convert.ToInt16(attr.Value);
                        }
                        break;
                    case "datatype":
                        {
                            ret.downWave.DataType = Convert.ToInt16(attr.Value);
                        }
                        break;
                    case "rev":
                        {
                            string[] strData = attr.Value.Split(",");
                            ret.downWave.HeadList = new char[24];
                            if (strData.Length <= 16)
                                for (int i = 0; i < strData.Length; i++)
                                {
                                    ret.downWave.HeadList[i] = Convert.ToChar(strData[i]);
                                }
                        }
                        break;
                    default:
                        break;
                }
                if (attr.Name.ToLower() == "name")
                    ret.Name = attr.Value;
                else if (attr.Name.ToLower() == "order")
                {
                    ret.Oder = GetValueByStr(attr.Value);//int.Parse(attr.Value);

                    if (ret.Oder != checkOder)
                        throw new Exception("Items(" + ret.Name + ")的Oder与实际顺序不匹配!!请确认.");
                }
                else if (attr.Name.ToLower() == "type")
                {
                    Array EnumValues =Enum.GetValues(typeof(TypeCode));
                    for(int i = 0; i < EnumValues.Length;i++)
                    {
                        if(attr.Value.Trim().ToLower() == Enum.GetName(typeof(TypeCode),EnumValues.GetValue(i)).ToLower())
                        {
                            ret.ItemType = (TypeCode)EnumValues.GetValue(i);
                            break;
                        }
                    }
                }
                else if (attr.Name.ToLower() == "len")
                    ret.len = (short)GetValueByStr(attr.Value); //short.Parse(attr.Value);
            }
            ret.Data = item.InnerText.Split(new char[] { SPLITERCHAR });

            if(ret.len != ret.Data.Length)
                throw new Exception("Items(" + ret.Name +")的Len和实际包含的数据长度不匹配,请确认.");

            ret.realLen = (short)(ret.len * GetTypeSize(ret.ItemType));

            return ret;
        }

        private MAP_1_Item Get_MAP_1_Item(XmlNode item, int checkOder)//, ref short AddrOffset)
        {
            MAP_1_Item ret = new MAP_1_Item(null);

            ret.CommondItem = Get_CommondItem(item, checkOder);//, ref AddrOffset);
            foreach (XmlAttribute attr in item.Attributes)
            {
                if (attr.Name.ToLower() == "colornumber")
                    ret.ColorNumber = (byte)GetValueByStr(attr.Value); //byte.Parse(attr.Value);
            }

            return ret;
        }

        private MAP_2_Item Get_MAP_2_Item(XmlNode item, int checkOder)//, ref short AddrOffset)
        {
            MAP_2_Item ret = new MAP_2_Item(null);
            ret.CommondItem = Get_CommondItem(item, checkOder);//, ref AddrOffset);
            foreach (XmlAttribute attr in item.Attributes)
            {
                if (attr.Name.ToLower() == "headtype")
                    ret.headType = (byte)GetValueByStr(attr.Value);//byte.Parse(attr.Value);
                else if (attr.Name.ToLower() == "inktype")
                    ret.inkType = (byte)GetValueByStr(attr.Value);
                else if (attr.Name.ToLower() == "speed")
                    ret.Speed = (byte)GetValueByStr(attr.Value); //byte.Parse(attr.Value);
            }

            return ret;
        }

        unsafe private void CalculatedAreaHeader(ref AREA area)
        {
            if (area.type == AREA_Type.map1)
            {
                area.AreaHeader.TableNumber = (byte)area.Items.Count;

                int totalItemSize = 0;
                foreach (MAP_1_Item item in area.Items)
                {
                    totalItemSize += this.GetTypeSize(item.CommondItem.ItemType) * item.CommondItem.Data.Length + MAP_HEADER_SIZE;
                }
                area.AreaHeader.Size = (short)(sizeof(HEADER_S) + totalItemSize);
            }
            else if (area.type == AREA_Type.map2)
            {
                area.AreaHeader.TableNumber = (byte)area.Items.Count;

                int totalItemSize = 0;
                foreach (MAP_2_Item item in area.Items)
                {
                    totalItemSize += this.GetTypeSize(item.CommondItem.ItemType) * item.CommondItem.Data.Length + MAP_HEADER_SIZE;
                }
                area.AreaHeader.Size = (short)(sizeof(HEADER_S) + totalItemSize);
            }
            else
            {
                area.AreaHeader.TableNumber = 0;
                int totalItemSize = 0;
                foreach (CommondItem item in area.Items)
                {
                    totalItemSize += this.GetTypeSize(item.ItemType) * item.Data.Length;
                }
                area.AreaHeader.Size = (short)(sizeof(HEADER_S) + totalItemSize);
            }

            //area.AreaHeader.Checksum1 += area.AreaHeader.Reserve;
            //area.AreaHeader.Checksum1 += area.AreaHeader.TableNumber;
            //area.AreaHeader.Checksum1 += area.AreaHeader.Size;

            //area.AreaHeader.Checksum2 ^= area.AreaHeader.Reserve;
            //area.AreaHeader.Checksum2 ^= area.AreaHeader.TableNumber;
            //area.AreaHeader.Checksum2 ^= area.AreaHeader.Size;
        }

        unsafe private int GetTypeSize(TypeCode tc)
        {
            switch (tc)
            {
                case TypeCode.Byte:
                    return sizeof(byte);
                case TypeCode.Decimal:
                    return sizeof(decimal);
                case TypeCode.Double:
                    return sizeof(double);
                case TypeCode.Int16:
                    return sizeof(Int16);
                case TypeCode.Int32:
                    return sizeof(Int32);
                case TypeCode.Int64:
                    return sizeof(Int64);
                case TypeCode.SByte:
                    return sizeof(sbyte);
                case TypeCode.Single:
                    return sizeof(Single);
                case TypeCode.UInt16:
                    return sizeof(UInt16);
                case TypeCode.UInt32:
                    return sizeof(UInt32);
                case TypeCode.UInt64:
                    return sizeof(UInt64);
                case TypeCode.Char:
                    return sizeof(Char);
                default :
                    System.Diagnostics.Debug.Assert(false);
                    return -1;
            }
        }

        unsafe private byte[] GetSubDateBuffer(TypeCode tc ,string data)
        {
            try
            {
                switch (tc)
                {
                    case TypeCode.Byte:
                        return new byte[] { byte.Parse(data) };//BitConverter.SByte(byte.Parse(data));
                    case TypeCode.Decimal:
                        return BitConverter.GetBytes((float)decimal.Parse(data));
                    case TypeCode.Double:
                        return BitConverter.GetBytes(double.Parse(data));
                    case TypeCode.Int16:
                        return BitConverter.GetBytes(Int16.Parse(data));
                    case TypeCode.Int32:
                        return BitConverter.GetBytes(Int32.Parse(data));
                    case TypeCode.Int64:
                        return BitConverter.GetBytes(Int64.Parse(data));
                    case TypeCode.SByte:
                        return new byte[] { byte.Parse(data) };//BitConverter.SByte(byte.Parse(data));
                    case TypeCode.Single:
                        return BitConverter.GetBytes(float.Parse(data));
                    case TypeCode.UInt16:
                        return BitConverter.GetBytes(UInt16.Parse(data));
                    case TypeCode.UInt32:
                        return BitConverter.GetBytes(UInt32.Parse(data));
                    case TypeCode.UInt64:
                        return BitConverter.GetBytes(UInt64.Parse(data));
                    case TypeCode.Char:
                        return BitConverter.GetBytes(Char.Parse(data));
                    default:
                        System.Diagnostics.Debug.Assert(false);
                        return null;
                }
            }
            catch
            {
                return null;
            }
        }

        private byte[] GetDatesBuffer(CommondItem item)
        {
            byte[] datas = new byte[item.realLen];
            int i = 0;
            foreach(string data in item.Data)
            {
                byte[] Bdatas = this.GetSubDateBuffer(item.ItemType, data.Trim());
                foreach (byte bdata in Bdatas)
                {
                    datas[i++] = bdata;
                }
            }

            return datas;
        }

        private int GetValueByStr(string data)
        {
            int ret = -1;
            try
            {
                data = data.Replace(" ", string.Empty);
                if (data.Length > 2 && data.Substring(0, 2).ToLower() == "0x")
                {
                    ret = Convert.ToInt32(data, 16);
                }
                else
                {
					try
					{
						ret = Convert.ToInt32(data);
					}
					catch
					{
						ret = Convert.ToInt32(data, 16);
					}
                }

                return ret;
            }
            catch
            {
                return ret;
            }
        }

        unsafe public byte[] GetAREABuffer(AREA area)
        {
            int AllSize = area.AreaHeader.Size;

            byte[] AreaBuffer = new byte[AllSize];
            int i = 0;

            byte[] areaheader1 = area.AreaHeader.GetAreaHeader(); //俩个checknum先按0计算
            for (int j = 0; j < areaheader1.Length; j++)
                AreaBuffer[i++] = areaheader1[j];

            short offset = (short)(sizeof(HEADER_S) + area.Items.Count * MAP_HEADER_SIZE);

            switch (area.type)
            {
                case AREA_Type.none:
                    foreach (CommondItem comm in area.Items)
                    {
                        byte[] commDatas = this.GetDatesBuffer(comm);
                        for (int j = 0; j < commDatas.Length; j++)
                            AreaBuffer[i++] = commDatas[j];
                    }
                    break;
                case AREA_Type.map1:
                    foreach (MAP_1_Item map1 in area.Items)
                    {
                        byte[] maphead = map1.GetMapHeader(MAP_1_MARK, offset);
                        for (int j = 0; j < maphead.Length; j++)
                            AreaBuffer[i++] = maphead[j];
                        offset += map1.CommondItem.realLen;
                    }
                    foreach (MAP_1_Item map1 in area.Items)
                    {
                        byte[] map1Datas = this.GetDatesBuffer(map1.CommondItem);
                        for (int j = 0; j < map1Datas.Length; j++)
                            AreaBuffer[i++] = map1Datas[j];
                    }
                    break;
                case AREA_Type.map2:
                    foreach (MAP_2_Item map2 in area.Items)
                    {
                        byte[] maphead = map2.GetMapHeader(MAP_2_MARK, offset);
                        for (int j = 0; j < maphead.Length; j++)
                            AreaBuffer[i++] = maphead[j];
                        offset += map2.CommondItem.realLen;
                    }
                    foreach (MAP_2_Item map2 in area.Items)
                    {
                        byte[] map2Datas = this.GetDatesBuffer(map2.CommondItem);
                        for (int j = 0; j < map2Datas.Length; j++)
                            AreaBuffer[i++] = map2Datas[j];
                    }
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }

            for (int m = 4; m < AreaBuffer.Length; m++)
            {
                area.AreaHeader.Checksum1 += AreaBuffer[m];
                area.AreaHeader.Checksum2 ^= AreaBuffer[m];
            }

            i = 0;
            byte[] areaheader2 = area.AreaHeader.GetAreaHeader(); //计算checknum后重写areaheader
            for (int j = 0; j < areaheader2.Length; j++)
                AreaBuffer[i++] = areaheader2[j];

            return AreaBuffer;

        }
    }

}
