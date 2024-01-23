<template>
    <a-spin :loading="isLoading" style="width: 100%;">
        <a-form ref="formRef" :model="formData">
            <a-form-item field="Key" label="Key" :rules="formRules.Key">
                <a-input v-model="formData.Key" />
            </a-form-item>
            <a-form-item field="TakeServersFromDownstreamService" label="服务发现"
                :rules="formRules.TakeServersFromDownstreamService">
                <a-switch v-model="formData.TakeServersFromDownstreamService" />
            </a-form-item>
        </a-form>
    </a-spin>
</template>
<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import AddSwaggerModel from '../models/swagger/AddSwaggerModel';
import service from '../services/SwaggerConfigService';
import { Form, Message } from '@arco-design/web-vue';

const formRef = ref<InstanceType<typeof Form>>();
const props = defineProps({
    id: String
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
const formData = reactive<AddSwaggerModel>({
    Key: "",
    TakeServersFromDownstreamService: false
});
/**
 * 表单验证规则
 */
const formRules = reactive({
    Key: [
        { required: true, message: 'SwaggerKey必填', trigger: 'blur' }
    ],
    TakeServersFromDownstreamService: [
        { required: true, message: '服务发现必填', trigger: 'blur' }
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
        Message.error("保存Swagger配置失败");
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
        formData.TakeServersFromDownstreamService = httpResult.TakeServersFromDownstreamService;
    } catch (error) {
        Message.error("获取Swagger配置失败");
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