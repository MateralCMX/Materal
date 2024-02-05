<template>
    <a-select v-model="environmentServer" @change="selectedEnvironmentServer">
        <a-option v-for="item in serverManagement.environmentServerList" :value="item.Service">
            {{ item.Name }}[{{ item.Service }}-{{ item.Host }}:{{ item.Port }}]
        </a-option>
    </a-select>
</template>
<script setup lang="ts">
import { onMounted, ref } from 'vue';
import serverManagement from '../serverManagement';

const environmentServer = ref<string>('');
const emit = defineEmits(['change'])
function selectedEnvironmentServer() {
    serverManagement.checkEnvironmentServer(environmentServer.value);
    emit('change')
}
async function initAsync() {
    if (!serverManagement.selectedEnvironmentServer) {
        await serverManagement.initAsync();
    }
    if (serverManagement.selectedEnvironmentServer) {
        environmentServer.value = serverManagement.selectedEnvironmentServer.Service;
    }
}
onMounted(initAsync);
</script>