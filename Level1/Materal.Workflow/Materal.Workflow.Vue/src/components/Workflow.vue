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
            :stepID="item.stepId" :ref="(ref: IStep<StepModel<IStepData>, IStepData>) => stepNodesInstanceList.push(ref)"
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
import { IStep } from "../scripts/IStep";

const ThenStep = defineAsyncComponent(() => import("./steps/ThenStep.vue"));
const StartStep = defineAsyncComponent(() => import("./steps/StartStep.vue"));
let instance = shallowRef<BrowserJsPlumbInstance>();
const workflowCanvas = ref<HTMLElement>();
const stepList: StepInfoModel[] = [
    new StepInfoModel("业务节点", "Step ThenStep", ThenStep)
];
const stepNodes = shallowReactive<{ component: VNode, stepId: string }[]>([]);
const stepNodesInstanceList = shallowRef<IStep<StepModel<IStepData>, IStepData>[]>([]);
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
/**
 * 从画布移除节点
 * @param StepModel 
 */
const removeStepToCanvas = (StepModel: StepModel<IStepData>) => {
    StepModel.Destroy();//节点模型销毁会移除端点、连接线
    let count = stepNodesInstanceList.value.length;
    for (let i = 0; i < count; i++) {
        const stepNode = stepNodesInstanceList.value[i];
        const stepID = stepNode.GetStepID();
        if (StepModel.ID === stepID){
            stepNodesInstanceList.value.splice(i, 1);//移除节点实例
            break;
        }
    }
    count = stepNodes.length;
    for (let i = 0; i < count; i++) {
        const element = stepNodes[i];
        if (element.stepId !== StepModel.ID) continue;
        stepNodes.splice(i, 1);//从画布上移除节点
        break;
    }
}
/**
 * 绑定下一步
 * @param sourceId
 * @param targetId
 */
const BindNext = (sourceId: string, targetId: string) => {
    let sourceStep: IStep<StepModel<IStepData>, IStepData> | null = null;
    let targetStep: IStep<StepModel<IStepData>, IStepData> | null = null;
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
    let sourceStep: IStep<StepModel<IStepData>, IStepData> | null = null;
    let targetStep: IStep<StepModel<IStepData>, IStepData> | null = null;
    const count = stepNodesInstanceList.value.length;
    for (let i = 0; i < count; i++) {
        const stepNode = stepNodesInstanceList.value[i];
        const stepID = stepNode.GetStepID();
        if (sourceId === stepID) sourceStep = stepNode;
        if (targetId === stepID) targetStep = stepNode;
        if (sourceStep && targetStep) break;
    }
    if (!sourceStep || !targetStep) throw new Error("解绑下一步失败");
    sourceStep.BindNext(undefined);
    targetStep.BindUp(undefined);
}
</script>