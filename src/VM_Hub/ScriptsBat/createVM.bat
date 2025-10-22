@echo off
setlocal

chcp 65001 >nul

set VM_NAME=%1
set PORT=%2
if "%VM_NAME%"=="" set VM_NAME=DefaultVM
if "%PORT%"=="" set PORT=3388

netstat -aon | findstr ":%PORT%" >nul
if %errorlevel%==0 (
    echo Порт %PORT% уже занят. Выберите другой порт или завершите процесс, его использующий.
    echo Пример: vm_create.bat MyNewVM 3390
    exit /b
)

echo Создаётся ВМ: %VM_NAME% с портом %PORT%
VBoxManage createvm --name "%VM_NAME%" --register
VBoxManage modifyvm "%VM_NAME%" --memory 2048 --vram 64 --cpus 1 --nic1 nat
VBoxManage modifyvm "%VM_NAME%" --audio none --usb off
VBoxManage modifyvm "%VM_NAME%" --boot1 dvd --boot2 disk --boot3 none

VBoxManage modifyvm "%VM_NAME%" --vrde on
VBoxManage modifyvm "%VM_NAME%" --vrdeport %PORT%

VBoxManage createhd --filename "%VM_NAME%.vdi" --size 20000
VBoxManage storagectl "%VM_NAME%" --name "SATA Controller" --add sata --controller IntelAhci
VBoxManage storageattach "%VM_NAME%" --storagectl "SATA Controller" --port 0 --device 0 --type hdd --medium "%VM_NAME%.vdi"

echo Виртуальная машина "%VM_NAME%" успешно создана с VRDE-портом %PORT%.
echo Для запуска используйте скрипт: vm_start.bat %VM_NAME%

endlocal