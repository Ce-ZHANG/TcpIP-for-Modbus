using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using HslCommunication.ModBus;
using HslCommunication;
using System.Net;
using System.Net.Sockets;
using System.Linq;

namespace Modbus_TCP
{
    class modbus
    {
        private SerialPort sp = new SerialPort();
        public string modbusStatus;

        #region StringToByteArray
        public static byte[] StringToByteArray(string s)
        {
            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(s);
            return buffer;
        }
        #endregion

        #region
        public static string ByteArrayToString(byte[] bytes)
        { 
            string returnStr = System.Text.Encoding.UTF8.GetString(bytes);
            return returnStr;
        }
        #endregion

        #region Change HexString to ByteArray
        public static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        #endregion

        #region  byteToHexStr
        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }
        #endregion

        #region  StringToHexString
        public string StringToHexString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符，以%隔开
            {
                result += "%" + Convert.ToString(b[i], 16);
            }
            return result;
        }
        #endregion

        #region  HexStringToString
        public static string HexStringToString(string HexString)
        {
            String stringValue = "";
            for (int i = 0; i < HexString.Length / 2; i++)
            {
                string hexChar = HexString.Substring(i * 2, 2);
                int hexValue = Convert.ToInt32(hexChar, 16);
                stringValue += Char.ConvertFromUtf32(hexValue);
            }
            return stringValue;
        }
        #endregion

        #region  ToBooleanArray
        public static bool[] ToBooleanArray(int integer)
        {
            var charArray = Convert.ToString(integer, 2).ToCharArray();
            var booleanArray = new bool[charArray.Length];
            for (int i = 0; i < charArray.Length; i++)
            {
                booleanArray[i] = charArray[i] == '1';
            }
            return booleanArray;
        }
        #endregion

        #region  BytesToInt32
        /// <summary>
        ///     将byte[]转换成int
        /// </summary>
        /// <param name="data">需要转换成整数的byte数组</param>
        public static int BytesToInt32(byte[] data)
        {
            //如果传入的字节数组长度小于4,则返回0
            if (data.Length < 4) return 0;

            //定义要返回的整数
            var num = 0;
            //如果传入的字节数组长度大于4,需要进行处理
            if (data.Length < 4) return num;
            //创建一个临时缓冲区
            var tempBuffer = new byte[4];
            //将传入的字节数组的前4个字节复制到临时缓冲区
            Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);
            //将临时缓冲区的值转换成整数，并赋给num
            num = BitConverter.ToInt32(tempBuffer, 0);
            //返回整数
            return num;
        }
        #endregion

        #region  HexString2BinString
        public static string HexString2BinString(string hexString)
        {
            string result = string.Empty;
            foreach (char c in hexString)
            {
                int v = Convert.ToInt32(c.ToString(), 16);
                int v2 = int.Parse(Convert.ToString(v, 2));
                // 去掉格式串中的空格，即可去掉每个4位二进制数之间的空格，
                result += string.Format("{0:d4}", v2);
            }
            return result;
        }
        #endregion







    }
}
