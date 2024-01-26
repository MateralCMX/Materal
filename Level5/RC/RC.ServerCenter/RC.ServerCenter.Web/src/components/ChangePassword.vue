<template>
    <a-spin :loading="isLoading" style="width: 100%;">
        <a-form ref="formRef" :model="formData">
            <a-form-item field="OldPassword" label="旧密码" :rules="formRules.OldPassword">
                <a-input-password v-model="formData.OldPassword" />
            </a-form-item>
            <a-form-item field="NewPassword" label="新密码" :rules="formRules.NewPassword">
                <a-input-password v-model="formData.NewPassword" />
            </a-form-item>
            <a-form-item field="NewPassword2" label="确认密码" :rules="formRules.NewPassword2">
                <a-input-password v-model="formData.NewPassword2" />
            </a-form-item>
        </a-form>
    </a-spin>
</template>
<script setup lang="ts">
import { reactive, ref } from 'vue';
import service from '../services/UserService';
import { Form, Message } from '@arco-design/web-vue';

const formRef = ref<InstanceType<typeof Form>>();
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
    OldPassword: "",
    NewPassword: "",
    NewPassword2: ""
});
/**
 * 表单验证规则
 */
const formRules = reactive({
    OldPassword: [
        { required: true, message: '旧密码必填', trigger: 'blur' }
    ],
    NewPassword: [
        { required: true, message: '新密码必填', trigger: 'blur' }
    ],
    NewPassword2: [
        { required: true, message: '确认密码必填', trigger: 'blur' }
    ],
});
async function saveAsync(): Promise<boolean> {
    const validate = await formRef.value?.validate();
    if (validate) return false;
    isLoading.value = true;
    try {
        await service.ChangePasswordAsync({
            OldPassword: formData.OldPassword,
            NewPassword: formData.NewPassword
        });
        return true;
    } catch (error) {
        Message.error("修改密码失败");
        return false;
    }
    finally {
        isLoading.value = false;
    }
}
</script>