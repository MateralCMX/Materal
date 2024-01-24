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
            <a-form-item field="Key" label="键" :rules="formRules.Key">
                <a-input v-model="formData.Key" />
            </a-form-item>
            <a-form-item field="ApplicationType" label="应用程序类型" :rules="formRules.ApplicationType">
                <a-select v-model="formData.ApplicationType">
                    <a-option v-for="item in applicationTypeList" :value="item.Key">{{ item.Value }}</a-option>
                </a-select>
            </a-form-item>
            <a-form-item field="ValueType" label="值类型">
                <a-select v-model="valueType">
                    <a-option :value="'text'">文本</a-option>
                    <a-option :value="'json'">json</a-option>
                </a-select>
            </a-form-item>
            <a-form-item field="Data" label="值" :rules="formRules.Data">
                <vue-monaco-editor class="monacoEditor" v-model:value="formData.Data" :language="valueType" :options="{
                    wordWrap: 'on',
                    theme: 'vs',
                    automaticLayout: true,
                    minimap: {
                        enabled: true
                    }
                }" />
            </a-form-item>
        </a-form>
    </a-spin>
</template>
<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import AddDefaultDataModel from '../models/defaultData/AddDefaultDataModel';
import service from '../services/DefaultDataService';
import { Form, Message } from '@arco-design/web-vue';
import serverManagement from '../serverManagement';
import VueMonacoEditor from '@guolao/vue-monaco-editor';
import deployEnumsService from '../services/DeployEnumsService';
import KeyValueModel from '../models/KeyValueModel';

const formRef = ref<InstanceType<typeof Form>>();
const props = defineProps({
    id: String
});
const applicationTypeList = ref<Array<KeyValueModel>>([]);
const valueType = ref<string>('text');
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
const formData = reactive<AddDefaultDataModel>({
    Key: "",
    Data: "",
    ApplicationType: 0
});
/**
 * 表单验证规则
 */
const formRules = reactive({
    Key: [
        { required: true, message: '键必填', trigger: 'blur' }
    ],
    Data: [
        { required: true, message: '值必填', trigger: 'blur' }
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
        Message.error("保存默认数据失败");
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
        formData.Key = httpResult.Key;
        formData.Data = httpResult.Data;
        if (formData.Data && isJson(formData.Data)) {
            valueType.value = 'json';
        }
        else {
            valueType.value = 'text';
        }
        formData.ApplicationType = httpResult.ApplicationType;
    } catch (error) {
        Message.error("获取默认数据失败");
    }
    finally {
        isLoading.value = false;
    }
}
function isJson(str: string) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
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
    if (serverManagement.selectedDeploy) {
        service.serviceName = serverManagement.selectedDeploy.Service;
        deployEnumsService.serviceName = serverManagement.selectedDeploy.Service;
        await loadApplicationTypeAsync();
    }
    if (!props.id) return;
    await queryAsync();
});
</script>