<style scoped>
.download-button {
    box-sizing: border-box;
    height: 32px;
    width: 32px;
}
</style>
<template>
    <a-list>
        <a-list-item v-for="item in dataList">
            <span style="line-height: 32px;">[{{ getDateTimeText(item.LastWriteTime) }}]{{ item.Name }}</span>
            <template #actions>
                <a-button-group>
                    <a-link class="download-button" :href="getDownloadUrl(item.DownloadUrl)" title="下载文件" target="_blank">
                        <icon-download />
                    </a-link>
                    <a-popconfirm :content="`是否删除${item.Name}?`" type="warning" ok-text="删除"
                        @ok="async () => await deleteAsync(item.Name)">
                        <a-button type="text" title="删除">
                            <template #icon><icon-delete style="color: red;" /></template>
                        </a-button>
                    </a-popconfirm>
                </a-button-group>
            </template>
        </a-list-item>
    </a-list>
</template>
<script setup lang="ts">
import { onMounted, ref } from 'vue';
import service from '../services/ApplicationInfoService';
import FileInfoDTO from '../models/applicationInfo/FileInfoDTO';
import { Message } from '@arco-design/web-vue';

const props = defineProps({
    id: String
});
const dataList = ref<Array<FileInfoDTO>>([]);
const isLoading = ref(false);
function getDownloadUrl(downloadUrl: string): string {
    const url = service.GetGetDownloadUrl(downloadUrl);
    return url;
}
function getDateTimeText(dateTimeStr: string): string {
    const dateTime = new Date(dateTimeStr);
    const year = dateTime.getFullYear();
    const month = dateTime.getMonth() + 1;
    const day = dateTime.getDate();
    const hour = dateTime.getHours();
    const minute = dateTime.getMinutes();
    const second = dateTime.getSeconds();
    return `${year}/${month}/${day} ${hour}:${minute}:${second}`;
}
async function deleteAsync(fileName: string) {
    if (!props.id) return;
    isLoading.value = true;
    try {
        await service.DeleteFileAsync(props.id, fileName);
        await queryAsync();
    } catch (error) {
        Message.error("删除文件失败");
    }
    finally {
        isLoading.value = false;
    }
}
async function queryAsync() {
    if (!props.id) return;
    isLoading.value = true;
    try {
        const httpResult = await service.GetUploadFilesAsync(props.id);
        if (!httpResult) return;
        dataList.value = httpResult;
    } catch (error) {
        Message.error("获取文件列表失败");
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