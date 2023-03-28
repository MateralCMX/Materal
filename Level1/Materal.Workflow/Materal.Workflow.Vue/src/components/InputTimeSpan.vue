<style scoped>
.ant-input-number-group-wrapper {
    width: 100%;
}
</style>
<template>
    <a-row :gutter="[16, 16]">
        <a-col :span="8">
            <a-input-number v-model:value="hours" @change="updateValue" :min="0">
                <template #addonAfter>小时</template>
            </a-input-number>
        </a-col>
        <a-col :span="8">
            <a-input-number v-model:value="minutes" @change="updateValue" :min="0">
                <template #addonAfter>分钟</template>
            </a-input-number>
        </a-col>
        <a-col :span="8">
            <a-input-number v-model:value="seconds" @change="updateValue" :min="0">
                <template #addonAfter>秒</template>
            </a-input-number>
        </a-col>
    </a-row>
</template>
<script setup lang="ts">
import TimeSpan from '@web-atoms/date-time/dist/TimeSpan';
import { ref } from 'vue';

const props = defineProps(['modelValue'])
const emits = defineEmits(['update:modelValue'])

const hours = ref((props.modelValue as TimeSpan).hours);
const minutes = ref((props.modelValue as TimeSpan).minutes);
const seconds = ref((props.modelValue as TimeSpan).seconds);
const updateValue = () => {
    emits('update:modelValue', new TimeSpan(0, hours.value, minutes.value, seconds.value));
}
</script>