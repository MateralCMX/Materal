namespace WebHelper.Template
{
    export class DebugViewModel
    {
        constructor()
        {
            const templateConfigs: ITemplateConfig[] = [
                {
                    element: document.createElement("header"),
                    url: "/Template/Header.html",
                    style: null,
                    cssClass: null
                },
                {
                    element: document.createElement("footer"),
                    url: "/Template/Footer.html",
                    style: null,
                    cssClass: null
                }
            ];
            for (let i = 0; i < templateConfigs.length; i++) {
                document.body.appendChild(templateConfigs[i].element);
            }
            Common.loadTemplate(templateConfigs, () =>
            {
                const topNavLi = document.getElementById("TopNavDebug");
                if (topNavLi) {
                    Materal.ElementHelper.addClass(topNavLi, "active");
                }
            });
        }
    }
}
window.addEventListener("load", () =>
{
    const viewModel = new WebHelper.Template.DebugViewModel();
});