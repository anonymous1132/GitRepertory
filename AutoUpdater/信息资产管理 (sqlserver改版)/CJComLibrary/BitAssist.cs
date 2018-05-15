using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJComLibrary
{
    /// <summary>
    /// 位 操作工具类(也可以使用MS中System.Collections.BitArray操作)
    /// author:NatureSex
    /// </summary>
    public sealed class BitAssist
    {
        private BitAssist() { }

        #region 获取

        /// <summary>
        /// 取byteSource目标位的值
        /// </summary>
        /// <param name="byteSource">源字节</param>
        /// <param name="location">位置(0-7)</param>
        /// <returns>目标值</returns>
        public static int GetTargetBit(short location, byte byteSource)
        {
            Byte baseNum = (byte)(Math.Pow(2, location + 1) / 2);
            return GetTargetBit(location, byteSource, baseNum);
        }

        /// <summary>
        /// 取byteSource目标位的值
        /// </summary>
        /// <param name="location"></param>
        /// <param name="byteSource"></param>
        /// <param name="baseNum">与 基数(1,2,4,8,16,32,64,128)</param>
        /// <returns></returns>
        private static int GetTargetBit(short location, byte byteSource, byte baseNum)
        {
            if (location > 7 || location < 0) return -1000;
            return (byteSource & baseNum) == baseNum ? 1 : 0;
        }

        /// <summary>
        /// 取一批byteSources目标位的对应的值集合
        /// </summary>
        /// <param name="location">位置(0-7)</param>
        /// <param name="byteSources">一批字节</param>
        /// <returns>一一对应的目标值</returns>
        public static int[] GetTargetBit(short location, params byte[] byteSources)
        {
            if (byteSources == null) return null;
            int bsLen = byteSources.Length;
            Byte baseNum = (byte)(Math.Pow(2, location + 1) / 2);
            int[] result = new int[bsLen];
            for (int i = 0; i < bsLen; i++)
            {
                result[i] = GetTargetBit(location, byteSources[i], baseNum);
            }
            return result;
        }

        #endregion

        #region 替换

        /// <summary>
        /// 替换byteSource目标位的值
        /// </summary>
        /// <param name="location">替换位置(0-7)</param>
        /// <param name="value">替换的值(1-true,0-false)</param>
        /// <param name="byteSource">源字节</param>
        /// <returns>替换后的值</returns>
        public static byte ReplaceTargetBit(short location, bool value, byte byteSource)
        {
            Byte baseNum = (byte)(Math.Pow(2, location + 1) / 2);
            return ReplaceTargetBit(location, value, byteSource, baseNum);
        }

        /// <summary>
        /// 替换byteSource目标位的值
        /// </summary>
        /// <param name="location"></param>
        /// <param name="value">替换的值(1-true,0-false)</param>
        /// <param name="byteSource"></param>
        /// <param name="baseNum">与 基数(1,2,4,8,16,32,64,128)</param>
        /// <returns></returns>
        private static byte ReplaceTargetBit(short location, bool value, byte byteSource, byte baseNum)
        {
            if (location > 7 || location < 0)
            {
                throw new FormatException("location params error!type range(0-7)");
            }
            bool locationValue = GetTargetBit(location, byteSource) == 1 ? true : false;
            if (locationValue != value)
            {
                return (byte)(value ? byteSource + baseNum : byteSource - baseNum);
            }
            return byteSource;
        }

        /// <summary>
        /// 替换一批byteSources目标位的对应的值集合
        /// </summary>
        /// <param name="location">替换位置(0-7)</param>
        /// <param name="value">替换的值(1-true,0-false)</param>        
        /// <param name="byteSources">一批字节</param>
        /// <returns>替换后一一对应的值</returns>
        public static byte[] ReplaceTargetBit(short location, bool value, params byte[] byteSources)
        {
            if (byteSources == null) return null;
            int bsLen = byteSources.Length;
            Byte baseNum = (byte)(Math.Pow(2, location + 1) / 2);
            byte[] result = new byte[bsLen];
            for (int i = 0; i < bsLen; i++)
            {
                result[i] = ReplaceTargetBit(location, value, byteSources[i], baseNum);
            }
            return result;
        }

        #endregion

        #region 互换
        /// <summary>
        /// 替换byteSource目标位的值
        /// </summary>
        /// <param name="firstlocation">替换位置(0-7)</param>
        /// <param name="lastlocation">替换位置(0-7)</param>
        /// <param name="byteSource">源字节</param>
        /// <returns>互换后的值</returns>
        public static byte ExchangeTargetBit(short firstlocation, short lastlocation, byte byteSource)
        {
            if (firstlocation > 7 || firstlocation < 0 || lastlocation > 7 || lastlocation < 0)
            {
                throw new FormatException("location params error!type range(0-7)");
            }
            if (firstlocation == lastlocation)
            { return byteSource; }
            int f = GetTargetBit(firstlocation, byteSource);
            int l = GetTargetBit(lastlocation, byteSource);
            byteSource = ReplaceTargetBit(firstlocation, Convert.ToBoolean(l), byteSource);
            byteSource = ReplaceTargetBit(lastlocation, Convert.ToBoolean(f), byteSource);
            return byteSource;
        }

        /// <summary>
        /// 替换byteSource目标位对应的值的集合
        /// </summary>
        /// <param name="firstlocation">替换位置(0-7)</param>
        /// <param name="lastlocation">替换位置(0-7)</param>
        /// <param name="byteSource">源字节</param>
        /// <returns>互换后的值</returns>
        public static byte[] ExchangeTargetBit(short firstlocation, short lastlocation, byte[] byteSources)
        {
            if (firstlocation > 7 || firstlocation < 0 || lastlocation > 7 || lastlocation < 0)
            {
                throw new FormatException("location params error!type range(0-7)");
            }
            if (firstlocation == lastlocation)
            { return byteSources; }
            if (byteSources == null) return null;
            int bsLen = byteSources.Length;
            byte[] result = new byte[bsLen];
            for (int i = 0; i < bsLen; i++)
            {
                result[i] = ExchangeTargetBit(firstlocation, lastlocation, byteSources[i]);
            }
            return result;
        }



        #endregion
    }
}
