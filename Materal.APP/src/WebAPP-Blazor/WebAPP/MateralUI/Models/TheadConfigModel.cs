namespace WebAPP.MateralUI.Models
{
    public class TableConfigModel
    {
        public TableConfigModel()
        {
        }
        public TableConfigModel(string title, int? width = null)
        {
            Title = title;
            Width = width;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public int? Width { get; set; }
    }
}
