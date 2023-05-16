<template>
    <a-row>
        <a-col :span="6">
            <div v-for="dataModelField in dataModelFields">
                <StringDataTypeComponent v-if="dataModelField.DataType == DataTypeEnum.String"
                    :data-model-field="dataModelField" @new-data="pushData" />
            </div>
        </a-col>
        <a-col :span="12" style="padding: 0 20px;">
            <a-form>
                <div v-for="formDataItem in formDataItems">
                    <InputFormOptionComponents v-if="formDataItem.Tag == 'input'"
                        :model-value="(formDataItem as InputComponentModel)" @selected="ShowPropertyConfig" />
                    <TextareaFormOptionComponents v-if="formDataItem.Tag == 'textarea'"
                        :model-value="(formDataItem as TextareaComponentModel)" @selected="ShowPropertyConfig" />
                </div>
            </a-form>
        </a-col>
        <a-col :span="6">
            {{ (nowSelectedFromDataItem as any)["Props"] }}
        </a-col>
    </a-row>
</template>
<script setup lang="ts">
import { DataModelField } from '../models/DataModelField/DataModelField';
import { DataTypeEnum } from '../models/DataModelField/DataTypeEnum';
import { DataTypeComponentModel } from '../models/DataTypeComponentModels/DataTypeComponentModel';
import { InputComponentModel } from '../models/DataTypeComponentModels/InputComponentModel';
import { TextareaComponentModel } from '../models/DataTypeComponentModels/TextareaComponentModel';

/**
 * 注入-数据模型字段
 */
const dataModelFields = inject<DataModelField[]>('dataModelFields');
/**
 * 表单数据项
 */
const formDataItems = ref<DataTypeComponentModel[]>([]);
/**
 * 当前选中的数据项
 */
const nowSelectedFromDataItem = ref<DataTypeComponentModel>();
/**
 * 追加数据
 */
const pushData = (data: DataTypeComponentModel) => {
    formDataItems.value.push(data);
}
/**
 * 初始化
 */
const init = (data: string | undefined) => {
    if (!data) {
        data = '[]';
    }
    formDataItems.value = JSON.parse(data);
}
/**
 * 获得数据
 */
const getData = (): string => {
    return JSON.stringify(formDataItems.value);
}
/**
 * 显示属性配置
 * @param model 
 */
const ShowPropertyConfig = (model: DataTypeComponentModel) => {
    nowSelectedFromDataItem.value = model;
}
/**
 * 暴露成员
 */
defineExpose({ init, getData });
</script>