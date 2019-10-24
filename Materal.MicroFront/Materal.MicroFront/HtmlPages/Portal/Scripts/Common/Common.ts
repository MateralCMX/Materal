namespace Materal.MicroFront.Scripts {
    export class Common {
        static getServerAddress() {
            return window.location.host;
        }
        /**
         * 设置权限信息
         * @param token token值
         */
        static setAuthoirtyInfo(token: string) {
            localStorage.setItem("token", token);
        }
        /**
         * 获得权限信息
         */
        static getAuthoirtyInfo(): string | null {
            return localStorage.getItem("token");
        }
        /**
         * 获得权限信息
         */
        static removeAuthoirtyInfo() {
            localStorage.removeItem("token");
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
                switch (status) {
                    case 401:
                        window.location.href = "/Login";
                        break;
                    default:
                        console.error(result);
                        break;
                }
            }
            let heads: any = {
                "Content-Type": "application/json"
            };
            const token = this.getAuthoirtyInfo();
            if (token) {
                heads["Authorization"] = `Bearer ${token}`;
            }
            var config = new HttpConfigModel(`http://${this.getServerAddress()}/api/${url}`, method, data, heads, successFunc, errorFunc);
            HttpManager.send(config);
        }
    }
}