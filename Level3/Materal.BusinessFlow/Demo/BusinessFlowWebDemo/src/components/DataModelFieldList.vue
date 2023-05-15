<template>
    <a-space direction="vertical" style="width: 100%;">
        <a-form layout="inline" :model="queryData" @finish="searchDataAsync">
            <a-form-item>
                <a-input v-model:value="queryData.Name" placeholder="名称" allow-clear />
            </a-form-item>
            <a-form-item>
                <DataTypeEnumSelect v-model:value="queryData.DataType" :has-all="true" style="width: 207px;"/>
            </a-form-item>
            <a-form-item>
                <a-input v-model:value="queryData.Description" placeholder="数据类型" allow-clear />
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
                        <a-popconfirm title="确定删除该项?" ok-text="确定" cancel-text="取消" @confirm="deleateAsync(record.ID)">
                            <a-button type="primary" :loading="searching" danger>删除</a-button>
                        </a-popconfirm>
                    </a-space>
                </template>
            </template>
        </a-table>
    </a-space>
    <a-drawer v-model:visible="optionDrawerVisible" :maskClosable="false" :title="'操作'" width="600px">
        <DataModelFieldOption @complate="optionComplate" ref="dataModelFieldOption"  />
    </a-drawer>
</template>
<script setup lang="ts">
import { computed, nextTick, reactive, ref } from 'vue';
import { PageModel } from '../models/PageModel';
import { QueryDataModelFieldModel } from '../models/DataModelField/QueryDataModelFieldModel';
import DataModelFieldService from '../services/DataModelFieldService';
import { DataModelField } from '../models/DataModelField/DataModelField';
import { TablePaginationConfig } from 'ant-design-vue';

/**
 * 操作组件
 */
 const dataModelFieldOption = ref<any>();
/**
 * 操作抽屉是否显示
 */
 const optionDrawerVisible = ref(false);
/**
 * 加载标识
 */
const searching = ref(false);
/**
 * 查询数据
 */
const queryData = reactive<QueryDataModelFieldModel>(new QueryDataModelFieldModel());
/**
 * 分页数据
 */
const pageInfo = ref<PageModel>(new PageModel());
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
        title: '数据类型',
        dataIndex: 'DataTypeText',
        key: 'DataTypeText',
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
const tableData = ref<DataModelField[]>([]);
/**
 * 初始化
 * @param id 唯一标识
 */
const initAsync = async (id: string) => {
    searching.value = true;
    queryData.DataModelID = id;
    await searchDataAsync();
    searching.value = false;
};
/**
 * 打开操作抽屉
 */
 const openOptionDrawer = (selectID?: string) => {
    optionDrawerVisible.value = true;
    nextTick(async () => {
        await dataModelFieldOption.value.initAsync(queryData.DataModelID, selectID);
    });
};
/**
 * 查询数据
 */
const searchDataAsync = async () => {
    searching.value = true;
    const result = await DataModelFieldService.GetListAsync(queryData);
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
    await DataModelFieldService.DeleteAsync(id);
    await searchDataAsync();
    searching.value = false;
};
/**
 * 操作完毕
 */
 const optionComplate = async () => {
    optionDrawerVisible.value = false;
    await searchDataAsync();
};
/**
 * 暴露
 */
defineExpose({ initAsync });
</script>