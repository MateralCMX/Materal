<template>
    <a-select v-model="deploy" @change="selectedDeploy">
        <a-option v-for="item in serverManagement.deployList" :value="item.Service">
            {{ item.Name }}[{{ item.Service }}-{{ item.Host }}:{{ item.Port }}]
        </a-option>
    </a-select>
</template>
<script setup lang="ts">
import { onMounted, ref } from 'vue';
import serverManagement from '../serverManagement';

const deploy = ref<string>('');
const emit = defineEmits(['change'])
function selectedDeploy() {
    serverManagement.checkDeploy(deploy.value);
    emit('change')
}
async function initAsync() {
    if (!serverManagement.selectedDeploy) {
        await serverManagement.initAsync();
    }
    if (serverManagement.selectedDeploy) {
        deploy.value = serverManagement.selectedDeploy.Service;
    }
}
onMounted(initAsync);
</script>