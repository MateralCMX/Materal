namespace Materal.MicroFront.Scripts {
    class IndexViewModel {
        private _serviceRepository: Repositories.ServiceRepository;
        private _services: Array<any> = [];
        private nowService: string = "";
        constructor() {
            this._serviceRepository = new Repositories.ServiceRepository();
            this.GetServices();
        }
        private GetServices() {
            this._serviceRepository.GetAppList((result: any) => {
                this._services = result.Data;
                const appInfos = document.getElementById("AppInfos") as HTMLDivElement;
                appInfos.innerHTML = "";
                for (let i = 0; i < this._services.length; i++) {
                    const service = this._services[i];
                    const aElement = document.createElement("a");
                    aElement.href = `/#/${service.Name}`;
                    aElement.innerHTML = service.Name;
                    appInfos.appendChild(aElement);
                }
            });
        }
        public ChangeService(serviceName: string) {
            this._services.forEach(service => {
                if (service.Name == serviceName && this.nowService != service.Name) {
                    let nowService = (window as any)[this.nowService];
                    if(nowService){
                        nowService.vue.$destroy();
                    }
                    this.nowService = service.Name;
                    let head = document.getElementsByTagName("head")[0] as HTMLHeadElement;
                    //清理路由
                    for (let i = 0; i < head.childNodes.length; i++) {
                        const element = head.childNodes[i];
                        if (element.nodeName == "LINK") {
                            head.removeChild(element);
                        }
                    }
                    //添加Link
                    for (let i = 0; i < service.Links.length; i++) {
                        const link = service.Links[i];
                        let linkElement = document.createElement("link");
                        linkElement.setAttribute("href", link.HrefAttribute);
                        linkElement.setAttribute("rel", link.RelAttribute);
                        if (link.AsAttribute) {
                            linkElement.setAttribute("as", link.AsAttribute);
                        }
                        head.appendChild(linkElement);
                    }
                    //添加Script
                    let scripts = document.getElementById("scripts") as HTMLDivElement;
                    scripts.innerHTML = "";
                    this.LoadScripts(0, service.Scripts, scripts);
                }
            });
        }
        private LoadScripts(index: number, scripts: Array<string>, scriptsDiv: HTMLDivElement) {
            if (index >= scripts.length){                
                let nowService = (window as any)[this.nowService];
                nowService.Init();
                nowService.vue.$mount("#app");
                return;
            }
            let scriptElement = document.createElement("script");
            scriptElement.setAttribute("src", scripts[index]);
            scriptElement.addEventListener("load", () => {
                this.LoadScripts(index + 1, scripts, scriptsDiv);
            });
            scriptsDiv.appendChild(scriptElement);
        }
    }
    let viewModel: IndexViewModel;
    window.addEventListener("load", () => {
        viewModel = new IndexViewModel();
        let hashs = window.location.hash.split('/');
        if (hashs.length >= 2) {
            viewModel.ChangeService(hashs[1]);
        }
    });
    window.addEventListener("hashchange", () => {
        let hashs = window.location.hash.split('/');
        if (hashs.length >= 2) {
            viewModel.ChangeService(hashs[1]);
        }
    })
}