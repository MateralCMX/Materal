namespace WebHelper
{
    /**
     * 公共库
     */
    export class Common
    {
        /**
         * 加载模板
         * @param templateConfigs
         */
        static loadTemplate(templateConfigs: ITemplateConfig[], callFunc: Function = null): void
        {
            let successCount = 0;
            for (let i = 0; i < templateConfigs.length; i++) {
                const templateConfig = templateConfigs[i];
                if (!Materal.StringHelper.isNullOrrUndefinedOrEmpty(templateConfig.style)) {
                    templateConfig.element.setAttribute("style", templateConfig.style);
                }
                if (!Materal.StringHelper.isNullOrrUndefinedOrEmpty(templateConfig.cssClass)) {
                    Materal.ElementHelper.addClass(templateConfig.element, templateConfig.cssClass);
                }
                const httpConfig = new Materal.HttpConfigModel(templateConfig.url, Materal.HttpMethod.GET);
                httpConfig.success = (result: any, xhr: XMLHttpRequest, state: number) =>
                {
                    templateConfig.element.innerHTML = result;
                    if (++successCount === templateConfigs.length && callFunc) {
                        callFunc();
                    }
                };
                httpConfig.error = (result: any, xhr: XMLHttpRequest, state: number) =>
                {
                    throw "加载模板失败";
                };
                Materal.HttpManager.send(httpConfig);
            }
        }
        /**
         * 加载默认模板
         */
        static loadDefaultTemplate(activeTopNavId: string = "TopNavHome"): void
        {
            const templateConfigs: ITemplateConfig[] = [];
            const headers = document.getElementsByTagName("header");
            if (headers.length <= 0) throw "加载头部失败";
            templateConfigs.push({ element: headers[0], url: "/Template/Header.html", style: null, cssClass: null });
            const footers = document.getElementsByTagName("footer");
            if (footers.length <= 0) throw "加载腿部失败";
            templateConfigs.push({ element: footers[0], url: "/Template/Footer.html", style: null, cssClass: null });
            this.loadTemplate(templateConfigs, () =>
            {
                const topNavLi = document.getElementById(activeTopNavId);
                if (topNavLi) {
                    Materal.ElementHelper.addClass(topNavLi, "active");
                }
            });
        }
    }
    /**
     * 模板配置
     */
    export interface ITemplateConfig
    {
        url: string;
        element: HTMLElement;
        style: string;
        cssClass: string;
    }
}