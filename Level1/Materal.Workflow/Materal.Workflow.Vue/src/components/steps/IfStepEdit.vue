<template>
    <a-form v-if="stepData" :model="stepData" :label-col="{ span: 2 }" :wrapper-col="{ span: 22 }" autocomplete="off">
        <a-form-item label="名称">
            <a-input v-model:value="stepData.Name" />
        </a-form-item>
        <a-form-item label="描述">
            <a-input v-model:value="stepData.Description" />
        </a-form-item>
        <BuildDataEdit :stepData="stepData" />
        <a-form-item label="决策条件">
            <a-button type="primary" @click="NewConditionItem">+</a-button>
        </a-form-item>
        <a-form-item v-for="(item, index) in stepData.Conditions" :label="`条件${index}`">
            <ConditionItem :step-data="stepData" :index="index" :condition="item" @remove="RemoveConditionItem" />
        </a-form-item>
    </a-form>
</template>
<script setup lang="ts">
import BuildDataEdit from './BuildDataEdit.vue';
import ConditionItem from './ConditionItem.vue';
import { IfStepData } from '../../scripts/StepDatas/IfStepData';
import { DecisionConditionData } from '../../scripts/StepDatas/Base/DecisionConditionData';
import { ref } from 'vue';

const props = defineProps<{ stepData: IfStepData }>();
const conditions = props.stepData.Conditions;
const ConditionTypes = ref<Array<"String" | "Number">>([]);
const NewConditionItem = () => {
    conditions.push(new DecisionConditionData());
    ConditionTypes.value.push('String');
}
const RemoveConditionItem = (index: number) => {
    conditions.splice(index, 1);
}
</script>