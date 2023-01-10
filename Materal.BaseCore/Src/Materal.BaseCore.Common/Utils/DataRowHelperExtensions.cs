using System.Data;

namespace Materal.BaseCore.Common.Utils
{
    public static class DataRowHelperExtensions
    {
        public static string? GetStringValue(this DataRow row, int index)
        {
            if (row.ItemArray == null) return null;
            if (row.ItemArray.Count() <= index) return null;
            var result = row[index].ToString();
            return result;
        }
    }
}
