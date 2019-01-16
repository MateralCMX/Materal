namespace WebHelper.Template {
    import StringHelper = Materal.StringHelper;
    export class DebugViewModel
    {
        constructor(element:HTMLElement, name: string)
        {
            if (!element) return;
            if (StringHelper.isNullOrrUndefinedOrEmpty(name)) return;
            const url = `/Template/${name}.html`;
            const config = new Materal.HttpConfigModel(url, Materal.HttpMethod.GET);
            config.success = (result: any, xhr: XMLHttpRequest, state: number) =>
            {
                element.innerHTML = result;
            };
            config.error = (result: any, xhr: XMLHttpRequest, state: number) =>
            {
                console.error("加载模板失败");
            };
            Materal.HttpManager.send(config);
        }
    }
}
window.addEventListener("load", () => {
    const element = document.getElementById("DebugBody");
    const viewModel = new WebHelper.Template.DebugViewModel(element, "Header");
});