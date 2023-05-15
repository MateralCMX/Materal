import './style.css'
import 'ant-design-vue/lib/message/style/index.css';
import { router } from "./router";

import App from './App.vue';

const app = createApp(App);
app.use(router).mount('#app');
