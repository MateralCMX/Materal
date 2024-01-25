<style scoped>
.data-panel {
    display: grid;
    grid-template-columns: repeat(3, minmax(0, 1fr));
    justify-content: space-evenly;
    gap: 2rem;
}

.data-panel-value {
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
}

.yellow {
    color: #faad14;
}

.green {
    color: #52c41a;
}

.red {
    color: #f5222d;
}

.upload-button {
    width: 32px;
    height: 32px;
    text-align: center;
}

.upload-button:hover {
    background-color: #f2f3f5;
}
</style>
<template>
    <a-spin :loading="isLoading" style="width: 100%;">
        <a-space direction="vertical" fill>
            <a-form :model="queryData" layout="inline" @submit-success="onQueryAsync">
                <a-form-item field="EnvironmentServer" label="目标">
                    <DeploySelect @change="onQueryAsync" />
                </a-form-item>
                <a-form-item field="ProjectID" label="应用程序类型">
                    <a-select v-model="queryData.ApplicationType" :style="{ width: '320px' }" allow-clear
                        @change="queryAsync">
                        <a-option v-for="item in applicationTypeList" :value="item.Key">{{ item.Value }}</a-option>
                    </a-select>
                </a-form-item>
                <a-form-item field="Name" label="名称">
                    <a-input v-model="queryData.Name" />
                </a-form-item>
                <a-form-item field="RootPath" label="路径">
                    <a-input v-model="queryData.RootPath" />
                </a-form-item>
                <a-form-item field="MainModule" label="模块">
                    <a-input v-model="queryData.MainModule" />
                </a-form-item>
                <a-form-item>
                    <a-button-group>
                        <a-button html-type="submit" type="primary" title="查询">
                            <template #icon><icon-search /></template>
                        </a-button>
                        <a-button type="outline" @click="startAllAsync()" title="启动所有">
                            <template #icon><icon-play-circle /></template>
                        </a-button>
                        <a-button type="outline" @click="stopAllAsync()" title="停止所有">
                            <template #icon><icon-record-stop /></template>
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
                        <span :class="getClassByApplicationStatus(item.ApplicationStatus)">
                            <icon-play-circle v-if="item.ApplicationStatus == 1" />
                            <icon-record-stop v-if="item.ApplicationStatus == 3" />
                            <icon-swap v-if="item.ApplicationStatus == 4" />
                            {{ item.Name }}
                        </span>
                    </template>
                    <template #extra>
                        <a-button-group>
                            <a-button type="text" @click="openConsolePanel(item.ID)" title="控制台">
                                <template #icon><icon-code-square /></template>
                            </a-button>
                            <a-button v-if="item.ApplicationStatus === 3" type="text" @click="startAsync(item.ID)"
                                title="启动">
                                <template #icon><icon-play-circle /></template>
                            </a-button>
                            <a-button v-if="item.ApplicationStatus === 1" type="text" @click="stopAsync(item.ID)"
                                title="关闭">
                                <template #icon><icon-record-stop /></template>
                            </a-button>
                            <a-button v-if="item.ApplicationStatus === 1" type="text" @click="killAsync(item.ID)"
                                title="强制关闭">
                                <template #icon><icon-poweroff /></template>
                            </a-button>
                            <a-upload class="upload-button" v-if="item.ApplicationStatus === 3" draggable
                                :custom-request="(option) => customRequest(item.ID, option)">
                                <template #upload-button>
                                    <div style="line-height: 32px;">
                                        <icon-upload />
                                    </div>
                                </template>
                            </a-upload>
                            <a-button v-if="item.ApplicationStatus === 3" type="text" @click="applyLasetFileAsync(item.ID)"
                                title="使用最后一次上传的文件">
                                <template #icon><icon-copy /></template>
                            </a-button>
                            <a-button type="text" @click="openFileListPanel(item.ID)" title="文件列表">
                                <template #icon><icon-list /></template>
                            </a-button>
                            <a-button v-if="item.ApplicationStatus === 3" type="text" @click="openEditPanel(item.ID)"
                                title="编辑">
                                <template #icon><icon-edit /></template>
                            </a-button>
                            <a-popconfirm v-if="item.ApplicationStatus === 3" :content="`是否删除${item.Name}?`" type="warning"
                                ok-text="删除" @ok="async () => await deleteAsync(item.ID)">
                                <a-button type="text" title="删除">
                                    <template #icon><icon-delete style="color: red;" /></template>
                                </a-button>
                            </a-popconfirm>
                        </a-button-group>
                    </template>
                    <a-space direction="vertical" :size="16" style="display: block;">
                        <a-row>
                            <a-col :span="24">
                                名称：{{ item.Name }}
                            </a-col>
                        </a-row>
                        <a-row>
                            <a-col :span="12">
                                路径：{{ item.RootPath }}
                            </a-col>
                            <a-col :span="12">
                                模块：{{ item.MainModule }}
                            </a-col>
                        </a-row>
                        <a-row>
                            <a-col :span="6">
                                类型：{{ item.ApplicationTypeTxt }}
                            </a-col>
                            <a-col :span="6">
                                增量更新：{{ item.IsIncrementalUpdating ? '是' : '否' }}
                            </a-col>
                            <a-col :span="12">
                                状态：<span :class="getClassByApplicationStatus(item.ApplicationStatus)">{{
                                    item.ApplicationStatusTxt }}</span>
                            </a-col>
                        </a-row>
                        <a-row>
                            <a-col :span="24" class="data-panel-value">
                                参数：{{ item.RunParams }}
                            </a-col>
                        </a-row>
                    </a-space>
                </a-card>
            </div>
        </a-space>
    </a-spin>
    <a-drawer :width="1000" :visible="editPanelVisible" @ok="onEditPanelOKAsync" @cancel="onEditPanelCancelAsync"
        unmountOnClose>
        <template #title>
            {{ editID ? "编辑" : "新增" }}应用程序
        </template>
        <ApplicationInfoEditor ref="applicationInfoEditorRef" :id="editID" />
    </a-drawer>
    <a-drawer :width="800" :visible="fileListPanelVisible" @cancel="onFileListPanelCancelAsync" unmountOnClose
        :footer="false">
        <template #title>
            应用程序文件列表
        </template>
        <FileListPanel ref="applicationInfoEditorRef" :id="selectApplicationID" />
    </a-drawer>
    <a-drawer :width="1400" :visible="consolePanelVisible" @cancel="onConsolePanelCancelAsync" unmountOnClose
        :footer="false">
        <template #title>
            控制台消息
        </template>
        <ConsolePanel ref="applicationInfoEditorRef" :id="selectApplicationID" />
    </a-drawer>
</template>
<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import QueryApplicationInfoModel from '../models/applicationInfo/QueryApplicationInfoModel';
import service from '../services/ApplicationInfoService';
import deployEnumsService from '../services/DeployEnumsService';
import { Message, RequestOption, UploadRequest } from '@arco-design/web-vue';
import ApplicationInfoDTO from '../models/applicationInfo/ApplicationInfoDTO';
import ApplicationInfoEditor from './ApplicationInfoEditor.vue';
import FileListPanel from './FileListPanel.vue';
import ConsolePanel from './ConsolePanel.vue';
import KeyValueModel from '../models/KeyValueModel';
import loginManagement from '../loginManagement';
import DeploySelect from './DeploySelect.vue';

/**
 * 加载数据标识
 */
const isLoading = ref(false);
const editPanelVisible = ref(false);
const fileListPanelVisible = ref(false);
const consolePanelVisible = ref(false);
const applicationInfoEditorRef = ref<InstanceType<typeof ApplicationInfoEditor>>();
const queryData = reactive<QueryApplicationInfoModel>({
    Name: "",
    RootPath: "",
    MainModule: "",
    ApplicationType: undefined,
    PageIndex: 1,
    PageSize: 99999
});
const dataList = ref<Array<ApplicationInfoDTO>>([]);
const applicationTypeList = ref<Array<KeyValueModel>>([]);
const editID = ref<string | undefined>();
const selectApplicationID = ref<string | undefined>();
function getClassByApplicationStatus(applicationStatus: number) {
    switch (applicationStatus) {
        case 0:
        case 1:
            return 'green';
        case 2:
        case 3:
            return 'red';
        default:
            return 'yellow';
    }
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
        if (queryData.ApplicationType as any === '') {
            queryData.ApplicationType = undefined;
        }
        const httpResult = await service.GetListAsync(queryData);
        if (!httpResult) return;
        dataList.value = httpResult;
    } catch (error) {
        Message.error("获取应用程序列表失败");
    }
}
async function deleteAsync(id: string) {
    isLoading.value = true;
    try {
        await service.DeleteAsync(id);
        await queryAsync();
    } catch (error) {
        Message.error("删除应用程序失败");
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
    if (!await applicationInfoEditorRef.value?.saveAsync()) return;
    editPanelVisible.value = false;
    await queryAsync();
}
async function onEditPanelCancelAsync() {
    editPanelVisible.value = false;
}
async function loadApplicationTypeAsync() {
    isLoading.value = true;
    try {
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
async function startAllAsync() {
    isLoading.value = true;
    try {
        await service.StartAllAsync();
    } catch (error) {
        Message.error("启动应用程序失败");
    }
    finally {
        isLoading.value = false;
    }
}
async function stopAllAsync() {
    isLoading.value = true;
    try {
        await service.StopAllAsync();
    } catch (error) {
        Message.error("停止应用程序失败");
    }
    finally {
        isLoading.value = false;
    }
}
async function startAsync(id: string) {
    isLoading.value = true;
    try {
        await service.StartAsync(id);
    } catch (error) {
        Message.error("启动应用程序失败");
    }
    finally {
        isLoading.value = false;
    }
}
async function stopAsync(id: string) {
    isLoading.value = true;
    try {
        await service.StopAsync(id);
    } catch (error) {
        Message.error("停止应用程序失败");
    }
    finally {
        isLoading.value = false;
    }
}
async function killAsync(id: string) {
    isLoading.value = true;
    try {
        await service.KillAsync(id);
    } catch (error) {
        Message.error("强制关闭应用程序失败");
    }
    finally {
        isLoading.value = false;
    }
}
async function applyLasetFileAsync(id: string) {
    isLoading.value = true;
    try {
        await service.ApplyLasetFileAsync(id);
    } catch (error) {
        Message.error("应用最后一次上传文件失败");
    }
    finally {
        isLoading.value = false;
    }
}
function openFileListPanel(id: string) {
    selectApplicationID.value = id;
    fileListPanelVisible.value = true;
}
async function onFileListPanelCancelAsync() {
    fileListPanelVisible.value = false;
}
function openConsolePanel(id: string) {
    selectApplicationID.value = id;
    consolePanelVisible.value = true;
}
async function onConsolePanelCancelAsync() {
    consolePanelVisible.value = false;
}
function customRequest(id: string, option: RequestOption): UploadRequest {
    const xhr = new XMLHttpRequest();
    if (xhr.upload) {
        xhr.upload.onprogress = function (event) {
            let percent = 0;
            if (event.total > 0) {
                percent = event.loaded / event.total;
            }
            option.onProgress(percent, event);
        };
    }
    xhr.onerror = function error(e) {
        option.onError(e);
    };
    xhr.onload = function onload() {
        if (xhr.status < 200 || xhr.status >= 300) return option.onError(xhr.responseText);
        option.onSuccess(xhr.response);
        debugger;
    };
    const formData = new FormData();
    formData.append("file", option.fileItem.file as File);
    const url = service.GetUploadFileUrl(id);
    xhr.open('put', url, true);
    xhr.setRequestHeader('Authorization', `Bearer ${loginManagement.getToken()}`);
    xhr.send(formData);
    return { abort() { xhr.abort() } }
}
onMounted(loadApplicationTypeAsync);
</script>