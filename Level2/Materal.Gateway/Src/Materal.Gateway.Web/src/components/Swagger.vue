<style scoped>
.data-panel {
    display: grid;
    grid-template-columns: repeat(4, minmax(0, 1fr));
    justify-content: space-evenly;
    gap: 2rem;
}

.detail-title {
    font-weight: bold;
}

.sub-list-item {
    border-bottom: 1px solid #e8e8e8;
    line-height: 32px;
    position: relative;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    padding: 0 64px 0 0;
}

.sub-list-item-actions {
    position: absolute;
    right: 0;
    top: 0;
}
</style>
<template>
    <a-spin :loading="isLoading" style="width: 100%;">
        <a-space direction="vertical" fill>
            <div>
                <a-form :model="queryData" layout="inline" @submit-success="onQueryAsync">
                    <a-form-item field="Key" label="SwaggerKey">
                        <a-input v-model="queryData.Key" />
                    </a-form-item>
                    <a-form-item>
                        <a-button-group>
                            <a-button html-type="submit" type="primary"><template
                                    #icon><icon-search /></template></a-button>
                            <a-button type="outline" @click="openEditPanel()"><template
                                    #icon><icon-plus /></template></a-button>
                        </a-button-group>
                    </a-form-item>
                </a-form>
            </div>
            <div class="data-panel">
                <a-card v-for="item in swaggerList">
                    <template #title>
                        <a-tooltip content="服务发现：是" v-if="item.TakeServersFromDownstreamService">
                            <icon-cloud-download />
                        </a-tooltip>
                        <a-tooltip content="服务发现：否" v-if="!item.TakeServersFromDownstreamService">
                            <icon-file />
                        </a-tooltip>
                        {{ item.Key }}
                    </template>
                    <template #extra>
                        <a-button-group>
                            <a-button type="text"
                                @click="openEditItemPanel(item.ID, item.TakeServersFromDownstreamService)">
                                <template #icon><icon-plus /></template>
                            </a-button>
                            <a-button type="text" @click="openEditPanel(item.ID)">
                                <template #icon><icon-edit /></template>
                            </a-button>
                            <a-popconfirm :content="`是否删除${item.Key}?`" type="warning" ok-text="删除"
                                @ok="async () => await deleteAsync(item.ID)">
                                <a-button type="text">
                                    <template #icon><icon-delete style="color: red;" /></template>
                                </a-button>
                            </a-popconfirm>
                        </a-button-group>
                    </template>
                    <div>
                        <div class="sub-list-item" v-for="subItem in item.Items">
                            [{{ subItem.Version }}]{{ subItem.Name }}
                            <div class="sub-list-item-actions">
                                <a-button type="text"
                                    @click="openEditItemPanel(item.ID, item.TakeServersFromDownstreamService, subItem.ID)">
                                    <template #icon><icon-edit /></template>
                                </a-button>
                                <a-popconfirm :content="`是否删除${item.Key}?`" type="warning" ok-text="删除"
                                    @ok="async () => await deleteItemAsync(item.ID, subItem.ID)">
                                    <a-button type="text">
                                        <template #icon><icon-delete style="color: red;" /></template>
                                    </a-button>
                                </a-popconfirm>
                            </div>
                        </div>
                    </div>
                </a-card>
            </div>
        </a-space>
    </a-spin>
    <a-drawer :width="440" :visible="editPanelVisible" @ok="onEditPanelOKAsync" @cancel="onEditPanelCancelAsync"
        unmountOnClose>
        <template #title>
            {{ editID ? "编辑" : "新增" }}Swagger配置
        </template>
        <SwaggerEditor ref="swaggerEditorRef" :id="editID" />
    </a-drawer>
    <a-drawer :width="550" :visible="editItemPanelVisible" @ok="onEditItemPanelOKAsync" @cancel="onEditItemPanelCancelAsync"
        unmountOnClose>
        <template #title>
            {{ editItemID ? "编辑" : "新增" }}Swagger项配置
        </template>
        <SwaggerItemEditor ref="swaggerItemEditorRef" :id="editItemID" :swagger-config-i-d="editID"
            :is-service-item="isServiceItem" />
    </a-drawer>
</template>
<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import QuerySwaggerModel from '../models/swagger/QuerySwaggerModel';
import service from '../services/SwaggerConfigService';
import { Message } from '@arco-design/web-vue';
import SwaggerDTO from '../models/swagger/SwaggerDTO';
import SwaggerEditor from './SwaggerEditor.vue';
import SwaggerItemEditor from './SwaggerItemEditor.vue';

/**
 * 加载数据标识
 */
const isLoading = ref(false);
const editPanelVisible = ref(false);
const editItemPanelVisible = ref(false);
const swaggerEditorRef = ref<InstanceType<typeof SwaggerEditor>>();
const swaggerItemEditorRef = ref<InstanceType<typeof SwaggerItemEditor>>();
const queryData = reactive<QuerySwaggerModel>({
    Key: ""
});
const swaggerList = ref<Array<SwaggerDTO>>([]);
const editID = ref<string | undefined>();
const editItemID = ref<string | undefined>();
const isServiceItem = ref<boolean | undefined>(false);
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
        swaggerList.value = httpResult;
    } catch (error) {
        Message.error("获取Swagger配置失败");
    }
}
async function deleteAsync(id: string) {
    isLoading.value = true;
    try {
        await service.DeleteAsync(id);
        await queryAsync();
    } catch (error) {
        Message.error("删除Swagger配置失败");
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
    if (!await swaggerEditorRef.value?.saveAsync()) return;
    editPanelVisible.value = false;
    await queryAsync();
}
async function onEditPanelCancelAsync() {
    editPanelVisible.value = false;
}
async function deleteItemAsync(swaggerConfigID: string, id: string) {
    isLoading.value = true;
    try {
        await service.DeleteItemAsync(swaggerConfigID, id);
        await queryAsync();
    } catch (error) {
        Message.error("删除Swagger配置失败");
    }
    finally {
        isLoading.value = false;
    }
}
function openEditItemPanel(swaggerConfigID: string, serviceItem: boolean, id?: string) {
    editID.value = swaggerConfigID;
    editItemID.value = id;
    isServiceItem.value = serviceItem;
    editItemPanelVisible.value = true;
}
async function onEditItemPanelOKAsync() {
    if (!await swaggerItemEditorRef.value?.saveAsync()) return;
    editItemPanelVisible.value = false;
    await queryAsync();
}
async function onEditItemPanelCancelAsync() {
    editItemPanelVisible.value = false;
}
onMounted(onQueryAsync);
</script>