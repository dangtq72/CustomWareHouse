/* 
* Project: Monitor_TAM
* Author : Tong Quang Dang – Navisoft.
* Summary: cung cap cac ham chuyen doi giua cac kieu du lieu
* Modification Logs:
* DATE             AUTHOR      DESCRIPTION
* --------------------------------------------------------
* Apr 10, 2015  	Dangtq     Created
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace NaviCommon
{
    public class ConvertData
    {

        #region Định dạng datetime

        public const string strDate = "dd/MM/yyyy";
        public const string strDate_Time = "dd/MM/yyyy HH:mm";

        public static string ConvertDate2String(DateTime p_date)
        {
            try
            {
                return p_date.ToString(strDate);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return "";
            }
        }

        public static DateTime ConvertString2Date(string str)
        {
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                return DateTime.ParseExact(str, strDate, provider);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Convert string to datetime format dd/MM/yyyy HH:mm
        /// </summary>
        /// <param name="str">ví dụ 17/01/2015 09:10</param>
        /// <returns></returns>
        public static DateTime ConvertString2DateWithTime(string str)
        {
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                return DateTime.ParseExact(str, strDate_Time, provider);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        #endregion

        #region Dinh dang kieu Money
        public static String ConvertString2Money(string str)
        {

            try
            {
                decimal _st = Convert.ToDecimal(str);
                //"{0:0.##}"
                //"{0:0,0 VND}"
                return String.Format("{0:0,0.##}", _st);

            }
            catch
            {
                return "";
            }
        }

        public static Decimal ConvertStringMoney2Decimal(string soTien)
        {
            try
            {
                Decimal tid = Convert.ToDecimal(soTien);
                return tid;
            }
            catch
            {
                return -1;
            }

        }
        #endregion

        #region Convert DataTable

        public static void ConvertArrayListToDataTable(ArrayList arrayList, ref DataTable p_dt)
        {
            //DataTable dt = new DataTable();

            if (arrayList.Count != 0)
            {
                ConvertObjectToDataTableSchema(arrayList[0], ref p_dt);
                FillData(arrayList, p_dt);
            }

            //return p_dt;
        }

        public static void ConvertObjectToDataTableSchema(Object o, ref DataTable dt)
        {
            //DataTable dt = new DataTable();
            PropertyInfo[] properties = o.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                DataColumn dc = new DataColumn(property.Name);
                dc.DataType = property.PropertyType; dt.Columns.Add(dc);
            }
            //return dt;
        }

        private static void FillData(ArrayList arrayList, DataTable dt)
        {
            foreach (Object o in arrayList)
            {
                DataRow dr = dt.NewRow();
                PropertyInfo[] properties = o.GetType().GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    dr[property.Name] = property.GetValue(o, null);
                }
                dt.Rows.Add(dr);
            }
        }

        public static DataTable ConvertToDatatable<T>(IList<T> data)
        {
            try
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                DataTable table = new DataTable();
                for (int i = 0; i < props.Count; i++)
                {
                    PropertyDescriptor prop = props[i];
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }

                int SoThuTu = 0;
                table.Columns.Add("STT");
                object[] values = new object[props.Count + 1];
                foreach (T item in data)
                {
                    SoThuTu++;
                    for (int i = 0; i < values.Length - 1; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }
                    values[values.Length - 1] = SoThuTu.ToString();
                    table.Rows.Add(values);
                }
                return table;
            }
            catch (Exception)
            {
                return new DataTable();
            }

        }

        public static DataSet ConvertToDataSet<T>(IList<T> data)
        {
            try
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                DataTable table = new DataTable();
                for (int i = 0; i < props.Count; i++)
                {
                    PropertyDescriptor prop = props[i];
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }

                int SoThuTu = 0;
                table.Columns.Add("STT");
                object[] values = new object[props.Count + 1];
                foreach (T item in data)
                {
                    SoThuTu++;
                    for (int i = 0; i < values.Length - 1; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }
                    values[values.Length - 1] = SoThuTu.ToString();
                    table.Rows.Add(values);
                }

                DataSet _ds = new DataSet();
                _ds.Tables.Add(table);
                return _ds;
            }
            catch (Exception)
            {
                return new DataSet();
            }

        }

        #endregion
    }
}
