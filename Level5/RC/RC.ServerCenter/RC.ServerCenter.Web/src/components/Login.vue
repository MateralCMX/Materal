<style scoped>
.box-card {
  width: 400px;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translateX(-50%) translateY(-50%);
}

.title {
  font-size: 16px;
}
</style>
<template>
  <a-card class="box-card" title="RC">
    <a-form :model="formData" @submit-success="onLoginAsync">
      <a-form-item field="Account" label="帐号" :rules="formRules.Account">
        <a-input v-model="formData.Account" placeholder="请输入帐号" />
      </a-form-item>
      <a-form-item field="Password" label="密码" :rules="formRules.Password">
        <a-input-password v-model="formData.Password" placeholder="请输入密码" />
      </a-form-item>
      <a-form-item>
        <a-button html-type="submit" type="primary" style="width:100%" :loading="isLoading">登录</a-button>
      </a-form-item>
    </a-form>
  </a-card>
</template>
<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import loginManagement from '../loginManagement';
import LoginRequestModel from '../models/user/LoginRequestModel';
import userService from '../services/UserService';

/**
 * 加载数据标识
 */
const isLoading = ref(false);
/**
 * 表单数据
 */
const formData = reactive<LoginRequestModel>({
  Account: '',
  Password: ''
});
/**
 * 表单验证规则
 */
const formRules = reactive({
  Account: [
    { required: true, message: '帐号必填', trigger: 'blur' }
  ],
  Password: [
    { required: true, message: '密码必填', trigger: 'blur' }
  ],
});
/**
 * 登录
 */
async function onLoginAsync() {
  isLoading.value = true;
  try {
    const result = await userService.LoginAsync(formData);
    if (result == null) {
      isLoading.value = false;
      return;
    }
    loginManagement.setToken(result.Token);
    window.location.hash = "/";
  }
  catch {
    isLoading.value = false;
  }
}

onMounted(() => {
  loginManagement.loginOut();
});
</script>
  ../models/user/LoginRequestModel