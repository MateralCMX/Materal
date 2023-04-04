<style scoped>
.ant-input-number {
    width: 100%;
}
</style>
<template>
    <a-col :span="4">
        <a-select v-model:value="valueSourceModel">
            <a-select-option :value="ValueSourceEnum.Constant">常量</a-select-option>
            <a-select-option :value="ValueSourceEnum.BuildDataProperty">构建数据</a-select-option>
            <a-select-option :value="ValueSourceEnum.RuntimeDataProperty">运行时数据</a-select-option>
        </a-select>
    </a-col>
    <a-col :span="20">
        <a-select v-if="valueSource === ValueSourceEnum.BuildDataProperty" v-model:value="valueModel">
            <a-select-option v-for="property in stepData.BuildDatas.Properties" :value="property.Name">
                {{ property.Name }}
            </a-select-option>
        </a-select>
        <a-select v-else-if="valueSource === ValueSourceEnum.RuntimeDataProperty" v-model:value="valueModel">
            <a-select-option v-for="property in NowRuntimeDataType.Properties" :value="property.Name">
                {{ property.Name }}
            </a-select-option>
        </a-select>
        <div v-else>
            <a-row :gutter="[5, 5]">
                <a-col :span="4">
                    <a-select v-model:value="conditionTypeModel">
                        <a-select-option :value="'String'">字符串</a-select-option>
                        <a-select-option :value="'Number'">数字</a-select-option>
                    </a-select>
                </a-col>
                <a-col :span="20">
                    <a-input v-if="conditionTypeModel === 'String'" v-model:value="valueModel" />
                    <a-input-number v-else-if="conditionTypeModel === 'Number'" v-model:value="valueModel" />
                </a-col>
            </a-row>
        </div>
    </a-col>
</template>

<script setup lang="ts">
import { IfStepData } from '../../scripts/StepDatas/IfStepData';
import { ValueSourceEnum } from '../../scripts/StepDatas/Base/ValueSourceEnum';
import { NowRuntimeDataType } from "../../scripts/RuntimeDataType";
import { useVModel } from '@vueuse/core'

const props = defineProps<{ conditionType: "String" | "Number", stepData: IfStepData, value: string | number, valueSource: ValueSourceEnum }>();

const valueSourceModel = useVModel(props, 'valueSource');
const valueModel = useVModel(props, 'value');
const conditionTypeModel = useVModel(props, 'conditionType');
</script>