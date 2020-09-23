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
var notificationManage = new NotificationManage();
