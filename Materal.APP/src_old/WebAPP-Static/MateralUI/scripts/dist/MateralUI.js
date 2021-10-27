var NotificationManage = /** @class */ (function () {
    function NotificationManage() {
    }
    /**
     * 消息通知
     * @param title 标题
     * @param content 内容
     * @param timer 持续时间
     */
    NotificationManage.prototype.Show = function (title, content, timer) {
        if (timer === void 0) { timer = 3000; }
        this.CreateNotification(title, content, null, timer);
    };
    /**
     * 消息通知
     * @param title 标题
     * @param content 内容
     * @param timer 持续时间
     */
    NotificationManage.prototype.Info = function (title, content, timer) {
        if (timer === void 0) { timer = 3000; }
        this.CreateNotification(title, content, 'info', timer);
    };
    /**
     * 成功通知
     * @param title 标题
     * @param content 内容
     * @param timer 持续时间
     */
    NotificationManage.prototype.Success = function (title, content, timer) {
        if (timer === void 0) { timer = 3000; }
        this.CreateNotification(title, content, 'success', timer);
    };
    /**
     * 错误通知
     * @param title 标题
     * @param content 内容
     * @param timer 持续时间
     */
    NotificationManage.prototype.Error = function (title, content, timer) {
        if (timer === void 0) { timer = 3000; }
        this.CreateNotification(title, content, 'error', timer);
    };
    /**
     * 警告通知
     * @param title 标题
     * @param content 内容
     * @param timer 持续时间
     */
    NotificationManage.prototype.Warning = function (title, content, timer) {
        if (timer === void 0) { timer = 3000; }
        this.CreateNotification(title, content, 'warning', timer);
    };
    /**
     * 获得提示面板
     */
    NotificationManage.prototype.GetMateralUINotificationPanelElement = function () {
        if (!this._materalUINotificationPanelElement) {
            this._materalUINotificationPanelElement = document.getElementById('MateralUINotificationPanel');
        }
        return this._materalUINotificationPanelElement;
    };
    /**
     * 创建通知
     * @param title 标题
     * @param content 内容
     * @param type 类型
     */
    NotificationManage.prototype.CreateNotification = function (title, content, type, timer) {
        var _this = this;
        var notificationElement = document.createElement('section');
        notificationElement.classList.add('m_notification');
        if (type) {
            notificationElement.classList.add("m_notification_" + type);
        }
        var headerElement = document.createElement('div');
        headerElement.classList.add('m_notification_header');
        var titleElement = document.createElement('div');
        titleElement.classList.add('m_notification_title');
        titleElement.innerHTML = title;
        var btnCloseElement = document.createElement('button');
        btnCloseElement.classList.add('m_notification_btn_close');
        btnCloseElement.classList.add('Micon');
        btnCloseElement.classList.add('Micon_close');
        btnCloseElement.onclick = function () {
            _this.CloseNotification(notificationElement);
        };
        var contentElement = document.createElement('div');
        contentElement.classList.add('m_notification_content');
        contentElement.innerHTML = content;
        headerElement.appendChild(titleElement);
        headerElement.appendChild(btnCloseElement);
        notificationElement.appendChild(headerElement);
        notificationElement.appendChild(contentElement);
        this.GetMateralUINotificationPanelElement().appendChild(notificationElement);
        if (timer && timer > 0) {
            setTimeout(function () {
                _this.CloseNotification(notificationElement);
            }, timer);
        }
    };
    /**
     * 关闭通知
     * @param notificationElement 元素
     */
    NotificationManage.prototype.CloseNotification = function (notificationElement) {
        var _this = this;
        var buttons = notificationElement.getElementsByTagName('button');
        for (var _i = 0, buttons_1 = buttons; _i < buttons_1.length; _i++) {
            var button = buttons_1[_i];
            button.disabled = true;
        }
        notificationElement.classList.add('out');
        setTimeout(function () {
            try {
                _this.GetMateralUINotificationPanelElement().removeChild(notificationElement);
            }
            catch (_a) {
            }
        }, 200);
    };
    return NotificationManage;
}());
var MessageManage = /** @class */ (function () {
    function MessageManage() {
    }
    /**
     * 消息
     * @param content 内容
     * @param timer 持续时间
     */
    MessageManage.prototype.Show = function (content, timer) {
        if (timer === void 0) { timer = 3000; }
        this.CreateMessage(content, null, timer);
    };
    /**
     * 成功消息
     * @param content 内容
     * @param timer 持续时间
     */
    MessageManage.prototype.Success = function (content, timer) {
        if (timer === void 0) { timer = 3000; }
        this.CreateMessage(content, 'success', timer);
    };
    /**
     * 错误消息
     * @param content 内容
     * @param timer 持续时间
     */
    MessageManage.prototype.Error = function (content, timer) {
        if (timer === void 0) { timer = 3000; }
        this.CreateMessage(content, 'error', timer);
    };
    /**
     * 警告消息
     * @param content 内容
     * @param timer 持续时间
     */
    MessageManage.prototype.Warning = function (content, timer) {
        if (timer === void 0) { timer = 3000; }
        this.CreateMessage(content, 'warning', timer);
    };
    /**
     * 获得提示面板
     */
    MessageManage.prototype.GetMateralUIMessagePanelElement = function () {
        if (!this._materalUIMessagePanelElement) {
            this._materalUIMessagePanelElement = document.getElementById('MateralUIMessagePanel');
        }
        return this._materalUIMessagePanelElement;
    };
    /**
     * 创建消息
     * @param content 内容
     * @param type 类型
     */
    MessageManage.prototype.CreateMessage = function (content, type, timer) {
        var _this = this;
        var messageElement = document.createElement('section');
        messageElement.classList.add('m_message');
        if (type) {
            messageElement.classList.add("m_message_" + type);
        }
        var contentElement = document.createElement('p');
        contentElement.classList.add('m_message_content');
        contentElement.innerHTML = content;
        var btnCloseElement = document.createElement('button');
        btnCloseElement.classList.add('m_message_btn_close');
        btnCloseElement.classList.add('Micon');
        btnCloseElement.classList.add('Micon_close');
        btnCloseElement.onclick = function () {
            _this.CloseMessage(messageElement);
        };
        messageElement.appendChild(contentElement);
        messageElement.appendChild(btnCloseElement);
        this.GetMateralUIMessagePanelElement().appendChild(messageElement);
        if (timer && timer > 0) {
            setTimeout(function () {
                _this.CloseMessage(messageElement);
            }, timer);
        }
    };
    /**
     * 关闭消息
     * @param notificationElement 元素
     */
    MessageManage.prototype.CloseMessage = function (notificationElement) {
        this.GetMateralUIMessagePanelElement().removeChild(notificationElement);
    };
    return MessageManage;
}());
var notificationManage = new NotificationManage();
var messageManage = new MessageManage();
