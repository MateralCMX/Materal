<template>
    <a-form-item>
        <a-button type="primary" @click="addItem">+</a-button>
    </a-form-item>
    <a-form-item v-for="(item, index) in data">
        <a-row :gutter="[16, 16]">
            <a-col :span="8">
                <a-input v-model:value="item.Key" />
            </a-col>
            <a-col :span="14">
                <a-input v-model:value="item.Value" />
            </a-col>
            <a-col :span="2">
                <a-button type="primary" danger @click="removeItem(index)">X</a-button>
            </a-col>
        </a-row>
    </a-form-item>
</template>
<script lang="ts" setup>
import { KeyValueModel } from '../../models/KeyValueModel';

const props = defineProps<{ data: KeyValueModel[] }>();

const addItem = () => props.data.push({ Key: '', Value: '' });
const removeItem = (index: number) => props.data.splice(index, 1);
</script>