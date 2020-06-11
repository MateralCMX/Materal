declare namespace WebHelper {
    /**
     * 公共库
     */
    class Common {
        /**
         * 加载模板
         * @param templateConfigs
         */
        static loadTemplate(templateConfigs: ITemplateConfig[], callFunc?: Function): void;
        /**
         * 加载默认模板
         */
        static loadDefaultTemplate(activeTopNavId?: string): void;
    }
    /**
     * 模板配置
     */
    interface ITemplateConfig {
        url: string;
        element: HTMLElement;
        style: string;
        cssClass: string;
    }
}
