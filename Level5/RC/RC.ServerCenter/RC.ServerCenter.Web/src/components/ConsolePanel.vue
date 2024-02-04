<style scoped>
ul {
    list-style: none;
    background-color: black;
    width: 1380;
    height: 750px;
    overflow-y: scroll;
}

pre {
    margin: 0;
    color: #c6c6c6;
    white-space: pre-wrap;
}
</style>
<template>
    <a-spin style="width: 100%;">
        <div style="display: flex;align-items: center;">
            <a-button-group>
                <a-button type="text" @click="queryAsync" :loading="isLoading" title="刷新">
                    <template #icon><icon-refresh /></template>
                </a-button>
                <a-button type="text" @click="clearAsync" :loading="isLoading" title="清空">
                    <template #icon><icon-delete /></template>
                </a-button>
            </a-button-group>
            <a-switch v-model="autoRoll" title="自动滚动" style="margin-right: 5px;" /> 自动滚动
        </div>
        <ul id="messageContent">
            <li v-for="item in messageList">
                <pre>{{ item }}</pre>
            </li>
        </ul>
    </a-spin>
</template>
<script setup lang="ts">
import { nextTick, onMounted, ref } from 'vue';
import service from '../services/ApplicationInfoService';
import { Message } from '@arco-design/web-vue';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { onUnmounted } from 'vue';

const props = defineProps({
    id: String
});
const messageList = ref<string[]>([]);
const isLoading = ref(false);
const autoRoll = ref(true);
const maxMessageCount = ref(500);
async function clearAsync() {
    if (!props.id) return;
    isLoading.value = true;
    try {
        await service.ClearConsoleMessagesAsync(props.id);
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
        messageList.value = httpResult;
        await scrollToBottomAsync();
    } catch (error) {
        Message.error("获取控制台信息失败");
    }
    finally {
        isLoading.value = false;
    }
}
let connection: HubConnection;
async function connectSignalRAsync() {
    const url = await service.GetConsoleMessageHubUrlAsync("/hubs/ConsoleMessage");
    console.log(url);
    connection = new HubConnectionBuilder()
        .withUrl(url, {})
        .withAutomaticReconnect([1000, 4000, 1000, 4000])// 断线自动重连
        .configureLogging(LogLevel.Error)
        .build();
    connection.on("NewConsoleMessageEvent", async (id: string, message: string) => {
        if (!props.id || props.id != id) return;
        messageList.value.push(message);
        if (messageList.value.length > maxMessageCount.value) {
            messageList.value.splice(0, messageList.value.length - maxMessageCount.value);
        }
        if (!autoRoll.value) return;
        await scrollToBottomAsync();
    });
    connection.on("ClearConsoleMessageEvent", (id: string) => {
        if (!props.id || props.id != id) return;
        messageList.value = [];
    });
    connection.onreconnected((connectionId) => {
        console.log(connectionId, '自动重新连接成功')
    })
    await connection.start();
}
async function scrollToBottomAsync() {
    const messageContentElement = document.getElementById("messageContent");
    if (!messageContentElement) return;
    await nextTick()
    messageContentElement.scrollTop = messageContentElement.scrollHeight;
}
async function getMaxConsoleMessageCountAsync() {
    isLoading.value = true;
    try {
        const httpResult = await service.GetMaxConsoleMessageCountAsync();
        if (!httpResult) return;
        maxMessageCount.value = httpResult;
    } catch (error) {
        Message.error("获取最大控制台消息数量失败");
    }
    finally {
        isLoading.value = false;
    }
}
onMounted(async () => {
    if (!props.id) return;
    await getMaxConsoleMessageCountAsync();
    await queryAsync();
    await connectSignalRAsync();
});
onUnmounted(async () => {
    if (!connection) return;
    await connection.stop();
});
</script>