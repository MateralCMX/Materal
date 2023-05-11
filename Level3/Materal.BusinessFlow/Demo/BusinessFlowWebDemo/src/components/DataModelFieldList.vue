<template>
    <a-space direction="vertical" style="width: 100%;">
        <a-form layout="inline" :model="_queryData" @finish="SearchDataAsync">
            <a-form-item>
                <a-input v-model:value="_queryData.Name" placeholder="名称" allow-clear />
            </a-form-item>
            <a-form-item>
                <a-select ref="select" v-model:value="_queryData.DataType" style="width: 120px">
                    <a-select-option :value="null">所有类型</a-select-option>
                    <a-select-option :value="0">字符串</a-select-option>
                    <a-select-option :value="1">数字</a-select-option>
                    <a-select-option :value="2">日期时间</a-select-option>
                    <a-select-option :value="3">日期</a-select-option>
                    <a-select-option :value="4">时间</a-select-option>
                    <a-select-option :value="5">布尔</a-select-option>
                    <a-select-option :value="6">枚举</a-select-option>
                </a-select>
            </a-form-item>
            <a-form-item>
                <a-input v-model:value="_queryData.Description" placeholder="数据类型" allow-clear />
            </a-form-item>
            <a-form-item>
                <a-button type="primary" html-type="submit" :loading="_searching">查询</a-button>
            </a-form-item>
        </a-form>
        <div>
            <a-button type="primary" :loading="_searching">添加</a-button>
        </div>
        <a-table :columns="_tableColumns" :data-source="_tableData" :loading="_searching" :pagination="TablePagination"
            @change="TableChange">
            <template #bodyCell="{ column, record }">
                <template v-if="column.key === 'Action'">
                    <a-space>
                        <a-button type="primary" :loading="_searching">编辑</a-button>
                        <a-popconfirm title="确定删除该项?" ok-text="确定" cancel-text="取消" @confirm="DeleateAsync(record.ID)">
                            <a-button type="primary" :loading="_searching" danger>删除</a-button>
                        </a-popconfirm>
                    </a-space>
                </template>
            </template>
        </a-table>
    </a-space>
</template>
<script setup lang="ts">
import { computed, reactive, ref } from 'vue';
import { PageModel } from '../models/PageModel';
import { QueryDataModelFieldModel } from '../models/DataModelField/QueryDataModelFieldModel';
import DataModelFieldService from '../services/DataModelFieldService';
import { DataModelField } from '../models/DataModelField/DataModelField';
import { TablePaginationConfig } from 'ant-design-vue';
import DataModelService from '../services/DataModelService';

/**
 * 加载标识
 */
const _searching = ref(false);
/**
 * 查询数据
 */
const _queryData = reactive<QueryDataModelFieldModel>(new QueryDataModelFieldModel());
/**
 * 分页数据
 */
const _pageInfo = ref<PageModel>(new PageModel());/**
 * 表格列
 */
const _tableColumns = [
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
const _tableData = ref<DataModelField[]>([]);
/**
 * 初始化
 * @param id 唯一标识
 */
const InitAsync = async (id: string) => {
    _searching.value = true;
    _queryData.DataModelID = id;
    await SearchDataAsync();
    _searching.value = false;
};
/**
 * 查询数据
 */
const SearchDataAsync = async () => {
    _searching.value = true;
    const result = await DataModelFieldService.GetListAsync(_queryData);
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
 * 暴露
 */
defineExpose({ InitAsync });
</script>