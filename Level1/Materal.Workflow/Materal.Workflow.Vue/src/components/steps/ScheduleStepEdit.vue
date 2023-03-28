<style scoped>
.ant-input-number {
    width: 100%;
}
</style>

<template>
    <a-form v-if="stepData" :model="stepData" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }" autocomplete="off">
        <a-form-item label="名称">
            <a-input v-model:value="stepData.Name" />
        </a-form-item>
        <a-form-item label="描述">
            <a-input v-model:value="stepData.Description" />
        </a-form-item>
        <a-form-item label="计划时间">
            <InputTimeSpan v-model="stepData.DelayValue" />
        </a-form-item>
    </a-form>
</template>
<script setup lang="ts">
import InputTimeSpan from '../InputTimeSpan.vue';
import { ScheduleStepData } from '../../scripts/StepDatas/ScheduleStepData';
defineProps<{ stepData: ScheduleStepData }>();
</script>