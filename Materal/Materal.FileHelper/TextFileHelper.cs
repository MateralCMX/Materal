using System.IO;
using System.Threading.Tasks;

namespace Materal.FileHelper
{
    public class TextFileHelper
    {
        /// <summary>
        /// 写入文本
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task WriteText(string fileName, string message)
        {
            using (var streamWriter = new StreamWriter(fileName, true))
            {
                await streamWriter.WriteLineAsync(message);
                await streamWriter.FlushAsync();
            }
        }
    }
}
