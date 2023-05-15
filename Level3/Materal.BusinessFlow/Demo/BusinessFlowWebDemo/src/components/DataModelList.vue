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
                        <a-button :loading="searching" @click="OpenFieldDrawer(record.ID)">编辑字段</a-button>
                        <a-popconfirm title="确定删除该项?" ok-text="确定" cancel-text="取消" @confirm="deleateAsync(record.ID)">
                            <a-button type="primary" :loading="searching" danger>删除</a-button>
                        </a-popconfirm>
                    </a-space>
                </template>
            </template>
        </a-table>
    </a-space>
    <a-drawer v-model:visible="optionDrawerVisible" :maskClosable="false" :title="'操作'" width="600px">
        <DataModelOption @complate="optionComplate" ref="dataModelOption" />
    </a-drawer>
    <a-drawer v-model:visible="fieldDrawerVisible" :maskClosable="false" :title="'字段操作'" width="1200px">
        <DataModelFieldList ref="dataModelFieldList" />
    </a-drawer>
</template>
<script setup lang="ts">
import { computed, onMounted, reactive, ref, nextTick } from 'vue';
import { TablePaginationConfig } from 'ant-design-vue';
import { QueryDataModelModel } from '../models/DataModel/QueryDataModelModel';
import { DataModel } from '../models/DataModel/DataModel';
import { PageModel } from '../models/PageModel';
import DataModelService from '../services/DataModelService';

/**
 * 操作组件
 */
const dataModelOption = ref<any>();
/**
 * 操作字段组件
 */
const dataModelFieldList = ref<any>();
/**
 * 查询数据
 */
const queryData = reactive<QueryDataModelModel>(new QueryDataModelModel());
/**
 * 分页数据
 */
const pageInfo = ref<PageModel>(new PageModel());
/**
 * 字段抽屉是否显示
 */
const fieldDrawerVisible = ref(false);
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
        title: '描述',
        dataIndex: 'Description',
        key: 'Description',
    },
    {
        title: '操作',
        key: 'Action',
    },
];
/**
 * 表格数据
 */
const tableData = ref<DataModel[]>([]);
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
        await dataModelOption.value.initAsync(selectID);
    });
};
/**
 * 打开字段抽屉
 */
 const OpenFieldDrawer = (selectID: string) => {
    fieldDrawerVisible.value = true;
    nextTick(async () => {
        await dataModelFieldList.value.initAsync(selectID);
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
    const result = await DataModelService.GetListAsync(queryData);
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
    await DataModelService.DeleteAsync(id);
    await searchDataAsync();
    searching.value = false;
};
/**
 * 页面加载完毕事件
 */
onMounted(async () => { await searchDataAsync(); });
</script>