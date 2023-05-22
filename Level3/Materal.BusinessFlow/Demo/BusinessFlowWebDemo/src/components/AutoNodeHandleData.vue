<template>
    <a-form-item label="自动节点" name="HandleData">
        <a-select ref="select" v-model:value="model.HandleData" placeholder="处理类型">
            <a-select-option value="ConsoleMessageAutoNode">控制台消息</a-select-option>
            <a-select-option value="HttpAutoNode">Http请求</a-select-option>
        </a-select>
    </a-form-item>
    <ConsoleMessageAutoNode v-if="model.HandleData == 'ConsoleMessageAutoNode'" v-model="model.Data" />
    <HttpAutoNode v-if="model.HandleData == 'HttpAutoNode'" v-model="model.Data" />
</template>
<script setup lang="ts">
import { EditNodeModel } from '../models/Node/EditNodeModel';

/**
 * 暴露成员
 */
const props = defineProps<{ modelValue: EditNodeModel }>();
/**
 * 绑定模型
 */
const model = useVModel(props, 'modelValue')
</script>