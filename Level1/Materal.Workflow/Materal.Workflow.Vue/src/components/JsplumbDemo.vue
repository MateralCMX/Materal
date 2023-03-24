<style>
.Canvas {
    position: absolute;
    top: 0;
    bottom: 0;
    left: 175px;
    right: 300px;
    background-color: #F7F7F7;
}

.Step {
    position: absolute;
    text-align: center;
    width: 140px;
    height: 80px;
    line-height: 80px;
    border-radius: 5px;
}

.StartStep {
    background-color: #C5E99B;
}
</style>
<template>
    <div id="workflowCanvas" class="Canvas">
        <StartStep :instance="instance" />
    </div>
</template>
<script setup lang="ts">
import { onMounted, ref, shallowRef } from 'vue';
import StartStep from './steps/StartStep.vue';
import { BrowserJsPlumbInstance, newInstance, ContainmentType } from "@jsplumb/browser-ui";
import { DotEndpoint, RectangleEndpoint, EVENT_CONNECTION, EVENT_CONNECTION_DETACHED } from "@jsplumb/core";

let workflowCanvasElement: HTMLElement | null;
let instance = shallowRef<BrowserJsPlumbInstance>();
const initCanvas = () => {
    workflowCanvasElement = document.getElementById("workflowCanvas");
    if (!workflowCanvasElement) return;
    instance.value = newInstance({
        container: workflowCanvasElement,
        dragOptions: {
            containment: ContainmentType.parentEnclosed,
            grid: {
                w: 10,
                h: 10
            }
        }
    });
}
onMounted(initCanvas);
const count = ref(0)
</script>
