# Hospital.API

Проект работает с MS SQL Server 2014
Для подключения другой баз данных укажите другую строку подключения в файле appsettings.json в ключе "DefaultConnection"
Для наполнения базы тестовыми данными, перед выполнением команды dotnet run расскоментируйте строку //seeder.SeedData(); в файле Startup.cs 
Для входа в роли регистратора используйте login: admin и password: admin
Для входа в роли пациента нужно пройти регистацию на главной странице
Для входа в роли специалиста нужно пройти регистацию под регистатором
