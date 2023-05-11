import { createRouter, createWebHashHistory } from "vue-router";

const routes = [
    { path: '/', component: () => import("./components/Home.vue") },
    { path: '/UserList', component: () => import("./components/UserList.vue") },
    { path: '/DataModelList', component: () => import("./components/DataModelList.vue") },
];

export const router = createRouter({
    history: createWebHashHistory(),
    routes: routes,
});