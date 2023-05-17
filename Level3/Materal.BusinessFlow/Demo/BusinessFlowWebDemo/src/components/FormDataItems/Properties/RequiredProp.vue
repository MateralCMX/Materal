<template>
    <a-form-item label="必填" name="Required">
        <a-switch v-model:checked="model" />
    </a-form-item>
</template>
<script setup lang="ts">
/**
 * 暴露成员
 */
const props = defineProps<{ modelValue: boolean }>();
/**
 * 绑定模型
 */
 const model = useVModel(props, 'modelValue');
</script>