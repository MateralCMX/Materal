<template>
    <a-form v-if="stepData" :model="stepData" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }" autocomplete="off">
        <a-form-item label="名称">
            <a-input v-model:value="stepData.Name" />
        </a-form-item>
        <a-form-item label="描述">
            <a-input v-model:value="stepData.Description" />
        </a-form-item>
        <a-form-item label="构建数据">
            <a-button type="primary">+</a-button>
        </a-form-item>
        {{ stepData }}
        <!-- <div v-for="(item, index) in stepData.BuildDatas">
            {{ item }} {{ index }}
            <a-form-item :label="`构建数据${index}`">
                <a-row :gutter="[16, 16]">
                    <a-col :span="12">
                        <a-input v-model:value="item" />
                    </a-col>
                    <a-col :span="8">
                        <a-select ref="select" v-model:value="item.Type">
                            <a-select-option value="String">字符串</a-select-option>
                            <a-select-option value="Number">数字</a-select-option>
                        </a-select>
                    </a-col>
                     <a-col :span="4">
                        <a-button type="primary" danger @click="removeItem(index)">X</a-button>
                    </a-col>
                </a-row>
            </a-form-item>
        </div> -->
        <!-- <a-form-item label="节点体">
            <a-select ref="select" v-model:value="stepData.StepBodyType">
                <a-select-option v-for="item in AllStepBodys" :value="item.Name">{{ item.Name }}</a-select-option>
            </a-select>
        </a-form-item> -->
    </a-form>
</template>
<script setup lang="ts">
import { ThenStepData } from '../../scripts/StepDatas/ThenStepData';

const props = defineProps<{ stepData: ThenStepData }>();
const data = props.stepData;
</script>