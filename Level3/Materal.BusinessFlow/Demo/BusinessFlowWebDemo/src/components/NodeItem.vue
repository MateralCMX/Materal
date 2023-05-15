<style scoped>
.node-item {
    text-align: left;
    position: relative;
    background-color: white;
    border: 1px solid #e5e5e5;
    border-radius: 2px;
    margin-bottom: 5px;
    padding: 5px 15px;
}

.node-item>.node-item-button-group {
    position: absolute;
    top: 0;
    right: 0;
}
</style>
<template>
    <div class="node-item">
        <span>{{ nodeData.Name }}</span>
        <div class="node-item-button-group">
            <a-button type="primary">编辑</a-button>
            <a-popconfirm title="确定删除该项?" ok-text="确定" cancel-text="取消" @confirm="deleteNodeAsync">
                <a-button type="primary" danger :loading="loading"> {{ loading ? "" : "删除" }} </a-button>
            </a-popconfirm>
        </div>
    </div>
</template>
<script setup lang="ts">
import { Node } from "../models/Node/Node";
import NodeService from "../services/NodeService";

/**
 * 暴露成员
 */
const props = defineProps<{ nodeData: Node; loading: boolean }>();
/**
 * 事件
 */
const emits = defineEmits<{
    (event: "editNode"): void;
    (event: "deleteNode"): void;
    (event: "update:loading", value: boolean): void;
}>();
/**
 * 删除步骤
 */
const deleteNodeAsync = async () => {
    emits('update:loading', true);
    await NodeService.DeleteAsync(props.nodeData.ID);
    emits("deleteNode");
    emits('update:loading', false);
}
</script>