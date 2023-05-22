<template>
    <a-form-item label="请求地址" name="Data" :rules="[{ required: true, message: '请输入请求地址' }]">
        <a-input v-model="dataModel.Url" />
    </a-form-item>
    <a-form-item label="请求方法" name="Data" :rules="[{ required: true, message: '请输入请求方法' }]">
        <a-input v-model="dataModel.Method" />
    </a-form-item>
</template>
<script setup lang="ts">
import { HttpAutoNodeDataModel } from '../../models/AutoNodeDataModel/HttpAutoNodeDataModel';


const dataModel = ref<HttpAutoNodeDataModel>(new HttpAutoNodeDataModel());
/**
 * 暴露成员
 */
const props = defineProps<{ modelValue?: string }>();//这个modelValue是父组件传过来的值，是一个Json字符串
/**
 * 绑定模型
 */
const model = useVModel(props, 'modelValue');
</script>