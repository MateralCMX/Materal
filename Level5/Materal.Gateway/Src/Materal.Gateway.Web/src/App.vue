
<template>
  <component :is="currentView" />
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import Layout from './components/Layout.vue';
import Login from './components/Login.vue';
import loginManagement from "./loginManagement";

const currentPath = ref(window.location.hash);
window.addEventListener('hashchange', () => {
  currentPath.value = window.location.hash;
});
const currentView = computed(() => {
  if(currentPath.value.slice(1) == '/login'){
    return Login;
  }
  return Layout;
});
onMounted(() => {
  if (loginManagement.isLogin()) return;
  window.location.hash = "/login";
});
</script>