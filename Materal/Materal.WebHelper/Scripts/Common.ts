namespace WebHelper
{
    export class Common
    {
        static loadHeader(headerElement: HTMLHeadElement): void
        {
            if (!headerElement) return;
            const url = "/Template/Header.html";
            const config = new Materal.HttpConfigModel(url, Materal.HttpMethod.GET);
            config.success = (result: any, xhr: XMLHttpRequest, state: number) =>
            {
                headerElement.innerHTML = result;
            };
            config.error = (result: any, xhr: XMLHttpRequest, state: number) =>
            {
                console.error("加载头部模板失败");
            };
            Materal.HttpManager.send(config);
        }
    }
}