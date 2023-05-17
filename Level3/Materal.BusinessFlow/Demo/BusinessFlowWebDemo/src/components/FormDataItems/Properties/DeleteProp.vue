<template>
        <a-form-item>
            <a-button type="primary" block danger @click="() => emits('delete')">删除</a-button>
        </a-form-item>
</template>
<script setup lang="ts">
/**
 * 事件
 */
const emits = defineEmits<{ (event: "delete"): void }>();
</script>