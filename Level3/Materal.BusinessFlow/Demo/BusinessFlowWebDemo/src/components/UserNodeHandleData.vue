<template>
    <a-form-item label="用户" name="HandleData">
        <a-select v-model:value="model.HandleData" show-search placeholder="输入用户名称" :default-active-first-option="false"
            :show-arrow="false" :filter-option="false" :not-found-content="null" :options="users"
            @search="handlerSearch"></a-select>
    </a-form-item>
    <a-form-item label="页面" name="Data">
        <a-button @click="openPageTemplateDrawer">编辑模版</a-button>
    </a-form-item>
    <a-drawer v-model:visible="optionDrawerVisible" :maskClosable="false" :title="'页面模版编辑'" width="1600px">
        <PageTemplateOption ref="pageTemplateOption" />
    </a-drawer>
</template>
<script setup lang="ts">
import { EditNodeModel } from '../models/Node/EditNodeModel';
import type { SelectProps } from 'ant-design-vue';
import UserService from '../services/UserService';

/**
 * 页面模版配置
 */
 const pageTemplateOption = ref<any>();
/**
 * 操作抽屉是否显示
 */
const optionDrawerVisible = ref(false);
/**
 * 暴露成员
 */
const props = defineProps<{ modelValue: EditNodeModel }>();
/**
 * 绑定模型
 */
const model = useVModel(props, 'modelValue');
/**
 * 用户列表
 */
const users = ref<SelectProps['options']>([]);
/**
 * 打开页面模版抽屉
 */
const openPageTemplateDrawer = () => {
    optionDrawerVisible.value = true;
    nextTick(() => {
        pageTemplateOption.value.init(props.modelValue.Data);
    });
}
/**
 * 处理查询
 */
const handlerSearch = useDebounceFn(async (val: string) => await searchUserAsync(val), 500);
/**
 * 搜索用户
 * @param name 
 */
const searchUserAsync = async (name: string) => {
    const result = await UserService.GetListAsync({ PageIndex: 1, PageSize: 10, Name: name });
    if (users.value) {
        users.value = [];
    }
    if (result) {
        users.value?.splice(0, users.value.length);
        for (const item of result.Data) {
            users.value?.push({ label: item.Name, value: item.ID });
        }
    }
}
onMounted(async () => {
    await searchUserAsync('');
});
</script>