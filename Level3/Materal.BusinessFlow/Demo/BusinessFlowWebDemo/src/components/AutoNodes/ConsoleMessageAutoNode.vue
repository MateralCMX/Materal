<template>
    <a-form-item label="数据" name="Data" :rules="[{ required: true, message: '请选择要显示的数据' }]">
        <a-select ref="select" v-model:value="model" placeholder="数据">
            <a-select-option v-for="item in dataModelFields" :value="item.Name">{{ (!item.Description ? item.Name : item.Description ) }}</a-select-option>
        </a-select>
    </a-form-item>
</template>
<script setup lang="ts">
import { DataModelField } from '../../models/DataModelField/DataModelField';
/**
 * 注入-数据模型字段
 */
const dataModelFields = inject<DataModelField[]>('dataModelFields');
/**
 * 暴露成员
 */
const props = defineProps<{ modelValue?: string }>();
/**
 * 绑定模型
 */
const model = useVModel(props, 'modelValue');
</script>