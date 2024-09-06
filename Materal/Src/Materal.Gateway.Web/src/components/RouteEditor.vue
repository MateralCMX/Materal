<style scoped>
.inner-form {
    display: flex;
}

.inner-form-item {
    margin-bottom: 0;
}

.inner-form-actions {
    width: 32px;
    padding-left: 5px;
}
</style>
<template>
    <a-spin :loading="isLoading" style="width: 100%;">
        <a-form ref="formRef" :model="formData">
            <a-tabs default-active-key="1">
                <a-tab-pane key="1" title="基础信息">
                    <a-form-item field="UpstreamPathTemplate" label="上游路径模版" :rules="formRules.UpstreamPathTemplate">
                        <a-input v-model="formData.UpstreamPathTemplate" />
                    </a-form-item>
                    <a-form-item field="DownstreamPathTemplate" label="下游路径模版" :rules="formRules.DownstreamPathTemplate">
                        <a-input v-model="formData.DownstreamPathTemplate" />
                    </a-form-item>
                    <a-form-item field="DownstreamScheme" label="转发方式" :rules="formRules.DownstreamScheme">
                        <a-select v-model="formData.DownstreamScheme">
                            <a-option value="http">http</a-option>
                            <a-option value="https">https</a-option>
                            <a-option value="ws">ws</a-option>
                            <a-option value="wss">wss</a-option>
                            <a-option value="grpc">grpc</a-option>
                            <a-option value="grpcs">grpcs</a-option>
                        </a-select>
                    </a-form-item>
                    <a-form-item field="DownstreamHttpVersion" label="Http版本" :rules="formRules.DownstreamHttpVersion">
                        <a-select v-model="formData.DownstreamHttpVersion">
                            <a-option value="1.0">1.0</a-option>
                            <a-option value="1.1">1.1</a-option>
                            <a-option value="2.0">2.0</a-option>
                        </a-select>
                    </a-form-item>
                    <a-form-item field="UpstreamHttpMethod" label="转发类型">
                        <a-select v-model="formData.UpstreamHttpMethod" multiple>
                            <a-option value="GET">GET</a-option>
                            <a-option value="POST">POST</a-option>
                            <a-option value="PUT">PUT</a-option>
                            <a-option value="DELETE">DELETE</a-option>
                            <a-option value="OPTIONS">OPTIONS</a-option>
                        </a-select>
                    </a-form-item>
                    <a-form-item field="SwaggerKey" label="Swagger">
                        <a-input v-model="formData.SwaggerKey" />
                    </a-form-item>
                    <a-form-item field="DangerousAcceptAnyServerCertificateValidator" label="忽略安全证书">
                        <a-switch v-model="formData.DangerousAcceptAnyServerCertificateValidator" />
                    </a-form-item>
                </a-tab-pane>
                <a-tab-pane key="2" title="下游配置">
                    <a-form-item field="LoadBalancerOptions" label="负载均衡">
                        <a-select v-model="formData.LoadBalancerOptions.Type">
                            <a-option value="NoLoadBalancer">不使用负载均衡</a-option>
                            <a-option value="LeastConnection">最小连接</a-option>
                            <a-option value="RoundRobin">循环</a-option>
                            <a-option value="CookieStickySessions">会话黏贴</a-option>
                        </a-select>
                    </a-form-item>
                    <a-form-item field="isService" label="服务发现">
                        <a-switch v-model="formData.isService" />
                    </a-form-item>
                    <a-form-item v-if="!formData.isService" label="下游配置">
                        <div style="width: 100%;">
                            <a-space direction="vertical" fill>
                                <a-button type="outline" @click="addDownstreamHostAndPort()">
                                    <template #icon><icon-plus /></template>
                                </a-button>
                                <a-list style="overflow: hidden;">
                                    <a-list-item v-for="(item, index) in formData.DownstreamHostAndPorts" :key="index">
                                        <div class="inner-form">
                                            <a-form-item class="inner-form-item"
                                                :field="`DownstreamHostAndPorts[${index}].Host`" label="Host"
                                                :rules="formRules.Host">
                                                <a-input v-model="item.Host" />
                                            </a-form-item>
                                            <a-form-item class="inner-form-item"
                                                :field="`DownstreamHostAndPorts[${index}].Port`" label="Port"
                                                :rules="formRules.Prot">
                                                <a-input-number v-model="item.Port" />
                                            </a-form-item>
                                            <a-form-item class="inner-form-item inner-form-actions">
                                                <a-button type="text" @click="removeDownstreamHostAndPort(item)">
                                                    <template #icon><icon-delete /></template>
                                                </a-button>
                                            </a-form-item>
                                        </div>
                                    </a-list-item>
                                </a-list>
                            </a-space>
                        </div>
                    </a-form-item>
                    <a-form-item v-if="formData.isService" field="ServiceName" label="服务名称" :rules="formRules.ServiceName">
                        <a-input v-model="formData.ServiceName" />
                    </a-form-item>
                </a-tab-pane>
                <a-tab-pane key="3" title="缓存配置">
                    <a-form-item field="FileCacheOptions.Enable" label="启用标识">
                        <a-switch v-model="formData.FileCacheOptions.Enable" />
                    </a-form-item>
                    <a-form-item field="FileCacheOptions.Region" label="缓存域" v-if="formData.FileCacheOptions.Enable"
                        :rules="formRules.Region">
                        <a-input v-model="formData.FileCacheOptions.Region" />
                    </a-form-item>
                    <a-form-item field="FileCacheOptions.TtlSeconds" label="缓存域" v-if="formData.FileCacheOptions.Enable"
                        :rules="formRules.TtlSeconds">
                        <a-input-number :min="1" v-model="formData.FileCacheOptions.TtlSeconds" />
                    </a-form-item>
                </a-tab-pane>
                <a-tab-pane key="4" title="熔断超时">
                    <a-form-item field="QoSOptions.Enable" label="启用标识">
                        <a-switch v-model="formData.QoSOptions.Enable" />
                    </a-form-item>
                    <a-form-item field="QoSOptions.TimeoutValue" label="超时时间(ms)" v-if="formData.QoSOptions.Enable"
                        :rules="formRules.TimeoutValue">
                        <a-input-number :min="1" v-model="formData.QoSOptions.TimeoutValue" />
                    </a-form-item>
                    <a-form-item field="QoSOptions.ExceptionsAllowedBeforeBreaking" label="超时次数"
                        v-if="formData.QoSOptions.Enable" :rules="formRules.ExceptionsAllowedBeforeBreaking">
                        <a-input-number :min="1" v-model="formData.QoSOptions.ExceptionsAllowedBeforeBreaking" />
                    </a-form-item>
                    <a-form-item field="QoSOptions.DurationOfBreak" label="熔断时间(ms)" v-if="formData.QoSOptions.Enable"
                        :rules="formRules.DurationOfBreak">
                        <a-input-number :min="1" v-model="formData.QoSOptions.DurationOfBreak" />
                    </a-form-item>
                </a-tab-pane>
                <a-tab-pane key="5" title="限流配置">
                    <a-form-item field="RateLimitOptions.Enable" label="启用标识">
                        <a-switch v-model="formData.RateLimitOptions.Enable" />
                    </a-form-item>
                    <a-form-item field="RateLimitOptions.Period" label="统计时间" v-if="formData.RateLimitOptions.Enable"
                        :rules="formRules.Period">
                        <a-input v-model="formData.RateLimitOptions.Period" placeholder="1s 1m 1h 1d" />
                    </a-form-item>
                    <a-form-item field="RateLimitOptions.Limit" label="请求次数" v-if="formData.RateLimitOptions.Enable"
                        :rules="formRules.Limit">
                        <a-input-number :min="1" v-model="formData.RateLimitOptions.Limit" />
                    </a-form-item>
                    <a-form-item field="RateLimitOptions.PeriodTimespan" label="重试时间(s)"
                        v-if="formData.RateLimitOptions.Enable" :rules="formRules.PeriodTimespan">
                        <a-input-number :min="1" v-model="formData.RateLimitOptions.PeriodTimespan" />
                    </a-form-item>
                    <a-form-item v-if="formData.RateLimitOptions.Enable" label="白名单">
                        <div style="width: 100%;">
                            <a-space direction="vertical" fill>
                                <a-button type="outline" @click="addClientWhitelist()">
                                    <template #icon><icon-plus /></template>
                                </a-button>
                                <a-list>
                                    <a-list-item v-for="(item, index) in formData.RateLimitOptions.ClientWhitelist"
                                        :key="index">
                                        <div class="inner-form">
                                            <a-form-item class="inner-form-item"
                                                :field="`RateLimitOptions.ClientWhitelist[${index}].Name`" label="名称"
                                                :rules="formRules.ClientWhitelist">
                                                <a-input v-model="item.Name" />
                                            </a-form-item>
                                            <a-form-item class="inner-form-item inner-form-actions">
                                                <a-button type="text" @click="removeClientWhitelist(item)">
                                                    <template #icon><icon-delete /></template>
                                                </a-button>
                                            </a-form-item>
                                        </div>
                                    </a-list-item>
                                </a-list>
                            </a-space>
                        </div>
                    </a-form-item>
                </a-tab-pane>
                <a-tab-pane key="6" title="鉴权配置">
                    <a-form-item field="AuthenticationOptions.Enable" label="启用标识">
                        <a-switch v-model="formData.AuthenticationOptions.Enable" />
                    </a-form-item>
                    <a-form-item field="AuthenticationOptions.AuthenticationProviderKey" label="验证键"
                        v-if="formData.AuthenticationOptions.Enable" :rules="formRules.AuthenticationProviderKey">
                        <a-input v-model="formData.AuthenticationOptions.AuthenticationProviderKey" />
                    </a-form-item>
                    <a-form-item v-if="formData.AuthenticationOptions.Enable" label="允许域">
                        <div style="width: 100%;">
                            <a-space direction="vertical" fill>
                                <a-button type="outline" @click="addAllowedScopes()">
                                    <template #icon><icon-plus /></template>
                                </a-button>
                                <a-list>
                                    <a-list-item v-for="(item, index) in formData.AuthenticationOptions.AllowedScopes"
                                        :key="index">
                                        <div class="inner-form">
                                            <a-form-item class="inner-form-item" label="域名称"
                                                :field="`AuthenticationOptions.AllowedScopes.${index}.Name`"
                                                :rules="formRules.AllowedScopes">
                                                <a-input v-model="item.Name" />
                                            </a-form-item>
                                            <a-form-item class="inner-form-item inner-form-actions">
                                                <a-button type="text" @click="removeAllowedScopes(item)">
                                                    <template #icon><icon-delete /></template>
                                                </a-button>
                                            </a-form-item>
                                        </div>
                                    </a-list-item>
                                </a-list>
                            </a-space>
                        </div>
                    </a-form-item>
                </a-tab-pane>
            </a-tabs>
        </a-form>
    </a-spin>
</template>
<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import service from '../services/RouteService';
import { Form, Message } from '@arco-design/web-vue';
import HostAndPortModel from '../models/route/HostAndPortModel';
import AddRouteModel from '../models/route/AddRouteModel';
import EditRouteModel from '../models/route/EditRouteModel';

const formRef = ref<InstanceType<typeof Form>>();
const props = defineProps({
    id: String
});
defineExpose({
    saveAsync
});
/**
 * 表单数据
 */
const formData = reactive({
    UpstreamPathTemplate: "/api/{everything}",
    DownstreamPathTemplate: "/api/{everything}",
    DownstreamScheme: "http",
    DownstreamHttpVersion: "1.1",
    UpstreamHttpMethod: ["GET", "POST", "PUT", "DELETE", "OPTIONS"],
    SwaggerKey: '',
    DangerousAcceptAnyServerCertificateValidator: true,
    LoadBalancerOptions: {
        Type: "NoLoadBalancer"
    },
    isService: false,
    ServiceName: '',
    DownstreamHostAndPorts: [] as HostAndPortModel[],
    FileCacheOptions: {
        Enable: false,
        TtlSeconds: 180,
        Region: "MyRegion"
    },
    QoSOptions: {
        Enable: false,
        DurationOfBreak: 60000,
        TimeoutValue: 2000,
        ExceptionsAllowedBeforeBreaking: 3,
    },
    RateLimitOptions: {
        Enable: false,
        Period: "1h",
        Limit: 100,
        PeriodTimespan: 600,
        ClientWhitelist: [] as { Name: string }[]
    },
    AuthenticationOptions: {
        Enable: false,
        AuthenticationProviderKey: "Bearer",
        AllowedScopes: [] as { Name: string }[]
    }
});
/**
 * 表单验证规则
 */
const formRules = reactive({
    UpstreamPathTemplate: [
        { required: true, message: '上游路径模版必填', trigger: 'blur' }
    ],
    DownstreamPathTemplate: [
        { required: true, message: '下游路径模版必填', trigger: 'blur' }
    ],
    DownstreamScheme: [
        { required: true, message: '转发方式必填', trigger: 'blur' }
    ],
    DownstreamHttpVersion: [
        { required: true, message: 'Http版本必填', trigger: 'blur' }
    ],
    ServiceName: [
        { required: true, message: '服务名称必填', trigger: 'blur' }
    ],
    Host: [
        { required: true, message: 'Host必填', trigger: 'blur' }
    ],
    Prot: [
        { required: true, message: '端口号必填', trigger: 'blur' }
    ],
    TtlSeconds: [
        { required: true, message: '缓存时间必填', trigger: 'blur' }
    ],
    Region: [
        { required: true, message: '缓存域必填', trigger: 'blur' }
    ],
    DurationOfBreak: [
        { required: true, message: '熔断时间必填', trigger: 'blur' }
    ],
    TimeoutValue: [
        { required: true, message: '超时时间必填', trigger: 'blur' }
    ],
    ExceptionsAllowedBeforeBreaking: [
        { required: true, message: '超时次数必填', trigger: 'blur' }
    ],
    Period: [
        { required: true, message: '统计时间必填', trigger: 'blur' }
    ],
    Limit: [
        { required: true, message: '请求次数必填', trigger: 'blur' }
    ],
    PeriodTimespan: [
        { required: true, message: '重试时间必填', trigger: 'blur' }
    ],
    ClientWhitelist: [
        { required: true, message: '客户端名称必填', trigger: 'blur' }
    ],
    AuthenticationProviderKey: [
        { required: true, message: '验证键必填', trigger: 'blur' }
    ],
    AllowedScopes: [
        { required: true, message: '允许域必填', trigger: 'blur' }
    ]
});
/**
 * 加载数据标识
 */
const isLoading = ref(false);
async function saveAsync(): Promise<boolean> {
    const validate = await formRef.value?.validate();
    if (validate) return false;
    isLoading.value = true;
    try {
        if (props.id) {
            await service.EditAsync(getEditData());
        }
        else {
            await service.AddAsync(getAddData());
        }
        return true;
    } catch (error) {
        Message.error("保存Swagger配置失败");
        return false;
    }
    finally {
        isLoading.value = false;
    }
}
function getAddData(): AddRouteModel {
    const result: AddRouteModel = {
        UpstreamPathTemplate: formData.UpstreamPathTemplate,
        DownstreamPathTemplate: formData.DownstreamPathTemplate,
        DownstreamScheme: formData.DownstreamScheme,
        DownstreamHttpVersion: formData.DownstreamHttpVersion,
        UpstreamHttpMethod: formData.UpstreamHttpMethod,
        SwaggerKey: formData.SwaggerKey,
        ServiceName: formData.ServiceName,
        DangerousAcceptAnyServerCertificateValidator: formData.DangerousAcceptAnyServerCertificateValidator,
        LoadBalancerOptions: formData.LoadBalancerOptions,
        FileCacheOptions: formData.FileCacheOptions.Enable ? formData.FileCacheOptions : undefined,
        QoSOptions: formData.QoSOptions.Enable ? formData.QoSOptions : undefined,
        RateLimitOptions: formData.RateLimitOptions.Enable ? {
            EnableRateLimiting: formData.RateLimitOptions.Enable,
            Period: formData.RateLimitOptions.Period,
            Limit: formData.RateLimitOptions.Limit,
            PeriodTimespan: formData.RateLimitOptions.PeriodTimespan,
            ClientWhitelist: formData.RateLimitOptions.ClientWhitelist.map(x => x.Name)
        } : undefined,
        AuthenticationOptions: formData.AuthenticationOptions.Enable ? {
            AuthenticationProviderKey: formData.AuthenticationOptions.AuthenticationProviderKey,
            AllowedScopes: formData.AuthenticationOptions.AllowedScopes.map(x => x.Name)
        } : undefined
    };
    return result;
}
function getEditData(): EditRouteModel {
    if(!props.id) throw new Error("id不能为空");
    const result: EditRouteModel = {
        ID: props.id,
        UpstreamPathTemplate: formData.UpstreamPathTemplate,
        DownstreamPathTemplate: formData.DownstreamPathTemplate,
        DownstreamScheme: formData.DownstreamScheme,
        DownstreamHttpVersion: formData.DownstreamHttpVersion,
        UpstreamHttpMethod: formData.UpstreamHttpMethod,
        SwaggerKey: formData.SwaggerKey,
        ServiceName: formData.ServiceName,
        DangerousAcceptAnyServerCertificateValidator: formData.DangerousAcceptAnyServerCertificateValidator,
        LoadBalancerOptions: formData.LoadBalancerOptions,
        FileCacheOptions: formData.FileCacheOptions.Enable ? formData.FileCacheOptions : undefined,
        QoSOptions: formData.QoSOptions.Enable ? formData.QoSOptions : undefined,
        RateLimitOptions: formData.RateLimitOptions.Enable ? {
            EnableRateLimiting: formData.RateLimitOptions.Enable,
            Period: formData.RateLimitOptions.Period,
            Limit: formData.RateLimitOptions.Limit,
            PeriodTimespan: formData.RateLimitOptions.PeriodTimespan,
            ClientWhitelist: formData.RateLimitOptions.ClientWhitelist.map(x => x.Name)
        } : undefined,
        AuthenticationOptions: formData.AuthenticationOptions.Enable ? {
            AuthenticationProviderKey: formData.AuthenticationOptions.AuthenticationProviderKey,
            AllowedScopes: formData.AuthenticationOptions.AllowedScopes.map(x => x.Name)
        } : undefined
    };
    return result;
}
async function queryAsync() {
    if (!props.id) return;
    isLoading.value = true;
    try {
        const httpResult = await service.GetInfoAsync(props.id);
        if (!httpResult) return;
        formData.UpstreamPathTemplate = httpResult.UpstreamPathTemplate;
        formData.DownstreamPathTemplate = httpResult.DownstreamPathTemplate;
        formData.DownstreamScheme = httpResult.DownstreamScheme;
        formData.DownstreamHttpVersion = httpResult.DownstreamHttpVersion;
        formData.UpstreamHttpMethod = httpResult.UpstreamHttpMethod;
        formData.SwaggerKey = httpResult.SwaggerKey ?? '';
        formData.DangerousAcceptAnyServerCertificateValidator = httpResult.DangerousAcceptAnyServerCertificateValidator;
        formData.LoadBalancerOptions = httpResult.LoadBalancerOptions ?? {
            Type: "NoLoadBalancer"
        };
        formData.isService = httpResult.ServiceName ? true : false;
        formData.ServiceName = httpResult.ServiceName ?? '';
        formData.DownstreamHostAndPorts = httpResult.DownstreamHostAndPorts ?? [];
        if (httpResult.FileCacheOptions) {
            formData.FileCacheOptions.Enable = true;
            formData.FileCacheOptions.Region = httpResult.FileCacheOptions.Region;
            formData.FileCacheOptions.TtlSeconds = httpResult.FileCacheOptions.TtlSeconds;
        }
        else {
            formData.FileCacheOptions.Enable = false;
            formData.FileCacheOptions.Region = "MyRegion";
            formData.FileCacheOptions.TtlSeconds = 180;
        }
        if (httpResult.QoSOptions) {
            formData.QoSOptions.Enable = true;
            formData.QoSOptions.DurationOfBreak = httpResult.QoSOptions.DurationOfBreak;
            formData.QoSOptions.TimeoutValue = httpResult.QoSOptions.TimeoutValue;
            formData.QoSOptions.ExceptionsAllowedBeforeBreaking = httpResult.QoSOptions.ExceptionsAllowedBeforeBreaking;
        }
        else {
            formData.QoSOptions.Enable = false;
            formData.QoSOptions.DurationOfBreak = 60000;
            formData.QoSOptions.TimeoutValue = 2000;
            formData.QoSOptions.ExceptionsAllowedBeforeBreaking = 3;
        }
        if (httpResult.RateLimitOptions) {
            formData.RateLimitOptions.Enable = true;
            formData.RateLimitOptions.Period = httpResult.RateLimitOptions.Period;
            formData.RateLimitOptions.Limit = httpResult.RateLimitOptions.Limit;
            formData.RateLimitOptions.PeriodTimespan = httpResult.RateLimitOptions.PeriodTimespan;
            formData.RateLimitOptions.ClientWhitelist.splice(0, formData.RateLimitOptions.ClientWhitelist.length);
            for (const iterator of httpResult.RateLimitOptions.ClientWhitelist) {
                formData.RateLimitOptions.ClientWhitelist.push({ Name: iterator });
            }
        }
        else {
            formData.RateLimitOptions.Enable = false;
            formData.RateLimitOptions.Period = "1h";
            formData.RateLimitOptions.Limit = 100;
            formData.RateLimitOptions.PeriodTimespan = 600;
            formData.RateLimitOptions.ClientWhitelist.splice(0, formData.RateLimitOptions.ClientWhitelist.length);
        }
        if (httpResult.AuthenticationOptions) {
            formData.AuthenticationOptions.Enable = true;
            formData.AuthenticationOptions.AuthenticationProviderKey = httpResult.AuthenticationOptions.AuthenticationProviderKey;
            formData.AuthenticationOptions.AllowedScopes.splice(0, formData.AuthenticationOptions.AllowedScopes.length);
            for (const iterator of httpResult.AuthenticationOptions.AllowedScopes) {
                formData.AuthenticationOptions.AllowedScopes.push({ Name: iterator });
            }
        }
        else {
            formData.AuthenticationOptions.Enable = false;
            formData.AuthenticationOptions.AuthenticationProviderKey = "Bearer";
            formData.AuthenticationOptions.AllowedScopes.splice(0, formData.AuthenticationOptions.AllowedScopes.length);
        }
    } catch (error) {
        Message.error("获取Swagger配置失败");
    }
    finally {
        isLoading.value = false;
    }
}
function addDownstreamHostAndPort() {
    formData.DownstreamHostAndPorts.push({
        Host: "localhost",
        Port: 5000
    });
}
function removeDownstreamHostAndPort(item: HostAndPortModel) {
    const index = formData.DownstreamHostAndPorts.indexOf(item);
    formData.DownstreamHostAndPorts.splice(index, 1);
}
function addClientWhitelist() {
    formData.RateLimitOptions.ClientWhitelist.push({ Name: "MyClient" });
}
function removeClientWhitelist(item: { Name: string }) {
    const index = formData.RateLimitOptions.ClientWhitelist.indexOf(item);
    formData.RateLimitOptions.ClientWhitelist.splice(index, 1);
}
function addAllowedScopes() {
    formData.AuthenticationOptions.AllowedScopes.push({ Name: "NewScope" });
}
function removeAllowedScopes(item: { Name: string }) {
    const index = formData.AuthenticationOptions.AllowedScopes.indexOf(item);
    formData.AuthenticationOptions.AllowedScopes.splice(index, 1);
}
onMounted(async () => {
    if (!props.id) return;
    await queryAsync();
});
</script>