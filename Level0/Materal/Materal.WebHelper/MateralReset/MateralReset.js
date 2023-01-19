window.addEventListener("load", function () {
    var clientInfoM = new Materal.ClientInfoModel();
    //是IE时执行
    if (clientInfoM.browserInfo.internetExplorer) {
        var internetExplorerVersion = parseFloat(clientInfoM.browserInfo.version);
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
//# sourceMappingURL=MateralReset.js.map