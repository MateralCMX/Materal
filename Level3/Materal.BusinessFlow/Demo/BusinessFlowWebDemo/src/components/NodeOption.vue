<template>
    <a-form :model="formData" @finish="submitData">
        <a-form-item label="名称" name="Name" :rules="[{ required: true, message: '请输入名称' }]">
            <a-input v-model:value="formData.Name" />
        </a-form-item>
        <a-form-item label="描述" name="Description">
            <NodeHandleTypeEnumSelect v-model="formData.HandleType" :has-all="false" />
        </a-form-item>
        <a-form-item>
            <a-button type="primary" html-type="submit" block :loading="loading">
                {{ formData.ID ? "保存" : "添加" }}
            </a-button>
        </a-form-item>
    </a-form>
</template>
<script setup lang="ts">
import { EditNodeModel } from '../models/Node/EditNodeModel';

/**
 * 加载标识
 */
const loading = ref(false);
/**
 * 表单数据
 */
const formData = reactive<EditNodeModel>(new EditNodeModel());
/**
 * 提交数据
 */
const submitData = async () => {
    loading.value = true;
    if (formData.ID) {
    }
    else {
    }
    loading.value = false;
    emits('complate');
};
/**
 * 初始化
 */
const initAsync = async (stepID: string, id?: string) => {
    formData.StepID = stepID;
    formData.Name = '12345';
    console.log(formData);
    console.log(id);
}
/**
 * 事件
 */
const emits = defineEmits<{ (event: 'complate'): void }>();
/**
 * 暴露成员
 */
defineExpose({ initAsync });
</script>