import Vue from 'vue';
import SubApp from './components/SubApp';
import { registerMicroApps, start } from 'qiankun';
const portalApp = function () {
    const subApps = [
        {
            name: "myAngular",
            // entry: "//localhost:4200",
            entry: "//192.168.0.101:8800/myAngular",
            render,
            activeRule: activeRule("/myAngular")
        },
        {
            name: "myVue",
            // entry: "//localhost:4201",
            entry: "//192.168.0.101:8800/myVue",
            render,
            activeRule: activeRule("/myVue")
        }
    ];
    let app = null;
    /**
     * 获得vue渲染对象
     */
    function getVueRender(renderEvent) {
        return new Vue({
            el: "#container",
            data() {
                return {
                    content: renderEvent.appContent,
                    loading: renderEvent.loading
                };
            },
            render(event) {
                return event(SubApp, {
                    props: {
                        content: this.content,
                        loading: this.loading
                    }
                });
            }
        });
    }
    /**
     * 渲染函数
     */
    function render(renderEvent) {
        if (!app) {
            app = getVueRender(renderEvent);
        } else {
            app.content = renderEvent.appContent;
            app.loading = renderEvent.loading;
        }
    }
    /**
    * 路由监听
    */
    function activeRule(routerPrefix) {
        return location => location.pathname.startsWith(routerPrefix);
    }
    /**
     * 加载前回调
     */
    function beforeLoad(app) {
        console.log("加载前回调", app);
    }
    /**
     * 挂载前回调
     */
    function beforeMount(app) {
        console.log("挂载前回调", app);
    }
    /**
     * 挂载后回调
     */
    function afterMount(app) {
        console.log("挂载后回调", app);
    }
    /**
     * 卸载前回调
     */
    function beforeUnmount(app) {
        console.log("卸载前回调", app);
    }
    /**
     * 卸载后回调
     */
    function afterUnmount(app) {
        const subAppDiv = document.getElementById("single-spa-application:" + app.name);
        if (subAppDiv) {
            subAppDiv.remove();
        }
        console.log("卸载后回调", app);
    }
    /**
     * 初始化
     */
    function Init() {
        mountFunction();
        const lifeCycles = {
            beforeLoad,
            beforeMount,
            afterMount,
            beforeUnmount,
            afterUnmount
        };
        registerMicroApps(subApps, lifeCycles);
        const startOptions = {
            prefetch: true,
            jsSandbox: true,
            singular: true,
            fetch: window.fetch
        };
        start(startOptions);
    }
    /**
     * 挂载方法
     */
    function mountFunction() {
        window.testFunction = () => {
            console.log('portal testFunction Runing');
        };
        // window.goto = (subAppName, params) => {
        //     const url = subAppName + '#/';
        //     for (const key in params) {
        //         if (params.hasOwnProperty(key)) {
        //             const element = params[key];
        //             subAppName += element + '/';
        //         }
        //     }
        //     history.pushState(null, url, url);
        // };
    }
    return {
        Init
    };
};
export default new portalApp();