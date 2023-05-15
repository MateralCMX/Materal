<template>
    <a-space direction="vertical" style="width: 100%;">
        <a-form layout="inline" :model="queryData" @finish="searchDataAsync">
            <a-form-item>
                <a-input v-model:value="queryData.Name" placeholder="名称" allow-clear />
            </a-form-item>
            <a-form-item>
                <a-button type="primary" html-type="submit" :loading="searching">查询</a-button>
            </a-form-item>
        </a-form>
        <div>
            <a-button type="primary" :loading="searching" @click="openOptionDrawer()">添加</a-button>
        </div>
        <a-table :columns="tableColumns" :data-source="tableData" :loading="searching" :pagination="tablePagination"
            @change="tableChange">
            <template #bodyCell="{ column, record }">
                <template v-if="column.key === 'Action'">
                    <a-space>
                        <a-button type="primary" :loading="searching" @click="openOptionDrawer(record.ID)">编辑</a-button>
                        <a-button :loading="searching"><router-link :to="`/FlowTemplateCanvas/${record.ID}`">编辑流程</router-link></a-button>
                        <a-popconfirm title="确定删除该项?" ok-text="确定" cancel-text="取消" @confirm="deleateAsync(record.ID)">
                            <a-button type="primary" :loading="searching" danger>删除</a-button>
                        </a-popconfirm>
                    </a-space>
                </template>
            </template>
        </a-table>
    </a-space>
    <a-drawer v-model:visible="optionDrawerVisible" :maskClosable="false" :title="'操作'" width="600px">
        <FlowTemplateOption @complate="optionComplate" ref="flowTemplateOption" />
    </a-drawer>
</template>
<script setup lang="ts">
import { TablePaginationConfig } from 'ant-design-vue';
import { QueryFlowTemplateModel } from '../models/FlowTemplate/QueryFlowTemplateModel';
import { FlowTemplate } from '../models/FlowTemplate/FlowTemplate';
import { PageModel } from '../models/PageModel';
import FlowTemplateService from '../services/FlowTemplateService';

/**
 * 操作组件
 */
const flowTemplateOption = ref<any>();
/**
 * 查询数据
 */
const queryData = reactive<QueryFlowTemplateModel>(new QueryFlowTemplateModel());
/**
 * 分页数据
 */
const pageInfo = ref<PageModel>(new PageModel());
/**
 * 操作抽屉是否显示
 */
const optionDrawerVisible = ref(false);
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
 * 查询中标识
 */
const searching = ref(false);
/**
 * 打开操作抽屉
 */
const openOptionDrawer = (selectID?: string) => {
    optionDrawerVisible.value = true;
    nextTick(async () => {
        await flowTemplateOption.value.initAsync(selectID);
    });
};
/**
 * 操作完毕
 */
const optionComplate = async () => {
    optionDrawerVisible.value = false;
    await searchDataAsync();
};
/**
 * 检索数据
 */
const searchDataAsync = async () => {
    searching.value = true;
    const result = await FlowTemplateService.GetListAsync(queryData);
    if (result) {
        tableData.value = result.Data;
        pageInfo.value = result.PageModel;
    }
    searching.value = false;
}
/**
 * 表格数据加载完毕事件
 */
const tablePagination = computed(() => ({
    total: pageInfo.value.DataCount,
    current: pageInfo.value.PageIndex,
    pageSize: pageInfo.value.PageSize
}));
/**
 * 表格更改事件
 * @param pageinfo 分页信息
 */
const tableChange = async (pagination: TablePaginationConfig) => {
    queryData.PageIndex = pagination.current ?? 1;
    queryData.PageSize = pagination.pageSize ?? 10;
    await searchDataAsync();
};
/**
 * 删除数据
 * @param id ID
 */
const deleateAsync = async (id: string) => {
    searching.value = true;
    await FlowTemplateService.DeleteAsync(id);
    await searchDataAsync();
    searching.value = false;
};
/**
 * 页面加载完毕事件
 */
onMounted(async () => { await searchDataAsync(); });
</script>