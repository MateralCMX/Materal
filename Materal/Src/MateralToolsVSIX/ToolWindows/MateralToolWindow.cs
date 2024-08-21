#nullable enable
using Microsoft.VisualStudio.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MateralToolsVSIX.ToolWindows
{
    public class MateralToolWindow : BaseToolWindow<MateralToolWindow>
    {
        public override string GetTitle(int toolWindowId) => "MateralTool";
        public override Type PaneType => typeof(Pane);
        public override Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
            => Task.FromResult<FrameworkElement>(new MateralToolWindowControl());
        [Guid("00d11efd-764e-414f-8742-163d6267065a")]
        internal class Pane : ToolkitToolWindowPane
        {
            public Pane() => BitmapImageMoniker = KnownMonikers.ToolWindow;
        }
    }
}