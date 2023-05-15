<template>
    <a-form :model="formData" @finish="submitData">
        <a-form-item label="名称" name="Name" :rules="[{ required: true, message: '请输入名称' }]">
            <a-input v-model:value="formData.Name" />
        </a-form-item>
        <a-form-item>
            <a-button type="primary" html-type="submit" block :loading="loading">
                {{ formData.ID ? "保存" : "添加" }}
            </a-button>
        </a-form-item>
    </a-form>
</template>
<script setup lang="ts">
import UserService from '../services/UserService';
import { EditUserModel } from '../models/User/EditUserModel';

/**
 * 加载标识
 */
const loading = ref(false);
/**
 * 表单数据
 */
const formData = reactive<EditUserModel>(new EditUserModel());
/**
 * 提交数据
 */
const submitData = async () => {
    loading.value = true;
    if (formData.ID) {
        await UserService.EditAsync(formData);
    }
    else {
        await UserService.AddAsync(formData);
    }
    loading.value = false;
    emits('complate');
};
/**
 * 初始化
 * @param id 唯一标识
 */
const initAsync = async (id?: string) => {
    loading.value = true;
    formData.ID = id ?? '';
    if (id && id !== '') {
        await initEidtAsync();
    }
    else {
        await initAddAsync();
    }
    loading.value = false;
};
/**
 * 初始化编辑
 */
const initEidtAsync = async () => {
    const result = await UserService.GetInfoAsync(formData.ID);
    if (result) {
        formData.Name = result?.Data.Name;
    }
    else {
        await initAddAsync();
    }
}
/**
 * 初始化添加
 */
const initAddAsync = async () => {
    formData.ID = '';
    formData.Name = '';
}
/**
 * 事件
 */
const emits = defineEmits<{ (event: 'complate'): void }>();
/**
 * 暴露成员
 */
defineExpose({ initAsync });
</script>