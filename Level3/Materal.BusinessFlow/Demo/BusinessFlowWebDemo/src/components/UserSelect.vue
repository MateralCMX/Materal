<template>
    <a-select v-model:value="model" show-search placeholder="输入用户名称" :default-active-first-option="false"
        :show-arrow="false" :filter-option="false" :not-found-content="null" :options="users"
        @search="handlerSearch"></a-select>
</template>
<script setup lang="ts">
import type { SelectProps } from 'ant-design-vue';
import UserService from '../services/UserService';

/**
 * 暴露成员
 */
const props = defineProps<{ modelValue: string | undefined }>();
/**
 * 绑定模型
 */
const model = useVModel(props, 'modelValue');
/**
 * 用户列表
 */
const users = ref<SelectProps['options']>([]);
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