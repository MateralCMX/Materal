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
                    <div class="menu-title" @click="changeMenu('/')">RC</div>
                </a-menu-item>
                <a-menu-item key="/">主页</a-menu-item>
                <a-sub-menu key="0">
                    <template #title>发布中心</template>
                    <a-menu-item key="/ApplicationInfoList">程序管理</a-menu-item>
                    <a-menu-item key="/DefaultDataList">默认数据</a-menu-item>
                </a-sub-menu>
                <a-sub-menu key="1">
                    <template #title>配置中心</template>
                    <a-menu-item key="/ProjectList">项目管理</a-menu-item>
                    <a-menu-item key="/NamespaceList">命名空间</a-menu-item>
                    <a-menu-item key="/ConfigurationItemList">配置管理</a-menu-item>
                    <a-menu-item key="/SyncConfigurationItem">配置同步</a-menu-item>
                </a-sub-menu>
                <a-menu-item key="/UserList">用户管理</a-menu-item>
            </a-menu>
        </a-layout-header>
        <a-layout-content class="main-page-content">
            <component :is="currentView" />
        </a-layout-content>
    </a-layout>
</template>
<script setup lang="ts">
import { computed, onMounted, ref } from 'vue';
import Home from './Home.vue';
import NotFound from './NotFound.vue';
import UserList from './UserList.vue';
import ProjectList from './ProjectList.vue';
import NamespaceList from './NamespaceList.vue';
import ConfigurationItemList from './ConfigurationItemList.vue';
import SyncConfigurationItem from './SyncConfigurationItem.vue';

const routes = {
    '/': Home,
    '/UserList': UserList,
    '/ProjectList': ProjectList,
    '/NamespaceList': NamespaceList,
    '/ConfigurationItemList': ConfigurationItemList,
    '/SyncConfigurationItem': SyncConfigurationItem
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