<template>
    {{ data }}
    <KeyValueModelComponent :data="data" title="测试" />
</template>
<script lang="ts" setup>
import { KeyValueModel } from '../models/KeyValueModel';

const data = ref<KeyValueModel[]>([
    { Key: 'key1', Value: 'value1' },
    { Key: 'key2', Value: 'value2' }
]);
</script>