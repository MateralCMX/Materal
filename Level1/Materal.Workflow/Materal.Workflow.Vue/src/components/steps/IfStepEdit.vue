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
        <a-form-item label="决策条件">
            <a-button type="primary" @click="NewConditionItem">+</a-button>
        </a-form-item>
        <a-form-item v-for="(item, index) in stepData.Conditions" :label="`条件${index}`">
            <a-row :gutter="[10, 10]">
                <a-col :span="3">
                    <a-select v-model:value="item.LeftValueSource">
                        <a-select-option :value="ValueSourceEnum.Constant">常量</a-select-option>
                        <a-select-option :value="ValueSourceEnum.BuildDataProperty">构建数据</a-select-option>
                        <a-select-option :value="ValueSourceEnum.RuntimeDataProperty">运行时数据</a-select-option>
                    </a-select>
                </a-col>
                <a-col :span="6">
                    <a-select v-if="item.LeftValueSource === ValueSourceEnum.BuildDataProperty"
                        v-model:value="item.LeftValue">
                        <a-select-option v-for="property in stepData.BuildDatas.Properties" :value="property.Name">
                            {{ property.Name }}
                        </a-select-option>
                    </a-select>
                    <a-select v-else-if="item.LeftValueSource === ValueSourceEnum.RuntimeDataProperty"
                        v-model:value="item.LeftValue">
                        <a-select-option v-for="property in NowRuntimeDataType.Properties" :value="property.Name">
                            {{ property.Name }}
                        </a-select-option>
                    </a-select>
                    <div v-else>
                        <a-row :gutter="[5, 5]">
                            <a-col :span="12">
                                <a-select v-model:value="ConditionTypes[index]">
                                    <a-select-option :value="'String'">字符串</a-select-option>
                                    <a-select-option :value="'Number'">数字</a-select-option>
                                </a-select>
                            </a-col>
                            <a-col :span="12">
                                <a-input v-if="ConditionTypes[index] === 'String'" v-model:value="item.LeftValue" />
                                <a-input-number v-else-if="ConditionTypes[index] === 'Number'"
                                    v-model:value="item.LeftValue" />
                            </a-col>
                        </a-row>
                    </div>
                </a-col>
                <a-col :span="4">
                    <a-select v-model:value="item.Condition">
                        <a-select-option :value="ComparisonTypeEnum.Equal">等于</a-select-option>
                        <a-select-option :value="ComparisonTypeEnum.NotEqual">不等于</a-select-option>
                        <a-select-option :value="ComparisonTypeEnum.LessThan">小于</a-select-option>
                        <a-select-option :value="ComparisonTypeEnum.LessThanOrEqual">小于等于</a-select-option>
                        <a-select-option :value="ComparisonTypeEnum.GreaterThan">大于</a-select-option>
                        <a-select-option :value="ComparisonTypeEnum.GreaterThanOrEqual">大于等于</a-select-option>
                    </a-select>
                </a-col>
                <a-col :span="3">
                    <a-select v-model:value="item.RightValueSource">
                        <a-select-option :value="ValueSourceEnum.Constant">常量</a-select-option>
                        <a-select-option :value="ValueSourceEnum.BuildDataProperty">构建数据</a-select-option>
                        <a-select-option :value="ValueSourceEnum.RuntimeDataProperty">运行时数据</a-select-option>
                    </a-select>
                </a-col>
                <a-col :span="6">
                    <a-select v-if="item.RightValueSource === ValueSourceEnum.BuildDataProperty"
                        v-model:value="item.RightValue">
                        <a-select-option v-for="property in stepData.BuildDatas.Properties" :value="property.Name">
                            {{ property.Name }}
                        </a-select-option>
                    </a-select>
                    <a-select v-else-if="item.RightValueSource === ValueSourceEnum.RuntimeDataProperty"
                        v-model:value="item.RightValue">
                        <a-select-option v-for="property in NowRuntimeDataType.Properties" :value="property.Name">
                            {{ property.Name }}
                        </a-select-option>
                    </a-select>
                    <div v-else>
                        <a-row :gutter="[5, 5]">
                            <a-col :span="12">
                                <a-select v-model:value="ConditionTypes[index]">
                                    <a-select-option :value="'String'">字符串</a-select-option>
                                    <a-select-option :value="'Number'">数字</a-select-option>
                                </a-select>
                            </a-col>
                            <a-col :span="12">
                                <a-input v-if="ConditionTypes[index] === 'String'" v-model:value="item.RightValue" />
                                <a-input-number v-else-if="ConditionTypes[index] === 'Number'"
                                    v-model:value="item.RightValue" />
                            </a-col>
                        </a-row>
                    </div>
                </a-col>
                <a-col :span="2">
                    <a-button type="primary" danger @click="RemoveConditionItem(index)">X</a-button>
                </a-col>
            </a-row>
        </a-form-item>
    </a-form>
</template>
<script setup lang="ts">
import BuildDataEdit from './BuildDataEdit.vue';
import { IfStepData } from '../../scripts/StepDatas/IfStepData';
import { DecisionConditionData } from '../../scripts/StepDatas/Base/DecisionConditionData';
import { ValueSourceEnum } from '../../scripts/StepDatas/Base/ValueSourceEnum';
import { ComparisonTypeEnum } from "../../scripts/StepDatas/Base/ComparisonTypeEnum";
import { NowRuntimeDataType } from "../../scripts/RuntimeDataType";
import { ref } from 'vue';

const props = defineProps<{ stepData: IfStepData }>();
const conditions = props.stepData.Conditions;
const ConditionTypes = ref<Array<"String" | "Number">>([]);
const NewConditionItem = () => {
    conditions.push(new DecisionConditionData());
    ConditionTypes.value.push('String');
}
const RemoveConditionItem = (index: number) => {
    conditions.splice(index, 1);
}
</script>