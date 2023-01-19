namespace WebHelper
{
    export class IndexViewModel
    {
        constructor() {
            Common.loadDefaultTemplate();
        }
    }
}

window.addEventListener("load", () =>
{
    const viewModel = new WebHelper.IndexViewModel();
});