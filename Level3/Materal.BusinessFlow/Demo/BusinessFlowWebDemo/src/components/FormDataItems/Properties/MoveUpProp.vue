<template>
    <a-form-item>
        <a-button block @click="() => emits('moveUp')">上移</a-button>
    </a-form-item>
</template>
<script setup lang="ts">
/**
 * 事件
 */
const emits = defineEmits<{ (event: "moveUp"): void }>();
</script>