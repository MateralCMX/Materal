<template>
    <a-space direction="vertical" style="width: 100%;">
        <a-form layout="inline" :model="_queryData" @finish="SearchDataAsync">
            <a-form-item>
                <a-input v-model:value="_queryData.Name" placeholder="名称" allow-clear />
            </a-form-item>
            <a-form-item>
                <a-button type="primary" html-type="submit" :loading="_searching">查询</a-button>
            </a-form-item>
        </a-form>
        <div>
            <a-button type="primary" :loading="_searching" @click="OpenOptionDrawer()">添加</a-button>
        </div>
        <a-table :columns="_tableColumns" :data-source="_tableData" :loading="_searching" :pagination="TablePagination"
            @change="TableChange">
            <template #bodyCell="{ column, record }">
                <template v-if="column.key === 'Action'">
                    <a-space>
                        <a-button type="primary" :loading="_searching" @click="OpenOptionDrawer(record.ID)">编辑</a-button>
                        <a-button :loading="_searching" @click="OpenFieldDrawer(record.ID)">编辑字段</a-button>
                        <a-popconfirm title="确定删除该项?" ok-text="确定" cancel-text="取消" @confirm="DeleateAsync(record.ID)">
                            <a-button type="primary" :loading="_searching" danger>删除</a-button>
                        </a-popconfirm>
                    </a-space>
                </template>
            </template>
        </a-table>
    </a-space>
    <a-drawer v-model:visible="_optionDrawerVisible" :maskClosable="false" :title="'操作'" width="600px">
        <DataModelOption @complate="OptionComplate" ref="_dataModelOption" />
    </a-drawer>
    <a-drawer v-model:visible="_fieldDrawerVisible" :maskClosable="false" :title="'字段操作'" width="1200px">
        <DataModelFieldList ref="_dataModelFieldList" />
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
const _dataModelOption = ref<any>();
/**
 * 操作字段组件
 */
const _dataModelFieldList = ref<any>();
/**
 * 查询数据
 */
const _queryData = reactive<QueryDataModelModel>(new QueryDataModelModel());
/**
 * 分页数据
 */
const _pageInfo = ref<PageModel>(new PageModel());
/**
 * 字段抽屉是否显示
 */
const _fieldDrawerVisible = ref(false);
/**
 * 操作抽屉是否显示
 */
const _optionDrawerVisible = ref(false);
/**
 * 表格列
 */
const _tableColumns = [
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
const _tableData = ref<DataModel[]>([]);
/**
 * 查询中标识
 */
const _searching = ref(false);
/**
 * 打开操作抽屉
 */
const OpenOptionDrawer = (selectID?: string) => {
    _optionDrawerVisible.value = true;
    nextTick(async () => {
        await _dataModelOption.value.InitAsync(selectID);
    });
};
/**
 * 打开字段抽屉
 */
 const OpenFieldDrawer = (selectID: string) => {
    _fieldDrawerVisible.value = true;
    nextTick(async () => {
        await _dataModelFieldList.value.InitAsync(selectID);
    });
};
/**
 * 操作完毕
 */
const OptionComplate = async () => {
    _optionDrawerVisible.value = false;
    await SearchDataAsync();
};
/**
 * 检索数据
 */
const SearchDataAsync = async () => {
    _searching.value = true;
    const result = await DataModelService.GetListAsync(_queryData);
    if (result) {
        _tableData.value = result.Data;
        _pageInfo.value = result.PageModel;
    }
    _searching.value = false;
}
/**
 * 表格数据加载完毕事件
 */
const TablePagination = computed(() => ({
    total: _pageInfo.value.DataCount,
    current: _pageInfo.value.PageIndex,
    pageSize: _pageInfo.value.PageSize
}));
/**
 * 表格更改事件
 * @param pageinfo 分页信息
 */
const TableChange = async (pagination: TablePaginationConfig) => {
    _queryData.PageIndex = pagination.current ?? 1;
    _queryData.PageSize = pagination.pageSize ?? 10;
    await SearchDataAsync();
};
/**
 * 删除数据
 * @param id ID
 */
const DeleateAsync = async (id: string) => {
    _searching.value = true;
    await DataModelService.DeleteAsync(id);
    await SearchDataAsync();
    _searching.value = false;
};
/**
 * 页面加载完毕事件
 */
onMounted(async () => { await SearchDataAsync(); });
</script>