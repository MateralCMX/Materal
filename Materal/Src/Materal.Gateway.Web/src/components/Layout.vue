<style scoped>
.main-page {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
}

.main-page-content {
    padding: 10px 2rem;
}

.menu-title {
    font-size: 16px;
    color: black;
    cursor: pointer;
    padding: 0px 10px;
}
.menu-title:hover {
    background-color: var(--color-fill-2);
}
</style>
<template>
    <a-layout class="main-page">
        <a-layout-header>
            <a-menu mode="horizontal" :selected-keys="selectedKey" @menu-item-click="changeMenu">
                <a-menu-item disabled>
                    <div class="menu-title" @click="changeMenu('/')">Materal Gateway</div>
                </a-menu-item>
                <a-menu-item key="/">主页</a-menu-item>
                <a-menu-item key="/global">全局配置</a-menu-item>
                <a-menu-item key="/swaggerConfig">Swagger配置</a-menu-item>
                <a-menu-item key="/route">路由配置</a-menu-item>
                <a-menu-item key="/tools">工具</a-menu-item>
                <a-menu-item key="#/swagger">Swagger文档</a-menu-item>
            </a-menu>
        </a-layout-header>
        <a-layout-content class="main-page-content">
            <component :is="currentView" />
        </a-layout-content>
    </a-layout>
</template>
<script setup lang="ts">
import { computed, onMounted, ref } from 'vue';
import GlobalConfig from './GlobalConfig.vue';
import Home from './Home.vue';
import NotFound from './NotFound.vue';
import Route from './Route.vue';
import Swagger from './Swagger.vue';
import Tools from './Tools.vue';

const routes = {
    '/': Home,
    '/global': GlobalConfig,
    '/swaggerConfig': Swagger,
    '/route': Route,
    '/tools': Tools
};
const currentPath = ref(window.location.hash);
window.addEventListener('hashchange', () => {
    currentPath.value = window.location.hash;
});
const currentView = computed(() => {
    return (routes as Record<string, any>)[currentPath.value.slice(1) || '/'] || NotFound
});
function changeMenu(url: string) {
    if (url.startsWith('#')) {
        window.open(url.substring(1), '_blank');
    }
    else {
        window.location.hash = url;
        selectedKey.value = [window.location.hash.slice(1) || '/'];
    }
}
const selectedKey = ref<Array<string>>([]);
onMounted(() => {
    selectedKey.value = [window.location.hash.slice(1) || '/'];
});
</script>