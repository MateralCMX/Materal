<style scoped>
.Step {
    position: absolute;
}
</style>
<template>
    <div :id=stepID ref="stepElement" class="Step StartStep" @dblclick="OpenEditModal()" :title="stepModel?.StepData?.Name">
        {{ stepModel?.StepData?.Name }}
    </div>
    <a-modal v-if="stepData" v-model:visible="editModalVisible" width="1000px" title="开始节点">
        <template #footer>
            <a-button type="primary" @click="CloseEditModal">确定</a-button>
        </template>
        <StartStepEdit :step-data="stepData" />
    </a-modal>
</template>
<script setup lang="ts">
import StartStepEdit from "./StartStepEdit.vue";
import { onMounted, reactive, ref, shallowRef, toRaw, toRef, UnwrapNestedRefs, watch } from 'vue';
import { BrowserJsPlumbInstance } from "@jsplumb/browser-ui";
import { StartStepModel } from '../../scripts/StepModels/StartStepModel';
import { IStepData } from '../../scripts/StepDatas/Base/IStepData';
import { StartStepData } from '../../scripts/StepDatas/StartStepData';
import { StepModel } from '../../scripts/StepModels/Base/StepModel';
import { IStep } from '../../scripts/IStep';

const emits = defineEmits<{
    (event: "deleteStep", stepID: StartStepModel): void
}>();
const props = defineProps<{ stepID: string, instance?: BrowserJsPlumbInstance }>();
const exposeModel: IStep<StartStepModel, StartStepData> = {
    GetStepModel: (): StartStepModel | undefined => toRaw(stepModel.value),
    GetStepID: (): string => toRaw(id.value),
    BindNext: (next?: StepModel<IStepData>): void => stepModel.value?.BindNext(next),
    BindUp: (up?: StepModel<IStepData>): void => { }
};
defineExpose(exposeModel);
const instance = toRef(props, "instance");
const id = toRef(props, "stepID");
const stepElement = ref<HTMLElement>();
let stepModel = shallowRef<StartStepModel>();
let stepData: UnwrapNestedRefs<StartStepData>;
let editModalVisible = ref<boolean>(false);
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
    stepModel.value = new StartStepModel(id.value, canvas, stepElement.value);
    stepData = reactive<StartStepData>(stepModel.value.StepData)
};
/**
 * 打开编辑模态框
 */
const OpenEditModal = () => {
    editModalVisible.value = true;
    console.log(stepModel.value);
}
/**
 * 关闭编辑模态框
 */
const CloseEditModal = () => {
    editModalVisible.value = false;
}
</script>