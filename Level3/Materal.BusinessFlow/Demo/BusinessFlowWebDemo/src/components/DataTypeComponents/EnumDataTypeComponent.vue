<template>
    <div class="data-type-component">
        <span class="data-type-component-title">{{ dataModelField.Description ? dataModelField.Description :
            dataModelField.Name }}</span>
        <div>
            <a-button @click="addSelectData">选择器</a-button>
        </div>
    </div>
</template>
<script setup lang="ts">
import { DataModelField } from '../../models/DataModelField/DataModelField';
import { DataTypeComponentModel } from '../../models/DataTypeComponentModels/DataTypeComponentModel';
import { SelectComponentModel } from '../../models/DataTypeComponentModels/SelectComponentModel';

/**
 * 暴露成员
 */
const props = defineProps<{ dataModelField: DataModelField }>();
/**
 * 事件
 */
const emits = defineEmits<{ (event: "newData", data: DataTypeComponentModel): void }>();
/**
 * 添加数字框数据
 */
const addSelectData = () => {
    const data = new SelectComponentModel(props.dataModelField.ID);
    emits("newData", data);
}
</script>