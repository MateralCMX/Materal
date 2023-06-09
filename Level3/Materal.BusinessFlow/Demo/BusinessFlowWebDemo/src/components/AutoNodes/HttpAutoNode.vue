<template>
    <a-form-item label="请求地址" name="Data" :rules="rules.Url" required>
        <a-input v-model:value="dataModel.Url" />
    </a-form-item>
    <a-form-item label="请求方法" name="Data" :rules="rules.Method" required>
        <a-select v-model:value="dataModel.Method">
            <a-select-option value="Get">Get</a-select-option>
            <a-select-option value="Post">Post</a-select-option>
            <a-select-option value="Put">Put</a-select-option>
            <a-select-option value="Delete">Delete</a-select-option>
        </a-select>
    </a-form-item>
    <KeyValueModelComponent :data="dataModel.Headers" title="请求头" />
    <KeyValueModelComponent :data="dataModel.QueryParams" title="Url参数" />
    <a-form-item label="请求体" name="Data" :rules="rules.Body" required>
        <a-textarea v-model:value="dataModel.Body" :rows="4" />
    </a-form-item>
</template>
<script setup lang="ts">
import { RuleObject } from 'ant-design-vue/es/form';
import { HttpAutoNodeDataModel } from '../../models/AutoNodeDataModel/HttpAutoNodeDataModel';

const props = defineProps<{ modelValue?: string }>();
const emit = defineEmits<{
    (e: 'update:modelValue', v: string): void;
}>();
/**
 * 获得默认数据
 */
const getDefaultData = (): string => JSON.stringify(new HttpAutoNodeDataModel());
/**
 * 获得数据
 */
const getData = (): HttpAutoNodeDataModel => {
    const defaultData = JSON.parse(getDefaultData());
    try {
        if(!props.modelValue) return defaultData;
        const result: HttpAutoNodeDataModel = JSON.parse(props.modelValue);
        for (const key in result) {
            if (!Object.prototype.hasOwnProperty.call(defaultData, key)) return defaultData;
        }
        for (const key in defaultData) {
            if (!Object.prototype.hasOwnProperty.call(result, key)) return defaultData;
        }
        return result;
    } catch { 
        return defaultData;
    }
}
/**
 * 数据模型
 */
const dataModel = ref<HttpAutoNodeDataModel>(getData());
/**
 * 监视数据模型
 */
watch(dataModel, v => emit('update:modelValue', JSON.stringify(v)), { deep: true });
/**
 * 验证规则
 */
const rules: Record<string, RuleObject | RuleObject[]> = {
    Url: {
        validator() {
            if (!dataModel.value.Url) {
                return Promise.reject('请输入请求地址')
            }
            return Promise.resolve();
        },
    },
    Method: {
        validator() {
            if (!dataModel.value.Method) {
                return Promise.reject('请输入请求方法')
            }
            return Promise.resolve();
        },
    },
    Body: {
        validator() {
            if (!dataModel.value.Method) {
                return Promise.reject('请输入请求体')
            }
            return Promise.resolve();
        },
    },
};
</script>