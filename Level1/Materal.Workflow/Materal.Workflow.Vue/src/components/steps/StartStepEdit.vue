<template>
    <a-form :model="stepData" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }" autocomplete="off">
        <a-form-item label="名称">
            <a-input v-model:value="stepData.Name" />
        </a-form-item>
        <a-form-item label="描述">
            <a-input v-model:value="stepData.Description" />
        </a-form-item>
        <a-form-item label="Json数据">
            {{ JSON.stringify(stepData) }}
        </a-form-item>
    </a-form>
</template>
<script setup lang="ts">
import { StartStepData } from '../../scripts/StepDatas/StartStepData';

defineProps<{ stepData: StartStepData }>();
</script>