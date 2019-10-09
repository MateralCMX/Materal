namespace Materal.Model.Example
{
    public interface ITestService
    {
        [DataValidation]
        void Test01(string[] strings);
    }
}
