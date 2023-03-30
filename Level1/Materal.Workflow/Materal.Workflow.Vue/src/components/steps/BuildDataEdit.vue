<style scoped>
.ant-input-number {
    width: 100%;
}
</style>
<template>
    <a-form-item label="构建数据">
        <a-button type="primary" @click="NewBuildDataItem">+</a-button>
    </a-form-item>
    <a-form-item v-for="(item, index) in properties" :label="`构建数据${index}`">
        <a-row :gutter="[16, 16]">
            <a-col :span="6">
                <a-input v-model:value="item.Name" @change="BuildDataChangeValue" />
            </a-col>
            <a-col :span="6">
                <a-select v-model:value="item.Type">
                    <a-select-option value="String">字符串</a-select-option>
                    <a-select-option value="Number">数字</a-select-option>
                </a-select>
            </a-col>
            <a-col :span="10">
                <a-input v-if="item.Type === 'String'" v-model:value="item.Value" @change="BuildDataChangeValue" />
                <a-input-number v-else-if="item.Type === 'Number'" v-model:value="item.Value"
                    @change="BuildDataChangeValue" />
            </a-col>
            <a-col :span="2">
                <a-button type="primary" danger @click="RemoveBuildDataItem(index)">X</a-button>
            </a-col>
        </a-row>
    </a-form-item>
</template>
<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { BuildDataPropertyInfo } from '../../scripts/BuildDataType';
import { StepData } from '../../scripts/StepDatas/Base/StepData';

onMounted(() => {
    properties.value = buildDatas.Properties;
});
const props = defineProps<{ stepData: StepData }>();
let buildDatas = props.stepData.BuildDatas;
const properties = ref<BuildDataPropertyInfo[]>([]);
const NewBuildDataItem = () => {
    properties.value.push({ Name: "", Type: "String", Value: "" });
}
const BuildDataChangeValue = () => {
    props.stepData.BuildDatas = buildDatas;
}
const RemoveBuildDataItem = (index: number) => {
    properties.value.splice(index, 1);
    props.stepData.BuildDatas = buildDatas;
}
</script>