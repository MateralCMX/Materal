<template>
    <a-card>
        <a-row :gutter="[10, 10]">
            <DecisionCondition :step-data="stepData" v-model:condition-type="conditionType" v-model:value="condition.LeftValue" v-model:value-source="condition.LeftValueSource" />
            <a-col :span="11">
                <a-select v-model:value="condition.ComparisonType">
                    <a-select-option :value="ComparisonTypeEnum.Equal">等于</a-select-option>
                    <a-select-option :value="ComparisonTypeEnum.NotEqual">不等于</a-select-option>
                    <a-select-option :value="ComparisonTypeEnum.LessThan">小于</a-select-option>
                    <a-select-option :value="ComparisonTypeEnum.LessThanOrEqual">小于等于</a-select-option>
                    <a-select-option :value="ComparisonTypeEnum.GreaterThan">大于</a-select-option>
                    <a-select-option :value="ComparisonTypeEnum.GreaterThanOrEqual">大于等于</a-select-option>
                </a-select>
            </a-col>
            <a-col :span="11">
                <a-select v-model:value="condition.Condition">
                    <a-select-option :value="ConditionEnum.And">并且</a-select-option>
                    <a-select-option :value="ConditionEnum.Or">或者</a-select-option>
                </a-select>
            </a-col>
            <a-col :span="2">
                <a-button type="primary" danger @click="RemoveConditionItem(index)">X</a-button>
            </a-col>
            <DecisionCondition :step-data="stepData" v-model:condition-type="conditionType" v-model:value="condition.RightValue" v-model:value-source="condition.RightValueSource" />
        </a-row>
    </a-card>
</template>
<script setup lang="ts">
import DecisionCondition from './DecisionCondition.vue'
import { IfStepData } from '../../scripts/StepDatas/IfStepData';
import { DecisionConditionData } from '../../scripts/StepDatas/Base/DecisionConditionData';
import { ComparisonTypeEnum } from "../../scripts/StepDatas/Base/ComparisonTypeEnum";
import { ConditionEnum } from "../../scripts/StepDatas/Base/ConditionEnum";
import { onMounted, ref } from 'vue';

const props = defineProps<{ index: number, condition: DecisionConditionData, stepData: IfStepData }>();
const conditionType = ref<"String" | "Number">("String");
onMounted(() => {
    if (typeof props.condition.LeftValue === 'number') {
        conditionType.value = 'Number';
    }
    else {
        conditionType.value = 'String';
    }
});
const emits = defineEmits<{
    (event: 'remove', index: number): void;
}>();
const RemoveConditionItem = (index: number) => {
    emits('remove', index);
}
</script>