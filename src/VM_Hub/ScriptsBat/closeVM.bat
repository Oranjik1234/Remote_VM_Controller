@echo off
setlocal

chcp 65001 >nul

set VM_NAME=%1
if "%VM_NAME%"=="" set VM_NAME=DefaultVM

VBoxManage showvminfo "%VM_NAME%" | findstr /i /c:"running (since" >nul
if %errorlevel%==0 (
    echo [INFO] Попытка мягко выключить виртуальную машину "%VM_NAME%"...
    VBoxManage controlvm "%VM_NAME%" acpipowerbutton
    echo [INFO] Команда ACPI отправлена. Ждите завершения работы ОС внутри ВМ.
) else (
    echo [INFO] Машина "%VM_NAME%" уже выключена или не существует.
)

endlocal