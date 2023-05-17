<template>
    <a-form :model="modelValue">
        <RequiredProp v-model="modelValue.Props.Required" />
        <ReadonlyProp v-model="modelValue.Props.Readonly" />
        <DisabledProp v-model="modelValue.Props.Disabled" />
        <MoveUpProp @move-up="() => emits('moveUp')" />
        <MoveDownProp @move-down="() => emits('moveDown')" />
        <DeleteProp @delete="() => emits('delete')" />
    </a-form>
</template>
<script setup lang="ts">
import { InputComponentModel } from '../../models/DataTypeComponentModels/InputComponentModel';

/**
 * 暴露成员
 */
defineProps<{ modelValue: InputComponentModel }>();
/**
 * 事件
 */
const emits = defineEmits<{
    (event: "delete"): void
    (event: "moveUp"): void
    (event: "moveDown"): void
}>();
</script>