# News

    Console.WriteLine("Hello");

## Ответы на возможные вопросы

### Почему Web API + SPA (Angular), а не ASP.NET MVC

В asp.net mvc есть razor, который был бы лишний при разработке клиента на angular. По сути, MVC + Angular был бы один в один как Web API + SPA, только в MVC добавился бы ещё один action, возвращающий View("index.cshtml"), где index файл содержал бы основную логику angular (подключение скриптов, стилей и т.д.), а остальные actions были бы как API.

### Какая почта и пароль у админа?
email: admin@gmail.com

password: admin
### Как запустить back?
в root folder:
```
cd .\src\Services\PRAS.Testovoe.Main\
dotnet restore
dotnet run
```
### Как запустить front?
в root folder:
```
cd .\src\Services\PRAS.Testovoe.Main\
cd wwwroot\pras-frontend
npm cache clean --force
npm start
```
### Вопросы по локализации
 Для локализации я использовал package @angular/localize. Данный package позволяет создавать разные локализованные версии приложения. В моем проекте в ahead of time создаются разные версии по путям dist/pras-frontend/ru и dist/pras-frontend/en-US. @angular/localize не поддерживает тестирование нескольких локализаций одновременно, поэтому по умолчанию я сконфигурировал запуск ru локализации. В production можно было бы в каком-нибудь apache, nginx настроить маршрутизацию по locales (/ru /en-US) и получать в этих папках index.html. Нужную папку и файл можно выбрать по Accept-Language хедеру, например. Сделать запуск прокси можно было бы и для development, но делать я этого не стал, т.к. заняло бы много времени. 