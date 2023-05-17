<template>
    <div :class="isEdit ? 'option-panel opeion-panel-edit' : 'option-panel'" @click="() => emits('selected', modelValue)">
        <a-form-item :label="dataModelField?.Description ? dataModelField?.Description : dataModelField?.Name"
            :name="dataModelField?.Name">
            <a-switch :readonly="isEdit || modelValue.Props.Readonly" :disabled="modelValue.Props.Disabled" />
        </a-form-item>
    </div>
</template>
<script setup lang="ts">
import { DataModelField } from '../../models/DataModelField/DataModelField';
import { DataTypeComponentModel } from '../../models/DataTypeComponentModels/DataTypeComponentModel';
import { SwitchComponentModel } from '../../models/DataTypeComponentModels/SwitchComponentModel';

/**
 * 暴露成员
 */
const props = defineProps<{ modelValue: SwitchComponentModel, isEdit: boolean }>();
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
        if (element.ID != props.modelValue.ID) continue;
        dataModelField.value = element;
    }
});
</script>