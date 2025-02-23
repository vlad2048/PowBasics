rem nuget setapikey xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx -Source https://api.nuget.org/v3/index.json
set ver=0.3.1
set slnFolder=D:\dev\nuget\PowBasics
set nugetFolder=C:\Users\admin\.nuget\packages
set nugetUrl=https://api.nuget.org/v3/index.json


set prjName=PowBasics
rem =================
set nupkgFile=%slnFolder%\Libs\%prjName%\bin\Release\%prjName%.%ver%.nupkg
rmdir /s /q %nugetFolder%\%prjName%\%ver%
del /q %nupkgFile%
cd /d %slnFolder%\Libs\%prjName%
dotnet pack -p:version=%ver%
nuget add %nupkgFile% -source %nugetFolder% -expand
nuget push %nupkgFile% -Source %nugetUrl%
del /q %nupkgFile%


set prjName=PowBasics.Geom
rem ======================
set nupkgFile=%slnFolder%\Libs\%prjName%\bin\Release\%prjName%.%ver%.nupkg
rmdir /s /q %nugetFolder%\%prjName%\%ver%
del /q %nupkgFile%
cd /d %slnFolder%\Libs\%prjName%
dotnet pack -p:version=%ver%
nuget add %nupkgFile% -source %nugetFolder% -expand
nuget push %nupkgFile% -Source %nugetUrl%
del /q %nupkgFile%


set prjName=PowDisp
rem ===============
set nupkgFile=%slnFolder%\Libs\%prjName%\bin\Release\%prjName%.%ver%.nupkg
rmdir /s /q %nugetFolder%\%prjName%\%ver%
del /q %nupkgFile%
cd /d %slnFolder%\Libs\%prjName%
dotnet pack -p:version=%ver%
nuget add %nupkgFile% -source %nugetFolder% -expand
nuget push %nupkgFile% -Source %nugetUrl%
del /q %nupkgFile%


set prjName=ReactiveVars
rem ====================
set nupkgFile=%slnFolder%\Libs\%prjName%\bin\Release\%prjName%.%ver%.nupkg
rmdir /s /q %nugetFolder%\%prjName%\%ver%
del /q %nupkgFile%
cd /d %slnFolder%\Libs\%prjName%
dotnet pack -p:version=%ver%
nuget add %nupkgFile% -source %nugetFolder% -expand
nuget push %nupkgFile% -Source %nugetUrl%
del /q %nupkgFile%



cd %slnFolder%
