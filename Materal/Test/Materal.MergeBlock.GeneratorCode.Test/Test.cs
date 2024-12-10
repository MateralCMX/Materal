using Materal.MergeBlock.Domain.Abstractions;
using Materal.Utils.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Materal.MergeBlock.GeneratorCode.Test;

/// <summary>
/// 测试接口
/// </summary>
[Description("测试接口")]
public interface ITest : IDisposable
{
    /// <summary>
    /// 名称
    /// </summary>
    string Name { get; set; }
    /// <summary>
    /// 年龄
    /// </summary>
    string Age { get; }
    /// <summary>
    /// 运行
    /// </summary>
    void Run();
    /// <summary>
    /// 异步运行
    /// </summary>
    /// <returns></returns>
    Task RunAsync();
    /// <summary>
    /// 说话
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    string SayHello(string name);
    /// <summary>
    /// 异步说话
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<string> SayHelloAsync(string name);
}
/// <summary>
/// 用户
/// </summary>
public class User : BaseDomain, IDomain
{
    /// <summary>
    /// 姓名
    /// </summary>
    [Required(ErrorMessage = "姓名为空"), StringLength(100, ErrorMessage = "姓名过长")]
    [Contains]
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 账号
    /// </summary>
    [Required(ErrorMessage = "账号为空"), StringLength(50, ErrorMessage = "账号过长")]
    [Equal]
    public string Account { get; set; } = string.Empty;
}
/// <summary>
/// 性别
/// </summary>
public enum SexEnum : byte
{
    /// <summary>
    /// 女性
    /// </summary>
    [Description("女性")]
    Woman = 0,
    /// <summary>
    /// 男性
    /// </summary>
    [Description("男性")]
    Man = 1
}