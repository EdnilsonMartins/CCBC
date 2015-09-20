@ECHO OFF
cls
Echo Rotina de Backup: Projeto CCBC + Banco de dados...

for /f "delims=" %%a in ('wmic OS Get localdatetime  ^| find "."') do set dt=%%a
set datestamp=%dt:~0,8%
set timestamp=%dt:~8,6%
set YYYY=%dt:~0,4%
set MM=%dt:~4,2%
set DD=%dt:~6,2%
set HH=%dt:~8,2%
set Min=%dt:~10,2%
set Sec=%dt:~12,2%

set name="C:\BACKUP FULL - CCBC SQL - %YYYY%%MM%%DD%_%HH%%Min%%Sec%.zip"

sqlcmd -Slocalhost\sqlexpress -Q "BACKUP DATABASE [dbCCBC] TO DISK = 'C:\Projetos\CCBC\Banco de Dados\dbCCBC-Full.bak' WITH INIT"

winrar a -r -ep1 %name% c:\Projetos\CCBC\*.*

copy %name% %1