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
            <a-form-item field="Description" label="描述" :rules="formRules.Description">
                <a-textarea v-model="formData.Description" placeholder="请输入配置项描述" allow-clear />
            </a-form-item>
            <a-form-item field="ValueType" label="值类型">
                <a-select v-model="valueType">
                    <a-option :value="'text'">文本</a-option>
                    <a-option :value="'json'">json</a-option>
                </a-select>
            </a-form-item>
            <a-form-item field="Value" label="值" :rules="formRules.Value">
                <vue-monaco-editor class="monacoEditor" v-model:value="formData.Value" :language="valueType" :options="{
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
import AddConfigurationItemModel from '../models/configurationItem/AddConfigurationItemModel';
import service from '../services/ConfigurationItemService';
import { Form, Message } from '@arco-design/web-vue';
import serverManagement from '../serverManagement';
import VueMonacoEditor from '@guolao/vue-monaco-editor';

const formRef = ref<InstanceType<typeof Form>>();
const props = defineProps({
    id: String,
    namespaceID: String
});
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
const formData = reactive<AddConfigurationItemModel>({
    Key: "",
    Value: "",
    Description: "",
    NamespaceID: ""
});
/**
 * 表单验证规则
 */
const formRules = reactive({
    Key: [
        { required: true, message: '键必填', trigger: 'blur' }
    ],
    Value: [
        { required: true, message: '值必填', trigger: 'blur' }
    ],
    Description: [
        { required: true, message: '描述必填', trigger: 'blur' }
    ],
});
async function saveAsync(): Promise<boolean> {
    if (props.namespaceID) {
        formData.NamespaceID = props.namespaceID;
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
        Message.error("保存配置项失败");
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
        formData.Value = httpResult.Value;
        if (formData.Value && isJson(formData.Value)) {
            valueType.value = 'json';
        }
        else {
            valueType.value = 'text';
        }
        formData.Description = httpResult.Description;
    } catch (error) {
        Message.error("获取配置项失败");
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
onMounted(async () => {
    if (serverManagement.selectedEnvironmentServer) {
        service.serviceName = serverManagement.selectedEnvironmentServer.Service;
    }
    if (!props.id) return;
    await queryAsync();
});
</script>