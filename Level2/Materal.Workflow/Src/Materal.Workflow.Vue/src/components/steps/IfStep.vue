<style scoped>
.Step {
    position: absolute;
}
</style>
<template>
    <div :id=stepID ref="stepElement" class="Step IfStep" @dblclick="OpenEditModal()" :title="stepModel?.StepData?.Name">
        {{ stepModel?.StepData?.Name }}
        <div class="Point NextPoint" title="下一步"></div>
        <div class="Point StepPoint" title="满足条件执行节点"></div>
        <div class="Point EndPoint" title="上一步"></div>
    </div>
</template>
<script setup lang="ts">
import { onMounted, ref, toRaw, toRef, watch } from 'vue';
import { BrowserJsPlumbInstance } from "@jsplumb/browser-ui";
import { IfStepModel } from '../../scripts/StepModels/IfStepModel';
import { IfStepData } from '../../scripts/StepDatas/IfStepData';
import { IStep } from '../../scripts/IStep';

const emits = defineEmits<{
    (event: "showStepEditModal", stepModel: IfStepModel): void
}>();
const props = defineProps<{ stepID: string, instance?: BrowserJsPlumbInstance }>();
const exposeModel: IStep<IfStepModel, IfStepData> = {
    GetStepModel: (): IfStepModel | undefined => toRaw(stepModel.value),
    GetStepID: (): string => toRaw(id.value)
};
defineExpose(exposeModel);
const instance = toRef(props, "instance");
const id = toRef(props, "stepID");
const stepElement = ref<HTMLElement>();
const stepModel = ref<IfStepModel>();
watch(instance, m => {
    if (!m) return;
    InitPage(m);
});
onMounted(() => {
    if (!instance || !instance.value) return;
    InitPage(instance.value);
});
/**
 * 初始化页面
 * @param canvas 
 */
const InitPage = (canvas: BrowserJsPlumbInstance) => {
    if (!stepElement || !stepElement.value) return;
    stepModel.value = new IfStepModel(id.value, canvas, stepElement.value);
}
/**
 * 打开编辑模态框
 */
const OpenEditModal = () => {
    if (!stepModel.value) return;
    emits("showStepEditModal", stepModel.value);
}
</script>