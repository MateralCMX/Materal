<template>
    <div :class="isEdit ? 'option-panel opeion-panel-edit' : 'option-panel'" @click="() => emits('selected', componentModel)">
        <a-form-item :label="dataModelField?.Description ? dataModelField?.Description : dataModelField?.Name"
            :name="dataModelField?.Name"
            :rules="[{ required: componentModel.Props.Required, message: `请输入${dataModelField?.Description ? dataModelField?.Description : dataModelField?.Name}` }]">
            <a-select :readonly="isEdit || componentModel.Props.Readonly" :disabled="componentModel.Props.Disabled" :allow-clear="componentModel.Props.CanNull">
                <a-select-option v-for="option in options" :value="option">{{ option }}</a-select-option>
            </a-select>
        </a-form-item>
    </div>
</template>
<script setup lang="ts">
import { DataModelField } from '../../models/DataModelField/DataModelField';
import { DataTypeComponentModel } from '../../models/DataTypeComponentModels/DataTypeComponentModel';
import { SelectComponentModel } from '../../models/DataTypeComponentModels/SelectComponentModel';

/**
 * 暴露成员
 */
const props = defineProps<{ componentModel: SelectComponentModel, isEdit: boolean }>();
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
 * 选项
 */
const options = ref<string[]>([]);
/**
 * 组件加载时
 */
onMounted(() => {
    if (!dataModelFields) return;
    for (let i = 0; i < dataModelFields.value.length; i++) {
        const element = dataModelFields.value[i];
        if (element.ID != props.componentModel.ID) continue;
        dataModelField.value = element;
        options.value = JSON.parse(dataModelField.value.Data ?? '[]');
    }
});
</script>