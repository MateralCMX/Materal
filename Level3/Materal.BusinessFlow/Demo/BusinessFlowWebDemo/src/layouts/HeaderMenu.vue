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
    <a-menu theme="dark" mode="horizontal" class="menu">
        <a-menu-item key="1"><router-link to="/">主页</router-link></a-menu-item>
        <a-menu-item key="2"><router-link to="/DataModelList">数据管理</router-link></a-menu-item>
        <a-menu-item key="3"><router-link to="/">模版管理</router-link></a-menu-item>
        <a-menu-item key="4"><router-link to="/UserList">用户管理</router-link></a-menu-item>
        <a-menu-item key="5"><router-link to="/">流程管理</router-link></a-menu-item>
    </a-menu>
</template>
<script setup lang="ts">
</script>