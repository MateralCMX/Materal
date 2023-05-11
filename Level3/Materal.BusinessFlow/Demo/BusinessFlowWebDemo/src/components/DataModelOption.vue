<template>
    <a-form :model="_formData" @finish="SubmitData">
        <a-form-item label="名称" name="Name" :rules="[{ required: true, message: '请输入名称' }]">
            <a-input v-model:value="_formData.Name" />
        </a-form-item>
        <a-form-item label="描述" name="Name">
            <a-textarea v-model:value="_formData.Description" :rows="4" />
        </a-form-item>
        <a-form-item>
            <a-button type="primary" html-type="submit" block :loading="_loading">
                {{ _formData.ID ? "保存" : "添加" }}
            </a-button>
        </a-form-item>
    </a-form>
</template>
<script setup lang="ts">
import { reactive, ref } from 'vue';
import DataModelService from '../services/DataModelService';
import { EditDataModelModel } from '../models/DataModel/EditDataModelModel';

/**
 * 加载标识
 */
const _loading = ref(false);
/**
 * 表单数据
 */
const _formData = reactive<EditDataModelModel>(new EditDataModelModel());
/**
 * 提交数据
 */
const SubmitData = async () => {
    _loading.value = true;
    if (_formData.ID) {
        await DataModelService.EditAsync(_formData);
    }
    else {
        await DataModelService.AddAsync(_formData);
    }
    _loading.value = false;
    _emits('complate');
};
/**
 * 初始化
 * @param id 唯一标识
 */
const InitAsync = async (id?: string) => {
    _loading.value = true;
    _formData.ID = id ?? '';
    if (id && id !== '') {
        await InitEidtAsync();
    }
    else {
        await InitAddAsync();
    }
    _loading.value = false;
};
/**
 * 初始化编辑
 */
const InitEidtAsync = async () => {
    const result = await DataModelService.GetInfoAsync(_formData.ID);
    if (result) {
        _formData.Name = result.Data.Name;
        _formData.Description = result.Data.Description;
    }
    else {
        await InitAddAsync();
    }
}
/**
 * 初始化添加
 */
const InitAddAsync = async () => {
    _formData.ID = '';
    _formData.Name = '';
    _formData.Description = undefined;
}
/**
 * 事件
 */
const _emits = defineEmits<{ (event: 'complate'): void }>();
/**
 * 暴露
 */
defineExpose({ InitAsync });
</script>