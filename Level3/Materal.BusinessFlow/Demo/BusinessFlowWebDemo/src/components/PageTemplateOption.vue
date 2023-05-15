<template>
    {{ data }}
</template>
<script setup lang="ts">

/**
 * 暴露成员
 */
defineProps<{ data: string | undefined }>();
</script>