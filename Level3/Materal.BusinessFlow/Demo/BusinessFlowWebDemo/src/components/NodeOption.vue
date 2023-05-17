<template>
    <a-form :model="formData" @finish="submitData">
        <a-form-item label="名称" name="Name" :rules="[{ required: true, message: '请输入名称' }]">
            <a-input v-model:value="formData.Name" />
        </a-form-item>
        <a-form-item label="运行条件" name="RunConditionExpression">
            <a-input v-model:value="formData.RunConditionExpression" />
        </a-form-item>
        <a-form-item label="节点处理方式" name="HandleType">
            <NodeHandleTypeEnumSelect v-model="formData.HandleType" @change="handleTypeChange" :has-all="false" />
        </a-form-item>
        <AutoNodeHandleData v-if="formData.HandleType == NodeHandleTypeEnum.Auto" v-model="formData"
            :data-model-fields="dataModelFields" />
        <UserNodeHandleData v-else-if="formData.HandleType == NodeHandleTypeEnum.User" v-model="formData"
            :data-model-fields="dataModelFields" />
        <InitiatorNodeHandleData v-else-if="formData.HandleType == NodeHandleTypeEnum.Initiator" v-model="formData"
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
const formData = ref<EditNodeModel>(new EditNodeModel());
/**
 * 提交数据
 */
const submitData = async () => {
    loading.value = true;
    if (formData.value.ID) {
        await NodeService.EditAsync(formData.value);
    }
    else {
        await NodeService.AddAsync(formData.value);
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
    formData.value.StepID = stepID;
    dataModelFields.value = dataModels;
    if (id) {
        formData.value.ID = id;
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
    const result = await NodeService.GetInfoAsync(formData.value.ID);
    if (result) {
        formData.value.Name = result.Data.Name;
        formData.value.HandleType = result.Data.HandleType;
        formData.value.Data = result.Data.Data;
        formData.value.HandleData = result.Data.HandleData;
        formData.value.RunConditionExpression = result.Data.RunConditionExpression;
    }
    else {
        await initAddAsync();
    }
}
/**
 * 初始化添加
 */
const initAddAsync = async () => {
    formData.value.ID = '';
    formData.value.Name = '';
    formData.value.HandleType = NodeHandleTypeEnum.Auto;
    formData.value.Data = '';
    formData.value.HandleData = 'ConsoleMessageAutoNode';
    formData.value.RunConditionExpression = '';
}
/**
 * 处理类型更改
 * @param type 
 */
const handleTypeChange = (type: NodeHandleTypeEnum) => {
    debugger;
    switch (type) {
        case NodeHandleTypeEnum.Auto:
            formData.value.HandleData = 'ConsoleMessageAutoNode';
            if (dataModelFields.value.length > 0) {
                formData.value.Data = dataModelFields.value[0].Name;
            }
            break;
        default:
            formData.value.HandleData = '';
            formData.value.Data = undefined;
            break;
    }
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