<template>
    <a-space direction="vertical" style="width: 100%;">
        <div>
            <UserSelect v-model="selectUserID" style="width: 200px;" />
        </div>
        <a-table :columns="tableColumns" :data-source="tableData" :loading="searching">
            <template #bodyCell="{ column, record }">
                <template v-if="column.key === 'Action'">
                    <a-space>
                        <a-button type="primary" :loading="searching"
                            @click="openOptionDrawer(record.FlowTemplateID, record.ID, record.NodeID)">完成节点</a-button>
                    </a-space>
                </template>
            </template>
        </a-table>
    </a-space>
    <a-drawer v-model:visible="optionDrawerVisible" :maskClosable="false" :title="'操作'" width="800px">
        <FlowOption ref="flowOption" @complate="optionComplate" />
    </a-drawer>
</template>
<script setup lang="ts">
import { FlowRecord } from '../models/Flow/FlowRecord';
import FlowService from '../services/FlowService';

const flowOption = ref<any>();
/**
 * 操作抽屉是否显示
 */
const optionDrawerVisible = ref(false);
/**
 * 查询标识
 */
const searching = ref(false);
/**
 * 路由
 */
const route = useRoute();
/**
 * 流程模板ID
 */
const selectUserID = ref<string>();
/**
 * 表格列
 */
const tableColumns = [
    {
        title: '流程名称',
        dataIndex: 'FlowTemplateName',
        key: 'FlowTemplateName',
    },
    {
        title: '步骤名称',
        dataIndex: 'StepName',
        key: 'StepName',
    },
    {
        title: '节点名称',
        dataIndex: 'NodeName',
        key: 'NodeName',
    },
    {
        title: '状态',
        dataIndex: 'StateText',
        key: 'StateText',
    },
    {
        title: '操作',
        key: 'Action',
    },
];
/**
 * 表格数据
 */
const tableData = ref<FlowRecord[]>([]);
/**
 * 获得待办事项
 */
const GetBacklogAsync = async () => {
    if (!selectUserID.value) return;
    searching.value = true;
    const result = await FlowService.GetBacklogAsync(selectUserID.value);
    if (result) {
        tableData.value = result.Data;
    }
    searching.value = false;
}
/**
 * 打开操作抽屉
 */
const openOptionDrawer = (flowTemplateID: string, flowRecordID: string, nodeID: string) => {
    optionDrawerVisible.value = true;
    nextTick(async () => {
        await flowOption.value.initAsync(flowTemplateID, flowRecordID, nodeID, selectUserID.value);
    });
};
/**
 * 操作完毕
 */
const optionComplate = async () => {
    optionDrawerVisible.value = false;
    await GetBacklogAsync();
};
/**
 * 监视选中用户更改
 */
watch(selectUserID, async () => {
    await GetBacklogAsync();
})
/**
 * 组件挂载时
 */
onMounted(() => {
    selectUserID.value = route.params.id.toString();
})
</script>