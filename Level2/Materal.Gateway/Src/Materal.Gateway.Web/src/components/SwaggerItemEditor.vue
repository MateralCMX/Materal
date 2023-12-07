<template>
    <a-spin :loading="isLoading" style="width: 100%;">
        <a-form ref="formRef" :model="formData">
            <a-form-item field="Name" label="名称" :rules="formRules.Name">
                <a-input v-model="formData.Name" />
            </a-form-item>
            <a-form-item field="Version" label="版本" :rules="formRules.Version">
                <a-input v-model="formData.Version" />
            </a-form-item>
            <a-form-item field="ServiceName" label="服务名称" :rules="formRules.ServiceName" v-if="props.isServiceItem">
                <a-input v-model="formData.ServiceName" />
            </a-form-item>
            <a-form-item field="ServicePath" label="服务路径" :rules="formRules.ServicePath" v-if="props.isServiceItem">
                <a-input v-model="formData.ServicePath" />
            </a-form-item>
            <a-form-item field="Url" label="Url" :rules="formRules.ServicePath" v-if="!props.isServiceItem">
                <a-input v-model="formData.Url" />
            </a-form-item>
        </a-form>
    </a-spin>
</template>
<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import service from '../services/SwaggerConfigService';
import { Form, Message } from '@arco-design/web-vue';

const formRef = ref<InstanceType<typeof Form>>();
const props = defineProps({
    id: String,
    swaggerConfigID: String,
    isServiceItem: Boolean
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
const formData = reactive({
    Name: "",
    Version: "v1",
    ServiceName: "",
    ServicePath: "/swagger/v1/swagger.json",
    Url: "http://localhost/swagger/v1/swagger.json"
});
/**
 * 表单验证规则
 */
const formRules = reactive({
    Name: [
        { required: true, message: '名称必填', trigger: 'blur' }
    ],
    Version: [
        { required: true, message: '版本必填', trigger: 'blur' }
    ],
    ServiceName: [
        { required: true, message: '服务名称必填', trigger: 'blur' }
    ],
    ServicePath: [
        { required: true, message: '服务路径必填', trigger: 'blur' }
    ],
    Url: [
        { required: true, message: 'Url必填', trigger: 'blur' }
    ],
});
async function saveAsync(): Promise<boolean> {
    if (!props.swaggerConfigID) return false;
    const validate = await formRef.value?.validate();
    if (validate) return false;
    isLoading.value = true;
    try {
        if (props.id) {
            if (props.isServiceItem) {
                await service.EditServiceItemAsync({
                    ID: props.id,
                    SwaggerConfigID: props.swaggerConfigID,
                    Name: formData.Name,
                    Version: formData.Version,
                    ServiceName: formData.ServiceName,
                    ServicePath: formData.ServicePath
                });
            }
            else {
                await service.EditJsonItemAsync({
                    ID: props.id,
                    SwaggerConfigID: props.swaggerConfigID,
                    Name: formData.Name,
                    Version: formData.Version,
                    Url: formData.Url
                });
            }
        }
        else {
            if (props.isServiceItem) {
                await service.AddServiceItemAsync({
                    SwaggerConfigID: props.swaggerConfigID,
                    Name: formData.Name,
                    Version: formData.Version,
                    ServiceName: formData.ServiceName,
                    ServicePath: formData.ServicePath
                });
            }
            else {
                await service.AddJsonItemAsync({
                    SwaggerConfigID: props.swaggerConfigID,
                    Name: formData.Name,
                    Version: formData.Version,
                    Url: formData.Url
                });
            }
        }
        return true;
    } catch (error) {
        Message.error("保存Swagger配置失败");
        return false;
    }
    finally {
        isLoading.value = false;
    }
}
async function queryAsync() {
    if (!props.id || !props.swaggerConfigID) return;
    isLoading.value = true;
    try {
        const httpResult = await service.GetItemInfoAsync(props.swaggerConfigID, props.id);
        if (!httpResult) return;
        formData.Name = httpResult.Name;
        formData.Version = httpResult.Version;
        formData.ServiceName = httpResult.ServiceName;
        formData.ServicePath = httpResult.ServicePath;
        formData.Url = httpResult.Url;
    } catch (error) {
        Message.error("获取Swagger配置失败");
    }
    finally {
        isLoading.value = false;
    }
}
onMounted(async () => {
    if (!props.id || !props.swaggerConfigID) return;
    await queryAsync();
});
</script>