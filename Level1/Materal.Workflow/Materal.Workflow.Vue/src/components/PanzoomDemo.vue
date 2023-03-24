<style>
.canvas {
  display: block;
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
}
</style>
<template>
  <div class="canvas">
    <div id="workflowCanvas">
      123456
    </div>
  </div>
</template>
<script setup lang="ts">
import { onMounted, ref } from 'vue';
import Panzoom from "@panzoom/panzoom";

let workflowCanvas: HTMLElement | null;
// defineProps<{ msg: string }>()

onMounted(() => {
  initCanvas();
});
const initCanvas = () => {
  workflowCanvas = document.getElementById("workflowCanvas");
  if (!workflowCanvas) return;
  const panzoom = Panzoom(workflowCanvas, {
    maxZoom: 5
  });
  panzoom.pan(100, 100);
  workflowCanvas.parentElement?.addEventListener('wheel', panzoom.zoomWithWheel)
}
const count = ref(0)
</script>
