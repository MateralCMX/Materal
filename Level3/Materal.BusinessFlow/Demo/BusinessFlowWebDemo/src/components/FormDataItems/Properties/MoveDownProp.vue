<template>
    <a-form-item>
        <a-button block @click="() => emits('moveDown')">下移</a-button>
    </a-form-item>
</template>
<script setup lang="ts">
/**
 * 事件
 */
const emits = defineEmits<{ (event: "moveDown"): void }>();
</script>