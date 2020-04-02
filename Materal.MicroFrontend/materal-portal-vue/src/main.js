import 'zone.js';
import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import { PortalApp } from './PortalApp/PortalApp';
import { subAppList } from './SubAppList'
const portalApp = new PortalApp();
portalApp.Init(subAppList);
Vue.config.productionTip = false
new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#mainApp')
