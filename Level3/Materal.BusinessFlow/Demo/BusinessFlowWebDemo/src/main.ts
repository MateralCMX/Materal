import './style.css'
import 'ant-design-vue/dist/antd.css';
import { router } from "./router";
import { createApp } from 'vue'
import Antd from 'ant-design-vue';
import App from './App.vue'

const app = createApp(App);
app.use(router).use(Antd).mount('#app');
