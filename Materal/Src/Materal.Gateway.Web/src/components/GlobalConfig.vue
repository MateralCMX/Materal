<style scoped>
.card-panel {
    display: flex;
    flex-direction: column;
    align-items: center;
}

.box-card {
    margin-bottom: 2rem;
    width: 600px;
}

.arco-col {
    padding: 0 5px;
}
</style>
<template>
    <div class="card-panel">
        <a-card class="box-card" title="BaseUrl配置">
            <BaseUrl />
        </a-card>
        <a-card class="box-card" title="服务发现配置">
            <ServiceDiscoveryProvider />
        </a-card>
        <a-card class="box-card" title="全局限流配置">
            <RateLimitOptions />
        </a-card>
    </div>
</template>
<script setup lang="ts">
import BaseUrl from './BaseUrl.vue';
import RateLimitOptions from './RateLimitOptions.vue';
import ServiceDiscoveryProvider from './ServiceDiscoveryProvider.vue';
</script>