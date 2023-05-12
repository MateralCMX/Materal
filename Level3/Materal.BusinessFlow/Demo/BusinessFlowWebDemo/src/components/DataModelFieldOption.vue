<template>
    <a-form :model="_formData" @finish="SubmitData">
        <a-form-item label="名称" name="Name" :rules="[{ required: true, message: '请输入名称' }]">
            <a-input v-model:value="_formData.Name" />
        </a-form-item>
        <a-form-item label="描述" name="Name">
            <a-input v-model:value="_formData.Description" />
        </a-form-item>
        <a-form-item v-if="!_formData.ID" label="数据类型" name="DataType" :rules="[{ required: true, message: '请选择数据类型名称' }]">
            <DataTypeEnumSelect v-model:value="_formData.DataType" :has-all="false" />
        </a-form-item>
        <div v-if="_formData.DataType == DataTypeEnum.Enum">
            <a-form-item :label="`选项`" name="Data">
                <a-button @click="AppendEnumItem" :loading="_loading"> 添加一个选项 </a-button>
            </a-form-item>
            <a-form-item v-for="(_, index) in _enumData" name="Data">
                <div>
                    <a-input v-model:value="_enumData[index]" style="width: 90%;" />
                    <a-button @click="RemoveEnumItem(index)" type="primary" danger :loading="_loading" style="width: 7%;">
                        {{ _loading ? "" : "X" }}
                    </a-button>
                </div>
            </a-form-item>
        </div>
        <a-form-item>
            <a-button type="primary" html-type="submit" block :loading="_loading">
                {{ _formData.ID ? "保存" : "添加" }}
            </a-button>
        </a-form-item>
    </a-form>
</template>
<script setup lang="ts">
import { reactive, ref } from 'vue';
import DataModelFieldService from '../services/DataModelFieldService';
import { EditDataModelFieldModel } from '../models/DataModelField/EditDataModelFieldModel';
import { DataTypeEnum } from '../models/DataModelField/DataTypeEnum';

/**
 * 加载标识
 */
const _loading = ref(false);
/**
 * 枚举数据
 */
const _enumData = ref<string[]>([]);
/**
 * 表单数据
 */
const _formData = reactive<EditDataModelFieldModel>(new EditDataModelFieldModel());
/**
 * 提交数据
 */
const SubmitData = async () => {
    _loading.value = true;
    if (_formData.DataType == DataTypeEnum.Enum) {
        _formData.SetEnumData(_enumData.value);
    }
    else {
        _formData.Data = undefined;
    }
    if (_formData.ID) {
        await DataModelFieldService.EditAsync(_formData);
    }
    else {
        await DataModelFieldService.AddAsync(_formData);
    }
    _loading.value = false;
    _emits('complate');
};
/**
 * 初始化
 * @param id 唯一标识
 */
const InitAsync = async (dataModelID: string, id?: string) => {
    _loading.value = true;
    _formData.DataModelID = dataModelID;
    _formData.ID = id ?? '';
    if (id && id !== '') {
        await InitEidtAsync();
    }
    else {
        await InitAddAsync();
    }
    _loading.value = false;
};
/**
 * 初始化编辑
 */
const InitEidtAsync = async () => {
    const result = await DataModelFieldService.GetInfoAsync(_formData.ID);
    if (result) {
        _formData.Name = result.Data.Name;
        _formData.Description = result.Data.Description;
        _formData.DataType = result.Data.DataType;
        _formData.Data = result.Data.Data;
        if (_formData.DataType == DataTypeEnum.Enum) {
            _enumData.value = _formData.GetEnumData();
        }
    }
    else {
        await InitAddAsync();
    }
}
/**
 * 初始化添加
 */
const InitAddAsync = async () => {
    _formData.ID = '';
    _formData.Name = '';
    _formData.Description = undefined;
    _formData.DataType = DataTypeEnum.String;
    _formData.Data = undefined;
    _enumData.value = [];
}
/**
 * 添加一个选项
 */
const AppendEnumItem = () => {
    _enumData.value.push('');
}
/**
 * 移除一个选项
 */
const RemoveEnumItem = (index: number) => {
    _enumData.value.splice(index, 1);
}
/**
 * 事件
 */
const _emits = defineEmits<{ (event: 'complate'): void }>();
/**
 * 暴露
 */
defineExpose({ InitAsync });
</script>