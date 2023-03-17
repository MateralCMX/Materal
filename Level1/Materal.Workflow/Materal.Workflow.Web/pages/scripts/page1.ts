import { ContainmentType, newInstance } from "@jsplumb/browser-ui";
import "@jsplumb/browser-ui/css/jsplumbtoolkit.css";
import { FlowchartConnector } from "@jsplumb/connector-flowchart";
import { DotEndpoint, BlankEndpoint } from "@jsplumb/core";

let canvas = document.getElementById("canvas");
if (canvas == null) throw new Error("未找到元素canvas");
//组装DOM开始
let node1 = document.createElement("div");
node1.classList.add("item");
node1.style.top = "20px";
node1.style.left = "360px";
canvas.appendChild(node1);
let node2 = document.createElement("div");
node2.classList.add("item");
node2.style.top = "60px";
node2.style.left = "150px";
canvas.appendChild(node2);
let node3 = document.createElement("div");
node3.classList.add("item");
node3.style.top = "130px";
node3.style.left = "360px";
canvas.appendChild(node3);
//组装DOM结束

let instance = newInstance({
    container: canvas,
    // connectionsDetachable: false
});

instance.bind("EVENT_ELEMENT_CLICK", e => console.log(e));
//创建连接
// instance.connect({
//     source: node1,
//     target: node2,
//     anchor: "AutoDefault",
//     connector: FlowchartConnector.type,
//     endpoint: DotEndpoint.type
// });
// instance.connect({
//     source: node2,
//     target: node3,
//     anchor: "AutoDefault",
//     endpoint: BlankEndpoint.type
// });
//创建端点
var sourceEndpoint = instance.addEndpoint(node1, {
    target: true,
    anchor: "AutoDefault",
    endpoint: "Rectangle",
    reattachConnections: true
});
var sourceEndpoint2 = instance.addEndpoint(node2, {
    source: true,
    anchor: "AutoDefault",
    endpoint: "Dot",
    reattachConnections: true
});
var sourceEndpoint3 = instance.addEndpoint(node3, {
    target: true,
    anchor: "AutoDefault",
    endpoint: "Rectangle",
    reattachConnections: true
});