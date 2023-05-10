<template>
    <a-space direction="vertical" style="width: 100%;">
        <a-form layout="inline" :model="queryData" @finish="searchData">
            <a-form-item>
                <a-input v-model:value="queryData.Name" placeholder="名称" />
            </a-form-item>
            <a-form-item>
                <a-button type="primary" html-type="sumbit" :loading="searching">查询</a-button>
            </a-form-item>
        </a-form>
        <div>
            <a-button type="primary" :loading="searching">添加</a-button>
        </div>
        <a-table :columns="tableColumns" :data-source="tableData" :loading="searching" :pagination="tablePagination"
            @change="tableChange"></a-table>
    </a-space>
</template>
<script setup lang="ts">
import { computed, onMounted, reactive, ref } from 'vue';
import { QueryUserModel } from '../models/User/QueryUserModel';
import { User } from '../models/User/User';
import { PageModel } from '../models/PageModel';
import UserService from '../services/UserService';
/**
 * 查询数据
 */
const queryData = reactive<QueryUserModel>(new QueryUserModel());
/**
 * 分页数据
 */
const pageInfo = ref<PageModel>(new PageModel());
/**
 * 表格列
 */
const tableColumns = [
    {
        title: '姓名',
        dataIndex: 'Name',
        key: 'Name',
    },
    {
        title: '操作',
        key: 'action',
    },
];
/**
 * 表格数据
 */
const tableData = ref<User[]>([]);
/**
 * 查询中标识
 */
const searching = ref(false);
/**
 * 检索数据
 */
const searchData = async () => {
    searching.value = true;
    const result = await UserService.GetListAsync(queryData);
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
const tableChange = (pageinfo: { pageSize: number; current: number }) => {
    queryData.PageIndex = pageinfo.current;
    queryData.PageSize = pageinfo.pageSize;
    searchData();
};
/**
 * 页面加载完毕事件
 */
onMounted(() => { searchData(); });
</script>