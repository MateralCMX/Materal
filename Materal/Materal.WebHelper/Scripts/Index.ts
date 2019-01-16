namespace WebHelper
{
    export class IndexViewModel
    {
        constructor() {
            const headers = document.getElementsByTagName("header");
            if (headers.length <= 0) throw "加载头部失败";
            Common.loadHeader(headers[0] as HTMLHeadElement);
        }
    }
}

window.addEventListener("load", () =>
{
    const viewModel = new WebHelper.IndexViewModel();
});