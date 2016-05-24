using System;
using System.Configuration;

namespace CW_Service
{
    public class Util
    {
        public const string strTimeFormat = "HH:mm:ss";
        public const string strTimeFormat_mini = "HH:mm:ss:fff";

        public static byte[] ObjectToByteArray(object _Object)
        {
            try
            {
                // create new memory stream
                System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream();

                // create new BinaryFormatter
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _BinaryFormatter
                            = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                // Serializes an object, or graph of connected objects, to the given stream.
                _BinaryFormatter.Serialize(_MemoryStream, _Object);

                // convert stream to byte array and return
                return _MemoryStream.ToArray();
            }
            catch
            {

            }

            // Error occured, return null
            return null;
        }

        public static object ByteArrayToObject(byte[] _ByteArray)
        {
            try
            {
                // convert byte array to memory stream
                System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream(_ByteArray);

                // create new BinaryFormatter
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _BinaryFormatter
                            = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                // set memory stream position to starting point
                _MemoryStream.Position = 0;

                // Deserializes a stream into an object graph and return as a object.
                return _BinaryFormatter.Deserialize(_MemoryStream);

            }
            catch
            {

            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTime"></param>
        /// <returns></returns>
        public static DateTime ConvertStringTime2Date(string strTime)
        {
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                return DateTime.ParseExact(CorrectStringTime(strTime), strTimeFormat, provider);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Chuan hoa chuoi ky tu Time truyen vao dam bao phai dung dinh dang HH:mm:ss
        /// </summary>
        /// <param name="strTime"></param>
        /// <returns></returns>
        private static string CorrectStringTime(string strTime)
        {
            try
            {
                string _return = "00:00:00";
                string[] _arr = strTime.Split(':');

                if (_arr.Length >= 3)
                    _return = _arr[0].PadLeft(2, '0') + ":" + _arr[1].PadLeft(2, '0') + ":" + _arr[2].PadLeft(2, '0');
                return _return;
            }
            catch
            {
                return "00:00:00";
            }
        }

        public static bool CompareObject<T>(T Object1, T object2)
        {
            //Get the type of the object
            Type type = typeof(T);

            //return false if any of the object is false
            if (Object1 == null || object2 == null)
                return false;

            //Loop through each properties inside class and get values for the property from both the objects and compare
            foreach (System.Reflection.PropertyInfo property in type.GetProperties())
            {
                if (property.Name != "ExtensionData")
                {
                    string Object1Value = string.Empty;
                    string Object2Value = string.Empty;
                    if (type.GetProperty(property.Name).GetValue(Object1, null) != null)
                        Object1Value = type.GetProperty(property.Name).GetValue(Object1, null).ToString();
                    if (type.GetProperty(property.Name).GetValue(object2, null) != null)
                        Object2Value = type.GetProperty(property.Name).GetValue(object2, null).ToString();
                    if (Object1Value.Trim() != Object2Value.Trim())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool CompareObject_LogFix<T>(T Object1, T object2)
        {
            //Get the type of the object
            Type type = typeof(T);

            //return false if any of the object is false
            if (Object1 == null || object2 == null)
                return false;

            //Loop through each properties inside class and get values for the property from both the objects and compare
            foreach (System.Reflection.PropertyInfo property in type.GetProperties())
            {
                if (property.Name != "ExtensionData")
                {
                    string Object1Value = string.Empty;
                    string Object2Value = string.Empty;
                    if (type.GetProperty(property.Name).GetValue(Object1, null) != null)
                        Object1Value = type.GetProperty(property.Name).GetValue(Object1, null).ToString();
                    if (type.GetProperty(property.Name).GetValue(object2, null) != null)
                        Object2Value = type.GetProperty(property.Name).GetValue(object2, null).ToString();
                    if (Object1Value.Trim() != Object2Value.Trim())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// So sánh 2 thời gian T1 và T2, =  1 là T1 > T2, = 2 là T2 > T1, = 0 là T1 = T2
        /// </summary>
        public static int Compare_Timspan(string p_time_1, string p_time_2)
        {
            try
            {
                string[] _times_1 = p_time_1.Split(':');
                string[] _times_2 = p_time_2.Split(':');

                string t1 = "2015" + _times_1[0] + _times_1[1] + _times_1[2];
                string t2 = "2015" + _times_2[0] + _times_2[1] + _times_2[2];

                int _t_1 = Convert.ToInt32(t1);
                int _t_2 = Convert.ToInt32(t2);

                if (_t_1 > _t_2)
                    return 1;
                else if (_t_2 > _t_1)
                    return 2;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return -1;
            }
        }

        public static int Minus_Timspan(string p_time_1, string p_time_2)
        {
            try
            {
                string[] _times_1 = p_time_1.Split(':');
                string[] _times_2 = p_time_2.Split(':');

                string t1 = "2015" + _times_1[0] + _times_1[1] + _times_1[2];
                string t2 = "2015" + _times_2[0] + _times_2[1] + _times_2[2];

                return Convert.ToInt32(t1) - Convert.ToInt32(t2);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return -1;
            }
        }

    }
}
