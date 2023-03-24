<style scoped>
.Canvas {
    position: absolute;
    top: 0;
    bottom: 0;
    left: 175px;
    right: 0;
    background-color: #F7F7F7;
}

.Steps {
    position: absolute;
    width: 175px;
    height: 100%;
    overflow: auto;
    top: 0;
    bottom: 0;
    left: 0;
}

.Steps>.Step {
    margin: 10px auto 0;
}
</style>
<template>
    <div class="Steps">
        <div v-for="item in stepList" :key="item.Name" :class="item.Style" @click="addStepToCanvas(item)">
            {{ item.Name }}
        </div>
    </div>
    <div ref="workflowCanvas" class="Canvas">
        <component v-for="item in stepNodes" :is="item.component" :key="item.stepId" :instance="instance"
            :stepID="item.stepId" :ref="(ref: IStep) => stepNodesInstanceList.push(ref)"
            @deleteStep="removeStepToCanvas($event)" />
    </div>
</template>
<script setup lang="ts">
import "../css/Step.css";
import { defineAsyncComponent, onMounted, ref, shallowReactive, shallowRef, VNode } from 'vue';
import { BrowserJsPlumbInstance, newInstance, ContainmentType } from "@jsplumb/browser-ui";
import { StepInfoModel as StepInfoModel } from "../scripts/StepInfoModel";
import { EVENT_CONNECTION, EVENT_CONNECTION_DETACHED } from "@jsplumb/core";
import { StepModel } from "../scripts/StepModels/Base/StepModel";
import { IStepData } from "../scripts/StepDatas/Base/IStepData";

const ThenStep = defineAsyncComponent(() => import("./steps/ThenStep.vue"));
const StartStep = defineAsyncComponent(() => import("./steps/StartStep.vue"));
let instance = shallowRef<BrowserJsPlumbInstance>();
const workflowCanvas = ref<HTMLElement>();
const stepList: StepInfoModel[] = [
    new StepInfoModel("业务节点", "Step ThenStep", ThenStep)
];
const stepNodes = shallowReactive<{ component: VNode, stepId: string }[]>([]);
const stepNodesInstanceList = shallowRef<IStep[]>([]);
let stepIndex = 0;

onMounted(() => {
    initCanvas();
});
/**
 * 初始化画布
 */
const initCanvas = () => {
    if (!workflowCanvas || !workflowCanvas.value) return;
    instance.value = newInstance({
        container: workflowCanvas.value,
        dragOptions: {
            containment: ContainmentType.parentEnclosed,
            grid: { w: 10, h: 10 }
        }
    });
    instance.value.bind(EVENT_CONNECTION, (params) => BindNext(params.sourceId, params.targetId));
    instance.value.bind(EVENT_CONNECTION_DETACHED, (params) => UnbindNext(params.sourceId, params.targetId));
    addStepToCanvas(new StepInfoModel("开始节点", "Step StartStep", StartStep));
}
/**
 * 添加节点到画布
 * @param item 
 */
const addStepToCanvas = (item: StepInfoModel) => {
    stepNodes.push({
        component: item.Component as any,
        stepId: `step${stepIndex++}`
    });
}
const removeStepToCanvas = (StepModel: StepModel<IStepData>) => {
    for (let i = 0; i < stepNodes.length; i++) {
        const element = stepNodes[i];
        if (element.stepId !== StepModel.ID) continue;
        stepNodes.splice(i, 1);
        break;
    }
    const count = stepNodesInstanceList.value.length;
    for (let i = 0; i < count; i++) {
        const stepNode = stepNodesInstanceList.value[i];
        const stepID = stepNode.GetStepID();
        if (StepModel.ID === stepID){
            stepNodesInstanceList.value.splice(i, 1);
            break;
        }
    }
    StepModel.Destroy();
}
/**
 * 绑定下一步
 * @param sourceId
 * @param targetId
 */
const BindNext = (sourceId: string, targetId: string) => {
    let sourceStep: IStep | null = null;
    let targetStep: IStep | null = null;
    const count = stepNodesInstanceList.value.length;
    for (let i = 0; i < count; i++) {
        const stepNode = stepNodesInstanceList.value[i];
        if(!stepNode) continue;
        const stepID = stepNode.GetStepID();
        if (sourceId === stepID) sourceStep = stepNode;
        if (targetId === stepID) targetStep = stepNode;
        if (sourceStep && targetStep) break;
    }
    if (!sourceStep || !targetStep) throw new Error("绑定下一步失败");
    const targetStepData = targetStep.GetStepModel();
    const sourceStepData = sourceStep.GetStepModel();
    sourceStep.BindNext(targetStepData);
    targetStep.BindUp(sourceStepData);
}
/**
 * 解绑下一步
 * @param sourceId 
 * @param targetId
 */
const UnbindNext = (sourceId: string, targetId: string) => {
    let sourceStep: IStep | null = null;
    let targetStep: IStep | null = null;
    const count = stepNodesInstanceList.value.length;
    for (let i = 0; i < count; i++) {
        const stepNode = stepNodesInstanceList.value[i];
        const stepID = stepNode.GetStepID();
        if (sourceId === stepID) sourceStep = stepNode;
        if (targetId === stepID) targetStep = stepNode;
        if (sourceStep && targetStep) break;
    }
    if (!sourceStep || !targetStep) throw new Error("绑定下一步失败");
    sourceStep.BindNext(undefined);
    targetStep.BindUp(undefined);
}
interface IStep {
    GetStepModel: () => StepModel<any>,
    GetStepID: () => string,
    BindNext: (next?: StepModel<any>) => void,
    BindUp: (next?: StepModel<any>) => void,
}
</script>