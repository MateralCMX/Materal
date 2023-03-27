<template>
    <a-form :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }" autocomplete="off">
        <a-form-item label="操作">
            <a-button type="primary" @click="pushNewItem">+</a-button>
        </a-form-item>
        <div v-for="(item, index) in items">
            <a-form-item :label="`属性${index}`">
                <a-row :gutter="[16, 16]">
                    <a-col :span="12">
                        <a-input v-model:value="item.Name" />
                    </a-col>
                    <a-col :span="8">
                        <a-select ref="select" v-model:value="item.Type">
                            <a-select-option value="String">字符串</a-select-option>
                            <a-select-option value="Number">数字</a-select-option>
                        </a-select>
                    </a-col>
                    <a-col :span="2">
                        <a-button type="primary" danger @click="removeItem(index)">X</a-button>
                    </a-col>
                </a-row>
            </a-form-item>
        </div>
    </a-form>
</template>
<script setup lang="ts">
import { RuntimeDataType } from '../scripts/RuntimeDataType';

const props = defineProps<{ runTimeDataType: RuntimeDataType }>();
const items = props.runTimeDataType.Properties;
const pushNewItem = () => {
    items.push({ Name: "", Type: "String" });
}
const removeItem = (index: number) => {
    items.splice(index, 1);
}
</script>