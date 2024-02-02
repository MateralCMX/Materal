<template>
    <a-spin :loading="isLoading" style="width: 100%;">
        <a-form ref="formRef" :model="formData">
            <a-form-item v-if="id" field="Name" label="名称">
                {{ formData.Name }}
            </a-form-item>
            <a-form-item v-else field="Name" label="名称" :rules="formRules.Name">
                <a-input v-model="formData.Name" />
            </a-form-item>
            <a-form-item field="Description" label="描述" :rules="formRules.Description">
                <a-textarea v-model="formData.Description" placeholder="请输入命名空间描述" allow-clear />
            </a-form-item>
        </a-form>
    </a-spin>
</template>
<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import AddNamespaceModel from '../models/namespace/AddNamespaceModel';
import service from '../services/NamespaceService';
import { Form, Message } from '@arco-design/web-vue';

const formRef = ref<InstanceType<typeof Form>>();
const props = defineProps({
    id: String,
    projectID: String
});
defineExpose({
    saveAsync
});
/**
 * 加载数据标识
 */
const isLoading = ref(false);
/**
 * 表单数据
 */
const formData = reactive<AddNamespaceModel>({
    Name: "",
    Description: "",
    ProjectID: ""
});
/**
 * 表单验证规则
 */
const formRules = reactive({
    Name: [
        { required: true, message: '名称必填', trigger: 'blur' }
    ],
    Description: [
        { required: true, message: '描述必填', trigger: 'blur' }
    ],
});
async function saveAsync(): Promise<boolean> {
    if (props.projectID) {
        formData.ProjectID = props.projectID;
    }
    else {
        Message.error("项目ID不能为空");
    }
    const validate = await formRef.value?.validate();
    if (validate) return false;
    isLoading.value = true;
    try {
        if (props.id) {
            await service.EditAsync({
                ...formData,
                ID: props.id
            });
        }
        else {
            await service.AddAsync(formData);
        }
        return true;
    } catch (error) {
        Message.error("保存命名空间失败");
        return false;
    }
    finally {
        isLoading.value = false;
    }
}
async function queryAsync() {
    if (!props.id) return;
    isLoading.value = true;
    try {
        const httpResult = await service.GetInfoAsync(props.id);
        if (!httpResult) return;
        formData.Name = httpResult.Name;
        formData.Description = httpResult.Description;
    } catch (error) {
        Message.error("获取命名空间失败");
    }
    finally {
        isLoading.value = false;
    }
}
onMounted(async () => {
    if (!props.id) return;
    await queryAsync();
});
</script>