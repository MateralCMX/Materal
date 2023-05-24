<template>
    <a-form-item label="请求地址" name="Data" :rules="rules.Url" required>
        <a-input v-model:value="dataModel.Url" />
    </a-form-item>
    <a-form-item label="请求方法" name="Data" :rules="rules.Method" required>
        <a-input v-model:value="dataModel.Method" />
    </a-form-item>
    {{ modelValue }}
</template>
<script setup lang="ts">
import { RuleObject } from 'ant-design-vue/es/form';
import { HttpAutoNodeDataModel } from '../../models/AutoNodeDataModel/HttpAutoNodeDataModel';

const props = defineProps<{ modelValue?: string }>();
const emit = defineEmits<{
    (e: 'update:modelValue', v: string): void;
}>();
const dataModel = ref<HttpAutoNodeDataModel>(JSON.parse(props.modelValue || '{}'));
watch(dataModel, v => emit('update:modelValue', JSON.stringify(v)), { deep: true })
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
    }
};
</script>