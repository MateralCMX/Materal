create view UserOwnedActionAuthority
as
select 
	[dbo].[ActionAuthority].[ID],
	[dbo].[ActionAuthority].[Code],
	[dbo].[ActionAuthority].[Name],
	[dbo].[ActionAuthority].[ActionGroupCode],
	[dbo].[UserRole].[UserID]
from 
	[dbo].[ActionAuthority]
	inner join [dbo].[RoleActionAuthority] on [dbo].[ActionAuthority].[ID] = [dbo].[RoleActionAuthority].[ActionAuthorityID]
	inner join [dbo].[UserRole] on [dbo].[RoleActionAuthority].[RoleID] = [dbo].[UserRole].[RoleID]
	inner join [dbo].[Role] on [dbo].[UserRole].[RoleID] = [dbo].[Role].[ID]
	inner join [dbo].[User] on [dbo].[UserRole].[UserID] = [dbo].[User].[ID]
where [dbo].[Role].[IsDelete] = 0 and [dbo].[User].[IsDelete] = 0
go
drop view UserOwnedActionAuthority
go
create view UserOwnedAPIAuthority
as
select 
	[dbo].[APIAuthority].[ID],
	[dbo].[APIAuthority].[Code],
	[dbo].[APIAuthority].[ParentID],
	[dbo].[UserRole].[UserID]
from 
	[dbo].[APIAuthority]
	inner join [dbo].[RoleAPIAuthority] on [dbo].[APIAuthority].[ID] = [dbo].[RoleAPIAuthority].[APIAuthorityID]
	inner join [dbo].[UserRole] on [dbo].[RoleAPIAuthority].[RoleID] = [dbo].[UserRole].[RoleID]
	inner join [dbo].[Role] on [dbo].[UserRole].[RoleID] = [dbo].[Role].[ID]
	inner join [dbo].[User] on [dbo].[UserRole].[UserID] = [dbo].[User].[ID]
where [dbo].[Role].[IsDelete] = 0 and [dbo].[User].[IsDelete] = 0
go
drop view UserOwnedAPIAuthority
go
create view UserOwnedWebMenuAuthority
as
select 
	[dbo].[WebMenuAuthority].[ID],
	[dbo].[WebMenuAuthority].[Code],
	[dbo].[WebMenuAuthority].[Name],
	[dbo].[WebMenuAuthority].[Style],
	[dbo].[WebMenuAuthority].[Index],
	[dbo].[WebMenuAuthority].[ParentID],
	[dbo].[UserRole].[UserID]
from 
	[dbo].[WebMenuAuthority]
	inner join [dbo].[RoleWebMenuAuthority] on [dbo].[WebMenuAuthority].[ID] = [dbo].[RoleWebMenuAuthority].[WebMenuAuthorityID]
	inner join [dbo].[UserRole] on [dbo].[RoleWebMenuAuthority].[RoleID] = [dbo].[UserRole].[RoleID]
	inner join [dbo].[Role] on [dbo].[UserRole].[RoleID] = [dbo].[Role].[ID]
	inner join [dbo].[User] on [dbo].[UserRole].[UserID] = [dbo].[User].[ID]
where [dbo].[Role].[IsDelete] = 0 and [dbo].[User].[IsDelete] = 0
go
drop view UserOwnedWebMenuAuthority
go