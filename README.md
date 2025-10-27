# Remote_VM_Controller

A system that allows you to control the creation, launch, configuration, and operation of a virtual machine on the server from a single machine. The main goal of the project is to enable remote connection to another PC to create a workspace and work on it.

VM_Hub: Has a list of scripts for each task. Calls them on request from the client API. .Bat and PowerShell are separated and have their own separate managers due to the difference in approaches. They are also responsible for handling key errors. A separate module is responsible for security, it is represented by the necessary password and salt to it. A file with user secrets in a hash should be generated at the first launch and stored separately from the application repository

 ApiIntefase: A console that sends script launch requests to the hub. Allows you to connect via remote desktop in a running VM. Passes the specified password in the request body each time, if necessary

 A Virtual Box must also be installed on the machine implementing the launch to work with it. And an ISO file for the correct OS launch 

The project is currently under development. All modules individually work almost correctly. Scripts, authorization requests are functioning. The problem is to run it all together. 
During one of the initialization runs of the box, something caused the GPT scan of the hard disk to break. The specific reason is unknown. Until the decision is made, the system assembly may be dangerous for the disk. Just in case, the ISO was deleted and now it needs to be configured separately.

VM_Hub: Имеет в себе список скриптов для каждой задачи. Вызывает их по запросу с клиентского АПИ. .Bat и PowerShell разделены и имеют свои отдельные менеджеры из-за разницы подходов. Они же отвечают за обработку ключевых ошибок
За безопасность отвечает отдельный модуль, она представлена необходимым паролем и солью к нему. Файл с секретами пользователя в хэше должен генерироваться при первом запуске и храниться отдельно от репозитория приложения

ApiIntefase: Консоль что отправляет запросы на запуск скриптов в хаб. Позволит подключиться по удаленному рабочему столу в запущенной ВМ. Передает указанный пароль в теле запроса каждый раз при необходимости

На машине реализующей запуск так же должен быть установлен Виртуальная коробка для работы с ним. И ISO файл для корректного запуска OS

На данный момент проект в разработке. Все модули по отдельности работают почти корректно скрипты запросы авторизация функционируют. Проблема в то чтобы запустить все это вместе. 
При одном из запусков инициализации коробки что то стало причиной поломки GPT развертки жесткого диска. Конкретная причина неизвестна, до момента решения сборка системы может быть опасной для диска. На всякий случай ISO был удален и теперь его надо настраивать отдельно




