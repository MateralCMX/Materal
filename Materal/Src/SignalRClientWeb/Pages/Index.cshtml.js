const hubUrlKey = "HubUrl";
const hubNameKey = "HubName";
const actionNameKey = "ActionName";
const messageKey = "SendMessage";
const subscribesKey = "Subscribes";
const inputUrl = document.getElementById("inputUrl");
const inputHubName = document.getElementById("inputHubName");
const inputActionName = document.getElementById("inputActionName");
const textareaMessage = document.getElementById("textareaMessage");
const listReplyMessage = document.getElementById("listReplyMessage");
const labelStatus = document.getElementById("labelStatus");
const btnConnect = document.getElementById("btnConnect");
const btnSend = document.getElementById("btnSend");
const btnClearReplyMessage = document.getElementById("btnClearReplyMessage");
const inputSubscribeName = document.getElementById("inputSubscribeName");
const btnSubscribe = document.getElementById("btnSubscribe");
const btnCancelSubscribe = document.getElementById("btnCancelSubscribe");
const subscribeList = document.getElementById("subscribeList");
let connection;
let subscribes = [];
/**
 * 获取LocalStorage值
 */
function getLocalStorageValue(key, defaultValue) {
    let value = window.localStorage.getItem(key);
    if (!value) {
        value = defaultValue;
        window.localStorage.setItem(key, value);
    }
    return value;
}
/**
 * 设置按钮状态为运行中
 */
function setBtnStatusByRuning(btn, text) {
    btn.disabled = true;
    if (!text) {
        text = btn.innerHTML;
    }
    btn.innerHTML = '<span id="btnConnectLoading" class="spinner-border spinner-border-sm" role="status"></span>' + text + '...';
}
/**
 * 设置按钮状态为运行中
 */
function setBtnStatusByStop(btn, text) {
    btn.disabled = false;
    if (!text) {
        text = btn.innerHTML;
    }
    btn.innerHTML = text;
}
/**
 * 更改状态为成功
 */
function changeStatusBySuccess() {
    setBtnStatusByStop(btnConnect, "断开");
    btnConnect.classList.remove("btn-success");
    btnConnect.classList.remove("btn-danger");
    btnConnect.classList.add("btn-danger");
    inputUrl.disabled = true;
    inputHubName.disabled = true;
    inputActionName.disabled = false;
    textareaMessage.disabled = false;
    inputSubscribeName.disabled = false;
    btnSubscribe.disabled = false;
    btnCancelSubscribe.disabled = false;
    btnSend.disabled = false;
    labelStatus.innerHTML = "连接成功";
    labelStatus.classList.remove("dot-red");
    labelStatus.classList.remove("dot-green");
    labelStatus.classList.remove("dot-yellow");
    labelStatus.classList.add("dot-green");
}
/**
 * 更改状态为断开
 */
function changeStatusByFail() {
    setBtnStatusByStop(btnConnect, "连接");
    btnConnect.classList.remove("btn-success");
    btnConnect.classList.remove("btn-danger");
    btnConnect.classList.add("btn-success");
    inputUrl.disabled = false;
    inputHubName.disabled = false;
    inputActionName.disabled = true;
    textareaMessage.disabled = true;
    inputSubscribeName.disabled = true;
    btnSubscribe.disabled = true;
    btnCancelSubscribe.disabled = true;
    btnSend.disabled = true;
    labelStatus.innerHTML = "连接断开";
    labelStatus.classList.remove("dot-red");
    labelStatus.classList.remove("dot-green");
    labelStatus.classList.remove("dot-yellow");
    labelStatus.classList.add("dot-red");
}
/**
 * 更改状态为连接中
 */
function changeStatusByConnecting() {
    setBtnStatusByRuning(btnConnect, "正在连接");
    btnConnect.classList.remove("btn-success");
    btnConnect.classList.remove("btn-danger");
    btnConnect.classList.add("btn-success");
    inputUrl.disabled = true;
    inputHubName.disabled = true;
    inputActionName.disabled = true;
    textareaMessage.disabled = true;
    inputSubscribeName.disabled = false;
    btnSubscribe.disabled = false;
    btnCancelSubscribe.disabled = false;
    btnSend.disabled = true;
    labelStatus.innerHTML = "正在连接...";
    labelStatus.classList.remove("dot-red");
    labelStatus.classList.remove("dot-green");
    labelStatus.classList.remove("dot-yellow");
    labelStatus.classList.add("dot-yellow");
}
/**
 * 连接按钮单击
 */
async function btnConnectClick() {
    if (connection && connection.state == 'Connected') {
        await connection.stop();
        connection = null;
        changeStatusByFail();
    }
    else {
        changeStatusByConnecting();
        window.localStorage.setItem(hubUrlKey, inputUrl.value);
        window.localStorage.setItem(hubNameKey, inputHubName.value);
        connection = new signalR.HubConnectionBuilder()
            .withUrl(inputUrl.value + inputHubName.value, {
                skipNegotiation: true,
                transport: signalR.HttpTransportType.WebSockets
            })
            .configureLogging(signalR.LogLevel.Information)
            .build();
        try {
            await connection.start();
            connection.onclose(() => {
                showReplyMessage("与服务器连接中断");
                changeStatusByFail();
            });
            for (var i in subscribes) {
                subscribEvent(subscribes[i]);
            }
            updateSubscribeList();
            changeStatusBySuccess();
        } catch (err) {
            showReplyMessage(err);
            changeStatusByFail();
        }
    }
}
/**
 * 发送按钮单击
 */
async function btnSendClick() {
    if (!connection || connection.state != 'Connected') return;
    try {
        window.localStorage.setItem(actionNameKey, inputActionName.value);
        window.localStorage.setItem(messageKey, textareaMessage.value);
        let values = textareaMessage.value.split('|');
        let invokeString = "connection.invoke(inputActionName.value";
        for (var i in values) {
            if (isJson(values[i])) {
                let jsonValue = JSON.parse(values[i]);
                invokeString += ", " + jsonValue;
            }
            else {
                invokeString += ", " + values[i];
            }
        }
        invokeString += ")";
        await eval(invokeString);
    } catch (err) {
        showReplyMessage(err);
    }
}
/**
 * 清空按钮单击
 */
function btnClearReplyMessageClick() {
    listReplyMessage.innerHTML = "";
}
/**
 * 订阅按钮单击
 */
function btnSubscribeClick() {
    if (!connection || connection.state != 'Connected') return;
    inputSubscribeName.disabled = true;
    btnSubscribe.disabled = true;
    btnCancelSubscribe.disabled = true;
    subscribEvent(inputSubscribeName.value);
    subscribes.push(inputSubscribeName.value);
    updateSubscribeList();
    inputSubscribeName.disabled = false;
    btnSubscribe.disabled = false;
    btnCancelSubscribe.disabled = false;
}
/**
* 取消订阅按钮单击
*/
function btnCancelSubscribeClick() {
    if (!connection || connection.state != 'Connected') return;
    inputSubscribeName.disabled = true;
    btnSubscribe.disabled = true;
    btnCancelSubscribe.disabled = true;
    cancelSubscribEvent(inputSubscribeName.value);
    for (var i = 0; i < subscribes.length; i++) {
        if (subscribes[i] == inputSubscribeName.value) {
            subscribes.splice(i--, 1);
        }
    }
    updateSubscribeList();
    inputSubscribeName.disabled = false;
    btnSubscribe.disabled = false;
    btnCancelSubscribe.disabled = false;
}
/**
 * 是否为Json字符串
 */
function isJson(value) {
    if (value.constructor != String) return false;
    let result = value.startsWith("{") && value.startsWith("}");
    if (result) return result;
    result = value.startsWith("[") && value.startsWith("]");
    return result;
}
/**
 * 订阅
 */
function subscribEvent(eventName) {
    if (!connection || connection.state != 'Connected') return;
    connection.on(eventName, showReplyMessage);
}
/**
 * 取消订阅
 */
function cancelSubscribEvent(eventName) {
    if (!connection || connection.state != 'Connected') return;
    connection.off(eventName);
}
/**
 * 更新订阅列表
 */
function updateSubscribeList() {
    subscribeList.innerHTML = "";
    for (var i in subscribes) {
        const li = document.createElement("li");
        li.textContent = subscribes[i];
        subscribeList.appendChild(li);
    }
    localStorage.setItem(subscribesKey, JSON.stringify(subscribes));
}
/**
 * 显示回复消息
 */
function showReplyMessage() {
    const li = document.createElement("li");
    let args = [];
    for (let index in arguments) {
        if (arguments[index].constructor == Error) {
            args.push(arguments[index].message);
        }
        else if (arguments[index] instanceof Object) {
            args.push(JSON.stringify(arguments[index]));
        }
        else {
            args.push(arguments[index]);
        }
    }
    li.textContent = args.join(", ");
    listReplyMessage.appendChild(li);
}
window.onload = () => {
    //加载配置
    inputUrl.value = getLocalStorageValue(hubUrlKey, "http://localhost:5000/hubs/");
    inputHubName.value = getLocalStorageValue(hubNameKey, "Test");
    inputActionName.value = getLocalStorageValue(actionNameKey, "Test");
    textareaMessage.value = getLocalStorageValue(messageKey, "");
    subscribes = JSON.parse(getLocalStorageValue(subscribesKey, "[]"));
    if (subscribes.length > 0) {
        inputSubscribeName.value = subscribes[0];
    }
    //添加事件
    btnConnect.addEventListener("click", btnConnectClick);
    btnSend.addEventListener("click", btnSendClick);
    btnClearReplyMessage.addEventListener("click", btnClearReplyMessageClick);
    btnSubscribe.addEventListener("click", btnSubscribeClick);
    btnCancelSubscribe.addEventListener("click", btnCancelSubscribeClick);
}