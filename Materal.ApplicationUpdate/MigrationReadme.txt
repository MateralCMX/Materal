/*���Ǩ��*/
Add-migration -context AppUpdateContext xxxxxxxxxx

/*����Ǩ��*/
update-database -context AppUpdateContext


/*�Ƴ�Ǩ��*/
remove-migration -context AppUpdateContext