@echo off
setlocal

chcp 65001 >nul

set "VM_NAME=DefaultVM"

echo Проверка существования виртуальной машины "%VM_NAME%"...
VBoxManage showvminfo "%VM_NAME%" >nul 2>&1
if errorlevel 1 (
    echo Виртуальная машина "%VM_NAME%" не найдена.
    goto end
)

echo Проверка состояния ВМ...
VBoxManage showvminfo "%VM_NAME%" | findstr /C:"running" >nul
if %errorlevel%==0 (
    echo Виртуальная машина запущена. Завершаем...
    VBoxManage controlvm "%VM_NAME%" acpipowerbutton
    timeout /t 10 >nul
)

echo Удаление виртуальной машины и всех её данных...
VBoxManage unregistervm "%VM_NAME%" --delete

echo Виртуальная машина "%VM_NAME%" успешно удалена.

:end
endlocal