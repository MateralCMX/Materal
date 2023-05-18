<template>
    <a-form-item label="页面" name="Data">
        <a-button @click="openPageTemplateDrawer">编辑模版</a-button>
    </a-form-item>
    <a-drawer v-model:visible="optionDrawerVisible" :maskClosable="false" :title="'页面模版编辑'" width="1600px">
        <PageTemplateOption ref="pageTemplateOption" @save-data="saveData" />
    </a-drawer>
</template>
<script setup lang="ts">
import { EditNodeModel } from '../models/Node/EditNodeModel';

/**
 * 页面模版配置
 */
const pageTemplateOption = ref<any>();
/**
 * 操作抽屉是否显示
 */
const optionDrawerVisible = ref(false);
/**
 * 暴露成员
 */
const props = defineProps<{ modelValue: EditNodeModel }>();
/**
 * 打开页面模版抽屉
 */
const openPageTemplateDrawer = () => {
    optionDrawerVisible.value = true;
    nextTick(() => {
        pageTemplateOption.value.init(props.modelValue.Data);
    });
}
/**
 * 保存数据
 * @param data 
 */
const saveData = (data: string) => {
    props.modelValue.Data = data;
    optionDrawerVisible.value = false;
}
</script>