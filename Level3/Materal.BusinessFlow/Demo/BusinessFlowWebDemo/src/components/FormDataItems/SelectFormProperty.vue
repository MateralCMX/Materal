<template>
    <a-form :model="modelValue">
        <RequiredProp v-model="modelValue.Props.Required" />
        <ReadonlyProp v-model="modelValue.Props.Readonly" />
        <DisabledProp v-model="modelValue.Props.Disabled" />
        <a-form-item label="可空" name="CanNull">
            <a-switch v-model:checked="modelValue.Props.CanNull" />
        </a-form-item>
        <MoveUpProp @move-up="() => emits('moveUp')" />
        <MoveDownProp @move-down="() => emits('moveDown')" />
        <DeleteProp @delete="() => emits('delete')" />
    </a-form>
</template>
<script setup lang="ts">
import { SelectComponentModel } from '../../models/DataTypeComponentModels/SelectComponentModel';

/**
 * 暴露成员
 */
defineProps<{ modelValue: SelectComponentModel }>();
/**
 * 事件
 */
const emits = defineEmits<{
    (event: "delete"): void
    (event: "moveUp"): void
    (event: "moveDown"): void
}>();
</script>