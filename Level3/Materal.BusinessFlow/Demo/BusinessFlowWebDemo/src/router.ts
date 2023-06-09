import { createRouter, createWebHashHistory } from "vue-router";

const routes = [
    { path: '/', component: () => import("./components/Home.vue") },
    { path: '/Test', component: () => import("./components/Test.vue") },
    { path: '/UserList', component: () => import("./components/UserList.vue") },
    { path: '/DataModelList', component: () => import("./components/DataModelList.vue") },
    { path: '/FlowTemplateList', component: () => import("./components/FlowTemplateList.vue") },
    { path: '/FlowTemplateCanvas/:id', component: () => import("./components/FlowTemplateCanvas.vue") },
    { path: '/FlowList/:id?', component: () => import("./components/FlowList.vue") },
    { path: '/FlowDetail/:id/:userid', component: () => import("./components/FlowDetail.vue") },
];

export const router = createRouter({
    history: createWebHashHistory(),
    routes: routes,
});