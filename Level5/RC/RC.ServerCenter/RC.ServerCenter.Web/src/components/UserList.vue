<style scoped>
.data-panel {
    display: grid;
    grid-template-columns: repeat(6, minmax(0, 1fr));
    justify-content: space-evenly;
    gap: 2rem;
}

.data-card {
    border: 1px solid #e8e8e8;
    padding: 15px;
    display: flex;
    align-items: center;
    flex-direction: row;
    justify-content: space-between;
}

.data-card-title {
    font-size: 16px;
}
</style>
<template>
    <a-spin :loading="isLoading" style="width: 100%;">
        <a-space direction="vertical" fill>
            <a-form :model="queryData" layout="inline" @submit-success="onQueryAsync">
                <a-form-item field="Key" label="账号">
                    <a-input v-model="queryData.Account" />
                </a-form-item>
                <a-form-item field="Key" label="姓名">
                    <a-input v-model="queryData.Name" />
                </a-form-item>
                <a-form-item>
                    <a-button-group>
                        <a-button html-type="submit" type="primary" title="查询">
                            <template #icon><icon-search /></template>
                        </a-button>
                        <a-button type="outline" @click="openEditPanel()" title="添加">
                            <template #icon><icon-plus /></template>
                        </a-button>
                    </a-button-group>
                </a-form-item>
            </a-form>
            <div class="data-panel">
                <div class="data-card" v-for="item in dataList">
                    <div class="data-card-title">
                        {{ item.Account }}-{{ item.Name }}
                    </div>
                    <a-button-group>
                        <a-button type="text" @click="openEditPanel(item.ID)" title="编辑">
                            <template #icon><icon-edit /></template>
                        </a-button>
                        <a-popconfirm :content="`是否重置${item.Name}的密码?`" type="warning" ok-text="重置"
                            @ok="async () => await resetPasswordAsync(item.ID)">
                            <a-button type="text" title="重置密码">
                                <template #icon><icon-sync /></template>
                            </a-button>
                        </a-popconfirm>
                        <a-popconfirm :content="`是否删除${item.Name}?`" type="warning" ok-text="删除"
                            @ok="async () => await deleteAsync(item.ID)">
                            <a-button type="text" title="删除">
                                <template #icon><icon-delete style="color: red;" /></template>
                            </a-button>
                        </a-popconfirm>
                    </a-button-group>
                </div>
            </div>
        </a-space>
    </a-spin>
    <a-drawer :width="440" :visible="editPanelVisible" @ok="onEditPanelOKAsync" @cancel="onEditPanelCancelAsync"
        unmountOnClose>
        <template #title>
            {{ editID ? "编辑" : "新增" }}用户
        </template>
        <UserEditor ref="userEditorRef" :id="editID" />
    </a-drawer>
</template>
<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import QueryUserModel from '../models/user/QueryUserModel';
import service from '../services/UserService';
import { Message } from '@arco-design/web-vue';
import UserDTO from '../models/user/UserDTO';
import UserEditor from './UserEditor.vue';

/**
 * 加载数据标识
 */
const isLoading = ref(false);
const editPanelVisible = ref(false);
const userEditorRef = ref<InstanceType<typeof UserEditor>>();
const queryData = reactive<QueryUserModel>({
    Account: "",
    Name: "",
    PageIndex: 1,
    PageSize: 99999
});
const dataList = ref<Array<UserDTO>>([]);
const editID = ref<string | undefined>();
async function onQueryAsync() {
    isLoading.value = true;
    try {
        await queryAsync();
    }
    finally {
        isLoading.value = false;
    }
}
async function queryAsync() {
    try {
        const httpResult = await service.GetListAsync(queryData);
        if (!httpResult) return;
        dataList.value = httpResult;
    } catch (error) {
        Message.error("获取用户列表失败");
    }
}
async function resetPasswordAsync(id: string) {
    isLoading.value = true;
    try {
        const newPassword = await service.ResetPasswordAsync(id);
        if (newPassword) {
            Message.success(`重置用户密码成功，新密码为：${newPassword}`);
        }
    } catch (error) {
        Message.error("重置用户密码失败");
    }
    finally {
        isLoading.value = false;
    }
}
async function deleteAsync(id: string) {
    isLoading.value = true;
    try {
        await service.DeleteAsync(id);
        await queryAsync();
    } catch (error) {
        Message.error("删除用户失败");
    }
    finally {
        isLoading.value = false;
    }
}
function openEditPanel(id?: string) {
    editID.value = id;
    editPanelVisible.value = true;
}
async function onEditPanelOKAsync() {
    if (!await userEditorRef.value?.saveAsync()) return;
    editPanelVisible.value = false;
    await queryAsync();
}
async function onEditPanelCancelAsync() {
    editPanelVisible.value = false;
}
onMounted(onQueryAsync);
</script>