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
                            @click="gotoDetail(record.ID)">查看明细</a-button>
                    </a-space>
                </template>
            </template>
        </a-table>
    </a-space>
</template>
<script setup lang="ts">
import { FlowTemplate } from '../models/FlowTemplate/FlowTemplate';
import FlowService from '../services/FlowService';

/**
 * 查询标识
 */
const searching = ref(false);
/**
 * 路由
 */
const route = useRoute();
/**
 * 路由
 */
const router = useRouter();
/**
 * 流程模板ID
 */
const selectUserID = ref<string>();
/**
 * 表格列
 */
const tableColumns = [
    {
        title: '名称',
        dataIndex: 'Name',
        key: 'Name',
    },
    {
        title: '数据模型',
        dataIndex: 'DataModelName',
        key: 'DataModelName',
    },
    {
        title: '操作',
        key: 'Action',
    },
];
/**
 * 表格数据
 */
const tableData = ref<FlowTemplate[]>([]);
/**
 * 获得待办事项
 */
const GetUserFlowTemplatesAsync = async () => {
    if (!selectUserID.value) return;
    searching.value = true;
    const result = await FlowService.GetUserFlowTemplatesAsync(selectUserID.value);
    if (result) {
        tableData.value = result.Data;
    }
    searching.value = false;
}
/**
 * 跳转到流程明细
 */
const gotoDetail = (flowTemplateID: string) => {
    router.push(`/FlowDetail/${flowTemplateID}/${selectUserID.value}`);
}
/**
 * 监视选中用户更改
 */
watch(selectUserID, async () => {
    await GetUserFlowTemplatesAsync();
})
/**
 * 组件挂载时
 */
onMounted(() => {
    selectUserID.value = route.params.id.toString();
})
</script>