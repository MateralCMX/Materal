<template>
    <a-form :model="formData" @finish="submitData">
        <a-form-item label="名称" name="Name" :rules="[{ required: true, message: '请输入名称' }]">
            <a-input v-model:value="formData.Name" />
        </a-form-item>
        <a-form-item label="描述" name="Name">
            <a-input v-model:value="formData.Description" />
        </a-form-item>
        <a-form-item v-if="!formData.ID" label="数据类型" name="DataType" :rules="[{ required: true, message: '请选择数据类型名称' }]">
            <DataTypeEnumSelect v-model="formData.DataType" :has-all="false" />
        </a-form-item>
        <div v-if="formData.DataType == DataTypeEnum.Enum">
            <a-form-item :label="`选项`" name="Data">
                <a-button @click="appendEnumItem" :loading="loading"> 添加一个选项 </a-button>
            </a-form-item>
            <a-form-item v-for="(_, index) in enumData" name="Data">
                <div>
                    <a-input v-model:value="enumData[index]" style="width: 90%;" />
                    <a-button @click="removeEnumItem(index)" type="primary" danger :loading="loading" style="width: 7%;">
                        {{ loading ? "" : "X" }}
                    </a-button>
                </div>
            </a-form-item>
        </div>
        <a-form-item>
            <a-button type="primary" html-type="submit" block :loading="loading">
                {{ formData.ID ? "保存" : "添加" }}
            </a-button>
        </a-form-item>
    </a-form>
</template>
<script setup lang="ts">
import DataModelFieldService from '../services/DataModelFieldService';
import { EditDataModelFieldModel } from '../models/DataModelField/EditDataModelFieldModel';
import { DataTypeEnum } from '../models/DataModelField/DataTypeEnum';

/**
 * 加载标识
 */
const loading = ref(false);
/**
 * 枚举数据
 */
const enumData = ref<string[]>([]);
/**
 * 表单数据
 */
const formData = reactive<EditDataModelFieldModel>(new EditDataModelFieldModel());
/**
 * 提交数据
 */
const submitData = async () => {
    loading.value = true;
    if (formData.DataType == DataTypeEnum.Enum) {
        formData.SetEnumData(enumData.value);
    }
    else {
        formData.Data = undefined;
    }
    if (formData.ID) {
        await DataModelFieldService.EditAsync(formData);
    }
    else {
        await DataModelFieldService.AddAsync(formData);
    }
    loading.value = false;
    emits('complate');
};
/**
 * 初始化
 * @param id 唯一标识
 */
const initAsync = async (dataModelID: string, id?: string) => {
    loading.value = true;
    formData.DataModelID = dataModelID;
    formData.ID = id ?? '';
    if (id && id !== '') {
        await initEidtAsync();
    }
    else {
        await initAddAsync();
    }
    loading.value = false;
};
/**
 * 初始化编辑
 */
const initEidtAsync = async () => {
    const result = await DataModelFieldService.GetInfoAsync(formData.ID);
    if (result) {
        formData.Name = result.Data.Name;
        formData.Description = result.Data.Description;
        formData.DataType = result.Data.DataType;
        formData.Data = result.Data.Data;
        if (formData.DataType == DataTypeEnum.Enum) {
            enumData.value = formData.GetEnumData();
        }
    }
    else {
        await initAddAsync();
    }
}
/**
 * 初始化添加
 */
const initAddAsync = async () => {
    formData.ID = '';
    formData.Name = '';
    formData.Description = undefined;
    formData.DataType = DataTypeEnum.String;
    formData.Data = undefined;
    enumData.value = [];
}
/**
 * 添加一个选项
 */
const appendEnumItem = () => {
    enumData.value.push('');
}
/**
 * 移除一个选项
 */
const removeEnumItem = (index: number) => {
    enumData.value.splice(index, 1);
}
/**
 * 事件
 */
const emits = defineEmits<{ (event: 'complate'): void }>();
/**
 * 暴露成员
 */
defineExpose({ initAsync });
</script>