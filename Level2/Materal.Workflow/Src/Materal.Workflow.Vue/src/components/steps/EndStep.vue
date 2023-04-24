<style scoped>
.Step {
    position: absolute;
}
</style>
<template>
    <div :id=stepID ref="stepElement" class="Step EndStep" @dblclick="OpenEditModal()" :title="stepModel?.StepData?.Name">
        {{ stepModel?.StepData?.Name }}
        <div class="Point EndPoint" title="上一步"></div>
    </div>
</template>
<script setup lang="ts">
import { onMounted, ref, toRaw, toRef, watch } from 'vue';
import { BrowserJsPlumbInstance } from "@jsplumb/browser-ui";
import { EndStepModel } from '../../scripts/StepModels/EndStepModel';
import { EndStepData } from '../../scripts/StepDatas/EndStepData';
import { IStep } from '../../scripts/IStep';

const emits = defineEmits<{
    (event: "showStepEditModal", stepModel: EndStepModel): void
}>();
const props = defineProps<{ stepID: string, instance?: BrowserJsPlumbInstance }>();
const exposeModel: IStep<EndStepModel, EndStepData> = {
    GetStepModel: (): EndStepModel | undefined => toRaw(stepModel.value),
    GetStepID: (): string => toRaw(id.value)
};
defineExpose(exposeModel);
const instance = toRef(props, "instance");
const id = toRef(props, "stepID");
const stepElement = ref<HTMLElement>();
const stepModel = ref<EndStepModel>();
watch(instance, m => {
    if (!m) return;
    InitPage(m);
});
onMounted(() => {
    if (!instance || !instance.value) return;
    InitPage(instance.value);
});
/**
 * 初始化界面
 * @param canvas 
 */
const InitPage = (canvas: BrowserJsPlumbInstance) => {
    if (!stepElement || !stepElement.value) return;
    stepModel.value = new EndStepModel(id.value, canvas, stepElement.value);
};
/**
 * 打开编辑模态框
 */
const OpenEditModal = () => {
    if (!stepModel.value) return;
    emits("showStepEditModal", stepModel.value);
}
</script>