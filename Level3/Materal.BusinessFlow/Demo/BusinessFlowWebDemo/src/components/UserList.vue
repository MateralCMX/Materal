<template>
    <a-space direction="vertical" style="width: 100%;">
        <a-form layout="inline" :model="_queryData" @finish="SearchData">
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
                        <a-popconfirm title="确定删除该项?" ok-text="确定" cancel-text="取消" @confirm="DeleateAsync(record.ID)">
                            <a-button type="primary" :loading="_searching" danger>删除</a-button>
                        </a-popconfirm>
                    </a-space>
                </template>
            </template>
        </a-table>
    </a-space>
    <a-drawer v-model:visible="_optionDrawerVisible" :maskClosable="false" :title="'用户操作'">
        <UserOption @complate="OptionComplate" ref="_userOption" />
    </a-drawer>
</template>
<script setup lang="ts">
import { computed, onMounted, reactive, ref, nextTick } from 'vue';
import { TablePaginationConfig } from 'ant-design-vue';
import { QueryUserModel } from '../models/User/QueryUserModel';
import { User } from '../models/User/User';
import { PageModel } from '../models/PageModel';
import UserService from '../services/UserService';
import UserOption from "./UserOption.vue";

/**
 * 用户操作组件
 */
const _userOption = ref<any>();
/**
 * 查询数据
 */
const _queryData = reactive<QueryUserModel>(new QueryUserModel());
/**
 * 分页数据
 */
const _pageInfo = ref<PageModel>(new PageModel());
/**
 * 操作抽屉是否显示
 */
const _optionDrawerVisible = ref(false);
/**
 * 表格列
 */
const _tableColumns = [
    {
        title: '姓名',
        dataIndex: 'Name',
        key: 'Name',
    },
    {
        title: '操作',
        key: 'Action',
    },
];
/**
 * 表格数据
 */
const _tableData = ref<User[]>([]);
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
        await _userOption.value.InitAsync(selectID);
    });
};
/**
 * 操作完毕
 */
const OptionComplate = () => {
    _optionDrawerVisible.value = false;
    SearchData();
};
/**
 * 检索数据
 */
const SearchData = async () => {
    _searching.value = true;
    const result = await UserService.GetListAsync(_queryData);
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
const TableChange = (pagination: TablePaginationConfig) => {
    _queryData.PageIndex = pagination.current ?? 1;
    _queryData.PageSize = pagination.pageSize ?? 10;
    SearchData();
};
/**
 * 删除数据
 * @param id ID
 */
const DeleateAsync = async (id: string) => {
    _searching.value = true;
    await UserService.DeleteAsync(id);
    SearchData();
    _searching.value = false;
};
/**
 * 页面加载完毕事件
 */
onMounted(() => { SearchData(); });
</script>