/*Ìí¼ÓÇ¨ÒÆ*/
Add-migration -context AppUpdateContext xxxxxxxxxx

/*¸üÐÂÇ¨ÒÆ*/
update-database -context AppUpdateContext


/*ÒÆ³ýÇ¨ÒÆ*/
remove-migration -context AppUpdateContext