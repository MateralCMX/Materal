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
                <a-button :loading="Loading" @click="saveStepAsync"> {{ Loading ? "" : "保存" }}</a-button>
                <a-popconfirm title="确定删除该项?" ok-text="确定" cancel-text="取消" @confirm="deleteStepAsync">
                    <a-button type="primary" danger :loading="Loading"> {{ Loading ? "" : "删除" }} </a-button>
                </a-popconfirm>
            </div>
            <a-space direction="vertical" style="width: 100%; text-align: center;">
                <a-input v-model:value="StepData.Name" placeholder="步骤名称" class="step-name" :disabled="Loading" />
                <a-button shape="circle" :loading="Loading" @click="() => openNodeEdit()">
                    {{ Loading ? "" : "+" }}
                </a-button>
            </a-space>
        </div>
        <div class="endpoint-plan">
            <a-button shape="circle" :loading="Loading" @click="addStep">
                {{ Loading ? "" : "+" }}
            </a-button>
        </div>
    </div>
</template>
<script setup lang="ts">
import { Step } from '../models/Step/Step';
import StepService from '../services/StepService';

/**
 * 暴露成员
 */
const props = defineProps<{ StepData: Step; Index: number; Loading: boolean }>();
/**
 * 事件
 */
const emits = defineEmits<{
    (event: "addStep", index: number): void;
    (event: "deleteStep", index: number): void;
    (event: "openNodeEdit", stepID: string, id: string | undefined): void;
    (event: "update:Loading", value: boolean): void;
}>();
/**
 * 添加步骤
 */
const addStep = () => {
    emits("addStep", props.Index);
}
/**
 * 删除步骤
 */
const deleteStepAsync = async () => {
    emits('update:Loading', true);
    await StepService.DeleteAsync(props.StepData.ID);
    emits("deleteStep", props.Index);
    emits('update:Loading', false);
}
/**
 * 保存步骤
 */
const saveStepAsync = async () => {
    emits('update:Loading', true);
    await StepService.EditAsync(props.StepData);
    emits('update:Loading', false);
}
/**
 * 打开节点编辑器
 * @param id 
 */
const openNodeEdit = (id?: string) => {
    emits('openNodeEdit', props.StepData.ID, id);
}
</script>