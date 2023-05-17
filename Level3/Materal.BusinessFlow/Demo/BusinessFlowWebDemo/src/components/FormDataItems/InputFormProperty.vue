<template>
    <a-form :model="modelValue">
        <a-form-item label="必填" name="Required">
            <a-switch v-model:checked="modelValue.Props.Required" />
        </a-form-item>
        <a-form-item label="只读" name="Readonly">
            <a-switch v-model:checked="modelValue.Props.Readonly" />
        </a-form-item>
        <a-form-item>
            <a-button block @click="() => emits('moveUp')">上移</a-button>
        </a-form-item>
        <a-form-item>
            <a-button block @click="() => emits('moveDown')">下移</a-button>
        </a-form-item>
        <a-form-item>
            <a-button type="primary" block danger @click="() => emits('delete')">删除</a-button>
        </a-form-item>
    </a-form>
</template>
<script setup lang="ts">
import { InputComponentModel } from '../../models/DataTypeComponentModels/InputComponentModel';

/**
 * 暴露成员
 */
defineProps<{ modelValue: InputComponentModel }>();
/**
 * 事件
 */
const emits = defineEmits<{
    (event: "delete"): void
    (event: "moveUp"): void
    (event: "moveDown"): void
}>();
</script>