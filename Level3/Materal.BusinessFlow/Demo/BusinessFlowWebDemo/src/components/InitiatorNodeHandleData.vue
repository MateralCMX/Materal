<template>
    <a-form-item label="页面" name="Data">
        <a-button @click="openPageTemplateDrawer">编辑模版</a-button>
    </a-form-item>
    <a-drawer v-model:visible="optionDrawerVisible" :maskClosable="false" :title="'页面模版编辑'" width="1600px">
        <PageTemplateOption :data="model.Data" />
    </a-drawer>
</template>
<script setup lang="ts">
import { EditNodeModel } from '../models/Node/EditNodeModel';

/**
 * 操作抽屉是否显示
 */
 const optionDrawerVisible = ref(false);
/**
 * 暴露成员
 */
const props = defineProps<{ modelValue: EditNodeModel }>();
/**
 * 绑定模型
 */
const model = useVModel(props, 'modelValue');
/**
 * 打开页面模版抽屉
 */
const openPageTemplateDrawer = () => {
    optionDrawerVisible.value = true;
}
</script>