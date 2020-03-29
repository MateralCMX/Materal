import Vue from 'vue'
import SubApp from './components/SubApp'
import { registerMicroApps, start } from 'qiankun';
const portalApp = function () {
    const subApps = [
        {
            name: "vue",
            entry: "//localhost:7101",
            render,
            activeRule: activeRule("/vue")
        },
        {
            name: "angular9",
            entry: "//localhost:7103",
            render,
            activeRule: activeRule("/angular9")
        },
        {
            name: 'react16',
            entry: '//localhost:7100',
            render,
            activeRule: activeRule('/react16'),
        },
        {
            name: 'react15',
            entry: '//localhost:7102',
            render,
            activeRule: activeRule('/react15'),
        },
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
        console.log("卸载后回调", app);
    }
    function Init() {
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
    return {
        Init
    };
}
export default new portalApp();