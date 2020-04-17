using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.JobListView
{
    public class ListViewHelper
    {
		public static Color m_CustomRowBackColor;
        /// <summary>
        /// ���캯��
        /// </summary>
        public ListViewHelper()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }


        public static void ListView_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            System.Windows.Forms.ListView lv = sender as System.Windows.Forms.ListView;
            // ����������ǲ������ڵ�������.
            if (e.Column == (lv.ListViewItemSorter as ListViewColumnSorter).SortColumn)
            {
                // �������ô��е����򷽷�.
                if ((lv.ListViewItemSorter as ListViewColumnSorter).Order == System.Windows.Forms.SortOrder.Ascending)
                {
                    (lv.ListViewItemSorter as ListViewColumnSorter).Order = System.Windows.Forms.SortOrder.Descending;
                }
                else
                {
                    (lv.ListViewItemSorter as ListViewColumnSorter).Order = System.Windows.Forms.SortOrder.Ascending;
                }
            }
            else
            {
                // ���������У�Ĭ��Ϊ��������
                (lv.ListViewItemSorter as ListViewColumnSorter).SortColumn = e.Column;
                (lv.ListViewItemSorter as ListViewColumnSorter).Order = System.Windows.Forms.SortOrder.Ascending;
            }
            // ���µ����򷽷���ListView����
            ((System.Windows.Forms.ListView)sender).Sort();
			SetItemBackColor((System.Windows.Forms.ListView)sender);
        }


		public static void SetItemBackColor(System.Windows.Forms.ListView lv)
		{
			foreach(ListViewItem jobItem in lv.Items)
				jobItem.BackColor = jobItem.Index % 2==0?lv.BackColor:m_CustomRowBackColor;

			lv.Invalidate();
		}
    }

    /// <summary>
    /// �̳���IComparer
    /// </summary>
    public class ListViewColumnSorter : System.Collections.IComparer
    {
        /// <summary>
        /// ָ�������ĸ�������
        /// </summary>
        private int ColumnToSort;
        /// <summary>
        /// ָ������ķ�ʽ
        /// </summary>
        private System.Windows.Forms.SortOrder OrderOfSort;
        /// <summary>
        /// ����CaseInsensitiveComparer�����
        /// </summary>
        private System.Collections.CaseInsensitiveComparer ObjectCompare;

        /// <summary>
        /// ���캯��
        /// </summary>
        public ListViewColumnSorter()
        {
            // Ĭ�ϰ���һ������
            ColumnToSort = -1;

            // ����ʽΪ������
            OrderOfSort = System.Windows.Forms.SortOrder.Ascending;

            // ��ʼ��CaseInsensitiveComparer�����
            ObjectCompare = new System.Collections.CaseInsensitiveComparer();
        }

        /// <summary>
        /// ��дIComparer�ӿ�.
        /// </summary>
        /// <param name="x">Ҫ�Ƚϵĵ�һ������</param>
        /// <param name="y">Ҫ�Ƚϵĵڶ�������</param>
        /// <returns>�ȽϵĽ��.�����ȷ���0�����x����y����1�����xС��y����-1</returns>
        public int Compare(object x, object y)
        {
            if (ColumnToSort < 0) return 0;

            int compareResult;
            System.Windows.Forms.ListViewItem listviewX, listviewY;

            // ���Ƚ϶���ת��ΪListViewItem����
            listviewX = (System.Windows.Forms.ListViewItem)x;
            listviewY = (System.Windows.Forms.ListViewItem)y;

            string xText = listviewX.SubItems[ColumnToSort].Text;
            string yText = listviewY.SubItems[ColumnToSort].Text;

            int xInt, yInt;
            // ����ǰ��ļ�������,ȥ����׺����Ƚ�
            if (ColumnToSort == 0)
            {
                xText = Path.GetFileNameWithoutExtension(xText);
                yText = Path.GetFileNameWithoutExtension(yText);
            }
            // �Ƚ�,���ֵΪIP��ַ�������IP��ַ�Ĺ�������
            if (IsIP(xText) && IsIP(yText))
            {
                compareResult = CompareIp(xText, yText);
            }
            else if (int.TryParse(xText, out xInt) && int.TryParse(yText, out yInt)) //�Ƿ�ȫΪ����
            {
                try
                {
                    xInt = int.Parse(xText);
                    yInt = int.Parse(yText);
                    //�Ƚ�����
                    compareResult = CompareInt(xInt, yInt);
                }
                catch
				{
					//�Ƚ϶���
					compareResult = ObjectCompare.Compare(xText, yText);
				}
			}
            else
            {
                //�Ƚ϶���
                compareResult = ObjectCompare.Compare(xText, yText);
            }
            // ��������ıȽϽ��������ȷ�ıȽϽ��
            if (OrderOfSort == System.Windows.Forms.SortOrder.Ascending)
            {
                // ��Ϊ��������������ֱ�ӷ��ؽ��
                return compareResult;
            }
            else if (OrderOfSort == System.Windows.Forms.SortOrder.Descending)
            {
                // ����Ƿ�����������Ҫȡ��ֵ�ٷ���
                return (-compareResult);
            }
            else
            {
                // �����ȷ���0
                return 0;
            }
        }

        /// <summary>
        /// �ж��Ƿ�Ϊ��ȷ��IP��ַ��IP��Χ��0.0.0.0��255.255.255��
        /// </summary>
        /// <param name="ip">����֤��IP��ַ</param>
        /// <returns></returns>
        public bool IsIP(String ip)
        {
            return System.Text.RegularExpressions.Regex.Match(ip, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$").Success;
        }

        /// <summary>
        /// �Ƚ��������ֵĴ�С
        /// </summary>
        /// <param name="ipx">Ҫ�Ƚϵĵ�һ������</param>
        /// <param name="ipy">Ҫ�Ƚϵĵڶ�������</param>
        /// <returns>�ȽϵĽ��.�����ȷ���0�����x����y����1�����xС��y����-1</returns>
        private int CompareInt(int x, int y)
        {
            if (x > y)
            {
                return 1;
            }
            else if (x < y)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// �Ƚ�����IP��ַ�Ĵ�С
        /// </summary>
        /// <param name="ipx">Ҫ�Ƚϵĵ�һ������</param>
        /// <param name="ipy">Ҫ�Ƚϵĵڶ�������</param>
        /// <returns>�ȽϵĽ��.�����ȷ���0�����x����y����1�����xС��y����-1</returns>
        private int CompareIp(string ipx, string ipy)
        {
            string[] ipxs = ipx.Split('.');
            string[] ipys = ipy.Split('.');

            for (int i = 0; i < 4; i++)
            {
                if (Convert.ToInt32(ipxs[i]) > Convert.ToInt32(ipys[i]))
                {
                    return 1;
                }
                else if (Convert.ToInt32(ipxs[i]) < Convert.ToInt32(ipys[i]))
                {
                    return -1;
                }
                else
                {
                    continue;
                }
            }
            return 0;
        }

        /// <summary>
        /// ��ȡ�����ð�����һ������.
        /// </summary>
        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }

        /// <summary>
        /// ��ȡ����������ʽ.
        /// </summary>
        public System.Windows.Forms.SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }
    }

}
