<template>
    <a-form :model="formData" @finish="submitData">
        <a-form-item label="名称" name="Name" :rules="[{ required: true, message: '请输入名称' }]">
            <a-input v-model:value="formData.Name" />
        </a-form-item>
        <a-form-item label="数据模型" name="DataModelID" :rules="[{ required: true, message: '请选择数据模型' }]">
            <a-select v-model:value="formData.DataModelID" :disabled="!!formData.ID">
                <a-select-option v-for="item in dataModels" :value="item.ID">{{ item.Name }}</a-select-option>
            </a-select>
        </a-form-item>
        <a-form-item>
            <a-button type="primary" html-type="submit" block :loading="loading">
                {{ formData.ID ? "保存" : "添加" }}
            </a-button>
        </a-form-item>
    </a-form>
</template>
<script setup lang="ts">
import FlowTemplateService from '../services/FlowTemplateService';
import DataModelService from '../services/DataModelService';
import { EditFlowTemplateModel } from '../models/FlowTemplate/EditFlowTemplateModel';
import { DataModel } from '../models/DataModel/DataModel';

/**
 * 加载标识
 */
const loading = ref(false);
/**
 * 表单数据
 */
const formData = reactive<EditFlowTemplateModel>(new EditFlowTemplateModel());
/**
 * 数据模型
 */
const dataModels = ref<DataModel[]>([]);
/**
 * 提交数据
 */
const submitData = async () => {
    loading.value = true;
    if (formData.ID) {
        await FlowTemplateService.EditAsync(formData);
    }
    else {
        await FlowTemplateService.AddAsync(formData);
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
    const result = await FlowTemplateService.GetInfoAsync(formData.ID);
    if (result) {
        formData.Name = result.Data.Name;
        formData.DataModelID = result.Data.DataModelID;
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
}
/**
 * 初始化数据模型
 */
const InitDataModelAsync = async () => {
    const result = await DataModelService.GetListAsync({ PageIndex: 1, PageSize: 1000 });
    if (result) {
        dataModels.value = result.Data;
    }
    else {
        dataModels.value = [];
    }
    if (!formData.Name && dataModels.value.length > 0) {
        formData.DataModelID = dataModels.value[0].ID;
    }
};
/**
 * 事件
 */
const emits = defineEmits<{ (event: 'complate'): void }>();
/**
 * 暴露成员
 */
defineExpose({ initAsync });
/**
 * 页面加载完毕事件
 */
onMounted(async () => { await InitDataModelAsync(); });
</script>