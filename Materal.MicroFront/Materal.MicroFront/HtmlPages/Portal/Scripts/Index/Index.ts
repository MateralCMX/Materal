namespace Materal.MicroFront.Scripts {
    class IndexViewModel {
        private _serviceRepository: Repositories.ServiceRepository;
        private _services: Array<any> = [];
        constructor() {
            this._serviceRepository = new Repositories.ServiceRepository();
            this.GetServices();
        }
        private GetServices() {
            this._serviceRepository.GetAppList((result: any) => {
                this._services = result.Data;
            });
        }
        public ChangeService(serviceName: string) {
            this._services.forEach(service => {
                if (service.Name == serviceName) {
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
                        if(link.AsAttribute){
                            linkElement.setAttribute("as", link.AsAttribute);
                        }
                        head.appendChild(linkElement);
                    }
                    //添加Script
                    let scripts = document.getElementById("scripts") as HTMLDivElement;
                    scripts.innerHTML = "";
                    for (let i = 0; i < service.Scripts.length; i++) {
                        const script = service.Scripts[i];
                        let scriptElement = document.createElement("script");
                        scriptElement.setAttribute("src", script);
                        scripts.appendChild(scriptElement);
                    }
                }
            });
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