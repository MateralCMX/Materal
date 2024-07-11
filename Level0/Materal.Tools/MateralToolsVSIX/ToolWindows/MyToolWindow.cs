//using Microsoft.VisualStudio.Imaging;
//using System.Runtime.InteropServices;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows;

//namespace MateralToolsVSIX
//{
//    public class MyToolWindow : BaseToolWindow<MyToolWindow>
//    {
//        public override string GetTitle(int toolWindowId) => "My Tool Window";

//        public override Type PaneType => typeof(Pane);

//        public override Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
//        {
//            return Task.FromResult<FrameworkElement>(new MyToolWindowControl());
//        }

//        [Guid("00d11efd-764e-414f-8742-163d6267065a")]
//        internal class Pane : ToolkitToolWindowPane
//        {
//            public Pane()
//            {
//                BitmapImageMoniker = KnownMonikers.ToolWindow;
//            }
//        }
//    }
//}