namespace Materal.Extensions
{
    /// <summary>
    /// Byte扩展
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// 转换为二进制字符串
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string GetBinaryString(this byte[] buffer)
        {
            IEnumerable<string> binaryStrings = buffer.Select(m => Convert.ToString(m, 2).PadLeft(8, '0'));
            string result = string.Join("", binaryStrings);
            return result;
        }
        /// <summary>
        /// 获得指定位数的二进制字符串
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startBitIndex"></param>
        /// <param name="endBitIndex"></param>
        /// <returns></returns>
        public static string GetBinaryStringByBitIndex(this byte[] buffer, int startBitIndex, int endBitIndex)
        {
            string result = buffer.GetBinaryString();
            result = result[startBitIndex..(endBitIndex + 1)];
            return result;
        }
        /// <summary>
        /// 获得指定位数的值
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startBitIndex"></param>
        /// <param name="endBitIndex"></param>
        /// <returns></returns>
        public static byte[] GetValueByBitIndex(this byte[] buffer, int startBitIndex, int endBitIndex)
        {
            int intValue = buffer.GetIntValueByBitIndex(startBitIndex, endBitIndex);
            byte[] result = BitConverter.GetBytes(intValue);
            return result;
        }
        /// <summary>
        /// 获得指定位数的值
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startBitIndex"></param>
        /// <param name="endBitIndex"></param>
        /// <returns></returns>
        public static int GetIntValueByBitIndex(this byte[] buffer, int startBitIndex, int endBitIndex)
        {
            string binaryString = buffer.GetBinaryStringByBitIndex(startBitIndex, endBitIndex);
            int result = Convert.ToInt32(binaryString, 2);
            return result;
        }
    }
}
