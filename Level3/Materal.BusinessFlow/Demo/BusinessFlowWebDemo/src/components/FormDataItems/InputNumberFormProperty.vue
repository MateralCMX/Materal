<template>
    <a-form :model="modelValue">
        <RequiredProp v-model="modelValue.Props.Required" />
        <ReadonlyProp v-model="modelValue.Props.Readonly" />
        <DisabledProp v-model="modelValue.Props.Disabled" />
        <a-form-item label="最小值" name="Min">
            <a-input-number v-model:value="modelValue.Props.Min" />
        </a-form-item>
        <a-form-item label="最大值" name="Max">
            <a-input-number v-model:value="modelValue.Props.Max" />
        </a-form-item>
        <MoveUpProp @move-up="() => emits('moveUp')" />
        <MoveDownProp @move-down="() => emits('moveDown')" />
        <DeleteProp @delete="() => emits('delete')" />
    </a-form>
</template>
<script setup lang="ts">
import { InputNumberComponentModel } from '../../models/DataTypeComponentModels/InputNumberComponentModel';

/**
 * 暴露成员
 */
defineProps<{ modelValue: InputNumberComponentModel }>();
/**
 * 事件
 */
const emits = defineEmits<{
    (event: "delete"): void
    (event: "moveUp"): void
    (event: "moveDown"): void
}>();
</script>