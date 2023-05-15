<template>
    <a-form :model="formData" @finish="submitData">
        <a-form-item label="名称" name="Name" :rules="[{ required: true, message: '请输入名称' }]">
            <a-input v-model:value="formData.Name" />
        </a-form-item>
        <a-form-item label="运行条件" name="RunConditionExpression">
            <a-input v-model:value="formData.RunConditionExpression" />
        </a-form-item>
        <a-form-item label="节点处理方式" name="HandleType">
            <NodeHandleTypeEnumSelect v-model="formData.HandleType" :has-all="false" />
        </a-form-item>
        <AutoNodeHandleData v-if="formData.HandleType == NodeHandleTypeEnum.Auto" v-model="formData"
            :data-model-fields="dataModelFields" />
        <a-form-item>
            <a-button type="primary" html-type="submit" block :loading="loading">
                {{ formData.ID ? "保存" : "添加" }}
            </a-button>
        </a-form-item>
    </a-form>
</template>
<script setup lang="ts">
import { DataModelField } from '../models/DataModelField/DataModelField';
import { EditNodeModel } from '../models/Node/EditNodeModel';
import { NodeHandleTypeEnum } from '../models/Node/NodeHandleTypeEnum';
import NodeService from '../services/NodeService';

/**
 * 加载标识
 */
const loading = ref(false);
/**
 * 表单数据
 */
const formData = reactive<EditNodeModel>(new EditNodeModel());
/**
 * 提交数据
 */
const submitData = async () => {
    loading.value = true;
    if (formData.ID) {
        await NodeService.EditAsync(formData);
    }
    else {
        await NodeService.AddAsync(formData);
    }
    loading.value = false;
    emits('complate');
};
/**
 * 数据模型字段组
 */
const dataModelFields = ref<DataModelField[]>([]);
/**
 * 初始化
 */
const initAsync = async (dataModels: DataModelField[], stepID: string, id?: string) => {
    formData.StepID = stepID;
    dataModelFields.value = dataModels;
    if (id) {
        formData.ID = id;
        await initEidtAsync();
    }
    else {
        await initAddAsync();
    }
}
/**
 * 初始化编辑
 */
const initEidtAsync = async () => {
    const result = await NodeService.GetInfoAsync(formData.ID);
    if (result) {
        formData.Name = result.Data.Name;
        formData.HandleType = result.Data.HandleType;
        formData.Data = result.Data.Data;
        formData.HandleData = result.Data.HandleData;
        formData.RunConditionExpression = result.Data.RunConditionExpression;
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
    formData.HandleType = NodeHandleTypeEnum.Auto;
    formData.Data = '';
    formData.HandleData = 'ConsoleMessageAutoNode';
    formData.RunConditionExpression = '';
}
/**
 * 事件
 */
const emits = defineEmits<{ (event: 'complate'): void }>();
/**
 * 提供
 */
provide('dataModelFields', dataModelFields);
/**
 * 暴露成员
 */
defineExpose({ initAsync });
</script>