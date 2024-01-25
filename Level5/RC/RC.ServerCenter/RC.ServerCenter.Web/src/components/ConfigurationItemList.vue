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
                <a-form-item field="EnvironmentServer" label="环境">
                    <EnvironmentServerSelect @change="selectedEnvironmentServerAsync" />
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
                            <a-button type="text" @click="openSyncPanel(item.ID)" title="同步">
                                <template #icon><icon-sync /></template>
                            </a-button>
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
                    <div>
                        {{ item.Description }}
                        <p class="data-panel-value">
                            {{ item.Value }}
                        </p>
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
    <a-modal v-model:visible="syncPanelVisible" title="同步配置项" @cancel="() => syncPanelVisible = false" draggable
        @before-ok="syncConfigurationItemAsync" :align-center="false" :top="150">
        <a-form :model="syncFormData">
            <a-form-item field="environmentServer" label="目标环境">
                <a-select v-model="syncFormData.environmentServers" multiple :style="{ width: '320px' }">
                    <a-option v-for="item in serverManagement.environmentServerList" :value="item.Service"
                        :disabled="serverManagement.selectedEnvironmentServer?.Service == item.Service">
                        {{ item.Name }}
                    </a-option>
                </a-select>
            </a-form-item>
        </a-form>
    </a-modal>
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
import EnvironmentServerSelect from './EnvironmentServerSelect.vue';

/**
 * 加载数据标识
 */
const isLoading = ref(false);
const editPanelVisible = ref(false);
const syncPanelVisible = ref(false);
const configurationItemEditorRef = ref<InstanceType<typeof ConfigurationItemEditor>>();
let projectID = ref("");
const queryData = reactive<QueryConfigurationItemModel>({
    Key: "",
    NamespaceID: "",
    PageIndex: 1,
    PageSize: 99999
});
const syncFormData = reactive({
    syncID: "",
    environmentServers: [] as string[]
});
const dataList = ref<Array<ConfigurationItemDTO>>([]);
const projectList = ref<Array<ProjectDTO>>([]);
const namespaceList = ref<Array<NamespaceDTO>>([]);
const editID = ref<string | undefined>();
async function selectedEnvironmentServerAsync() {
    syncFormData.environmentServers = [];
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
        const httpResult = await service.GetListAsync(queryData);
        if (!httpResult) return;
        dataList.value = httpResult;
    } catch (error) {
        Message.error("获取配置项列表失败");
    }
}
async function deleteAsync(id: string) {
    isLoading.value = true;
    try {
        await service.DeleteAsync(id);
        await queryAsync();
    } catch (error) {
        Message.error("删除配置项失败");
    }
    finally {
        isLoading.value = false;
    }
}
function copyValue(id: string) {
    const item = dataList.value.find(x => x.ID === id);
    if (!item) return;
    navigator.clipboard.writeText(item.Value);
    Message.success("复制成功");
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
function openSyncPanel(id: string) {
    syncFormData.syncID = id;
    syncPanelVisible.value = true;
}
async function syncConfigurationItemAsync() {
    if (syncFormData.environmentServers.length <= 0) {
        Message.warning('请选择至少一个目标环境');
        return false;
    }
    const defaultGetServiceNameAsync = service.getServiceNameAsync;
    for (const environmentServer of syncFormData.environmentServers) {
        service.getServiceNameAsync = async () => environmentServer;
        const data = dataList.value.find(x => x.ID === syncFormData.syncID);
        if (!data) continue;
        const key = data.Key;
        const httpResult = await service.GetListAsync({ Key: key, NamespaceID: queryData.NamespaceID, PageIndex: 1, PageSize: 1 });
        if (!httpResult) continue;
        if (httpResult.length > 0) {
            service.EditAsync({ ID: httpResult[0].ID, Key: data.Key, Value: data.Value, Description: data.Description });
        }
        else {
            service.AddAsync({ NamespaceID: data.NamespaceID, Key: data.Key, Value: data.Value, Description: data.Description });
        }
    }
    service.getServiceNameAsync = defaultGetServiceNameAsync;
    Message.success('同步完毕');
    return true;
}
async function onEditPanelCancelAsync() {
    editPanelVisible.value = false;
}
async function loadAllProjectAsync() {
    isLoading.value = true;
    try {
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
        Message.error("获取命名空间列表失败");
    }
}
async function onQueryNamespaceAsync() {
    isLoading.value = true;
    try {
        loadNamespaceAsync();
    } catch (error) {
        Message.error("获取命名空间列表失败");
    }
    finally {
        isLoading.value = false;
    }
}
onMounted(loadAllProjectAsync);
</script>