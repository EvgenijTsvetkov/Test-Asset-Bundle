## Небольшая "игра", которая подгружает данные с сервера.
---

### Экран загрузки
При запуске приложения сначала показывать экран загрузки.
На нём может быть простая анимация и/или прогресс-бар.
Во время загрузки нужно:
- Загрузить и распарсить JSON файлы: один с настройками (одно поле, например, startingNumber: 5), другой с приветственным сообщением.
- Загрузить Asset Bundle (содержащий один спрайт).
- После завершения загрузки перейти в основной экран.

Asset Bundle содержит один спрайт, который в дальнейшем будет использоваться в качестве фона для кнопки.
Допустимо использование Addressables, но умение работать с Asset Bundles все равно нужно.
Нужно добавить искусственную задержку, чтобы экран загрузки не пролетал мгновенно.

Настройки (JSON)
Файл Settings.json, структура примерно такая:
{ "startingNumber": 5 }
где startingNumber - будет задавать начальное значение счётчика.

Структура файла с приветственным сообщением на ваше усмотрение.

### Основной экран
Имеет приветственное сообщение и две кнопки: "Увеличить счетчик", "Обновить контент".

Фон кнопки "Увеличить счетчик" получается из загруженного Asset Bundle.
Рядом (или на самой кнопке) выводится текст со значением счётчика.
При клике по кнопке счётчик инкрементируется (увеличивается на 1).

Кнопка "Обновить контент" - см. п.4.

Текст приветственного сообщения подгружается из соответствующего JSON-файла.

### Сохранение состояния
Нужно сохранять текущее значение счётчика при выходе из приложения.
При новом запуске начинать с последнего сохранённого значения, если оно существует; иначе — брать число из startingNumber.
Способ сохранения на ваш выбор, кроме PlayerPrefs.

### Кнопка "Обновить контент".
Сделать простую кнопку, которая запрашивает новое содержимое (AssetBundle и обновлённые настройки) при нажатии.
После нажатия — старые объекты/настройки заменяются новыми из подгруженного AssetBundle и JSON.

### Git
Разместить проект в GitHub.
Использовать осмысленные коммиты, чтобы показать логику разработки.
