namespace Materal.ConDep.Scripts {
    export class Common {
        static serverAddress:string = "192.168.0.107:8910";
        /**
         * 设置权限信息
         * @param token token值
         */
        static setAuthoirtyInfo(token: string) {
            sessionStorage.setItem("token", token);
        }
        /**
         * 获得权限信息
         */
        static getAuthoirtyInfo(): string | null {
            return sessionStorage.getItem("token");
        }
        /**
         * 发送Post请求
         * @param url 
         * @param data 
         * @param success 
         * @param fail 
         */
        static sendPost(url: string, data: any, success?: Function, fail?: Function) {
            this.send(url, data, HttpMethod.POST, success, fail);
        }
        /**
         * 发送Get请求
         * @param url 
         * @param data 
         * @param success 
         * @param fail 
         */
        static sendGet(url: string, data: any, success?: Function, fail?: Function) {
            this.send(url, data, HttpMethod.GET, success, fail);
        }
        /**
         * 发送请求
         * @param url 
         * @param data 
         * @param method 
         * @param success 
         * @param fail 
         */
        private static send(url: string, data: any, method: HttpMethod, success?: Function, fail?: Function) {
            if (!fail) {
                fail = function (res: any) {
                    alert(res.Message);
                }
            }
            var successFunc = (result: any, xhr: XMLHttpRequest, status: number) => {
                if (result.ResultType == 0) {
                    if (success) {
                        success(result, xhr, status);
                    }
                }
                else {
                    if (fail) {
                        fail(result, xhr, status);
                    }
                }
            }
            var errorFunc = (result: any, xhr: XMLHttpRequest, status: number) => {
                console.error(result);
            }
            const token = this.getAuthoirtyInfo();
            if(token){
                if(method == HttpMethod.GET){
                    if(url.indexOf("?") > 0){
                        url+=`&token=${token}`
                    }
                    else{
                        url+=`?token=${token}`
                    }
                }
                else{
                    data["Token"] = token;
                }
            }
            var config = new HttpConfigModel(`http://${this.serverAddress}/api/${url}`, method, data, HttpHeadContentType.Json, successFunc, errorFunc);
            HttpManager.send(config);
        }
    }
}