<template>
    <a-row>
        <a-col :span="6">
            <div v-for="dataModelField in dataModelFields">
                <StringDataTypeComponent v-if="dataModelField.DataType == DataTypeEnum.String"
                    :data-model-field="dataModelField" @new-data="pushData" />
            </div>
        </a-col>
        <a-col :span="12">
            <div>
                
            </div>
        </a-col>
        <a-col :span="6">
            属性区域
        </a-col>
    </a-row>
</template>
<script setup lang="ts">
import { DataModelField } from '../models/DataModelField/DataModelField';
import { DataTypeEnum } from '../models/DataModelField/DataTypeEnum';
import { DataTypeComponentModel } from '../models/DataTypeComponentModels/DataTypeComponentModel';


/**
 * 注入-数据模型字段
 */
const dataModelFields = inject<DataModelField[]>('dataModelFields');
/**
 * 暴露成员
 */
defineProps<{ data: string | undefined }>();
const formDataItems = ref<DataTypeComponentModel[]>([]);
/**
 * 追加数据
 */
const pushData = (data: DataTypeComponentModel) => {
    formDataItems.value.push(data);
}
</script>