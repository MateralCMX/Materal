<template>
    <a-form :model="formData" ref="formRef">
        <div v-for="formDataItem in formDataItems">
            <InputFormOption v-if="formDataItem.Tag == 'input'" v-model="formData[formDataItem.ID]"
                :component-model="(formDataItem as InputComponentModel)" :is-edit="false" />
            <TextareaFormOption v-else-if="formDataItem.Tag == 'textarea'" v-model="formData[formDataItem.ID]"
                :is-edit="false" :component-model="(formDataItem as TextareaComponentModel)" />
            <InputNumberFormOption v-else-if="formDataItem.Tag == 'inputNumber'"
                :component-model="(formDataItem as InputNumberComponentModel)" v-model="formData[formDataItem.ID]"
                :is-edit="false" />
            <SwitchFormOption v-else-if="formDataItem.Tag == 'switch'"
                :component-model="(formDataItem as SwitchComponentModel)" v-model="formData[formDataItem.ID]"
                :is-edit="false" />
            <SelectFormOption v-else-if="formDataItem.Tag == 'select'"
                :component-model="(formDataItem as SelectComponentModel)" v-model="formData[formDataItem.ID]"
                :is-edit="false" />
            <DatePickerFromOption v-else-if="formDataItem.Tag == 'datePicker'"
                :component-model="(formDataItem as DatePickerComponentModel)" v-model="formData[formDataItem.ID]"
                :is-edit="false" />
        </div>
        <a-form-item>
            <a-button type="primary" block :loading="loading" @click="saveFlowData">暂存</a-button>
        </a-form-item>
        <a-form-item>
            <a-button type="primary" block :loading="loading" @click="complateFlow">提交</a-button>
        </a-form-item>
        <a-form-item>
            <a-button v-if="canRepulse" type="primary" block :loading="loading" @click="repulseFlow">打回</a-button>
        </a-form-item>
    </a-form>
</template>
<script setup lang="ts">
import { DataModelField } from '../models/DataModelField/DataModelField';
import { DataTypeComponentModel } from '../models/DataTypeComponentModels/DataTypeComponentModel';
import { DatePickerComponentModel } from '../models/DataTypeComponentModels/DatePickerComponentModel';
import { InputComponentModel } from '../models/DataTypeComponentModels/InputComponentModel';
import { InputNumberComponentModel } from '../models/DataTypeComponentModels/InputNumberComponentModel';
import { SelectComponentModel } from '../models/DataTypeComponentModels/SelectComponentModel';
import { SwitchComponentModel } from '../models/DataTypeComponentModels/SwitchComponentModel';
import { TextareaComponentModel } from '../models/DataTypeComponentModels/TextareaComponentModel';
import { FlowTemplate } from '../models/FlowTemplate/FlowTemplate';
import DataModelFieldService from '../services/DataModelFieldService';
import FlowService from '../services/FlowService';
import FlowTemplateService from '../services/FlowTemplateService';
import NodeService from '../services/NodeService';
import { FormInstance, message } from 'ant-design-vue';
import StepService from '../services/StepService';
import { OperationFlowRequestModel } from '../models/Flow/OperationFlowRequestModel';
import { SavaFlowDataRequestModel } from '../models/Flow/SavaFlowDataRequestModel';
import { DataTypeEnum } from '../models/DataModelField/DataTypeEnum';
import dayjs from 'dayjs';

/**
 * 事件
 */
const emits = defineEmits<{
    (event: "complate"): void;
}>()
/**
 * 加载标识
 */
const loading = ref(false);
/**
 * 表单数据项
 */
const formDataItems = ref<DataTypeComponentModel[]>([]);
/**
 * 表单数据
 */
const formData = reactive<any>({});
/**
 * 是否可以驳回
 */
const canRepulse = ref(false);
/**
 * 数据模型字段组
 */
const dataModelFields = ref<DataModelField[]>([]);
/**
 * 流程模版
 */
const flowTemplate = ref<FlowTemplate>();
const formRef = ref<FormInstance>();
/**
 * 选中的流程记录唯一标识
 */
let selectFlowRecordID: string = "";
/**
 * 选中的用户唯一标识
 */
let selectUserID: string = "";
/**
 * 初始化
 * @param flowTemplateID 
 * @param flowRecordID 
 * @param nodeID 
 * @param userID 
 */
const initAsync = async (flowTemplateID: string, flowRecordID: string, nodeID: string, userID: string) => {
    await initFlowTemplateAsync(flowTemplateID, flowRecordID);
    selectFlowRecordID = flowRecordID;
    await initFormDataItemsAsync(nodeID);
    selectUserID = userID;
}
/**
 * 初始化流程模版
 * @param flowTemplateID 
 */
const initFlowTemplateAsync = async (flowTemplateID: string, flowRecordID: string) => {
    const flowTemplateResult = await FlowTemplateService.GetInfoAsync(flowTemplateID);
    if (!flowTemplateResult) return;
    flowTemplate.value = flowTemplateResult.Data;
    const dataModelFieldResult = await DataModelFieldService.GetAllListAsync({ PageIndex: 1, PageSize: 1000, DataModelID: flowTemplateResult.Data.DataModelID });
    if (!dataModelFieldResult) return;
    dataModelFields.value = dataModelFieldResult.Data;
    const dataResult = await FlowService.GetFlowDatasByFlowRecordIDAsync(flowTemplateID, flowRecordID);
    if (!dataResult) return;
    for (const item of dataModelFields.value) {
        const name: string = item.Name;
        let value = dataResult.Data[name];
        switch (item.DataType) {
            case DataTypeEnum.Date:
            case DataTypeEnum.Time:
            case DataTypeEnum.DateTime:
                if (!value) {
                    value = dayjs();
                }
                else {
                    value = dayjs(value);
                }
                break;
        }
        formData[item.ID] = value;
    }
}
/**
 * 初始化表单数据项
 */
const initFormDataItemsAsync = async (nodeID: string) => {
    const result = await NodeService.GetInfoAsync(nodeID);
    if (!result) return;
    formDataItems.value = JSON.parse(result.Data.Data ?? "[]");
    const stepResult = await StepService.GetInfoAsync(result.Data.StepID);
    if (!stepResult) return;
    canRepulse.value = !!stepResult.Data.UpID;
}
/**
 * 提交
 */
const complateFlow = async () => {
    if (!flowTemplate.value) return;
    if (!formRef.value) return;
    try {
        await formRef.value.validateFields();
        loading.value = true;
        const jsonData = JSON.stringify(await getFormDataAsync());
        const data = new OperationFlowRequestModel();
        data.FlowRecordID = selectFlowRecordID;
        data.UserID = selectUserID;
        data.JsonData = jsonData;
        data.FlowTemplateID = flowTemplate.value?.ID
        const result = await FlowService.ComplateFlowNodeAsync(data);
        if (!result) return;
        message.success(result.Message);
        loading.value = false;
        emits("complate");
    } catch { }
}
/**
 * 打回
 */
const repulseFlow = async () => {
    if (!flowTemplate.value) return;
    if (!formRef.value) return;
    try {
        await formRef.value.validateFields();
        loading.value = true;
        const jsonData = JSON.stringify(await getFormDataAsync());
        const data = new OperationFlowRequestModel();
        data.FlowRecordID = selectFlowRecordID;
        data.UserID = selectUserID;
        data.JsonData = jsonData;
        data.FlowTemplateID = flowTemplate.value?.ID
        const result = await FlowService.RepulseFlowNodeAsync(data);
        if (!result) return;
        message.success(result.Message);
        loading.value = false;
        emits("complate");
    } catch { }
}
/**
 * 保存流程数据
 */
const saveFlowData = async () => {
    if (!flowTemplate.value) return;
    loading.value = true;
    const jsonData = JSON.stringify(await getFormDataAsync());
    const data = new SavaFlowDataRequestModel();
    data.FlowRecordID = selectFlowRecordID;
    data.JsonData = jsonData;
    data.FlowTemplateID = flowTemplate.value?.ID
    const result = await FlowService.SaveFlowDataAsync(data);
    if (!result) return;
    message.success(result.Message);
    loading.value = false;
}
/**
 * 获得表单数据
 */
const getFormDataAsync = async (): Promise<any> => {
    const data: any = {};
    for (const key in dataModelFields.value) {
        if (!Object.prototype.hasOwnProperty.call(dataModelFields.value, key)) continue;
        const value = formData[dataModelFields.value[key].ID];
        data[dataModelFields.value[key].Name] = value;
    }
    return data;
}
/**
 * 提供
 */
provide('dataModelFields', dataModelFields);
/**
 * 暴露成员
 */
defineExpose({ initAsync });
</script>