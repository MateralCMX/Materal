#nullable enable
using Microsoft.VisualStudio.PlatformUI;

namespace MateralToolsVSIX.ToolWindows.ViewModels
{
    public class MateralToolViewModel : ObservableObject
    {
        public InsertNewGuidViewModel InsertNewGuid { get; } = new();
    }
}
