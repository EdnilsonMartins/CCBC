@ECHO OFF
cls
Echo Publicando Arquivos: Site CCBC...
rem del c:\Site\PortalCCBC\Admin\Web.config /Q
rem del c:\Site\PortalCCBC\Admin\package.config /Q

rem SET dirname="%date:~6,4%%date:~3,2%%date:~0,2%_%date:~8,2%%date:~10,2%%date:~12,2%"
rem mkdir %dirname%


for /f "delims=" %%a in ('wmic OS Get localdatetime  ^| find "."') do set dt=%%a
set datestamp=%dt:~0,8%
set timestamp=%dt:~8,6%
set YYYY=%dt:~0,4%
set MM=%dt:~4,2%
set DD=%dt:~6,2%
set HH=%dt:~8,2%
set Min=%dt:~10,2%
set Sec=%dt:~12,2%

set name=Publish-CCBC-%3-%YYYY%%MM%%DD%_%HH%%Min%%Sec%
set stamp=C:\Site\PortalCCBC\Admin\%name%
rem mkdir %stamp%
rem echo stamp: "%stamp%"
rem xcopy /A /M /S /Y /Q "c:\Site\PortalCCBC\Admin\*.*" %stamp%
rem rar a -r %name%.zip %stamp%\*.*

@echo "Parâmetros: pub <YYYYMMDD> <HHMMSS> [Admin]"

winrar a -r -ta%1%2 -ep1 -apCCBC_%3 C:\Publish\%name%.zip c:\Site\PortalCCBC%3\*.*

@ECHO Pronto!
