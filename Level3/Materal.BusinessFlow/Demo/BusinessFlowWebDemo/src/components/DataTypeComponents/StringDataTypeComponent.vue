<template>
    <div class="data-type-component">
        <span class="data-type-component-title">{{ dataModelField.Description ? dataModelField.Description :
            dataModelField.Name }}</span>
        <div>
            <a-button @click="addInputData">文本框</a-button>
            <a-button @click="addTextareaData">文本域</a-button>
        </div>
    </div>
</template>
<script setup lang="ts">
import { DataModelField } from '../../models/DataModelField/DataModelField';
import { DataTypeComponentModel } from '../../models/DataTypeComponentModels/DataTypeComponentModel';
import { StringDataTypeComponentModel } from '../../models/DataTypeComponentModels/StringDataTypeComponentModel';
import { StringDataTypeComponentTextareModel } from '../../models/DataTypeComponentModels/StringDataTypeComponentTextareModel';

/**
 * 暴露成员
 */
const props = defineProps<{ dataModelField: DataModelField }>();
/**
 * 事件
 */
const emits = defineEmits<{ (event: "newData", data: DataTypeComponentModel): void }>();
/**
 * 添加文本框数据
 */
const addInputData = () => {
    const data = new StringDataTypeComponentModel(props.dataModelField.ID);
    emits("newData", data);
}
/**
 * 添加文本框数据
 */
const addTextareaData = () => {
    const data = new StringDataTypeComponentTextareModel(props.dataModelField.ID);
    emits("newData", data);
}
</script>