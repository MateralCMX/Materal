<template>
    <a-select ref="select" :value="modelValue" placeholder="数据类型" :allow-clear="hasAll" @input="$emit('update:modelValue', $event.target.value)">
        <a-select-option :value="0">字符串</a-select-option>
        <a-select-option :value="1">数字</a-select-option>
        <a-select-option :value="2">日期时间</a-select-option>
        <a-select-option :value="3">日期</a-select-option>
        <a-select-option :value="4">时间</a-select-option>
        <a-select-option :value="5">布尔</a-select-option>
        <a-select-option :value="6">枚举</a-select-option>
    </a-select>
</template>
<script setup lang="ts">
import { DataTypeEnum } from '../models/DataModelField/DataTypeEnum';
/**
 * 暴露
 */
defineProps<{ modelValue?: DataTypeEnum, hasAll: boolean }>();
/**
 * 事件
 */
defineEmits(['update:modelValue']);
</script>