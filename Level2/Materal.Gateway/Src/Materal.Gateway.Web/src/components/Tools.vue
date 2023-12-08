<style scoped>
.data-panel {
    display: grid;
    grid-template-columns: repeat(4, minmax(0, 1fr));
    justify-content: space-evenly;
    gap: 2rem;
}
</style>
<template>
    <div class="data-panel">
        <a-spin :loading="isLoading" style="width: 100%;">
            <a-card>
                <template #title>
                    从Consul获取Swagger配置
                </template>
                <template #extra>
                    <a-button type="text" @click="GetSwaggerFromConsulAsync">
                        <template #icon><icon-download /></template>
                    </a-button>
                </template>
                <a-form :model="swaggerFormData">
                    <a-form-item field="Name" label="Key">
                        <a-input v-model="swaggerFormData.Name" placeholder="可使用正则表达式" />
                    </a-form-item>
                    <a-form-item field="Tag" label="标签">
                        <a-input v-model="swaggerFormData.Tag" placeholder="可使用正则表达式" />
                    </a-form-item>
                    <a-form-item field="Clear" label="覆盖">
                        <a-switch v-model="swaggerFormData.Clear" />
                    </a-form-item>
                </a-form>
            </a-card>
        </a-spin>
        <a-spin :loading="isLoading" style="width: 100%;">
            <a-card>
                <template #title>
                    从Consul获取Route配置
                </template>
                <template #extra>
                    <a-button type="text" @click="GetRouteFromConsulAsync">
                        <template #icon><icon-download /></template>
                    </a-button>
                </template>
                <a-form :model="routeFormData">
                    <a-form-item field="Name" label="Key">
                        <a-input v-model="routeFormData.Name" placeholder="可使用正则表达式" />
                    </a-form-item>
                    <a-form-item field="Tag" label="标签">
                        <a-input v-model="routeFormData.Tag" placeholder="可使用正则表达式" />
                    </a-form-item>
                    <a-form-item field="UpstreamPathTemplate" label="上游模版">
                        <a-input v-model="routeFormData.UpstreamPathTemplate"/>
                    </a-form-item>
                    <a-form-item field="DownstreamPathTemplate" label="下游模版">
                        <a-input v-model="routeFormData.DownstreamPathTemplate" />
                    </a-form-item>
                    <a-form-item field="DangerousAcceptAnyServerCertificateValidator" label="忽略证书">
                        <a-switch v-model="routeFormData.DangerousAcceptAnyServerCertificateValidator" />
                    </a-form-item>
                    <a-form-item field="DownstreamPathTemplate" label="转发方式">
                        <a-select v-model="routeFormData.DownstreamPathTemplate">
                            <a-option value="http">http</a-option>
                            <a-option value="https">https</a-option>
                            <a-option value="ws">ws</a-option>
                            <a-option value="wss">wss</a-option>
                            <a-option value="grpc">grpc</a-option>
                            <a-option value="grpcs">grpcs</a-option>
                        </a-select>
                    </a-form-item>
                    <a-form-item field="HttpVersion" label="Http版本">
                        <a-select v-model="routeFormData.HttpVersion">
                            <a-option value="1.0">1.0</a-option>
                            <a-option value="1.1">1.1</a-option>
                            <a-option value="2.0">2.0</a-option>
                        </a-select>
                    </a-form-item>
                    <a-form-item field="Mode" label="获取模式">
                        <a-select v-model="routeFormData.Mode">
                            <a-option :value="0">添加[只加缺少的]</a-option>
                            <a-option :value="1">替换[添加缺少的，替换已有的]</a-option>
                            <a-option :value="2">覆盖[删除旧的,添加新的]</a-option>
                        </a-select>
                    </a-form-item>
                </a-form>
            </a-card>
        </a-spin>
    </div>
</template>
<script setup lang="ts">
import { reactive, ref } from 'vue';
import service from "../services/ToolsService";
import GetSwaggerFromConsulModel from '../models/tools/GetSwaggerFromConsulModel';
import GetRouteFromConsulModel from '../models/tools/GetRouteFromConsulModel';
import { Message } from '@arco-design/web-vue';

const isLoading = ref(false);
const swaggerFormData = reactive<GetSwaggerFromConsulModel>({
    Name: '',
    Tag: '',
    Clear: false,
});
async function GetSwaggerFromConsulAsync() {
    isLoading.value = true;
    try {
        await service.GetSwaggerFromConsulAsync(swaggerFormData);
    } catch (error) {
        Message.error("从Consul获取Swagger失败");
    } finally {
        isLoading.value = false;
    }
}
const routeFormData = reactive<GetRouteFromConsulModel>({
    Name: '',
    Tag: '',
    DangerousAcceptAnyServerCertificateValidator: true,
    Mode: 1,
    DownstreamScheme: "http",
    HttpVersion: "1.1",
    UpstreamPathTemplate: "/api/{everything}",
    DownstreamPathTemplate: "/api/{everything}",
});
async function GetRouteFromConsulAsync() {
    isLoading.value = true;
    try {
        await service.GetRouteFromConsulAsync(routeFormData);
    } catch (error) {
        Message.error("从Consul获取Swagger失败");
    } finally {
        isLoading.value = false;
    }
}
</script>