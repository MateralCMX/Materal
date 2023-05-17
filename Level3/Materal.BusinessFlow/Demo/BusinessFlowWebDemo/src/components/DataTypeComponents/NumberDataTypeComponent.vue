<template>
    <div class="data-type-component">
        <span class="data-type-component-title">{{ dataModelField.Description ? dataModelField.Description :
            dataModelField.Name }}</span>
        <div>
            <a-button @click="addInputData">数字框</a-button>
        </div>
    </div>
</template>
<script setup lang="ts">
import { DataModelField } from '../../models/DataModelField/DataModelField';
import { DataTypeComponentModel } from '../../models/DataTypeComponentModels/DataTypeComponentModel';
import { InputNumberComponentModel } from '../../models/DataTypeComponentModels/InputNumberComponentModel';

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
    const data = new InputNumberComponentModel(props.dataModelField.ID);
    emits("newData", data);
}
</script>