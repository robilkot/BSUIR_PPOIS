# Лабораторная работа 2

## Описание приложения

Программа Lazy Students предназначена для управления данными о студентах и их пропусках с помощью графического интерфейса и базы данных MS SQL Server либо XML файлов.
Реализован функционал поиска по имени, по номеру группы, по количеству пропусков заданных видов. Аналогичный функционал реализован для удаления записей.

## Демонстрация работы

### Главное окно приложения

Главное окно включает в себя основную область, состоящую из двух таблиц для просмотра студентов и их отсутствий, а также панель инструментов со всеми командами.

![image](https://github.com/robilkot/BSUIR_PPOIS/assets/82116328/eeedd54c-7a74-4baf-b248-e7d3b06233a0)

Реализована пагинация (навигация по таблицам осуществляется постранично). Возможно выбрать, сколько записей отображается на странице, а также отображается число доступных для просмотра страниц.

![image](https://github.com/robilkot/BSUIR_PPOIS/assets/82116328/d0ffb930-adc1-4287-b930-be2fcad4e61c)

Галочкой Treeview можно включить отображение записей в виде древовидной структуры.

![image](https://github.com/robilkot/BSUIR_PPOIS/assets/82116328/ec65eab1-9fa2-43cb-8573-aa21d9fb60c6)

При выборе в таблице студентов конкретной записи таблица отсутствий отображает пропуски, принадлежащие выбранному студенту.

### Источники данных

Для работы с приложением необходимо открыть источник данных, где будут храниться записи о студентах и отсутствиях.

![image](https://github.com/robilkot/BSUIR_PPOIS/assets/82116328/a8c4287b-7e91-4d9e-b400-2472eb88ccea)

Хранение возможно в:
- БД MS SQL Server
- XML файлах.

Для открытие подключения к базе данных необходимо нажать кнопку Open MS SQL.
Будет предложено задать строку подключения. Будет осуществлено подключение к указанной БД. В случае таймаута будет выведено сообщения об ошибке, активный источник данных установлен не будет.

Для работы с XML файлами необходимо либо создать новый с помощью кнопки Create file, либо открыть существующий с помощью Open file. При создании нового файла он сразу будет открыт и станет активным источником данных.

Сохранение данных происходит по мере внесения изменений, сохранять данные вручную не нужно.

### Окно поиска

При наличии активного источника данных возможно осуществить фильтрацию отображаемых в главном окне записей. Для этого необходимо нажать кнопку Search by filter.

![image](https://github.com/robilkot/BSUIR_PPOIS/assets/82116328/9d400cfe-bc1f-4fcc-a508-3f8ce2323a65)

В диалоговом окне будет предложено включить определенные критерии поиска, а также указать ограничения.

Возможен поиск по:
- Имени студента
- Группе студента
- Верхним и нижним пределам каждого вида отсутствия

После применения фильтра поиска будет осуществлена фильтрация записей в главном окне. Для сброса критериев поиска необходимо выключить все галочки в окне поиска и применить фильтр.

### Окно удаления

При наличии активного источника данных возможно осуществить удаление записей. Для этого необходимо нажать кнопку Delete by filter.

![image](https://github.com/robilkot/BSUIR_PPOIS/assets/82116328/f6d660f3-42a2-464d-8f57-ab14158d152b)

![image](https://github.com/robilkot/BSUIR_PPOIS/assets/82116328/bfa7769f-c6c8-4c7d-b21b-799693fa3afb)

Логика работы окна удаления аналогична окну поиска. Критерии удаления совпадают с критериями поиска. Отличие заключается в изначальном состоянии галочек критериев удаления. В окне удаления их нормальное состояние - включенное, чтобы сделать условия удаления более строгими и предотвратить ошибочное удаление записей.

### Добавление студента

![image](https://github.com/robilkot/BSUIR_PPOIS/assets/82116328/49405a25-0924-450b-8676-6f2cd3f2476c)

Для добавления студента необходимо открыть какой-либо источник данных, после чего нажать кнопку Add student. Будет предложено ввести имя студента и выбрать группу, к которой он принадлежит. Доступные группы считываются из активного источника данных.

### Добавление отсутствия

![image](https://github.com/robilkot/BSUIR_PPOIS/assets/82116328/5139831c-6012-4048-a94e-dc3a39fa9293)

Для добавления отсутствия необходимо выбрать запись студента в соответствующей таблице, после чего нажать кнопку Add absence. Будет предложено выбрать причину отсутствия. Доступные причины считываются из активного источника данных.

## Архитектура приложения

Приложение разработано с использованием паттерна MVC с пассивной моделью.

Использована чистая архитектура:
- Уровень domain: модели данной предметной области.
- Уровень use cases: абстракции для работы с данными 
- Уровень infrastructure: реализации абстракций уровня use cases.
- Уровень presentation: элементы графического интерфейса, которые также зависят от use cases.
