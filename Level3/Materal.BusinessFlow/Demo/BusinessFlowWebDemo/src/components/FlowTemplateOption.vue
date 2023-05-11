<template>
    <a-form :model="_formData" @finish="SubmitData">
        <a-form-item label="名称" name="Name" :rules="[{ required: true, message: '请输入名称' }]">
            <a-input v-model:value="_formData.Name" />
        </a-form-item>
        <a-form-item label="数据模型" name="DataModelID" :rules="[{ required: true, message: '请选择数据模型' }]">
            <a-select v-model:value="_formData.DataModelID" :disabled="!!_formData.ID">
                <a-select-option v-for="item in _dataModels" :value="item.ID">{{ item.Name }}</a-select-option>
            </a-select>
        </a-form-item>
        <a-form-item>
            <a-button type="primary" html-type="submit" block :loading="_loading">
                {{ _formData.ID ? "保存" : "添加" }}
            </a-button>
        </a-form-item>
    </a-form>
</template>
<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import FlowTemplateService from '../services/FlowTemplateService';
import DataModelService from '../services/DataModelService';
import { EditFlowTemplateModel } from '../models/FlowTemplate/EditFlowTemplateModel';
import { DataModel } from '../models/DataModel/DataModel';

/**
 * 加载标识
 */
const _loading = ref(false);
/**
 * 表单数据
 */
const _formData = reactive<EditFlowTemplateModel>(new EditFlowTemplateModel());
/**
 * 数据模型
 */
const _dataModels = ref<DataModel[]>([]);
/**
 * 提交数据
 */
const SubmitData = async () => {
    _loading.value = true;
    if (_formData.ID) {
        await FlowTemplateService.EditAsync(_formData);
    }
    else {
        await FlowTemplateService.AddAsync(_formData);
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
    const result = await FlowTemplateService.GetInfoAsync(_formData.ID);
    if (result) {
        _formData.Name = result.Data.Name;
        _formData.DataModelID = result.Data.DataModelID;
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
}
/**
 * 初始化数据模型
 */
const InitDataModelAsync = async () => {
    const result = await DataModelService.GetListAsync({ PageIndex: 1, PageSize: 1000 });
    if (result) {
        _dataModels.value = result.Data;
    }
    else {
        _dataModels.value = [];
    }
    if (!_formData.Name && _dataModels.value.length > 0) {
        _formData.DataModelID = _dataModels.value[0].ID;
    }
};
/**
 * 事件
 */
const _emits = defineEmits<{ (event: 'complate'): void }>();
/**
 * 暴露
 */
defineExpose({ InitAsync });
/**
 * 页面加载完毕事件
 */
onMounted(async () => { await InitDataModelAsync(); });
</script>