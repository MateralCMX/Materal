<template>
    <a-select ref="select" v-model:value="model" placeholder="处理类型" :allow-clear="hasAll">
        <a-select-option :value="0">自动</a-select-option>
        <a-select-option :value="1">用户</a-select-option>
        <!-- <a-select-option :value="2">用户组</a-select-option> -->
        <a-select-option :value="3">发起人</a-select-option>
    </a-select>
</template>
<script setup lang="ts">
import { NodeHandleTypeEnum } from '../models/Node/NodeHandleTypeEnum';
/**
 * 暴露成员
 */
const props = defineProps<{ modelValue?: NodeHandleTypeEnum, hasAll: boolean }>();
/**
 * 绑定模型
 */
const model = useVModel(props, 'modelValue')
</script>