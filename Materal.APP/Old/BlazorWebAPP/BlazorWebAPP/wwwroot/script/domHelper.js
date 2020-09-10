window.domHelper = {
    focus: function(elementID) {
        const element = document.getElementById(elementID);
        if (!element) throw "未找到元素";
        element.focus();
    },
    emitEvent: function (elementID, eventName) {
        const element = document.getElementById(elementID);
        if (!element) throw "未找到元素";
        if (!element[eventName]) throw "未找到事件";
        element[eventName]();
    }
};