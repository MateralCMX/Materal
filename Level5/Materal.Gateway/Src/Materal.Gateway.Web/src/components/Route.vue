<style scoped>
.list-actions {
    position: absolute;
    right: 10px;
    top: 50%;
    transform: translateY(-50%);
}

.list-item {
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    padding: 0 64px 0 0;
}

.list-item-title {
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    font-weight: bold;
}

.list-item-content {
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    font-size: 12px;
}
.data-panel{
    width: 800px;
    margin: 0 auto;
    background-color: white;
}
</style>
<template>
    <a-spin :loading="isLoading" style="width: 100%;">
        <a-space direction="vertical" fill>
            <a-form :model="queryData" layout="inline" @submit-success="onQueryAsync">
                <a-form-item field="UpstreamPathTemplate" label="上游路径模版">
                    <a-input v-model="queryData.UpstreamPathTemplate" />
                </a-form-item>
                <a-form-item field="DownstreamPathTemplate" label="下游路径模版">
                    <a-input v-model="queryData.DownstreamPathTemplate" />
                </a-form-item>
                <a-form-item field="ServiceName" label="服务名">
                    <a-input v-model="queryData.ServiceName" />
                </a-form-item>
                <a-form-item field="SwaggerKey" label="SwaggerKey">
                    <a-input v-model="queryData.SwaggerKey" />
                </a-form-item>
                <a-form-item field="EnableCache" label="缓存">
                    <a-select v-model="queryData.EnableCache" allow-clear style="width: 100px;">
                        <a-option :value="true">启用</a-option>
                        <a-option :value="false">禁用</a-option>
                    </a-select>
                </a-form-item>
                <a-form-item>
                    <a-button-group>
                        <a-button html-type="submit" type="primary">
                            <template #icon><icon-search /></template>
                        </a-button>
                        <a-button type="outline" @click="openEditPanel()">
                            <template #icon><icon-plus /></template>
                        </a-button>
                    </a-button-group>
                </a-form-item>
            </a-form>
            <a-list class="data-panel">
                <a-list-item v-for="item in dataList" style="position: relative;">
                    <a-row align="center">
                        <a-col :span="8">
                            <a-space direction="vertical" fill>
                                <div class="list-item-title"># 服务名称</div>
                                <div class="list-item-content">{{ item.ServiceName }}</div>
                                <div class="list-item-title"># Swagger</div>
                                <div class="list-item-content">{{ item.SwaggerKey }}</div>
                                <div class="list-item-title"># 其他配置</div>
                                <div class="list-item-content">
                                    <a-space>
                                        <a-badge :status="item.FileCacheOptions === undefined ? 'normal' : 'success'"
                                            text="缓存" />
                                        <a-badge :status="item.QoSOptions === undefined ? 'normal' : 'success'" text="熔断" />
                                        <a-badge :status="item.AuthenticationOptions === undefined ? 'normal' : 'success'"
                                            text="鉴权" />
                                        <a-badge :status="item.RateLimitOptions === undefined ? 'normal' : 'success'"
                                            text="限流" />
                                    </a-space>
                                </div>
                            </a-space>
                        </a-col>
                        <a-col :span="8">
                            <a-space direction="vertical" fill>
                                <div class="list-item-title"># 路径配置</div>
                                <div class="list-item-content">上游路径：{{ item.UpstreamPathTemplate }}</div>
                                <div class="list-item-content">下游路径：{{ item.DownstreamPathTemplate }}</div>
                                <div class="list-item-title"># WebData</div>
                                <div class="list-item-content">转发方式：{{ item.DownstreamScheme }}</div>
                                <div class="list-item-content">Http版本：{{ item.DownstreamHttpVersion }}</div>
                                <div class="list-item-content">转发类型：{{ item.UpstreamHttpMethod }}</div>
                            </a-space>
                        </a-col>
                        <a-col :span="5" :offset="3">
                            <a-button-group>
                                <a-button type="text" @click="openEditPanel(item.ID)">
                                    <template #icon><icon-edit /></template>
                                </a-button>
                                <a-popconfirm :content="`是否删除路由?`" type="warning" ok-text="删除"
                                    @ok="async () => await deleteAsync(item.ID)">
                                    <a-button type="text">
                                        <template #icon><icon-delete style="color: red;" /></template>
                                    </a-button>
                                </a-popconfirm>
                                <a-button type="text" @click="async () => await moveUpAsync(item.ID)">
                                    <template #icon><icon-arrow-up /></template>
                                </a-button>
                                <a-button type="text" @click="async () => await moveNextAsync(item.ID)">
                                    <template #icon><icon-arrow-down /></template>
                                </a-button>
                            </a-button-group>
                        </a-col>
                    </a-row>
                </a-list-item>
            </a-list>
        </a-space>
    </a-spin>
    <a-drawer :width="900" :visible="editPanelVisible" @ok="onEditPanelOKAsync" @cancel="onEditPanelCancelAsync"
        unmountOnClose>
        <template #title>
            {{ editID ? "编辑" : "新增" }}Route
        </template>
        <RouteEditor ref="routeEditorRef" :id="editID" />
    </a-drawer>
</template>
<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue';
import service from '../services/RouteService';
import QueryRouteModel from '../models/route/QueryRouteModel';
import { Message } from '@arco-design/web-vue';
import RouteDTO from '../models/route/RouteDTO';
import RouteEditor from './RouteEditor.vue';

/**
 * 加载数据标识
 */
const isLoading = ref(false);
const editPanelVisible = ref(false);
const editID = ref<string | undefined>();
const routeEditorRef = ref<InstanceType<typeof RouteEditor>>();
const queryData = reactive<QueryRouteModel>({
    UpstreamPathTemplate: '',
    DownstreamPathTemplate: '',
    ServiceName: '',
    SwaggerKey: '',
    EnableCache: undefined
});
const dataList = ref<Array<RouteDTO>>([]);
async function queryAsync() {
    try {
        const httpResult = await service.GetListAsync(queryData);
        if (!httpResult) return;
        dataList.value = httpResult;
    } catch (error) {
        Message.error("获取Route配置失败");
    }
}
async function deleteAsync(id: string) {
    isLoading.value = true;
    try {
        await service.DeleteAsync(id);
        await queryAsync();
    } catch (error) {
        Message.error("删除Route失败");
    }
    finally {
        isLoading.value = false;
    }
}
async function moveUpAsync(id: string) {
    isLoading.value = true;
    try {
        await service.moveUpAsync(id);
        await queryAsync();
    } catch (error) {
        Message.error("移动Route失败");
    }
    finally {
        isLoading.value = false;
    }
}
async function moveNextAsync(id: string) {
    isLoading.value = true;
    try {
        await service.moveNextAsync(id);
        await queryAsync();
    } catch (error) {
        Message.error("移动Route失败");
    }
    finally {
        isLoading.value = false;
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
function openEditPanel(id?: string) {
    editID.value = id;
    editPanelVisible.value = true;
}
async function onEditPanelOKAsync() {
    if (!await routeEditorRef.value?.saveAsync()) return;
    editPanelVisible.value = false;
    await queryAsync();
}
async function onEditPanelCancelAsync() {
    editPanelVisible.value = false;
}
onMounted(onQueryAsync);
</script>