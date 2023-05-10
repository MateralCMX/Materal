
<template>
  <a-layout>
    <a-layout-header class="header">
      <HeaderMenu />
    </a-layout-header>
    <a-layout>
      <MainContent />
    </a-layout>
  </a-layout>
</template>
<script setup lang="ts">
import HeaderMenu from "./HeaderMenu.vue";
import MainContent from "./MainContent.vue";
</script>