<style scoped>
.Canvas {
    position: absolute;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    background-color: #F7F7F7;
}

.Step {
    position: absolute;
}
</style>
<template>
    <div ref="workflowCanvas" class="Canvas">
        <div ref="step0" id="step0" class="Step StartStep">
            Step0
            <div class="Point NextPoint"></div>
        </div>
        <div ref="step1" id="step1" class="Step StartStep">
            Step1
            <div class="Point NextPoint"></div>
        </div>
        <div ref="step2" id="step2" class="Step ThenStep">
            Step2
            <div class="Point NextPoint"></div>
            <div class="Point CompensatePoint"></div>
            <div class="Point EndPoint"></div>
        </div>
        <div ref="step3" id="step3" class="Step ThenStep">
            Step3
            <div class="Point NextPoint"></div>
            <div class="Point CompensatePoint"></div>
            <div class="Point EndPoint"></div>
        </div>
    </div>
</template>
<script setup lang="ts">
import { BrowserJsPlumbInstance, ContainmentType, newInstance } from "@jsplumb/browser-ui";
import { BeforeDropParams, DotEndpoint, INTERCEPT_BEFORE_DROP, EVENT_CONNECTION, EVENT_CONNECTION_DETACHED, RectangleEndpoint, INTERCEPT_BEFORE_DETACH, INTERCEPT_BEFORE_START_DETACH, INTERCEPT_BEFORE_DRAG } from "@jsplumb/core";
import { onMounted, ref, shallowRef } from "vue";
import "../css/Step.css";

let instance = shallowRef<BrowserJsPlumbInstance>();
const workflowCanvas = ref<HTMLElement>();
const step0 = ref<Element>();
const step1 = ref<Element>();
const step2 = ref<Element>();
const step3 = ref<Element>();

onMounted(() => {
    InitCanvas();
});
const InitCanvas = () => {
    if (!workflowCanvas || !workflowCanvas.value) return;
    instance.value = newInstance({
        container: workflowCanvas.value,
        dragOptions: {
            containment: ContainmentType.parentEnclosed,
            grid: { w: 10, h: 10 }
        }
    });
    instance.value.bind(EVENT_CONNECTION, (params) => {
        console.log("EVENT_CONNECTION");
        // console.log(params);
    });
    // instance.value.bind(EVENT_CONNECTION_DETACHED, (params) => {        
    //     console.log(params);
    // });
    // instance.value.bind(INTERCEPT_BEFORE_DRAG, (params: BeforeDropParams) => {
    //     console.log("INTERCEPT_BEFORE_DRAG");//1
    // });
    instance.value.bind(INTERCEPT_BEFORE_DROP, (params: BeforeDropParams) => {
        if (params.sourceId === params.targetId) return false;
        console.log(params);
        return true;
    });
    // instance.value.bind(INTERCEPT_BEFORE_DETACH, (params: BeforeDropParams) => {
    //     console.log("INTERCEPT_BEFORE_DETACH");
    // });
    // instance.value.bind(INTERCEPT_BEFORE_START_DETACH, (params: BeforeDropParams) => {
    //     console.log("INTERCEPT_BEFORE_START_DETACH");
    // });

    instance.value.addSourceSelector(".NextPoint", {
        source: true,
        target: false,
        anchor: "Continuous",
        endpoint: DotEndpoint.type,
        connectorClass: "NextConnector"
    });

    instance.value.addSourceSelector(".CompensatePoint", {
        source: true,
        target: false,
        anchor: "Continuous",
        endpoint: DotEndpoint.type,
        connectorClass: "CompensateConnector"
    });

    instance.value.addTargetSelector(".EndPoint", {
        source: false,
        target: true,
        anchor: "Continuous",
        endpoint: RectangleEndpoint.type
    });
    if (!step0.value || !step1.value || !step2.value || !step3.value) return;
    instance.value.manage(step0.value, "step0");
    instance.value.manage(step1.value, "step1");
    instance.value.manage(step2.value, "step2");
    instance.value.manage(step3.value, "step3");
}
</script>