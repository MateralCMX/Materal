<style scoped>
.monacoEditor {
    width: 100%;
    height: 100%;
    min-height: 600px;
}
</style>
<template>
    <a-spin :loading="isLoading" style="width: 100%;">
        <a-form ref="formRef" :model="formData">
            <a-form-item v-if="!id" field="ApplicationType" label="应用程序类型" :rules="formRules.ApplicationType">
                <a-select v-model="formData.ApplicationType">
                    <a-option v-for="item in applicationTypeList" :value="item.Key">{{ item.Value }}</a-option>
                </a-select>
            </a-form-item>
            <a-form-item v-else field="ApplicationType" label="应用程序类型">
                {{ applicationTypeText }}
            </a-form-item>
            <a-form-item v-if="!id" field="Name" label="名称" :rules="formRules.Name">
                <a-input v-model="formData.Name" />
            </a-form-item>
            <a-form-item v-else field="Name" label="名称">
                {{ formData.Name }}
            </a-form-item>
            <a-form-item v-if="!id" field="RootPath" label="路径" :rules="formRules.RootPath">
                <a-input v-model="formData.RootPath" />
            </a-form-item>
            <a-form-item v-else field="RootPath" label="路径">
                {{ formData.RootPath }}
            </a-form-item>
            <a-form-item field="MainModule" label="主模块" :rules="formRules.MainModule">
                <a-input v-model="formData.MainModule" />
            </a-form-item>
            <a-form-item field="IsIncrementalUpdating" label="增量更新">
                <a-checkbox v-model="formData.IsIncrementalUpdating" />
            </a-form-item>
            <a-form-item field="RunParams" label="运行参数">
                <a-textarea v-model="formData.RunParams" />
            </a-form-item>
        </a-form>
    </a-spin>
</template>
<script setup lang="ts">
import { computed, onMounted, reactive, ref } from 'vue';
import AddApplicationInfoModel from '../models/applicationInfo/AddApplicationInfoModel';
import service from '../services/ApplicationInfoService';
import { Form, Message } from '@arco-design/web-vue';
import deployEnumsService from '../services/DeployEnumsService';
import KeyValueModel from '../models/KeyValueModel';

const formRef = ref<InstanceType<typeof Form>>();
const props = defineProps({
    id: String
});
const applicationTypeText = computed(() => applicationTypeList.value.find(m => m.Key=== formData.ApplicationType)?.Value);
const applicationTypeList = ref<Array<KeyValueModel>>([]);
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
const formData = reactive<AddApplicationInfoModel>({
    Name: "",
    RootPath: "",
    MainModule: "",
    ApplicationType: 0,
    IsIncrementalUpdating: false,
    RunParams: ""
});
/**
 * 表单验证规则
 */
const formRules = reactive({
    Name: [
        { required: true, message: '名称必填', trigger: 'blur' }
    ],
    RootPath: [
        { required: true, message: '路径必填', trigger: 'blur' }
    ],
    MainModule: [
        { required: true, message: '主模块必填', trigger: 'blur' }
    ],
    ApplicationType: [
        { required: true, message: '应用程序类型必填', trigger: 'blur' }
    ],
});
async function saveAsync(): Promise<boolean> {
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
        Message.error("保存应用程序失败");
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
        formData.RootPath = httpResult.RootPath;
        formData.MainModule = httpResult.MainModule;
        formData.ApplicationType = httpResult.ApplicationType;
        formData.IsIncrementalUpdating = httpResult.IsIncrementalUpdating;
        formData.RunParams = httpResult.RunParams;
    } catch (error) {
        Message.error("获取应用程序失败");
    }
    finally {
        isLoading.value = false;
    }
}
async function loadApplicationTypeAsync() {
    isLoading.value = true;
    try {
        const namespaceResult = await deployEnumsService.GetAllApplicationTypeEnumAsync();
        if (!namespaceResult) return;
        applicationTypeList.value = namespaceResult;
    } catch (error) {
        Message.error("获取应用程序类型列表失败");
    }
    finally {
        isLoading.value = false;
    }
}
onMounted(async () => {
    await loadApplicationTypeAsync();
    if (!props.id) return;
    await queryAsync();
});
</script>