<template>
    <a-form :model="formData" @finish="submitData">
        <a-form-item label="名称" name="Name" :rules="[{ required: true, message: '请输入名称' }]">
            <a-input v-model:value="formData.Name" />
        </a-form-item>
        <a-form-item label="描述" name="Name">
            <a-textarea v-model:value="formData.Description" :rows="4" />
        </a-form-item>
        <a-form-item>
            <a-button type="primary" html-type="submit" block :loading="loading">
                {{ formData.ID ? "保存" : "添加" }}
            </a-button>
        </a-form-item>
    </a-form>
</template>
<script setup lang="ts">
import DataModelService from '../services/DataModelService';
import { EditDataModelModel } from '../models/DataModel/EditDataModelModel';

/**
 * 加载标识
 */
const loading = ref(false);
/**
 * 表单数据
 */
const formData = reactive<EditDataModelModel>(new EditDataModelModel());
/**
 * 提交数据
 */
const submitData = async () => {
    loading.value = true;
    if (formData.ID) {
        await DataModelService.EditAsync(formData);
    }
    else {
        await DataModelService.AddAsync(formData);
    }
    loading.value = false;
    emits('complate');
};
/**
 * 初始化
 * @param id 唯一标识
 */
const initAsync = async (id?: string) => {
    loading.value = true;
    formData.ID = id ?? '';
    if (id && id !== '') {
        await initEidtAsync();
    }
    else {
        await initAddAsync();
    }
    loading.value = false;
};
/**
 * 初始化编辑
 */
const initEidtAsync = async () => {
    const result = await DataModelService.GetInfoAsync(formData.ID);
    if (result) {
        formData.Name = result.Data.Name;
        formData.Description = result.Data.Description;
    }
    else {
        await initAddAsync();
    }
}
/**
 * 初始化添加
 */
const initAddAsync = async () => {
    formData.ID = '';
    formData.Name = '';
    formData.Description = undefined;
}
/**
 * 事件
 */
const emits = defineEmits<{ (event: 'complate'): void }>();
/**
 * 暴露成员
 */
defineExpose({ initAsync });
</script>