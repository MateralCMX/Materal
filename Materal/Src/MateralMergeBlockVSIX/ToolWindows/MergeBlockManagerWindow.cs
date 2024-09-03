using Microsoft.VisualStudio.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MateralMergeBlockVSIX
{
    public class MergeBlockManagerWindow : BaseToolWindow<MergeBlockManagerWindow>
    {
        public override string GetTitle(int toolWindowId) => "MergeBlock管理器";

        public override Type PaneType => typeof(Pane);

        public override Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
        {
            return Task.FromResult<FrameworkElement>(new MergeBlockManagerWindowControl());
        }

        [Guid("b58b4f05-d649-42e7-a165-2158d9921adf")]
        internal class Pane : ToolkitToolWindowPane
        {
            public Pane()
            {
                BitmapImageMoniker = KnownMonikers.ToolWindow;
            }
        }
    }
}