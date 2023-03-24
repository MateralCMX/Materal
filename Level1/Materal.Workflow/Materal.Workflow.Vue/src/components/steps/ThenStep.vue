<style scoped>
.Step {
    position: absolute;
}
</style>
<template>
    <div :id=stepID ref="stepElement" class="Step ThenStep" @dblclick="OpenEditModal()" :title="stepModel?.StepData?.Name">
        {{ stepModel?.StepData?.Name }}
    </div>
    <a-modal v-if="stepData" v-model:visible="editModalVisible" width="1000px" title="业务节点">
        <template #footer>
            <a-button type="primary" @click="CloseEditModal">确定</a-button>
            <a-button type="primary" @click="DeleteStep" danger>删除</a-button>
        </template>
        <ThenStepEdit :step-data="stepData" />
    </a-modal>
</template>
<script setup lang="ts">
import ThenStepEdit from "./ThenStepEdit.vue";
import { onMounted, reactive, ref, shallowRef, toRaw, toRef, UnwrapNestedRefs, watch } from 'vue';
import { BrowserJsPlumbInstance } from "@jsplumb/browser-ui";
import { ThenStepModel } from '../../scripts/StepModels/ThenStepModel';
import { IStepData } from '../../scripts/StepDatas/Base/IStepData';
import { ThenStepData } from '../../scripts/StepDatas/ThenStepData';
import { StepModel } from '../../scripts/StepModels/Base/StepModel';
import { IStep } from '../../scripts/IStep';

const emits = defineEmits<{
    (event: "deleteStep", stepID: ThenStepModel): void
}>();
const props = defineProps<{ stepID: string, instance?: BrowserJsPlumbInstance }>();
const exposeModel: IStep<ThenStepModel, ThenStepData> = {
    GetStepModel: (): ThenStepModel | undefined => toRaw(stepModel.value),
    GetStepID: (): string => toRaw(id.value),
    BindNext: (next?: StepModel<IStepData>): void => stepModel.value?.BindNext(next),
    BindUp: (up?: StepModel<IStepData>): void => stepModel.value?.BindUp(up)
};
defineExpose(exposeModel);
const instance = toRef(props, "instance");
const id = toRef(props, "stepID");
const stepElement = ref<HTMLElement>();
let stepModel = shallowRef<ThenStepModel>();
let stepData: UnwrapNestedRefs<ThenStepData>;
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
 * 初始化页面
 * @param canvas 
 */
const InitPage = (canvas: BrowserJsPlumbInstance) => {
    if (!stepElement || !stepElement.value) return;
    stepModel.value = new ThenStepModel(id.value, canvas, stepElement.value);
    stepData = reactive<ThenStepData>(stepModel.value.StepData)
}
/**
 * 打开编辑模态框
 */
const OpenEditModal = () => editModalVisible.value = true;
/**
 * 关闭编辑模态框
 */
const CloseEditModal = () => editModalVisible.value = false;
/**
 * 删除节点
 */
const DeleteStep = () => {
    if (!stepModel || !stepModel.value) return;
    emits("deleteStep", stepModel.value);
    CloseEditModal();
}
</script>