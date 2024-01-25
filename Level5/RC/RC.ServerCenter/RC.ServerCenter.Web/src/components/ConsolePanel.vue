<style scoped>
ul {
    list-style: none;
    background-color: black;
    width: 1380;
    min-height: 400px;
}

pre {
    margin: 0;
    color: #c6c6c6;
    white-space: pre-wrap;
}
</style>
<template>
    <a-spin style="width: 100%;">
        <a-button-group>
            <a-button type="text" @click="queryAsync" :loading="isLoading" title="刷新">
                <template #icon><icon-refresh /></template>
            </a-button>
            <a-button type="text" @click="clearAsync" :loading="isLoading" title="清空">
                <template #icon><icon-delete /></template></a-button>
        </a-button-group>
        <ul>
            <li v-for="item in message">
                <pre>{{ item }}</pre>
            </li>
        </ul>
    </a-spin>
</template>
<script setup lang="ts">
import { onMounted, ref } from 'vue';
import service from '../services/ApplicationInfoService';
import { Message } from '@arco-design/web-vue';

const props = defineProps({
    id: String
});
const message = ref<string[]>([]);
const isLoading = ref(false);
async function clearAsync() {
    if (!props.id) return;
    isLoading.value = true;
    try {
        await service.ClearConsoleMessagesAsync(props.id);
        message.value = [];
    } catch (error) {
        Message.error("清空控制台信息失败");
    }
    finally {
        isLoading.value = false;
    }
}
async function queryAsync() {
    if (!props.id) return;
    isLoading.value = true;
    try {
        const httpResult = await service.GetConsoleMessagesAsync(props.id);
        if (!httpResult) return;
        message.value = httpResult;
    } catch (error) {
        Message.error("获取控制台信息失败");
    }
    finally {
        isLoading.value = false;
    }
}
onMounted(async () => {
    if (!props.id) return;
    await queryAsync();
});
</script>