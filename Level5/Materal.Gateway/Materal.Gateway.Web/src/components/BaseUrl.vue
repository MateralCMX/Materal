<template>
    <a-spin :loading="isLoading" style="width: 100%;">
        <a-form :model="formData" @submit-success="onSaveAsync">
            <a-form-item field="Enable" label="启用标识">
                <a-switch v-model="formData.Enable" />
            </a-form-item>
            <a-form-item field="BaseUrl" label="BaseUrl" :rules="formRules.BaseUrl" v-if="formData.Enable">
                <a-input v-model="formData.BaseUrl" />
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
import service from '../services/BaseUrlService';

/**
 * 加载数据标识
 */
const isLoading = ref(false);
/**
 * 表单数据
 */
const formData = reactive({
    Enable: false,
    BaseUrl: ''
});
/**
 * 表单验证规则
 */
const formRules = reactive({
    BaseUrl: [
        { required: true, message: 'BaseUrl必填', trigger: 'blur' }
    ],
});
/**
 * 保存数据
 */
async function onSaveAsync() {
    isLoading.value = true;
    try {
        if (formData.Enable) {
            await service.SetConfigAsync(formData.BaseUrl);
        }
        else {
            await service.SetConfigAsync();
        }
    } catch (error) {
        Message.error("设置BaseUrl失败");
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
            formData.BaseUrl = result;
        }
    } catch (error) {
        Message.error("获取BaseUrl失败");
    }
    finally {
        isLoading.value = false;
    }
}
onMounted(getInfoAsync);
</script>