using CommunityToolkit.Mvvm.Input;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.HttpClient;
using RC.Authority.PresentationModel.User;
using RCManagementTool.Controls;
using System.ComponentModel.DataAnnotations;

namespace RCManagementTool.Pages.User
{
    public partial class UserOptionsViewModel : ObservableValidatorModel, IDrawerPanelContent
    {
        /// <summary>
        /// 是否为新增
        /// </summary>
        public bool IsAdd => ID is null;
        /// <summary>
        /// 唯一标识
        /// </summary>
        [ObservableProperty]
        private Guid? _ID;
        /// <summary>
        /// 姓名
        /// </summary>
        [ObservableProperty]
        [Required(ErrorMessage = "姓名为空"), StringLength(100, ErrorMessage = "姓名最多100个字符")]
        private string _name = string.Empty;
        /// <summary>
        /// 姓名错误信息
        /// </summary>
        [ObservableProperty]
        private string _nameErrorMessage = string.Empty;
        /// <summary>
        /// 账号
        /// </summary>
        [ObservableProperty]
        [Required(ErrorMessage = "账号为空"), StringLength(100, ErrorMessage = "账号最多100个字符")]
        private string _account = string.Empty;
        /// <summary>
        /// 账号错误信息
        /// </summary>
        [ObservableProperty]
        private string _accountErrorMessage = string.Empty;
        /// <summary>
        /// 抽屉面板
        /// </summary>
        public DrawerPanelViewModel DrawerPanel { get; }
        /// <summary>
        /// 保存成功事件
        /// </summary>
        public event Func<Task>? OnSaveSuccess;
        public UserOptionsViewModel(DrawerPanelViewModel drawerPanel)
        {
            DrawerPanel = drawerPanel;
        }
        /// <summary>
        /// 加载用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task LoadUserInfoAsync(Guid? id)
        {
            if (id is null)
            {
                LoadAddUserInfo();
            }
            else
            {
                await LoadEditUserInfoAsync(id.Value);
            }
        }
        /// <summary>
        /// 加载新增用户信息
        /// </summary>
        private void LoadAddUserInfo()
        {
            ID = null;
            Name = string.Empty;
            Account = string.Empty;
        }
        /// <summary>
        /// 加载编辑用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task LoadEditUserInfoAsync(Guid id)
        {
            DrawerPanel.LoadingMask.IsShow = true;
            try
            {
                UserHttpClient userHttpClient = App.ServiceProvider.GetRequiredService<UserHttpClient>();
                UserDTO? user = await userHttpClient.GetInfoAsync(id);
                if(user is null)
                {
                    LoadAddUserInfo();
                    return;
                }
                user.CopyProperties(this);
            }
            catch (Exception ex)
            {
                RCMessageManager.SendExceptionMessage(ex);
            }
            finally
            {
                DrawerPanel.LoadingMask.IsShow = false;
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        [RelayCommand]
        private async Task SaveAsync()
        {
            DrawerPanel.LoadingMask.IsShow = true;
            try
            {
                if (!ValidateErrors()) return;
                if (ID is null)
                {
                    await AddAsync();
                }
                else
                {
                    await EditAsync();
                }
                DrawerPanel.LoadingMask.IsShow = false;
                Close();
                if(OnSaveSuccess is not null)
                {
                    await OnSaveSuccess.Invoke();
                }
            }
            catch (Exception ex)
            {
                RCMessageManager.SendExceptionMessage(ex);
                DrawerPanel.LoadingMask.IsShow = false;
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        [RelayCommand]
        private void Close() => DrawerPanel.Visibility = Visibility.Collapsed;
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        private async Task AddAsync()
        {
            if (ID is not null) return;
            UserHttpClient userHttpClient = App.ServiceProvider.GetRequiredService<UserHttpClient>();
            AddUserRequestModel requestModel = this.CopyProperties<AddUserRequestModel>();
            Guid id = await userHttpClient.AddAsync(requestModel);
            ID = id;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        private async Task EditAsync()
        {
            if (ID is null) return;
            UserHttpClient userHttpClient = App.ServiceProvider.GetRequiredService<UserHttpClient>();
            EditUserRequestModel requestModel = this.CopyProperties<EditUserRequestModel>();
            await userHttpClient.EditAsync(requestModel);
        }
    }
}
