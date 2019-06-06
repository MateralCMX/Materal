/*添加迁移*/
Add-migration -context LogDbContext xxxxxxxxxx
Add-migration -context AuthorityDbContext xxxxxxxxxx
Add-migration -context WeChatServiceDbContext xxxxxxxxxx

/*更新迁移*/
update-database -context LogDbContext
update-database -context AuthorityDbContext
update-database -context WeChatServiceDbContext


/*移除迁移*/
remove-migration -context LogDbContext
remove-migration -context AuthorityDbContext
remove-migration -context WeChatServiceDbContext