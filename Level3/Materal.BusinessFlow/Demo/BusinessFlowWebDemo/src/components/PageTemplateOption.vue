<template>
    <a-row>
        <a-col :span="6">
            <div v-for="dataModelField in dataModelFields">
                <StringDataTypeComponent v-if="dataModelField.DataType == DataTypeEnum.String"
                    :data-model-field="dataModelField" @new-data="pushData" />
                <NumberDataTypeComponent v-else-if="dataModelField.DataType == DataTypeEnum.Number"
                    :data-model-field="dataModelField" @new-data="pushData" />
                <BooleanDataTypeComponent v-else-if="dataModelField.DataType == DataTypeEnum.Boolean"
                    :data-model-field="dataModelField" @new-data="pushData" />
                <EnumDataTypeComponent v-else-if="dataModelField.DataType == DataTypeEnum.Enum"
                    :data-model-field="dataModelField" @new-data="pushData" />
                <DateDataTypeComponent v-else-if="dataModelField.DataType == DataTypeEnum.Date"
                    :data-model-field="dataModelField" @new-data="pushData" />
                <TimeDataTypeComponent v-else-if="dataModelField.DataType == DataTypeEnum.Time"
                    :data-model-field="dataModelField" @new-data="pushData" />
                <DateTimeDataTypeComponent v-else-if="dataModelField.DataType == DataTypeEnum.DateTime"
                    :data-model-field="dataModelField" @new-data="pushData" />
            </div>
            <a-button type="primary" block @click="saveData" style="margin-top: 20px;">确定</a-button>
        </a-col>
        <a-col :span="12" style="padding: 0 20px;">
            <a-form>
                <div v-for="formDataItem in formDataItems">
                    <InputFormOption v-if="formDataItem.Tag == 'input'"
                        :component-model="(formDataItem as InputComponentModel)" :is-edit="true"
                        @selected="ShowPropertyConfig" />
                    <TextareaFormOption v-else-if="formDataItem.Tag == 'textarea'" :is-edit="true"
                        :component-model="(formDataItem as TextareaComponentModel)" @selected="ShowPropertyConfig" />
                    <InputNumberFormOption v-else-if="formDataItem.Tag == 'inputNumber'"
                        :component-model="(formDataItem as InputNumberComponentModel)" :is-edit="true"
                        @selected="ShowPropertyConfig" />
                    <SwitchFormOption v-else-if="formDataItem.Tag == 'switch'"
                        :component-model="(formDataItem as SwitchComponentModel)" :is-edit="true"
                        @selected="ShowPropertyConfig" />
                    <SelectFormOption v-else-if="formDataItem.Tag == 'select'"
                        :component-model="(formDataItem as SelectComponentModel)" :is-edit="true"
                        @selected="ShowPropertyConfig" />
                    <DatePickerFromOption v-else-if="formDataItem.Tag == 'datePicker'"
                        :component-model="(formDataItem as DatePickerComponentModel)" :is-edit="true"
                        @selected="ShowPropertyConfig" />
                </div>
            </a-form>
        </a-col>
        <a-col :span="6">
            <InputFormProperty v-if="nowSelectedFromDataItem && nowSelectedFromDataItem.Tag == 'input'"
                :model-value="(nowSelectedFromDataItem as InputComponentModel)" @delete="deleteData" @move-up="moveUpData"
                @move-down="moveDownData" />
            <TextareaFormProperty v-else-if="nowSelectedFromDataItem && nowSelectedFromDataItem.Tag == 'textarea'"
                :model-value="(nowSelectedFromDataItem as TextareaComponentModel)" @delete="deleteData"
                @move-up="moveUpData" @move-down="moveDownData" />
            <InputNumberFormProperty v-else-if="nowSelectedFromDataItem && nowSelectedFromDataItem.Tag == 'inputNumber'"
                :model-value="(nowSelectedFromDataItem as InputNumberComponentModel)" @delete="deleteData"
                @move-up="moveUpData" @move-down="moveDownData" />
            <SwitchFormProperty v-else-if="nowSelectedFromDataItem && nowSelectedFromDataItem.Tag == 'switch'"
                :model-value="(nowSelectedFromDataItem as SwitchComponentModel)" @delete="deleteData" @move-up="moveUpData"
                @move-down="moveDownData" />
            <SelectFormProperty v-else-if="nowSelectedFromDataItem && nowSelectedFromDataItem.Tag == 'select'"
                :model-value="(nowSelectedFromDataItem as SelectComponentModel)" @delete="deleteData" @move-up="moveUpData"
                @move-down="moveDownData" />
            <DatePickerFromProperty v-else-if="nowSelectedFromDataItem && nowSelectedFromDataItem.Tag == 'datePicker'"
                :model-value="(nowSelectedFromDataItem as DatePickerComponentModel)" @delete="deleteData"
                @move-up="moveUpData" @move-down="moveDownData" />
        </a-col>
    </a-row>
</template>
<script setup lang="ts">
import { DataModelField } from '../models/DataModelField/DataModelField';
import { DataTypeEnum } from '../models/DataModelField/DataTypeEnum';
import { DataTypeComponentModel } from '../models/DataTypeComponentModels/DataTypeComponentModel';
import { DatePickerComponentModel } from '../models/DataTypeComponentModels/DatePickerComponentModel';
import { InputComponentModel } from '../models/DataTypeComponentModels/InputComponentModel';
import { InputNumberComponentModel } from '../models/DataTypeComponentModels/InputNumberComponentModel';
import { SelectComponentModel } from '../models/DataTypeComponentModels/SelectComponentModel';
import { SwitchComponentModel } from '../models/DataTypeComponentModels/SwitchComponentModel';
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