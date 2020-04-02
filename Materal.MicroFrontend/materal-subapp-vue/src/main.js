import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';

Vue.config.productionTip = false;
let app = null;

const ISDEV = process.env.NODE_ENV === 'develoment'

function render(isSubApp) {
  if(isSubApp) {
    if (ISDEV) {
      __webpack_public_path__ = window.__INJECTED_PUBLIC_PATH_BY_QIANKUN__;
    } else {
      __webpack_public_path__ = window.__INJECTED_PUBLIC_PATH_BY_QIANKUN__ + 'myVue/';
    }
  }
  app = new Vue({
    router,
    store,
    render: h => h(App),
  }).$mount('#app');
}

if (!window.__POWERED_BY_QIANKUN__) {
  render(false);
  console.log('非子系统');
} else {
  console.log('子系统');
}
export async function bootstrap() {
  console.log('[vue] vue app bootstraped');
}

export async function mount(props) {
  console.log('[vue] props from main framework', props);
  render(true);
}

export async function unmount() {
  if (app) {
    app.$destroy();
    app = null;
  }
}
