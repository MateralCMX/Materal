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
    </a-form>
</template>
<script setup lang="ts">
import { ParallelStepData } from '../../scripts/StepDatas/ParallelStepData';
defineProps<{ stepData: ParallelStepData }>();
</script>