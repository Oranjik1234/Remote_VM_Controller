@echo off
setlocal

chcp 65001 >nul

set VM_NAME=%1
if "%VM_NAME%"=="" set VM_NAME=DefaultVM

VBoxManage showvminfo "%VM_NAME%" | findstr /i /c:"running (since" >nul
if %errorlevel%==0 (
    echo [INFO] Машина "%VM_NAME%" уже запущена.
    goto :eof
)

echo [INFO] Запуск виртуальной машины "%VM_NAME%"...
VBoxManage startvm "%VM_NAME%" --type headless

endlocal
