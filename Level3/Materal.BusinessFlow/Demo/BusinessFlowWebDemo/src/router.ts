import { createRouter, createWebHashHistory } from "vue-router";

const routes = [
    { path: '/', component: () => import("./components/Home.vue") },
    { path: '/UserList', component: () => import("./components/UserList.vue") },
];

export const router = createRouter({
    history: createWebHashHistory(),
    routes: routes,
});