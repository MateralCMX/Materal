<template>
    <a-form-item label="数据" name="Data" :rules="rules.data">
        <a-select ref="select" v-model:value="dataModel" placeholder="数据">
            <a-select-option v-for="item in dataModelFields" :value="item.Name">
                {{ (!item.Description ? item.Name : item.Description) }}
            </a-select-option>
        </a-select>
    </a-form-item>
</template>
<script setup lang="ts">
import { RuleObject } from 'ant-design-vue/es/form';
import { DataModelField } from '../../models/DataModelField/DataModelField';
/**
 * 注入-数据模型字段
 */
const dataModelFields = inject<Ref<DataModelField[]>>('dataModelFields');
/**
 * 暴露成员
 */
const props = defineProps<{ modelValue?: string }>();
const emit = defineEmits<{
    (e: 'update:modelValue', v: string): void;
}>();
/**
 * 获得默认数据
 */
const getDefaultData = (): string => {
    if (!dataModelFields || dataModelFields.value.length <= 0) return "";
    return dataModelFields.value[0].Name;
}
/**
 * 获得数据
 */
const getData = (): string => {
    if (!dataModelFields) return getDefaultData();
    for (let i = 0; i < dataModelFields.value.length; i++) {
        const element = dataModelFields.value[i];
        if (element.Name == props.modelValue){
            return props.modelValue;
        }
    }
    return getDefaultData();
}
/**
 * 数据模型
 */
const dataModel = ref<string>(getData());
/**
 * 监视数据模型
 */
watch(dataModel, v => emit('update:modelValue', v), { deep: true });
/**
 * 验证规则
 */
const rules: Record<string, RuleObject | RuleObject[]> = {
    data: {
        validator() {
            if (!dataModel.value) {
                return Promise.reject('请输入请求地址')
            }
            return Promise.resolve();
        },
    }
};
</script>