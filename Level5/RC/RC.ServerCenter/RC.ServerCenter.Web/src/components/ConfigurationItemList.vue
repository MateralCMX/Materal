<style scoped>
.data-panel {
    display: grid;
    grid-template-columns: repeat(6, minmax(0, 1fr));
    justify-content: space-evenly;
    gap: 2rem;
}
</style>
<template>
    <a-spin :loading="isLoading" style="width: 100%;">
        <a-space direction="vertical" fill>
            <a-form :model="queryData" layout="inline" @submit-success="onQueryAsync">
                <a-form-item field="EnvironmentServer" label="环境">
                    <a-select v-model="environmentServer" :style="{ width: '320px' }" @change="selectedEnvironmentServer">
                        <a-option v-for="item in serverManagement.environmentServerList" :value="item.Service">
                            {{ item.Name }}
                        </a-option>
                    </a-select>
                </a-form-item>
                <a-form-item field="ProjectID" label="项目">
                    <a-select v-model="projectID" :style="{ width: '320px' }" @change="onQueryNamespaceAsync">
                        <a-option v-for="item in projectList" :value="item.ID">{{ item.Name }}</a-option>
                    </a-select>
                </a-form-item>
                <a-form-item field="NamespaceID" label="命名空间">
                    <a-select v-model="queryData.NamespaceID" :style="{ width: '320px' }" @change="onQueryAsync">
                        <a-option v-for="item in namespaceList" :value="item.ID">{{ item.Name }}</a-option>
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
                        {{ item.Key }}
                    </template>
                    <template #extra>
                        <a-button-group>
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
                    <div>
                        {{ item.Value }}
                    </div>
                </a-card>
            </div>
        </a-space>
    </a-spin>
    <a-drawer :width="1000" :visible="editPanelVisible" @ok="onEditPanelOKAsync" @cancel="onEditPanelCancelAsync"
        unmountOnClose>
        <template #title>
            {{ editID ? "编辑" : "新增" }}配置项
        </template>
        <ConfigurationItemEditor ref="configurationItemEditorRef" :id="editID" :namespace-i-d="queryData.NamespaceID" />
    </a-drawer>
</template>
<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import QueryConfigurationItemModel from '../models/configurationItem/QueryConfigurationItemModel';
import service from '../services/ConfigurationItemService';
import projectService from '../services/ProjectService';
import namespaceService from '../services/NamespaceService';
import { Message } from '@arco-design/web-vue';
import ConfigurationItemDTO from '../models/configurationItem/ConfigurationItemDTO';
import ConfigurationItemEditor from './ConfigurationItemEditor.vue';
import ProjectDTO from '../models/project/ProjectDTO';
import NamespaceDTO from '../models/namespace/NamespaceDTO';
import serverManagement from '../serverManagement';

/**
 * 加载数据标识
 */
const isLoading = ref(false);
const editPanelVisible = ref(false);
const configurationItemEditorRef = ref<InstanceType<typeof ConfigurationItemEditor>>();
let projectID = ref("");
const queryData = reactive<QueryConfigurationItemModel>({
    Key: "",
    NamespaceID: "",
    PageIndex: 1,
    PageSize: 99999
});
const dataList = ref<Array<ConfigurationItemDTO>>([]);
const projectList = ref<Array<ProjectDTO>>([]);
const namespaceList = ref<Array<NamespaceDTO>>([]);
const editID = ref<string | undefined>();
const environmentServer = ref<string>();
async function selectedEnvironmentServer() {
    serverManagement.checkEnvironmentServer(environmentServer.value);
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
        if (serverManagement.selectedEnvironmentServer) {
            service.serviceName = serverManagement.selectedEnvironmentServer.Service;
        }
        const httpResult = await service.GetListAsync(queryData);
        if (!httpResult) return;
        dataList.value = httpResult;
    } catch (error) {
        Message.error("获取命名空间列表失败");
    }
}
async function deleteAsync(id: string) {
    isLoading.value = true;
    try {
        await service.DeleteAsync(id);
        await queryAsync();
    } catch (error) {
        Message.error("删除命名空间失败");
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
    if (!await configurationItemEditorRef.value?.saveAsync()) return;
    editPanelVisible.value = false;
    await queryAsync();
}
async function onEditPanelCancelAsync() {
    editPanelVisible.value = false;
}
async function loadAllProjectAsync() {
    isLoading.value = true;
    try {
        environmentServer.value = serverManagement.selectedEnvironmentServer?.Service;
        const projectResult = await projectService.GetListAsync({ Name: "", PageIndex: 1, PageSize: 99999 });
        if (!projectResult) return;
        projectList.value = projectResult;
        if (projectList.value.length > 0) {
            projectID.value = projectList.value[0].ID;
            await loadNamespaceAsync();
        }
        queryAsync();
    } catch (error) {
        Message.error("获取项目列表失败");
    }
    finally {
        isLoading.value = false;
    }
}
async function loadNamespaceAsync() {
    try {
        const namespaceResult = await namespaceService.GetListAsync({ ProjectID: projectID.value, Name: "", PageIndex: 1, PageSize: 99999 });
        if (!namespaceResult) return;
        namespaceList.value = namespaceResult;
        if (namespaceList.value.length > 0) {
            queryData.NamespaceID = namespaceList.value[0].ID;
        }
        queryAsync();
    } catch (error) {
        Message.error("获取项目列表失败");
    }
}
async function onQueryNamespaceAsync() {
    isLoading.value = true;
    try {
        loadNamespaceAsync();
    } catch (error) {
        Message.error("获取项目列表失败");
    }
    finally {
        isLoading.value = false;
    }
}
onMounted(loadAllProjectAsync);
</script>