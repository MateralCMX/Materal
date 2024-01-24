<style scoped>
.data-panel {
    display: grid;
    grid-template-columns: repeat(4, minmax(0, 1fr));
    justify-content: space-evenly;
    gap: 2rem;
}

.data-panel-value {
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
}
</style>
<template>
    <a-spin :loading="isLoading" style="width: 100%;">
        <a-space direction="vertical" fill>
            <a-form :model="queryData" layout="inline" @submit-success="onQueryAsync">
                <a-form-item field="EnvironmentServer" label="目标">
                    <a-select v-model="deploy" :style="{ width: '320px' }" @change="selectedDeploy">
                        <a-option v-for="item in serverManagement.deployList" :value="item.Service">
                            {{ item.Name }}
                        </a-option>
                    </a-select>
                </a-form-item>
                <a-form-item field="ProjectID" label="应用程序类型">
                    <a-select v-model="queryData.ApplicationType" :style="{ width: '320px' }" allow-clear
                        @change="queryAsync">
                        <a-option v-for="item in applicationTypeList" :value="item.Key">{{ item.Value }}</a-option>
                    </a-select>
                </a-form-item>
                <a-form-item field="Key" label="键">
                    <a-input v-model="queryData.Key" />
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
                <a-card v-for="item in dataList">
                    <template #title>
                        {{ item.Key }}-{{ item.ApplicationTypeText }}
                    </template>
                    <template #extra>
                        <a-button-group>
                            <a-button type="text" @click="copyValue(item.ID)" title="复制值">
                                <template #icon><icon-copy /></template>
                            </a-button>
                            <a-button type="text" @click="openEditPanel(item.ID)" title="编辑">
                                <template #icon><icon-edit /></template>
                            </a-button>
                            <a-popconfirm :content="`是否删除${item.Key}?`" type="warning" ok-text="删除"
                                @ok="async () => await deleteAsync(item.ID)">
                                <a-button type="text" title="删除">
                                    <template #icon><icon-delete style="color: red;" /></template>
                                </a-button>
                            </a-popconfirm>
                        </a-button-group>
                    </template>
                    <div class="data-panel-value">
                        {{ item.Data }}
                    </div>
                </a-card>
            </div>
        </a-space>
    </a-spin>
    <a-drawer :width="1000" :visible="editPanelVisible" @ok="onEditPanelOKAsync" @cancel="onEditPanelCancelAsync"
        unmountOnClose>
        <template #title>
            {{ editID ? "编辑" : "新增" }}默认数据
        </template>
        <DefaultDataEditor ref="defaultDataEditorRef" :id="editID" />
    </a-drawer>
</template>
<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import QueryDefaultDataModel from '../models/defaultData/QueryDefaultDataModel';
import service from '../services/DefaultDataService';
import deployEnumsService from '../services/DeployEnumsService';
import { Message } from '@arco-design/web-vue';
import DefaultDataDTO from '../models/defaultData/DefaultDataDTO';
import DefaultDataEditor from './DefaultDataEditor.vue';
import serverManagement from '../serverManagement';
import KeyValueModel from '../models/KeyValueModel';

/**
 * 加载数据标识
 */
const isLoading = ref(false);
const editPanelVisible = ref(false);
const defaultDataEditorRef = ref<InstanceType<typeof DefaultDataEditor>>();
const queryData = reactive<QueryDefaultDataModel>({
    Key: "",
    ApplicationType: undefined,
    PageIndex: 1,
    PageSize: 99999
});

const dataList = ref<Array<DefaultDataDTO>>([]);
const applicationTypeList = ref<Array<KeyValueModel>>([]);
const editID = ref<string | undefined>();
const deploy = ref<string>();
async function selectedDeploy() {
    serverManagement.checkEnvironmentServer(deploy.value);
    await onQueryAsync();
}
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
        if (deploy.value) {
            service.serviceName = deploy.value;
        }
        if (queryData.ApplicationType as any === '') {
            queryData.ApplicationType = undefined;
        }
        const httpResult = await service.GetListAsync(queryData);
        if (!httpResult) return;
        dataList.value = httpResult;
    } catch (error) {
        Message.error("获取默认数据列表失败");
    }
}
async function deleteAsync(id: string) {
    isLoading.value = true;
    try {
        await service.DeleteAsync(id);
        await queryAsync();
    } catch (error) {
        Message.error("删除默认数据失败");
    }
    finally {
        isLoading.value = false;
    }
}
function copyValue(id: string) {
    const item = dataList.value.find(x => x.ID === id);
    if (!item) return;
    navigator.clipboard.writeText(item.Data);
    Message.success("复制成功");
}
function openEditPanel(id?: string) {
    editID.value = id;
    editPanelVisible.value = true;
}
async function onEditPanelOKAsync() {
    if (!await defaultDataEditorRef.value?.saveAsync()) return;
    editPanelVisible.value = false;
    await queryAsync();
}
async function onEditPanelCancelAsync() {
    editPanelVisible.value = false;
}
async function loadApplicationTypeAsync() {
    isLoading.value = true;
    try {
        if (!deploy.value) return;
        deployEnumsService.serviceName = deploy.value;
        const namespaceResult = await deployEnumsService.GetAllApplicationTypeEnumAsync();
        if (!namespaceResult) return;
        applicationTypeList.value = namespaceResult;
        queryAsync();
    } catch (error) {
        Message.error("获取应用程序类型列表失败");
    }
    finally {
        isLoading.value = false;
    }
}
async function initAsync() {
    if (!serverManagement.selectedDeploy) {
        await serverManagement.initAsync();
    }
    deploy.value = serverManagement.selectedDeploy?.Service;
    await loadApplicationTypeAsync();
}
onMounted(initAsync);
</script>