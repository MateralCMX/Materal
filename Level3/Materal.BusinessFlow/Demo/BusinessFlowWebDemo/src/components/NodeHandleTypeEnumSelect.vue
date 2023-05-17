<template>
    <a-select ref="select" v-model:value="model" placeholder="处理类型" @change="modelChange" :allow-clear="hasAll">
        <a-select-option :value="0">自动</a-select-option>
        <a-select-option :value="1">用户</a-select-option>
        <!-- <a-select-option :value="2">用户组</a-select-option> -->
        <a-select-option :value="3">发起人</a-select-option>
    </a-select>
</template>
<script setup lang="ts">
import { SelectValue } from 'ant-design-vue/lib/select';
import { NodeHandleTypeEnum } from '../models/Node/NodeHandleTypeEnum';
/**
 * 暴露成员
 */
const props = defineProps<{ modelValue?: NodeHandleTypeEnum, hasAll: boolean }>();
/**
 * 事件
 */
const emits = defineEmits<{ (event: "change", type: NodeHandleTypeEnum): void }>();
/**
 * 绑定模型
 */
const model = useVModel(props, 'modelValue');
/**
 * 模型更新 
 */
const modelChange = (value: SelectValue) => {
    emits('change', value as NodeHandleTypeEnum);
}
</script>