<template>
    <a-spin :loading="isLoading" style="width: 100%;">
        <a-form :model="formData" @submit-success="onSaveAsync">
            <a-form-item field="Enable" label="启用标识">
                <a-switch v-model="formData.Enable" />
            </a-form-item>
            <a-form-item field="QuotaExceededMessage" label="限流消息" v-if="formData.Enable"
                :rules="formRules.QuotaExceededMessage">
                <a-input v-model="formData.QuotaExceededMessage" />
            </a-form-item>
            <a-form-item field="StatusCode" label="限流状态码" v-if="formData.Enable" :rules="formRules.StatusCode">
                <a-input-number v-model="formData.StatusCode" :min="1" />
            </a-form-item>
            <a-form-item field="ClientIdHeader" label="客户端ID头" v-if="formData.Enable" :rules="formRules.ClientIdHeader">
                <a-input v-model="formData.ClientIdHeader" />
            </a-form-item>
            <a-form-item>
                <a-button html-type="submit" type="primary" style="width:100%">保存</a-button>
            </a-form-item>
        </a-form>
    </a-spin>
</template>
<script setup lang="ts">
import { Message } from '@arco-design/web-vue';
import { onMounted, reactive, ref } from 'vue';
import service from '../services/GlobalRateLimitOptionsService';

/**
 * 加载数据标识
 */
const isLoading = ref(false);
/**
 * 表单数据
 */
const formData = reactive({
    Enable: false,
    QuotaExceededMessage: "",
    StatusCode: 302,
    ClientIdHeader: "",
});
/**
 * 表单验证规则
 */
const formRules = reactive({
    QuotaExceededMessage: [
        { required: true, message: '限流消息必填', trigger: 'blur' }
    ],
    StatusCode: [
        { required: true, message: '限流状态码必填', trigger: 'blur' }
    ],
    ClientIdHeader: [
        { required: true, message: '客户端ID头必填', trigger: 'blur' }
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
        Message.error("设置全局限流失败");
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
            formData.QuotaExceededMessage = result.QuotaExceededMessage;
            formData.StatusCode = result.StatusCode;
            formData.ClientIdHeader = result.ClientIdHeader;
        }
    } catch (error) {
        Message.error("获取全局限流失败");
    }
    finally {
        isLoading.value = false;
    }
}
onMounted(getInfoAsync);
</script>