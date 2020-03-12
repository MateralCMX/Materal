using System;
using System.Collections.Generic;
using System.Text;

namespace Materal.DotNetty.Server.Core.Models
{
    public interface IUploadFileModel
    {
        /// <summary>
        /// 缓冲区
        /// </summary>
        byte[] Buffer { get; }
        /// <summary>
        /// 名字
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 大小
        /// </summary>
        int Size { get; }
        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="path"></param>
        void SaveAs(string path);
    }
}
