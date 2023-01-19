namespace Materal.BaseCore.Common.Utils
{
    public static class ArrayHelperExtensions
    {
        public static (ICollection<T> addArray, ICollection<T> removeArray) GetAddArrayAndRemoveArray<T>(this ICollection<T> sourceArray, ICollection<T> oldArray)
        {
            ICollection<T> addArray = sourceArray.Except(oldArray).ToArray();
            ICollection<T> removeArray = oldArray.Except(sourceArray).ToArray();
            return (addArray, removeArray);
        }
    }
}
