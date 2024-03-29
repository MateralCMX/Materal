<style scoped>
.steps {
    display: flex;
    overflow-x: auto;
}
</style>
<template>
    <div class="steps">
        <div class="endpoint-plan">
            <a-button shape="circle" :loading="loading" @click="addStepAsync(0)">
                {{ loading ? "" : "+" }}
            </a-button>
        </div>
        <StepOption v-for="(item, index) in allSteps" v-model:loading="loading" :Index="index + 1" :StepData="item"
            @add-step="addStepAsync" @delete-step="deleteStepAsync" @open-node-edit="openNodeDrawer"
            :ref="stepOptions.set" />
    </div>
    <a-drawer v-model:visible="nodeDrawerVisible" title="节点编辑器" :maskClosable="false" width="600px">
        <NodeOption v-if="nodeDrawerVisible" ref="nodeOption" @complate="nodeOptionComplate"></NodeOption>
    </a-drawer>
</template>
<script setup lang="ts">
import StepService from "../services/StepService";
import { Step } from '../models/Step/Step';
import FlowTemplateService from '../services/FlowTemplateService';
import DataModelFieldService from '../services/DataModelFieldService';
import { DataModelField } from '../models/DataModelField/DataModelField';

/**
 * 步骤操作组件组
 */
const stepOptions = useTemplateRefsList();
/**
 * 节点操作组件
 */
const nodeOption = ref<any>();
/**
 * 节点抽屉是否可见
 */
const nodeDrawerVisible = ref(false);
/**
 * 路由
 */
const route = useRoute();
/**
 * 流程模板ID
 */
const flowTemplateID = route.params.id.toString();
/**
 * 数据模型ID
 */
let dataModels: DataModelField[] = [];
/**
 * 所有步骤
 */
const allSteps = ref<Step[]>([]);
/**
 * 加载中
 */
const loading = ref(false);
/**
 * 当前打开节点的步骤ID
 */
let nowStepID = "";
/**
 * 绑定步骤
 */
const bindStepsAsync = async () => {
    const result = await StepService.GetListByFlowTemplateIDAsync(flowTemplateID);
    if (result) {
        allSteps.value = result.Data;
    }
}
/**
 * 绑定数据模型字段
 */
const bindDataModelFields = async () => {
    const result = await FlowTemplateService.GetInfoAsync(flowTemplateID);
    if (result) {
        const dataModelFields = await DataModelFieldService.GetAllListAsync({ PageIndex: 1, PageSize: 10, DataModelID: result.Data.DataModelID });
        if (dataModelFields) {
            dataModels = dataModelFields.Data;
        }
    }
}
/**
 * 添加步骤
 * @param index 
 */
const addStepAsync = async (index?: number) => {
    if (!index && index != 0) return;
    loading.value = true;
    const step = new Step();
    step.Name = "新步骤";
    step.FlowTemplateID = flowTemplateID;
    if (allSteps.value.length > index) {
        step.NextID = allSteps.value[index].ID;
    }
    const upIndex = index - 1;
    if (upIndex >= 0) {
        step.UpID = allSteps.value[upIndex].ID;
    }
    const result = await StepService.AddAsync({ Name: step.Name, FlowTemplateID: step.FlowTemplateID, NextID: step.NextID, UpID: step.UpID });
    if (result && result.Data) {
        step.ID = result.Data;
        if (allSteps.value.length > index) {
            allSteps.value[index].UpID = step.ID;
        }
        if (upIndex >= 0) {
            allSteps.value[upIndex].NextID = step.ID;
        }
        allSteps.value.splice(index, 0, step);
    }
    loading.value = false;
}
/**
 * 删除步骤
 * @param index 
 */
const deleteStepAsync = async (index?: number) => {
    if (!index && index != 0) return;
    allSteps.value.splice(index - 1, 1);
}
/**
 * 打开节点抽屉
 * @param id 
 */
const openNodeDrawer = (stepID: string, id?: string) => {
    nowStepID = stepID;
    nodeDrawerVisible.value = true;
    nextTick(async () => {
        await nodeOption.value.initAsync(dataModels, stepID, id);
    });
}
/**
 * 节点操作完成
 */
const nodeOptionComplate = () => {
    nodeDrawerVisible.value = false;
    stepOptions.value.forEach((item: any) => {
        item.refreshNodes(nowStepID);
    });
}
/**
 * 组件挂载时
 */
onMounted(async () => {
    await bindStepsAsync();
    await bindDataModelFields();
});
</script>