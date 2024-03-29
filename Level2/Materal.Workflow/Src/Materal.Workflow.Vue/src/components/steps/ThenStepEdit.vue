<style scoped>
.ant-input-number {
    width: 100%;
}
</style>

<template>
    <a-form v-if="stepData" :model="stepData" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }" autocomplete="off">
        <a-form-item label="名称">
            <a-input v-model:value="stepData.Name" />
        </a-form-item>
        <a-form-item label="描述">
            <a-input v-model:value="stepData.Description" />
        </a-form-item>
        <BuildDataEdit :stepData="stepData" />
        <a-form-item label="节点体">
            <a-select v-model:value="stepData.StepBodyType" @change="StepBodyTypeChangeValue">
                <a-select-option v-for="item in AllStepBodys" :value="item.Name">{{ item.Name }}</a-select-option>
            </a-select>
        </a-form-item>
        <a-form-item label="输入">
            <a-button type="primary" @click="NewInputDataItem">+</a-button>
        </a-form-item>
        <a-form-item v-for="(item, index) in stepData.Inputs" :label="`输入${index}`">
            <a-row :gutter="[16, 16]">
                <a-col :span="6">
                    <a-select v-model:value="item.StepProperty">
                        <a-select-option v-for="arg in stepBodyArgs" :value="arg.Name">{{ arg.Description
                        }}</a-select-option>
                    </a-select>
                </a-col>
                <a-col :span="6">
                    <a-select v-model:value="item.ValueSource">
                        <a-select-option :value="InputValueSourceEnum.Constant">常量</a-select-option>
                        <a-select-option :value="InputValueSourceEnum.BuildDataProperty">构建数据</a-select-option>
                        <a-select-option :value="InputValueSourceEnum.RuntimeDataProperty">运行时数据</a-select-option>
                    </a-select>
                </a-col>
                <a-col :span="10">
                    <a-select v-if="item.ValueSource === InputValueSourceEnum.BuildDataProperty" v-model:value="item.Value">
                        <a-select-option v-for="property in stepData.BuildDatas.Properties" :value="property.Name">
                            {{ property.Name }}
                        </a-select-option>
                    </a-select>
                    <a-select v-else-if="item.ValueSource === InputValueSourceEnum.RuntimeDataProperty"
                        v-model:value="item.Value">
                        <a-select-option v-for="property in NowRuntimeDataType.Properties" :value="property.Name">
                            {{ property.Name }}
                        </a-select-option>
                    </a-select>
                    <a-input
                        v-else-if="item.ValueSource === InputValueSourceEnum.Constant && GetInputConstantType(index) === 'String'"
                        v-model:value="item.Value" />
                    <a-input-number v-else v-model:value="item.Value" />
                </a-col>
                <a-col :span="2">
                    <a-button type="primary" danger @click="RemoveInputDataItem(index)">X</a-button>
                </a-col>
            </a-row>
        </a-form-item>
        <a-form-item label="输出">
            <a-button type="primary" @click="NewOutputDataItem">+</a-button>
        </a-form-item>
        <a-form-item v-for="(item, index) in stepData.Outputs" :label="`输出${index}`">
            <a-row :gutter="[16, 16]">
                <a-col :span="11">
                    <a-select v-model:value="item.StepProperty">
                        <a-select-option v-for="arg in stepBodyArgs" :value="arg.Name">{{ arg.Description
                        }}</a-select-option>
                    </a-select>
                </a-col>
                <a-col :span="11">
                    <a-select v-model:value="item.RuntimeDataProperty">
                        <a-select-option v-for="property in NowRuntimeDataType.Properties" :value="property.Name">
                            {{ property.Name }}
                        </a-select-option>
                    </a-select>
                </a-col>
                <a-col :span="2">
                    <a-button type="primary" danger @click="RemoveOutputDataItem(index)">X</a-button>
                </a-col>
            </a-row>
        </a-form-item>
        <a-form-item label="异常处理">
            <a-row :gutter="[16, 16]">
                <a-col :span="8">
                    <a-select v-model:value="stepData.Error.HandlerType">
                        <a-select-option :value="ErrorHandlerTypeEnum.Stop">停止</a-select-option>
                        <a-select-option :value="ErrorHandlerTypeEnum.Retry">重试</a-select-option>
                        <a-select-option :value="ErrorHandlerTypeEnum.Next">下一步</a-select-option>
                    </a-select>
                </a-col>
                <a-col :span="16">
                    <InputTimeSpan v-if="stepData.Error.HandlerType === ErrorHandlerTypeEnum.Retry"
                        v-model="stepData.Error.RetryIntervalValue" />
                </a-col>
            </a-row>
        </a-form-item>
    </a-form>
</template>
<script setup lang="ts">
import InputTimeSpan from '../InputTimeSpan.vue';
import BuildDataEdit from './BuildDataEdit.vue';
import { ThenStepData } from '../../scripts/StepDatas/ThenStepData';
import { AllStepBodys } from '../../scripts/StepBodys/StepBodyInfo';
import { InputValueSourceEnum } from '../../scripts/StepDatas/Base/InputValueSourceEnum';
import { ErrorHandlerTypeEnum } from '../../scripts/StepDatas/Base/ErrorHandlerTypeEnum';
import { onMounted, ref } from 'vue';
import { NowRuntimeDataType } from "../../scripts/RuntimeDataType";

const props = defineProps<{ stepData: ThenStepData }>();
const inputs = props.stepData.Inputs;
const outputs = props.stepData.Outputs;
const stepBodyArgs = ref(AllStepBodys[0].Args);

onMounted(() => {
    StepBodyTypeChangeValue();
});
const StepBodyTypeChangeValue = () => {
    for (let i = 0; i < AllStepBodys.length; i++) {
        const stepBody = AllStepBodys[i];
        if (stepBody.Name !== props.stepData.StepBodyType) continue;
        stepBodyArgs.value = stepBody.Args;
        return;
    }
}
const NewInputDataItem = () => {
    inputs.push({ StepProperty: "", Value: "", ValueSource: InputValueSourceEnum.Constant });
}
const RemoveInputDataItem = (index: number) => {
    inputs.splice(index, 1);
}
const GetInputConstantType = (index: number) => {
    for (let i = 0; i < stepBodyArgs.value.length; i++) {
        const element = stepBodyArgs.value[i];
        if (element.Name !== inputs[index].StepProperty) continue;
        return element.Type;
    }
    return "String";
}
const NewOutputDataItem = () => {
    outputs.push({ StepProperty: "", RuntimeDataProperty: "" });
}
const RemoveOutputDataItem = (index: number) => {
    outputs.splice(index, 1);
}
</script>