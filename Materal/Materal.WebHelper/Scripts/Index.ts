class IndexViewModel
{
    constructor() {
        const a = new Materal.SqlHelper("", "", "", 1024 * 1024 * 2, () => { });
    }
}

window.addEventListener("load", () =>
{
    const viewModel = new IndexViewModel();
});