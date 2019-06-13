--移除原始数据
delete [dbo].[UserRole]
delete [dbo].[RoleActionAuthority]
delete [dbo].[RoleAPIAuthority]
delete [dbo].[RoleWebMenuAuthority]
delete [dbo].[APIAuthority]
delete [dbo].[ActionAuthority]
delete [dbo].[WebMenuAuthority]
delete [dbo].[Role]
delete [dbo].[User]
--API权限
----基础开始
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'BaseAPI', N'基础API', '', NULL);
declare @BaseAPIID uniqueidentifier;
select @BaseAPIID = ID from [dbo].[APIAuthority] where [dbo].[APIAuthority].[Code] = 'BaseAPI';
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'Login', N'登录', '', @BaseAPIID);
declare @LoginID uniqueidentifier;
select @LoginID = ID from [dbo].[APIAuthority] where [dbo].[APIAuthority].[Code] = 'Login';
--基础结束
----用户
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'UserOperation', N'用户操作', '', @BaseAPIID);
declare @UserOperationID uniqueidentifier;
select @UserOperationID = ID from [dbo].[APIAuthority] where [dbo].[APIAuthority].[Code] = 'UserOperation';
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'AddUser', N'添加用户', '', @UserOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'EditUser', N'修改用户', '', @UserOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'DeleteUser', N'删除用户', '', @UserOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'QueryUser', N'查询用户', '', @UserOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'ResetPassword', N'重置密码', '', @UserOperationID);
----角色
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'RoleOperation', N'角色操作', '', @BaseAPIID);
declare @RoleOperationID uniqueidentifier;
select @RoleOperationID = ID from [dbo].[APIAuthority] where [dbo].[APIAuthority].[Code] = 'RoleOperation';
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'AddRole', N'添加角色', '', @RoleOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'EditRole', N'修改角色', '', @RoleOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'DeleteRole', N'删除角色', '', @RoleOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'QueryRole', N'查询角色', '', @RoleOperationID);
----功能权限
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'ActionAuthorityOperation', N'功能权限操作', '', @BaseAPIID);
declare @ActionAuthorityOperationID uniqueidentifier;
select @ActionAuthorityOperationID = ID from [dbo].[APIAuthority] where [dbo].[APIAuthority].[Code] = 'ActionAuthorityOperation';
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'AddActionAuthority', N'添加功能权限', '', @ActionAuthorityOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'EditActionAuthority', N'修改功能权限', '', @ActionAuthorityOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'DeleteActionAuthority', N'删除功能权限', '', @ActionAuthorityOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'QueryActionAuthority', N'查询功能权限', '', @ActionAuthorityOperationID);
----API权限
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'APIAuthorityOperation', N'API权限操作', '', @BaseAPIID);
declare @APIAuthorityOperationID uniqueidentifier;
select @APIAuthorityOperationID = ID from [dbo].[APIAuthority] where [dbo].[APIAuthority].[Code] = 'APIAuthorityOperation';
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'AddAPIAuthority', N'添加API权限', '', @APIAuthorityOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'EditAPIAuthority', N'修改API权限', '', @APIAuthorityOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'DeleteAPIAuthority', N'删除API权限', '', @APIAuthorityOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'QueryAPIAuthority', N'查询API权限', '', @APIAuthorityOperationID);
----网页菜单权限
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'WebMenuAuthorityOperation', N'网页菜单权限操作', '', @BaseAPIID);
declare @WebMenuAuthorityOperationID uniqueidentifier;
select @WebMenuAuthorityOperationID = ID from [dbo].[APIAuthority] where [dbo].[APIAuthority].[Code] = 'WebMenuAuthorityOperation';
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'AddWebMenuAuthority', N'添加网页菜单权限', '', @WebMenuAuthorityOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'EditWebMenuAuthority', N'修改网页菜单权限', '', @WebMenuAuthorityOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'DeleteWebMenuAuthority', N'删除网页菜单权限', '', @WebMenuAuthorityOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'QueryWebMenuAuthority', N'查询网页菜单权限', '', @WebMenuAuthorityOperationID);
----应用
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'ApplicationOperation', N'应用操作', '', @BaseAPIID);
declare @ApplicationOperationID uniqueidentifier;
select @ApplicationOperationID = ID from [dbo].[APIAuthority] where [dbo].[APIAuthority].[Code] = 'ApplicationOperation';
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'AddApplication', N'添加应用', '', @ApplicationOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'EditApplication', N'修改应用', '', @ApplicationOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'DeleteApplication', N'删除应用', '', @ApplicationOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'QueryApplication', N'查询应用', '', @ApplicationOperationID);
declare @QueryApplicationID uniqueidentifier;
select @QueryApplicationID = ID from [dbo].[APIAuthority] where [dbo].[APIAuthority].[Code] = 'QueryApplication';
----微信域名
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'WeChatDomainOperation', N'微信域名操作', '', @BaseAPIID);
declare @WeChatDomainOperationID uniqueidentifier;
select @WeChatDomainOperationID = ID from [dbo].[APIAuthority] where [dbo].[APIAuthority].[Code] = 'WeChatDomainOperation';
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'AddWeChatDomain', N'添加微信域名', '', @WeChatDomainOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'EditWeChatDomain', N'修改微信域名', '', @WeChatDomainOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'DeleteWeChatDomain', N'删除微信域名', '', @WeChatDomainOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'QueryWeChatDomain', N'查询微信域名', '', @WeChatDomainOperationID);
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'QueryWeChatDomain', N'查询微信域名', '', @WeChatDomainOperationID);
----微信小程序服务
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'WeChatMiniProgramServer', N'微信小程序服务', '', @BaseAPIID);
declare @WeChatMiniProgramServerID uniqueidentifier;
select @WeChatMiniProgramServerID = ID from [dbo].[APIAuthority] where [dbo].[APIAuthority].[Code] = 'WeChatMiniProgramServer';
insert into [dbo].[APIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'GetWeChatMiniProgramOpenIDCode', N'获取小程序OpenID', '', @WeChatMiniProgramServerID);
declare @GetWeChatMiniProgramOpenIDCodeID uniqueidentifier;
select @GetWeChatMiniProgramOpenIDCodeID = ID from [dbo].[APIAuthority] where [dbo].[APIAuthority].[Code] = 'GetWeChatMiniProgramOpenIDCode';
--网页菜单权限
declare @Index int;
set @Index = 1;
----权限管理
insert into [dbo].[WebMenuAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'AuthorityOperation', N'权限管理', '', @Index, '', NULL);
set @Index = @Index + 1;
declare @AuthorityOperationID uniqueidentifier;
select @AuthorityOperationID = ID from [dbo].[WebMenuAuthority] where [dbo].[WebMenuAuthority].[Code] = 'AuthorityOperation';
insert into [dbo].[WebMenuAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'APIAuthorityOperation', N'API权限管理', '', @Index, '', @AuthorityOperationID);
set @Index = @Index + 1;
insert into [dbo].[WebMenuAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'WebMenuAuthorityOperation', N'网页菜单权限管理', '', @Index, '', @AuthorityOperationID);
set @Index = @Index + 1;
insert into [dbo].[WebMenuAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'ActionAuthorityOperation', N'功能权限管理', '', @Index, '', @AuthorityOperationID);
set @Index = @Index + 1;
insert into [dbo].[WebMenuAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'RoleOperation', N'角色管理', '', @Index, '', @AuthorityOperationID);
set @Index = @Index + 1;
----用户管理
insert into [dbo].[WebMenuAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'UserOperation', N'用户管理', '', @Index, '', NULL);
set @Index = @Index + 1;
declare @WebUserOperationID uniqueidentifier;
select @WebUserOperationID = ID from [dbo].[WebMenuAuthority] where [dbo].[WebMenuAuthority].[Code] = 'UserOperation';
----微信域名管理
insert into [dbo].[WebMenuAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'WeChatDomainOperation', N'微信域名管理', '', @Index, '', NULL);
set @Index = @Index + 1;
declare @WebWeChatDomainOperationID uniqueidentifier;
select @WebWeChatDomainOperationID = ID from [dbo].[WebMenuAuthority] where [dbo].[WebMenuAuthority].[Code] = 'WeChatDomainOperation';
----应用管理
insert into [dbo].[WebMenuAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'ApplicationOperation', N'应用管理', '', @Index, '', NULL);
set @Index = @Index + 1;
declare @ApplicationOperationID uniqueidentifier;
select @ApplicationOperationID = ID from [dbo].[WebMenuAuthority] where [dbo].[WebMenuAuthority].[Code] = 'ApplicationOperation';
----我的应用管理
insert into [dbo].[WebMenuAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'MyApplicationOperation', N'应用管理', '', @Index, '', NULL);
set @Index = @Index + 1;
declare @MyApplicationOperationID uniqueidentifier;
select @MyApplicationOperationID = ID from [dbo].[WebMenuAuthority] where [dbo].[WebMenuAuthority].[Code] = 'MyApplicationOperation';
--功能权限
--角色
insert into [dbo].[Role]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', N'超级管理员', 0, NULL, 'SuperAdministrator');
declare @SuperAdministratorID uniqueidentifier;
select @SuperAdministratorID = ID from [dbo].[Role] where [dbo].[Role].[Code] = 'SuperAdministrator';
insert into [dbo].[Role]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', N'平台用户', 0, NULL, 'PlatformUser');
declare @PlatformUserID uniqueidentifier;
select @PlatformUserID = ID from [dbo].[Role] where [dbo].[Role].[Code] = 'PlatformUser';
--角色网页菜单权限
insert into [dbo].[RoleWebMenuAuthority]
select NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', @SuperAdministratorID, [dbo].[WebMenuAuthority].[ID]
from [dbo].[WebMenuAuthority]
insert into [dbo].[RoleWebMenuAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', @PlatformUserID, @MyApplicationOperationID);
--角色功能权限
--角色API权限
insert into [dbo].[RoleAPIAuthority]
select NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', @SuperAdministratorID, [dbo].[APIAuthority].[ID]
from [dbo].[APIAuthority]
insert into [dbo].[RoleAPIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', @PlatformUserID, @BaseAPIID);
insert into [dbo].[RoleAPIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', @PlatformUserID, @LoginID);
insert into [dbo].[RoleAPIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', @PlatformUserID, @ApplicationOperationID);
insert into [dbo].[RoleAPIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', @PlatformUserID, @QueryApplicationID);
insert into [dbo].[RoleAPIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', @PlatformUserID, @WeChatMiniProgramServerID);
insert into [dbo].[RoleAPIAuthority]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', @PlatformUserID, @GetWeChatMiniProgramOpenIDCodeID);
--用户
declare @DefaultPassword varchar(32);
set @DefaultPassword = 'F876A34227DE18F8B8AB176594B64D64';
insert into [dbo].[User]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'Admin', @DefaultPassword, N'超级管理员', 0, '', '', 0);
declare @UserAdminID uniqueidentifier;
select @UserAdminID = ID from [dbo].[User] where [dbo].[User].[Account] = 'Admin';
insert into [dbo].[User]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', 'User00', @DefaultPassword, N'用户00', 0, '', '', 0);
declare @UserUser00ID uniqueidentifier;
select @UserUser00ID = ID from [dbo].[User] where [dbo].[User].[Account] = 'User00';
--用户角色
insert into [dbo].[UserRole]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', @UserAdminID, @SuperAdministratorID);
insert into [dbo].[UserRole]
values (NEWID(), SYSDATETIME(), '0001-01-01 00:00:00.0000', @UserUser00ID, @PlatformUserID);