import 'zone.js';
import Vue from 'vue'
import MainApp from './App.vue'
import portallApp from './portalApp'

Vue.config.productionTip = false
portallApp.Init();
new Vue({
  render: h => h(MainApp),
}).$mount('#mainApp')
