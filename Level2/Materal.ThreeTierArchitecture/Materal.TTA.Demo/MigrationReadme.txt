/*添加迁移*/
Add-migration -context LogDbContext xxxxxxxxxx
Add-migration -context AuthorityDbContext xxxxxxxxxx

/*更新迁移*/
update-database -context LogDbContext
update-database -context AuthorityDbContext


/*移除迁移*/
remove-migration -context LogDbContext
remove-migration -context AuthorityDbContext