namespace Materal.MicroFront.Scripts {
    export class WebSocktHelper {
        private websocket: WebSocket;
        private eventHandlers = new Dictionary();
        constructor() {
            this.websocket = new WebSocket(`ws://${Common.getServerAddress()}/websocket`);
            this.websocket.onclose = event => {
                console.log("WebSocket已断开");
            };
            this.websocket.onerror = event => {
                console.error(event);
            };
            this.websocket.onopen = event => {
                console.log("WebSocket已连接");
            }
            this.websocket.onmessage = event => {
                const data = JSON.parse(event.data);
                if (!data.hasOwnProperty("HandlerName")) {
                    console.error("未找到事件处理器");
                }
                else {
                    var handler = this.eventHandlers.get(data.HandlerName)
                    if (handler == null) {
                        console.error(`未找到事件处理器${data["HandlerName"]}`);
                    }
                    else {
                        handler(data);
                    }
                }
            }
            this.registerEventHandler("ServerErrorEventHandler", (event: any) => {
                switch (event.Status) {
                    case 401:
                        window.location.href = "/Login";
                        break;
                    default:
                        console.error(event);
                        break;
                }
            });
        }
        public sendCommand(handlerName: string, data: any) {
            data["HandlerName"] = handlerName;
            data["Token"] = Common.getAuthoirtyInfo();
            const commandString = JSON.stringify(data);
            this.websocket.send(commandString);
        }
        public registerEventHandler(name: string, callback: Function) {
            this.eventHandlers.set(name, (event: any) => {
                callback(event);
            });
        }
    }
}