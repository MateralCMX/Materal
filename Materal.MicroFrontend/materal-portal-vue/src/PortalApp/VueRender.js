import Vue from 'vue/dist/vue.esm';
import SubApp from '../components/SubApp'
let app = null;
/**
 * 获得Vue渲染器
 */
function getVueRender({ appContent, loading }) {
    return new Vue({
        el: '#subapp-container',
        data() {
            return {
                appContent,
                loading,
            };
        },
        render(event) {
            return event(SubApp, {
                props: {
                    appContent: this.appContent,
                    loading: this.loading
                }
            });
        }
    });
}
/**
 * 渲染器
 */
export default function render({ appContent, loading }) {
    if (!app) {
        app = getVueRender({ appContent, loading });
    } else {
        app.appContent = appContent;
        app.loading = loading;
    }
}
