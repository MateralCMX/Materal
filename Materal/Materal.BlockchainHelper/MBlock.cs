using Materal.ConvertHelper;
using Materal.DateTimeHelper;
using Newtonsoft.Json;

namespace Materal.BlockchainHelper
{
    /// <summary>
    /// 消息区块
    /// </summary>
    public class MBlock
    {
        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 区块位置
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public long TimeStamp { get; set; }
        /// <summary>
        /// 当前区块散列值
        /// </summary>
        public string Hash{ get; set; }
        /// <summary>
        /// 前一个区块的散列值
        /// </summary>
        public string PrevHash{ get; set; }
        /// <summary>
        /// 构造一个创世区块
        /// </summary>
        private MBlock()
        {
        }
        /// <summary>
        /// 构造一个创世区块
        /// </summary>
        /// <param name="data"></param>
        public MBlock(object data)
        {
            Index = 0;
            TimeStamp = DateTimeManager.GetTimeStamp();
            Data = data.ToJson();
            PrevHash = string.Empty;
            Hash = GetHash();
        }
        /// <summary>
        /// 构造一个新的区块
        /// </summary>
        /// <param name="lastBlock"></param>
        /// <param name="data"></param>
        public MBlock(MBlock lastBlock, object data)
        {
            Index = lastBlock.Index + 1;
            TimeStamp = DateTimeManager.GetTimeStamp();
            Data = data.ToJson();
            PrevHash = lastBlock.Hash;
            Hash = GetHash();
        }
        /// <summary>
        /// 从Json生成
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public static MBlock FormJson(string jsonData)
        {
            var block = new MBlock();
            JsonConvert.PopulateObject(jsonData, block);
            if (block.Hash != block.GetHash()) return null;
            return block;
        }
        /// <summary>
        /// 获得散列
        /// </summary>
        /// <returns></returns>
        public virtual string GetHash()
        {
            var calculationString = $"{Index}Materal{TimeStamp}Materal{Data}Materal{PrevHash}";
            string result = calculationString.ToMd5_32Encode();
            return result;
        }
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T GetData<T>()
        {
            return Data.JsonToDeserializeObject<T>();
        }
        /// <summary>
        /// 是否为下一个区块
        /// </summary>
        /// <param name="nextBlock"></param>
        /// <returns></returns>
        public bool IsNextBlock(MBlock nextBlock)
        {
            if (Index != nextBlock.Index - 1) return false;
            if (Hash != nextBlock.PrevHash) return false;
            if (nextBlock.GetHash() != nextBlock.Hash) return false;
            return true;
        }
    }
}
