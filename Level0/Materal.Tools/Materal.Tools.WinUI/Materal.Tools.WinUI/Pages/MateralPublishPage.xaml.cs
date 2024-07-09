using Materal.Tools.WinUI.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Materal.Tools.WinUI.Pages
{
    [Menu("Materal发布", "\uE7B8")]
    public sealed partial class MateralPublishPage : Page
    {
        public MateralPublishViewModel ViewModel { get; } = new();
        public MateralPublishPage() => InitializeComponent();
    }
}
