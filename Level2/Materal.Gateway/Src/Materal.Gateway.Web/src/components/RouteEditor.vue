<template>
    <a-spin :loading="isLoading" style="width: 100%;">
        <a-tabs default-active-key="2">
            <a-tab-pane key="1" title="Tab 1">
                Content of Tab Panel 1
            </a-tab-pane>
            <a-tab-pane key="2" title="Tab 2">
                Content of Tab Panel 2
            </a-tab-pane>
            <a-tab-pane key="3">
                <template #title>Tab 3</template>
                Content of Tab Panel 3
            </a-tab-pane>
        </a-tabs>
    </a-spin>
</template>
<script setup lang="ts">
import { onMounted, ref } from 'vue';
import service from '../services/SwaggerConfigService';
import { Form, Message } from '@arco-design/web-vue';

const formRef = ref<InstanceType<typeof Form>>();
const props = defineProps({
    id: String
});
defineExpose({
    saveAsync
});
/**
 * 加载数据标识
 */
const isLoading = ref(false);
async function saveAsync(): Promise<boolean> {
    const validate = await formRef.value?.validate();
    if (validate) return false;
    return true;
    // isLoading.value = true;
    // try {
    //     if (props.id) {
    //         await service.EditAsync({
    //             ...formData,
    //             ID: props.id
    //         });
    //     }
    //     else {
    //         await service.AddAsync(formData);
    //     }
    //     return true;
    // } catch (error) {
    //     Message.error("保存Swagger配置失败");
    //     return false;
    // }
    // finally {
    //     isLoading.value = false;
    // }
}
async function queryAsync() {
    if (!props.id) return;
    isLoading.value = true;
    try {
        const httpResult = await service.GetInfoAsync(props.id);
        if (!httpResult) return;
        // formData.Key = httpResult.Key;
        // formData.TakeServersFromDownstreamService = httpResult.TakeServersFromDownstreamService;
    } catch (error) {
        Message.error("获取Swagger配置失败");
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