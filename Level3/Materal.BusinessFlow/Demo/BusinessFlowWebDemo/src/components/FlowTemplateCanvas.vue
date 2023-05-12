<style scoped>
.steps {
    display: flex;
    overflow-x: auto;
}
</style>
<template>
    <div class="steps">
        <div class="endpoint-plan">
            <a-button shape="circle" :loading="_loading" @click="AddStepAsync(0)">
                {{ _loading ? "" : "+" }}
            </a-button>
        </div>
        <StepOption v-for="(item, index) in _allSteps" :Loading="_loading" :Index="index + 1" :StepData="item" @OnAddStep="AddStepAsync" />
    </div>
</template>
<script setup lang="ts">
import { useRoute } from 'vue-router';
import { onMounted, ref } from "vue";
import StepService from "../services/StepService";
import { Step } from '../models/Step/Step';

const _route = useRoute();
const _flowTemplateID = _route.params.id.toString();
const _allSteps = ref<Step[]>([]);
const _loading = ref(false);

const BindStepsAsync = async () => {
    const result = await StepService.GetAllListAsync({
        PageIndex: 1,
        PageSize: 1000,
        FlowTemplateID: _flowTemplateID
    });
    if (result) {
        _allSteps.value = result.Data;
    }
}
const AddStepAsync = async (index?: number) => {
    if (!index && index != 0) return;
    _loading.value = true;
    const step = new Step();
    step.Name = "新步骤";
    step.FlowTemplateID = _flowTemplateID;
    if (_allSteps.value.length > index) {
        step.NextID = _allSteps.value[index].ID;
    }
    const upIndex = index - 1;
    if (upIndex >= 0) {
        step.UpID = _allSteps.value[upIndex].ID;
    }
    const result = await StepService.AddAsync({ Name: step.Name, FlowTemplateID: step.FlowTemplateID, NextID: step.NextID, UpID: step.UpID });
    if (result && result.Data) {
        step.ID = result.Data;
        if (_allSteps.value.length > index) {
            _allSteps.value[index].UpID = step.ID;
        }
        if (upIndex >= 0) {
            _allSteps.value[upIndex].NextID = step.ID;
        }
        _allSteps.value.splice(index, 0, step);
    }
    _loading.value = false;
}
onMounted(async () => { await BindStepsAsync(); });
</script>