
<template>
  <component :is="currentView" />
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import Layout from './components/Layout.vue';
import Login from './components/Login.vue';
import loginManagement from "./loginManagement";
import serverManagement from './serverManagement';

const currentPath = ref(window.location.hash);
window.addEventListener('hashchange', () => {
  currentPath.value = window.location.hash;
});
const currentView = computed(() => {
  if (currentPath.value.slice(1) == '/Login') {
    return Login;
  }
  return Layout;
});
onMounted(async () => {
  if (loginManagement.isLogin()) {
    await serverManagement.initAsync();
    return;
  }
  window.location.hash = "/Login";
});
</script>