Singularity 1.0
ASP.NET Application Development Framework
	

How to Install
--------------
Please follow instructions below to install:

   1. Click Download button above to obtain Singularity Community Edition ZIP.
   2. Unzip and restore SQL Server 2005 database from backup available under folder \App.Web\App_Data\Singularity.bak
   3. Create a SQL Server 2005 user.
          * User name = sig and
          * Password = sig.
   4. Create an IIS virtual directory named sig at folder App.Web. Make sure ASP.NET 2.0 is selected in IIS virtual directory settings.
   5. Open browser, enter URL http://localhost/sig and hit ENTER to view Singularity Home Page.
   6. Click Sign In link at the top of the home page. To Sign In as Admin, enter Email = admin@mysitename.com and Password = admin.

Troubleshooting
---------------

   1. If you get SQL Server priviliages error for ASPNET user, please run following SQL script on Singularity database:
          * exec sp_grantlogin 'MachineName\ASPNET' 
          * use database_name 
          * exec sp_grantdbaccess 'MachineName\ASPNET' 
          * exec sp_addrolemember 'db_owner', 'MachineName\ASPNET'

For More Information
--------------------
Email: info@rafeysoft.com
Url: http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=27&lid=4&cid=23&iid=2124