<style scoped>
.data-panel {
    display: grid;
    grid-template-columns: repeat(3, minmax(0, 1fr));
    justify-content: space-evenly;
    gap: 2rem;
}
</style>
<template>
    <div class="data-panel">
        <a-spin :loading="isLoading" style="width: 100%;">
            <a-card>
                <template #title>
                    同步配置
                </template>
                <template #extra>
                    <a-button type="text" @click="syncConfigAsync">
                        <template #icon><icon-sync /></template>
                    </a-button>
                </template>
                <a-form :model="syncConfigData">
                    <a-form-item field="sourceEnvironmentServer" label="源环境">
                        <EnvironmentServerSelect @change="verifyTargetEnvironments" />
                    </a-form-item>
                    <a-form-item field="TargetEnvironments" label="目标环境">
                        <a-select v-model="syncConfigData.TargetEnvironments" multiple>
                            <a-option v-for="item in serverManagement.environmentServerList" :value="item.Service" :disabled="item.Service == serverManagement.selectedEnvironmentServer?.Service">
                                {{ item.Name }}
                            </a-option>
                        </a-select>
                    </a-form-item>
                    <a-form-item field="ProjectID" label="项目">
                        <a-select v-model="syncConfigData.ProjectID" @change="onQueryNamespaceAsync">
                            <a-option v-for="item in projectList" :value="item.ID">
                                {{ item.Name }}
                            </a-option>
                        </a-select>
                    </a-form-item>
                    <a-form-item field="NamespaceIDs" label="命名空间">
                        <a-select v-model="syncConfigData.NamespaceIDs" multiple>
                            <a-option v-for="item in namespaceList" :value="item.ID">
                                {{ item.Name }}
                            </a-option>
                        </a-select>
                    </a-form-item>
                    <a-form-item field="Mode" label="同步方式">
                        <a-select v-model="syncConfigData.Mode">
                            <a-option :value="0">缺少项</a-option>
                            <a-option :value="1">替换</a-option>
                            <a-option :value="2">覆盖</a-option>
                        </a-select>
                    </a-form-item>
                </a-form>
            </a-card>
        </a-spin>
    </div>
</template>
<script setup lang="ts">
import { reactive, ref } from 'vue';
import SyncConfigRequestModel from '../models/configurationItem/SyncConfigRequestModel';
import service from '../services/ConfigurationItemService';
import projectService from '../services/ProjectService';
import namespaceService from '../services/NamespaceService';
import { Message } from '@arco-design/web-vue';
import serverManagement from '../serverManagement'
import { onMounted } from 'vue';
import NamespaceDTO from '../models/namespace/NamespaceDTO';
import ProjectDTO from '../models/project/ProjectDTO';
import EnvironmentServerSelect from './EnvironmentServerSelect.vue';

const isLoading = ref(false);
const projectList = ref<Array<ProjectDTO>>([]);
const namespaceList = ref<Array<NamespaceDTO>>([]);
const syncConfigData = reactive<SyncConfigRequestModel>({
    ProjectID: '',
    NamespaceIDs: [],
    Mode: 0,
    TargetEnvironments: [],
});
async function syncConfigAsync() {
    isLoading.value = true;
    try {
        await service.SyncConfigAsync(syncConfigData);
    } catch (error) {
        Message.error("同步配置失败");
    } finally {
        isLoading.value = false;
    }
}
function verifyTargetEnvironments(){
    for (let index = 0; index < syncConfigData.TargetEnvironments.length; index++) {
        const element = syncConfigData.TargetEnvironments[index];
        if(element != serverManagement.selectedEnvironmentServer?.Service) continue;
        syncConfigData.TargetEnvironments.splice(index, 1);
    }
}
async function loadAllProjectAsync() {
    isLoading.value = true;
    try {
        const projectResult = await projectService.GetListAsync({ Name: "", PageIndex: 1, PageSize: 99999 });
        if (!projectResult) return;
        projectList.value = projectResult;
        if (projectList.value.length > 0) {
            syncConfigData.ProjectID = projectList.value[0].ID;
            await loadNamespaceAsync();
        }
    } catch (error) {
        Message.error("获取项目列表失败");
    }
    finally {
        isLoading.value = false;
    }
}
async function loadNamespaceAsync() {
    try {
        const namespaceResult = await namespaceService.GetListAsync({ ProjectID: syncConfigData.ProjectID, Name: "", PageIndex: 1, PageSize: 99999 });
        if (!namespaceResult) return;
        syncConfigData.NamespaceIDs = [];
        namespaceList.value = namespaceResult;
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