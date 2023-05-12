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

.step-button-group {
    position: absolute;
    top: 0;
    right: 0;
}
</style>
<template>
    <div class="step">
        <div class="step-body">
            <div class="step-button-group">
                <a-button :loading="Loading" @click="SaveStepAsync"> {{ Loading ? "" : "保存" }}</a-button>
                <a-popconfirm title="确定删除该项?" ok-text="确定" cancel-text="取消" @confirm="DeleteStepAsync">
                    <a-button type="primary" danger :loading="Loading"> {{ Loading ? "" : "删除" }} </a-button>
                </a-popconfirm>
            </div>
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
import StepService from '../services/StepService';

const _props = defineProps<{ StepData: Step; Index: number; Loading: boolean }>();
const _emits = defineEmits<{
    (event: "OnAddStep", index: number): void;
    (event: "OnDeleteStep", index: number): void;
    (event: "update:Loading", value: boolean): void;
}>();

const AddStep = () => {
    _emits("OnAddStep", _props.Index);
}
const DeleteStepAsync = async () => {
    _emits('update:Loading', true);
    await StepService.DeleteAsync(_props.StepData.ID);
    _emits("OnDeleteStep", _props.Index);
    _emits('update:Loading', false);
}
const SaveStepAsync = async () => {
    _emits('update:Loading', true);
    await StepService.EditAsync(_props.StepData);
    _emits('update:Loading', false);
}
</script>