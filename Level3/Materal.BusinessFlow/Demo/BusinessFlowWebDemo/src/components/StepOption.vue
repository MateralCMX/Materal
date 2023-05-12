<style scoped>
.step {
    display: flex;
}

.step-body {
    width: 400px;
    height: 800px;
    overflow-y: auto;
    overflow-wrap: break-word;
    border-radius: 5px;
    background-color: #f5f5f5;
    padding: 45px 15px 15px;
    position: relative;
}

.step-remove-button {
    position: absolute;
    top: 0;
    right: 0;
}
</style>
<template>
    <div class="step">
        <div class="step-body">
            <a-button type="primary" class="step-remove-button" danger :loading="Loading">
                {{ Loading ? "" : "X" }}
            </a-button>
            <div>
                <a-input v-model:value="StepData.Name" placeholder="步骤名称" class="step-name" :disabled="Loading" />
            </div>
        </div>
        <div class="endpoint-plan">
            <a-button shape="circle" :loading="Loading" @click="AddStep">
                {{ Loading ? "" : "+" }}
            </a-button>
        </div>
    </div>
</template>
<script setup lang="ts">
import { Step } from '../models/Step/Step';

const _props = defineProps<{ StepData: Step; Index: number; Loading: boolean }>();
const _emits = defineEmits<{
    (event: "OnAddStep", index: number): void;
}>();

const AddStep = () => {
    _emits("OnAddStep", _props.Index);
}
</script>