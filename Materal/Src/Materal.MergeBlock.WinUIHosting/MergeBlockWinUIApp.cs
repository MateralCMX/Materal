using Microsoft.UI.Xaml;

namespace Materal.MergeBlock.WebHosting
{
    /// <summary>
    /// MergeBlockWinUIAPP
    /// </summary>
    /// <param name="window"></param>
    public class MergeBlockWinUIApp(Window window)
    {
        /// <summary>
        /// 主窗体
        /// </summary>
        public Window MainWindow { get; } = window;
    }
}
