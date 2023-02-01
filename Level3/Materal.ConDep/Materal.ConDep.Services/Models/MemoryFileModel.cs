using System;
using System.Threading.Tasks;

namespace Materal.ConDep.Services.Models
{
    /// <summary>
    /// 内存文件模型
    /// </summary>
    public class MemoryFileModel : IFileModel
    {
        /// <summary>
        /// 最后操作时间
        /// </summary>
        private DateTime _lastOperatingTime;
        public string FileName { get; set; }
        public string FileAbstract { get; set; }
        private int _size;
        public int Size
        {
            get => _size;
            set
            {
                _size = value;
                FileContent = new byte[_size];
            }
        }
        /// <summary>
        /// 已加载大小
        /// </summary>
        private int _loadSize;
        public bool AutoDestroy => _lastOperatingTime.AddMinutes(1) <= DateTime.Now;
        public bool CanComplete => _loadSize == Size;
        public byte[] FileContent { get; private set; }
        public void LoadBuffer(byte[] buffer, int index)
        {
            _lastOperatingTime = DateTime.Now;
            Parallel.For(0, buffer.Length, i => FileContent[index + i] = buffer[i]);
            _loadSize += buffer.Length;
        }
    }
}
