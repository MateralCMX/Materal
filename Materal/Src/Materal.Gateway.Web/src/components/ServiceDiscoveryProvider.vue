<template>
    <a-spin :loading="isLoading" style="width: 100%;">
        <a-form :model="formData" @submit-success="onSaveAsync">
            <a-form-item field="Enable" label="启用标识">
                <a-switch v-model="formData.Enable" />
            </a-form-item>
            <a-form-item field="Host" label="Host" v-if="formData.Enable" :rules="formRules.Host">
                <a-input v-model="formData.Host" />
            </a-form-item>
            <a-form-item field="Port" label="Port" v-if="formData.Enable" :rules="formRules.Port">
                <a-input-number v-model="formData.Port" :min="1" />
            </a-form-item>
            <a-form-item field="IsSSL" label="IsSSL" v-if="formData.Enable" :rules="formRules.IsSSL">
                <a-switch v-model="formData.IsSSL" />
            </a-form-item>
            <a-form-item>
                <a-button html-type="submit" type="primary" style="width:100%">保存</a-button>
            </a-form-item>
        </a-form>
    </a-spin>
</template>
<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import service from '../services/ServiceDiscoveryProviderService';
import { Message } from '@arco-design/web-vue';

/**
 * 加载数据标识
 */
const isLoading = ref(false);
/**
 * 表单数据
 */
const formData = reactive({
    Enable: false,
    Host: "",
    Port: 8500,
    IsSSL: false,
    Type: "Consul"
});
/**
 * 表单验证规则
 */
const formRules = reactive({
    Host: [
        { required: true, message: 'Host必填', trigger: 'blur' }
    ],
    Port: [
        { required: true, message: 'Port必填', trigger: 'blur' }
    ],
    IsSSL: [
        { required: true, message: 'IsSSL必填', trigger: 'blur' }
    ],
});
/**
 * 保存数据
 */
async function onSaveAsync() {
    isLoading.value = true;
    try {
        if (formData.Enable) {
            await service.SetConfigAsync(formData);
        }
        else {
            await service.SetConfigAsync();
        }
    } catch (error) {
        Message.error("设置服务发现失败");
    }
    finally {
        isLoading.value = false;
    }
}
/**
 * 获取数据
 */
async function getInfoAsync() {
    isLoading.value = true;
    try {
        const result = await service.GetConfigAsync();
        if (!result) {
            formData.Enable = false;
        }
        else {
            formData.Enable = true;
            formData.Host = result.Host;
            formData.Port = result.Port;
            formData.IsSSL = result.IsSSL;
            formData.Type = result.Type;
        }
    } catch (error) {
        Message.error("获取服务发现失败");
    }
    finally {
        isLoading.value = false;
    }
}
onMounted(getInfoAsync);
</script>