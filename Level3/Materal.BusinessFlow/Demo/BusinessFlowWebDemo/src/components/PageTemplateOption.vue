<template>
    <a-row>
        <a-col :span="6">
            <div v-for="dataModelField in dataModelFields">
                <StringDataTypeComponent v-if="dataModelField.DataType == DataTypeEnum.String"
                    :data-model-field="dataModelField" @new-data="pushData" />
                <NumberDataTypeComponent v-if="dataModelField.DataType == DataTypeEnum.Number"
                    :data-model-field="dataModelField" @new-data="pushData" />
            </div>
            <a-button type="primary" block @click="saveData" style="margin-top: 20px;">保存</a-button>
        </a-col>
        <a-col :span="12" style="padding: 0 20px;">
            <a-form>
                <div v-for="formDataItem in formDataItems">
                    <InputFormOption v-if="formDataItem.Tag == 'input'" :model-value="(formDataItem as InputComponentModel)"
                        :is-edit="true" @selected="ShowPropertyConfig" />
                    <TextareaFormOption v-if="formDataItem.Tag == 'textarea'" :is-edit="true"
                        :model-value="(formDataItem as TextareaComponentModel)" @selected="ShowPropertyConfig" />
                    <InputNumberFormOption v-if="formDataItem.Tag == 'inputNumber'" :model-value="(formDataItem as InputNumberComponentModel)"
                        :is-edit="true" @selected="ShowPropertyConfig" />
                </div>
            </a-form>
        </a-col>
        <a-col :span="6">
            <InputFormProperty v-if="nowSelectedFromDataItem && nowSelectedFromDataItem.Tag == 'input'"
                :model-value="(nowSelectedFromDataItem as InputComponentModel)" @delete="deleteData" @move-up="moveUpData"
                @move-down="moveDownData" />
            <TextareaFormProperty v-if="nowSelectedFromDataItem && nowSelectedFromDataItem.Tag == 'textarea'"
                :model-value="(nowSelectedFromDataItem as TextareaComponentModel)" @delete="deleteData"
                @move-up="moveUpData" @move-down="moveDownData" />
            <InputNumberFormProperty v-if="nowSelectedFromDataItem && nowSelectedFromDataItem.Tag == 'inputNumber'"
                :model-value="(nowSelectedFromDataItem as InputNumberComponentModel)" @delete="deleteData" @move-up="moveUpData"
                @move-down="moveDownData" />
        </a-col>
    </a-row>
</template>
<script setup lang="ts">
import { DataModelField } from '../models/DataModelField/DataModelField';
import { DataTypeEnum } from '../models/DataModelField/DataTypeEnum';
import { DataTypeComponentModel } from '../models/DataTypeComponentModels/DataTypeComponentModel';
import { InputComponentModel } from '../models/DataTypeComponentModels/InputComponentModel';
import { InputNumberComponentModel } from '../models/DataTypeComponentModels/InputNumberComponentModel';
import { TextareaComponentModel } from '../models/DataTypeComponentModels/TextareaComponentModel';

/**
 * 事件
 */
const emits = defineEmits<{ (event: "saveData", data: string): void }>();
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
 * 删除数据
 */
const deleteData = () => {
    if (!nowSelectedFromDataItem.value) return;
    const index = formDataItems.value.indexOf(nowSelectedFromDataItem.value);
    if (index < 0) return;
    formDataItems.value.splice(index, 1);
    nowSelectedFromDataItem.value = undefined;
}
/**
 * 向上移动数据
 */
const moveUpData = () => {
    if (!nowSelectedFromDataItem.value) return;
    const index = formDataItems.value.indexOf(nowSelectedFromDataItem.value);
    if (index <= 0) return;
    const temp = formDataItems.value[index - 1];
    formDataItems.value[index - 1] = formDataItems.value[index];
    formDataItems.value[index] = temp;
}
/**
 * 向下移动数据
 */
const moveDownData = () => {
    if (!nowSelectedFromDataItem.value) return;
    const index = formDataItems.value.indexOf(nowSelectedFromDataItem.value);
    if (index >= formDataItems.value.length - 1) return;
    const temp = formDataItems.value[index + 1];
    formDataItems.value[index + 1] = formDataItems.value[index];
    formDataItems.value[index] = temp;
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
const saveData = () => {
    const dataJson = JSON.stringify(formDataItems.value);
    emits('saveData', dataJson);
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
defineExpose({ init });
</script>