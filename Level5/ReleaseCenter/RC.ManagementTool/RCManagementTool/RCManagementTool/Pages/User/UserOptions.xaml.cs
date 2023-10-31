using RCManagementTool.Controls;
using System.ComponentModel.DataAnnotations;

namespace RCManagementTool.Pages.User
{
    public sealed partial class UserOptions : UserControl, IDrawerContentControl<UserOptionsViewModel>
    {
        public UserOptionsViewModel ViewModel { get; } = new();
        public UserOptions() => InitializeComponent();
        public async Task<bool> SaveAsync()
        {
            try
            {
                await ViewModel.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                RCMessageManager.SendExceptionMessage(ex);
                return false;
            }
        }
    }
    public partial class UserOptionsViewModel : ObservableValidatorModel
    {
        /// <summary>
        /// 是否为新增
        /// </summary>
        public bool IsAdd => ID is null;
        /// <summary>
        /// 唯一标识
        /// </summary>
        [ObservableProperty]
        public Guid? _ID;
        /// <summary>
        /// 姓名
        /// </summary>
        [ObservableProperty]
        [Required(ErrorMessage = "姓名为空"), StringLength(100, ErrorMessage = "姓名最多100个字符")]
        private string _name = string.Empty;
        /// <summary>
        /// 账号
        /// </summary>
        [ObservableProperty]
        [Required(ErrorMessage = "账号为空"), StringLength(100, ErrorMessage = "账号最多100个字符")]
        private string _account = string.Empty;
        /// <summary>
        /// 保存
        /// </summary>
        public async Task SaveAsync()
        {
            await Task.Delay(5000);
        }
    }
}
