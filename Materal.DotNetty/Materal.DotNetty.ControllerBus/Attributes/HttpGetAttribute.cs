namespace Materal.DotNetty.ControllerBus.Attributes
{
    public class HttpGetAttribute : HttpMethodAttribute
    {
        public HttpGetAttribute() : base("Get") { }
    }
}
