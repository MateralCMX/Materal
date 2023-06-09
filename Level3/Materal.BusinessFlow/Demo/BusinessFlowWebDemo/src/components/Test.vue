<template>
    {{ data }}
    <KeyValueModelComponent :data="data" />
</template>
<script lang="ts" setup>
import { KeyValueModel } from '../models/KeyValueModel';

const data = ref<KeyValueModel[]>([
    { Key: 'key1', Value: 'value1' },
    { Key: 'key2', Value: 'value2' }
]);
</script>