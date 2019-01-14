window.addEventListener("load", () =>
{
    const clientInfoM = new Materal.ClientInfoModel();
    //是IE时执行
    if (clientInfoM.browserInfoModel.internetExplorer) {
        const internetExplorerVersion = parseFloat(clientInfoM.browserInfoModel.version);
        //IE版本小于等于IE8
        if (internetExplorerVersion <= 8) {
            if (document.createElement) {
                document.createElement("header");
                document.createElement("main");
                document.createElement("nav");
                document.createElement("section");
                document.createElement("article");
                document.createElement("footer");
                document.createElement("video");
            }
        }
    }
});