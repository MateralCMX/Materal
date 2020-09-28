class NotificationManage {
    /** 
     * 消息通知
     * @param title 标题
     * @param content 内容
     * @param timer 持续时间
     */
    public Show(title: string, content: string, timer: number = 3000) {
        this.CreateNotification(title, content, null, timer);
    }
    /** 
     * 消息通知
     * @param title 标题
     * @param content 内容
     * @param timer 持续时间
     */
    public Info(title: string, content: string, timer: number = 3000) {
        this.CreateNotification(title, content, 'info', timer);
    }
    /** 
     * 成功通知
     * @param title 标题
     * @param content 内容
     * @param timer 持续时间
     */
    public Success(title: string, content: string, timer: number = 3000) {
        this.CreateNotification(title, content, 'success', timer);
    }
    /** 
     * 错误通知
     * @param title 标题
     * @param content 内容
     * @param timer 持续时间
     */
    public Error(title: string, content: string, timer: number = 3000) {
        this.CreateNotification(title, content, 'error', timer);
    }
    /** 
     * 警告通知
     * @param title 标题
     * @param content 内容
     * @param timer 持续时间
     */
    public Warning(title: string, content: string, timer: number = 3000) {
        this.CreateNotification(title, content, 'warning', timer);
    }
    private _materalUINotificationPanelElement: HTMLDivElement;
    /**
     * 获得提示面板
     */
    private GetMateralUINotificationPanelElement(): HTMLDivElement {
        if (!this._materalUINotificationPanelElement) {
            this._materalUINotificationPanelElement = document.getElementById('MateralUINotificationPanel') as HTMLDivElement;
        }
        return this._materalUINotificationPanelElement;
    }
    /**
     * 创建通知
     * @param title 标题
     * @param content 内容
     * @param type 类型
     */
    private CreateNotification(title: string, content: string, type: null | 'success' | 'info' | 'error' | 'warning', timer: number) {
        const notificationElement = document.createElement('section');
        notificationElement.classList.add('m_notification');
        if (type) {
            notificationElement.classList.add(`m_notification_${type}`);
        }
        const headerElement = document.createElement('div');
        headerElement.classList.add('m_notification_header');
        const titleElement = document.createElement('div');
        titleElement.classList.add('m_notification_title');
        titleElement.innerHTML = title;
        const btnCloseElement = document.createElement('button');
        btnCloseElement.classList.add('m_notification_btn_close');
        btnCloseElement.classList.add('Micon');
        btnCloseElement.classList.add('Micon_close');
        btnCloseElement.onclick = () => {
            this.CloseNotification(notificationElement);
        };
        const contentElement = document.createElement('div');
        contentElement.classList.add('m_notification_content');
        contentElement.innerHTML = content;
        headerElement.appendChild(titleElement);
        headerElement.appendChild(btnCloseElement);
        notificationElement.appendChild(headerElement);
        notificationElement.appendChild(contentElement);
        this.GetMateralUINotificationPanelElement().appendChild(notificationElement);
        if (timer && timer > 0) {
            setTimeout(() => {
                this.CloseNotification(notificationElement);
            }, timer);
        }
    }
    /**
     * 关闭通知
     * @param notificationElement 元素
     */
    private CloseNotification(notificationElement: HTMLElement) {
        const buttons = notificationElement.getElementsByTagName('button');
        for (const button of buttons) {
            button.disabled = true;
        }
        notificationElement.classList.add('out');
        setTimeout(() => {
            try {
                this.GetMateralUINotificationPanelElement().removeChild(notificationElement);
            }
            catch {

            }
        }, 200);
    }
}
class MessageManage {
    /** 
     * 消息
     * @param content 内容
     * @param timer 持续时间
     */
    public Show(content: string, timer: number = 3000) {
        this.CreateMessage(content, null, timer);
    }
    /** 
     * 成功消息
     * @param content 内容
     * @param timer 持续时间
     */
    public Success(content: string, timer: number = 3000) {
        this.CreateMessage(content, 'success', timer);
    }
    /** 
     * 错误消息
     * @param content 内容
     * @param timer 持续时间
     */
    public Error(content: string, timer: number = 3000) {
        this.CreateMessage(content, 'error', timer);
    }
    /** 
     * 警告消息
     * @param content 内容
     * @param timer 持续时间
     */
    public Warning(content: string, timer: number = 3000) {
        this.CreateMessage(content, 'warning', timer);
    }
    private _materalUIMessagePanelElement: HTMLDivElement;
    /**
     * 获得提示面板
     */
    private GetMateralUIMessagePanelElement(): HTMLDivElement {
        if (!this._materalUIMessagePanelElement) {
            this._materalUIMessagePanelElement = document.getElementById('MateralUIMessagePanel') as HTMLDivElement;
        }
        return this._materalUIMessagePanelElement;
    }
    /**
     * 创建消息
     * @param content 内容
     * @param type 类型
     */
    private CreateMessage(content: string, type: null | 'success' | 'info' | 'error' | 'warning', timer: number) {
        const messageElement = document.createElement('section');
        messageElement.classList.add('m_message');
        if (type) {
            messageElement.classList.add(`m_message_${type}`);
        }
        const contentElement = document.createElement('p');
        contentElement.classList.add('m_message_content');
        contentElement.innerHTML = content;
        const btnCloseElement = document.createElement('button');
        btnCloseElement.classList.add('m_message_btn_close');
        btnCloseElement.classList.add('Micon');
        btnCloseElement.classList.add('Micon_close');
        btnCloseElement.onclick = () => {
            this.CloseMessage(messageElement);
        };
        messageElement.appendChild(contentElement);
        messageElement.appendChild(btnCloseElement);
        this.GetMateralUIMessagePanelElement().appendChild(messageElement);
        if (timer && timer > 0) {
            setTimeout(() => {
                this.CloseMessage(messageElement);
            }, timer);
        }
    }
    /**
     * 关闭消息
     * @param notificationElement 元素
     */
    private CloseMessage(notificationElement: HTMLElement) {
        this.GetMateralUIMessagePanelElement().removeChild(notificationElement);
    }
}
const notificationManage = new NotificationManage();
const messageManage = new MessageManage();