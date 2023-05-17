<template>
    <a-form :model="modelValue">
        <ReadonlyProp v-model="modelValue.Props.Readonly" />
        <DisabledProp v-model="modelValue.Props.Disabled" />
        <MoveUpProp @move-up="() => emits('moveUp')" />
        <MoveDownProp @move-down="() => emits('moveDown')" />
        <DeleteProp @delete="() => emits('delete')" />
    </a-form>
</template>
<script setup lang="ts">
import { SwitchComponentModel } from '../../models/DataTypeComponentModels/SwitchComponentModel';

/**
 * 暴露成员
 */
defineProps<{ modelValue: SwitchComponentModel }>();
/**
 * 事件
 */
const emits = defineEmits<{
    (event: "delete"): void
    (event: "moveUp"): void
    (event: "moveDown"): void
}>();
</script>