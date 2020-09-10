using Materal.Model;

namespace BlazorWebAPP.Common
{
    public static class PageModelExtension
    {
        public static int GetTableIndex(this PageModel pageModel, int index)
        {
            return pageModel.Skip + index + 1;
        }
    }
}
