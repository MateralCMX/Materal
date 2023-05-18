<style scoped>
.logo {
    float: left;
    width: 120px;
    height: 100%;
    margin: 0;
    color: white;
    font-size: 17px;
    font-weight: bold;
}

.menu {
    line-height: 64px;
}
</style>
<template>
    <div class="logo">
        BusinessFlow
    </div>
    <a-menu theme="dark" mode="horizontal" class="menu" :selected-keys="[route.path]">
        <a-menu-item key="/"><router-link to="/">主页</router-link></a-menu-item>
        <a-menu-item key="/DataModelList"><router-link to="/DataModelList">数据管理</router-link></a-menu-item>
        <a-menu-item key="/FlowTemplateList"><router-link to="/FlowTemplateList">模版管理</router-link></a-menu-item>
        <a-menu-item key="/UserList"><router-link to="/UserList">用户管理</router-link></a-menu-item>
        <a-menu-item key="/FlowList"><router-link to="/FlowList">流程管理</router-link></a-menu-item>
    </a-menu>
</template>
<script setup lang="ts">
import { useRoute } from 'vue-router';

const route = useRoute()
</script>