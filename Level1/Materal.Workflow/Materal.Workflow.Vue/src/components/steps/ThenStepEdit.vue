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
        <a-form-item label="构建数据">
            <a-button type="primary" @click="pushNewItem">+</a-button>
        </a-form-item>
        <div v-for="(item, index) in buildDatas.Properties">
            <a-form-item :label="`构建数据${index}`">
                <a-row :gutter="[16, 16]">
                    <a-col :span="6">
                        <a-input v-model:value="item.Name" @change="changeValue" />
                    </a-col>
                    <a-col :span="6">
                        <a-select ref="select" v-model:value="item.Type">
                            <a-select-option value="String">字符串</a-select-option>
                            <a-select-option value="Number">数字</a-select-option>
                        </a-select>
                    </a-col>
                    <a-col :span="10">
                        <a-input v-if="item.Type === 'String'" v-model:value="item.Value" @change="changeValue" />
                        <a-input-number v-else-if="item.Type === 'Number'" v-model:value="item.Value"
                            @change="changeValue" />
                    </a-col>
                    <a-col :span="2">
                        <a-button type="primary" danger @click="removeItem(index)">X</a-button>
                    </a-col>
                </a-row>
            </a-form-item>
        </div>
        <a-form-item label="节点体">
            <a-select ref="select" v-model:value="stepData.StepBodyType">
                <a-select-option v-for="item in AllStepBodys" :value="item.Name">{{ item.Name }}</a-select-option>
            </a-select>
        </a-form-item>
    </a-form>
</template>
<script setup lang="ts">
import { ThenStepData } from '../../scripts/StepDatas/ThenStepData';
import { AllStepBodys } from '../../scripts/StepBodys/StepBodyInfo';

const props = defineProps<{ stepData: ThenStepData }>();
const buildDatas = props.stepData.BuildDatas;
const pushNewItem = () => {
    buildDatas.Properties.push({ Name: "", Type: "String", Value: "" });
}
const removeItem = (index: number) => {
    buildDatas.Properties.splice(index, 1);
    props.stepData.BuildDatas = buildDatas;
}
const changeValue = () => {
    props.stepData.BuildDatas = buildDatas;
}
</script>