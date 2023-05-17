<template>
    <div class="data-type-component">
        <span class="data-type-component-title">{{ dataModelField.Description ? dataModelField.Description :
            dataModelField.Name }}</span>
        <div>
            <a-button @click="addDatePickerData">日期选择器</a-button>
        </div>
    </div>
</template>
<script setup lang="ts">
import { DataModelField } from '../../models/DataModelField/DataModelField';
import { DataTypeComponentModel } from '../../models/DataTypeComponentModels/DataTypeComponentModel';
import { DatePickerComponentModel } from '../../models/DataTypeComponentModels/DatePickerComponentModel';

/**
 * 暴露成员
 */
const props = defineProps<{ dataModelField: DataModelField }>();
/**
 * 事件
 */
const emits = defineEmits<{ (event: "newData", data: DataTypeComponentModel): void }>();
/**
 * 添加时间选择器数据
 */
const addDatePickerData = () => {
    const data = new DatePickerComponentModel(props.dataModelField.ID, "Date");
    emits("newData", data);
}
</script>