<template>
    <ul>
        <li v-for="item in list" :key="item.name" @click="nodeList.push(item.component as any)">{{ item.name }}</li>
    </ul>
    <div>
        <component v-for="(item, index) in nodeList" :is="item" :key="index" />
    </div>
</template>

<script lang="ts" setup>
import { shallowReactive, defineAsyncComponent, VNode } from 'vue';
import Node2 from './node2.vue';

// 这一句是动态加载的时候，就是动态加载js
const Node1 = defineAsyncComponent(() => import("./node1.vue"))
// const Node2 = defineAsyncComponent(() => import("./node2.vue"))

const list = [
    { name: 'node1', component: Node1 },
    { name: 'node2', component: Node2 },
]

const nodeList = shallowReactive<VNode[]>([])
</script>