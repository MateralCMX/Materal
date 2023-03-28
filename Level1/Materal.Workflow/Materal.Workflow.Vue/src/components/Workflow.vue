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
    overflow: auto;
    top: 0;
    bottom: 0;
    left: 0;
}

.Steps>.Step {
    margin: 10px auto 0;
}

.Steps>.Step>.Point {
    display: none;
}
</style>
<template>
    <div class="Steps">
        <a-button type="primary" @click="ShowRuntimeDataEditModal">运行时数据结构</a-button>
        <div v-for="item in stepList" :key="item.Name" :class="item.Style" @click="AddStepToCanvas(item)">
            {{ item.Name }}
        </div>
    </div>
    <div ref="workflowCanvas" class="Canvas">
        <component v-for="item in stepNodes" :is="item.component" :key="item.stepId" :instance="instance"
            :stepID="item.stepId" :ref="PushStepNodesInstanceList" @showStepEditModal="ShowStepEditModal($event)" />
    </div>
    <a-modal v-model:visible="editModalVisible" width="1000px" title="编辑节点" destroyOnClose>
        <template #footer>
            <a-button v-if="stepCanDelete" type="primary" @click="DeleteStep" danger>删除</a-button>
            <a-button type="primary" @click="CloseStepEditModal">确定</a-button>
        </template>
        <component :is="editComponent" :step-data="editStepData" />
    </a-modal>
    <a-modal v-model:visible="runtimeDataEditModalVisible" title="运行时数据结构编辑">
        <template #footer>
            <a-button type="primary" @click="CloseRuntimeDataEditModal">确定</a-button>
        </template>
        <RunTimeDataEdit :run-time-data-type="runTimeDataType" />
    </a-modal>
</template>
<script setup lang="ts">
import "../css/Step.css";
import RunTimeDataEdit from "./RunTimeDataEdit.vue";
import { defineAsyncComponent, onMounted, reactive, ref, shallowReactive, shallowRef, UnwrapNestedRefs, VNode } from 'vue';
import { BrowserJsPlumbInstance, newInstance, ContainmentType } from "@jsplumb/browser-ui";
import { StepInfoModel as StepInfoModel } from "../scripts/StepInfoModel";
import { BeforeDropParams, ConnectionDetachedParams, DotEndpoint, EVENT_CONNECTION_DETACHED, INTERCEPT_BEFORE_DROP, RectangleEndpoint } from "@jsplumb/core";
import { StepModel } from "../scripts/StepModels/Base/StepModel";
import { StartStepModel } from "../scripts/StepModels/StartStepModel";
import { ThenStepModel } from "../scripts/StepModels/ThenStepModel";
import { IStepData } from "../scripts/StepDatas/Base/IStepData";
import { IStep } from "../scripts/IStep";
import { NowRuntimeDataType } from "../scripts/RuntimeDataType";
import { DelayStepModel } from "../scripts/StepModels/DelayStepModel";

const StartStep = defineAsyncComponent(() => import("./steps/StartStep.vue"));
const StartStepEdit = defineAsyncComponent(() => import("./steps/StartStepEdit.vue"));
const ThenStep = defineAsyncComponent(() => import("./steps/ThenStep.vue"));
const ThenStepEdit = defineAsyncComponent(() => import("./steps/ThenStepEdit.vue"));
const DelayStep = defineAsyncComponent(() => import("./steps/DelayStep.vue"));
const DelayStepEdit = defineAsyncComponent(() => import("./steps/DelayStepEdit.vue"));
let instance = shallowRef<BrowserJsPlumbInstance>();
const workflowCanvas = ref<HTMLElement>();
const stepList: StepInfoModel[] = [
    new StepInfoModel("业务节点", "Step ThenStep", ThenStep),
    new StepInfoModel("延时节点", "Step DelayStep", DelayStep),
];
const stepNodes = shallowReactive<{ component: VNode, stepId: string }[]>([]);
const stepNodesInstanceList = shallowRef<IStep<StepModel<IStepData>, IStepData>[]>([]);
let stepIndex = 0;
const runtimeDataEditModalVisible = ref<boolean>(false);
const editModalVisible = ref<boolean>(false);
const stepCanDelete = ref<boolean>(false);
const editStepData = ref<IStepData>();
let editStepModel: StepModel<IStepData> | undefined;
let editComponent: VNode | undefined;
const runTimeDataType = ref(NowRuntimeDataType);//运行时数据类型

onMounted(() => {
    InitCanvas();
});
/**
 * 初始化画布
 */
const InitCanvas = () => {
    if (!workflowCanvas || !workflowCanvas.value) return;
    instance.value = newInstance({
        container: workflowCanvas.value,
        dragOptions: {
            containment: ContainmentType.parentEnclosed,
            grid: { w: 10, h: 10 }
        }
    });
    instance.value.addSourceSelector(".NextPoint", {
        source: true,
        target: false,
        anchor: "Continuous",
        endpoint: DotEndpoint.type,
        connectorClass: "NextConnector"
    });
    instance.value.addSourceSelector(".CompensatePoint", {
        source: true,
        target: false,
        anchor: "Continuous",
        endpoint: DotEndpoint.type,
        connectorClass: "CompensateConnector"
    });
    instance.value.addTargetSelector(".EndPoint", {
        source: false,
        target: true,
        anchor: "Continuous",
        endpoint: RectangleEndpoint.type
    });
    instance.value.bind(INTERCEPT_BEFORE_DROP, (params: BeforeDropParams) => HandlerConnection(params));
    instance.value.bind(EVENT_CONNECTION_DETACHED, (params: ConnectionDetachedParams) => HandlerDisconnectioned(params));
    AddStepToCanvas(new StepInfoModel("开始节点", "Step StartStep", StartStep));
    AddStepToCanvas(new StepInfoModel("业务节点", "Step ThenStep", ThenStep));
}
/**
 * 添加节点到画布
 * @param item 
 */
const AddStepToCanvas = (item: StepInfoModel) => {
    stepNodes.push({
        component: item.Component as any,
        stepId: `step${stepIndex++}`
    });
}
/**
 * 添加节点实例到列表
 * @param item 
 */
const PushStepNodesInstanceList = (item: IStep<StepModel<IStepData>, IStepData> | null) => {
    if (!item) return;
    stepNodesInstanceList.value.push(item);
}
/**
 * 从画布移除节点
 * @param stepModel 
 */
const RemoveStepToCanvas = (stepModel: StepModel<IStepData>) => {
    stepModel.Destroy();//节点模型销毁会移除端点、连接线
    let count = stepNodesInstanceList.value.length;
    for (let i = 0; i < count; i++) {
        const stepNode = stepNodesInstanceList.value[i];
        if (!stepNode) continue;
        const stepID = stepNode.GetStepID();
        if (stepModel.ID === stepID) {
            stepNodesInstanceList.value.splice(i, 1);//移除节点实例
            break;
        }
    }
    for (let i = 0; i < stepNodes.length; i++) {
        const element = stepNodes[i];
        if (element.stepId !== stepModel.ID) continue;
        stepNodes.splice(i, 1);//从画布上移除节点
        break;
    }
}
/**
 * 连线
 * @param params
 */
const HandlerConnection = (params: BeforeDropParams): boolean => {
    if (params.sourceId === params.targetId) return false;
    const stepModel = GetStepModel(params.sourceId, params.targetId);
    return stepModel.sourceStepModel.HandlerConnection(params.connection, stepModel.targetStepModel);
}
/**
 * 处理连线断开
 * @param params
 */
const HandlerDisconnectioned = (params: ConnectionDetachedParams) => {
    if (params.sourceId === params.targetId) return;
    const stepModel = GetStepModel(params.sourceId, params.targetId);
    stepModel.sourceStepModel.HandlerDisconnection(params.connection, stepModel.targetStepModel);
}
const GetStepModel = (sourceId: string, targetId: string): { sourceStepModel: StepModel<IStepData>, targetStepModel: StepModel<IStepData> } => {
    let sourceStep: IStep<StepModel<IStepData>, IStepData> | null = null;
    let targetStep: IStep<StepModel<IStepData>, IStepData> | null = null;
    const count = stepNodesInstanceList.value.length;
    for (let i = 0; i < count; i++) {
        const stepNode = stepNodesInstanceList.value[i];
        if (!stepNode) continue;
        const stepID = stepNode.GetStepID();
        if (sourceId === stepID) sourceStep = stepNode;
        if (targetId === stepID) targetStep = stepNode;
        if (sourceStep && targetStep) break;
    }
    if (!sourceStep || !targetStep) throw new Error("获取节点失败");
    const targetStepModel = targetStep.GetStepModel();
    const sourceStepModel = sourceStep.GetStepModel();
    if (!targetStepModel || !sourceStepModel) throw new Error("获取节点模型失败");
    return { sourceStepModel, targetStepModel };
}

/**
 * 显示节点编辑弹窗
 * @param stepModel 
 */
const ShowStepEditModal = (stepModel: StepModel<IStepData>) => {
    editStepModel = stepModel;
    editStepData.value = stepModel.StepData;
    switch (stepModel.StepModelTypeName) {
        case `${StartStepModel.name}`:
            editComponent = StartStepEdit as any;
            stepCanDelete.value = false;
            break;
        case `${ThenStepModel.name}`:
            editComponent = ThenStepEdit as any;
            stepCanDelete.value = true;
            break;
        case `${DelayStepModel.name}`:
            editComponent = DelayStepEdit as any;
            stepCanDelete.value = true;
            break;
    }
    editModalVisible.value = true;
}
/**
 * 关闭节点编辑弹窗
 */
const CloseStepEditModal = () => {
    editModalVisible.value = false;
}
/**
 * 删除节点
 */
const DeleteStep = () => {
    if (!editStepModel) return;
    RemoveStepToCanvas(editStepModel);
    CloseStepEditModal();
}
/**
 * 显示运行时数据编辑弹窗
 */
const ShowRuntimeDataEditModal = () => {
    runtimeDataEditModalVisible.value = true;
}
/**
 * 关闭运行时数据编辑弹窗
 */
const CloseRuntimeDataEditModal = () => {
    runtimeDataEditModalVisible.value = false;
}
</script>