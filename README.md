# OneScriptFormsDesigner
Дизайнер форм для сценарного языка OneScript.

В дополнение к [графическому интерфейсу](https://github.com/ahyahy/OneScriptForms) для сценарного языка OneScript написан представленный здесь дизайнер форм.

### Подробнее можно узнать на этом сайте

> <https://ahyahy.github.io/OneScriptFormsDesigner/index.html>

Он позволяет создавать формы, размещать на форме элементы управления, устанавливать их свойства, обработчики событий в виде не заполненных кодом процедур. Спроектированную форму можно сразу запустить на исполнение и увидеть результаты, или сохранить форму в скрипт с синтакисом OneScript, или сгенерировать и просмотреть код непосредственно в дизайнере.

Как основная, ставилась цель полной русификации всех свойств компонентов и она достигнута. Исходными данными послужила справка из моего репозитория для OneScriptForms. Редакторы коллекций дизайнера не поддающиеся модификации были написаны с нуля.

![Дизайнер](https://github.com/ahyahy/OneScriptFormsDesigner/blob/main/docs/OneScriptFormsDesigner.png)

Огромную помощь оказал пример работы с дизайнером [picoFormDesigner](https://www.codeproject.com/Articles/60175/The-DesignSurface-Extended-Class-is-Back-Together) (coded by Paolo Foti) под лицензией ([CPOL](https://www.codeproject.com/info/cpol10.aspx)).

Предполагается, что OneScript на компьютере уже установлен. Дизайнер реализован в виде библиотеки и подключается как внешняя компонента.
Скачайте из [каталога docs](https://github.com/ahyahy/OneScriptFormsDesigner/tree/main/docs) архив с именем формата OneScriptFormsDesignerх_х_х_х.zip. Скачайте последнюю версию. Поместите находящуюся в архиве библиотеку, в какой либо каталог на диске. Возможно будет необходимо правой кнопкой мыши вызвать свойства распакованного файла OneScriptFormsDesigner.dll и разблокировать его. 
Запустить можно так:

```bsl
     ПодключитьВнешнююКомпоненту("ВашКаталогНаДиске\OneScriptFormsDesigner.dll");
     ДФ = Новый ДизайнерФормДляОдноСкрипта();
     ДФ.Дизайнер();
```

Формы можно сохранить в файл **osd** или в файл сценария **os** и в последующем восстановить для возобновления работы.

**ВАЖНО:** Файлы для сборки библиотеки из исходников найдете в релизе, в архиве Solution.zip. Недостающие для решения (solution) библиотеки односкрипта находятся рядом, в архиве Release.zip. Репозиторий лучше не клонировать. Он не полный для получения всех необходимых для сборки файлов. Полный набор именно в архиве Solution.zip положенном в релиз, плюс архив Release.zip. Это связано с тем, что при создании релиза гитхабом искажаются некоторые данные. Вышел из положения пока таким способом.
