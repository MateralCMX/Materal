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
                <a-form-item field="Name" label="名称">
                    <a-input v-model="queryData.Name" />
                </a-form-item>
                <a-form-item field="Description" label="描述">
                    <a-input v-model="queryData.Description" />
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
                        {{ item.Name }}
                    </template>
                    <template #extra>
                        <a-button-group>
                            <a-button type="text" @click="openEditPanel(item.ID)" title="编辑">
                                <template #icon><icon-edit /></template>
                            </a-button>
                            <a-popconfirm :content="`是否删除${item.Name}?`" type="warning" ok-text="删除"
                                @ok="async () => await deleteAsync(item.ID)">
                                <a-button type="text" title="删除">
                                    <template #icon><icon-delete style="color: red;" /></template>
                                </a-button>
                            </a-popconfirm>
                        </a-button-group>
                    </template>
                    <div>
                        {{ item.Description }}
                    </div>
                </a-card>
            </div>
        </a-space>
    </a-spin>
    <a-drawer :width="440" :visible="editPanelVisible" @ok="onEditPanelOKAsync" @cancel="onEditPanelCancelAsync"
        unmountOnClose>
        <template #title>
            {{ editID ? "编辑" : "新增" }}项目
        </template>
        <ProjectEditor ref="projectEditorRef" :id="editID" />
    </a-drawer>
</template>
<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import QueryProjectModel from '../models/project/QueryProjectModel';
import service from '../services/ProjectService';
import { Message } from '@arco-design/web-vue';
import ProjectDTO from '../models/project/ProjectDTO';
import ProjectEditor from './ProjectEditor.vue';

/**
 * 加载数据标识
 */
const isLoading = ref(false);
const editPanelVisible = ref(false);
const projectEditorRef = ref<InstanceType<typeof ProjectEditor>>();
const queryData = reactive<QueryProjectModel>({
    Name: "",
    Description: "",
    PageIndex: 1,
    PageSize: 99999
});
const dataList = ref<Array<ProjectDTO>>([]);
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
        Message.error("获取项目列表失败");
    }
}
async function deleteAsync(id: string) {
    isLoading.value = true;
    try {
        await service.DeleteAsync(id);
        await queryAsync();
    } catch (error) {
        Message.error("删除项目失败");
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
    if (!await projectEditorRef.value?.saveAsync()) return;
    editPanelVisible.value = false;
    await queryAsync();
}
async function onEditPanelCancelAsync() {
    editPanelVisible.value = false;
}
onMounted(onQueryAsync);
</script>