<template>
    <div :class="isEdit ? 'option-panel opeion-panel-edit' : 'option-panel'"
        @click="() => emits('selected', componentModel)">
        <a-form-item :label="dataModelField?.Description ? dataModelField?.Description : dataModelField?.Name"
            :name="dataModelField?.Name" :rules="[{ required: componentModel.Props.Required, message: '请输入名称' }]">
            <a-textarea :readonly="componentModel.Props.Readonly" :disabled="componentModel.Props.Disabled"
                :rows="componentModel.Props.Rows" />
        </a-form-item>
    </div>
</template>
<script setup lang="ts">
import { DataModelField } from '../../models/DataModelField/DataModelField';
import { DataTypeComponentModel } from '../../models/DataTypeComponentModels/DataTypeComponentModel';
import { TextareaComponentModel } from '../../models/DataTypeComponentModels/TextareaComponentModel';

/**
 * 暴露成员
 */
const props = defineProps<{ componentModel: TextareaComponentModel, isEdit: boolean }>();
/**
 * 事件
 */
const emits = defineEmits<{ (event: "selected", model: DataTypeComponentModel): void }>();
/**
 * 注入-数据模型字段
 */
const dataModelFields = inject<Ref<DataModelField[]>>('dataModelFields');
/**
 * 数据模型字段
 */
const dataModelField = ref<DataModelField>();
/**
 * 组件加载时
 */
onMounted(() => {
    if (!dataModelFields) return;
    for (let i = 0; i < dataModelFields.value.length; i++) {
        const element = dataModelFields.value[i];
        if (element.ID != props.componentModel.ID) continue;
        dataModelField.value = element;
    }
});
</script>