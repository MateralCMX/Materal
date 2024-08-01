#nullable enable
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Text;

namespace MateralToolsVSIX.ToolWindows.ViewModels
{
    /// <summary>
    /// 插入新的Guid视图模型
    /// </summary>
    public class InsertNewGuidViewModel : ObservableObject
    {
        public async Task InsertNewGuidStringAsync()
        {
            DocumentView? docView = await VS.Documents.GetActiveDocumentViewAsync();
            if (docView?.TextView is null) return;
            SnapshotPoint position = docView.TextView.Caret.Position.BufferPosition;
            docView.TextBuffer?.Insert(position, Guid.NewGuid().ToString());
        }
    }
}
