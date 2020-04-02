import { registerMicroApps, runAfterFirstMounted, setDefaultMountApp, start } from 'qiankun';
import render from './VueRender';

export const PortalApp = function () {
    const startOptions = {
        prefetch: true,
        jsSandbox: true,
        singular: false,
    };
    const lifeCycles = {
        beforeLoad: [app => beforeLoad(app)],
        beforeMount: [app => beforeMount(app)],
        afterMount: [app => afterMount(app)],
        beforeUnmount: [app => beforeUnmount(app)],
        afterUnmount: [app => afterUnmount(app)]
    };
    /**
     * 路由监听
     */
    function activeRule(routerPrefix) {
        return () => location.pathname.startsWith(routerPrefix);
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
    /**
     * 第一次挂载完毕
     */
    function onFirstMounted() {
        console.log('第一个App挂载完毕');
    }
    /**
     * 初始化
     */
    function Init(subAppList) {
        const subApps = [];
        subAppList.forEach(subApp => {
            subApps.push({
                name: subApp.name,
                entry: subApp.address,
                render,
                activeRule: activeRule('/' + subApp.name),
            });
        });
        registerMicroApps(subApps, lifeCycles);
        runAfterFirstMounted(() => onFirstMounted());
        setDefaultMountApp('/');
        start(startOptions);
    }
    return {
        Init
    };
};