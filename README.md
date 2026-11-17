# Лабораторна робота №2 — Варіант 16 «Кадри науковців (Звання)»

## Як запустити
1. У терміналі: `dotnet new maui -n ScientistsLab`
2. Розпакувати архів у проєкт із заміною: `unzip -o ~/Downloads/ScientistsLab_files.zip -d ScientistsLab`
3. `cd ScientistsLab` і запуск на Mac: `dotnet build -t:Run -f net10.0-maccatalyst`
   (для .NET 10). На Windows: `-f net10.0-windows10.0.19041.0`.
4. У програмі: «Відкрити XML-файл» → `Data/scientists.xml`,
   для HTML: «Трансформувати в HTML» → `Data/scientists.xsl`.

## Структура
| Файл | Що робить |
|---|---|
| `Data/scientists.xml` | вхідний файл (у різних `<Scientist>` різна кількість атрибутів, 2 рівні) |
| `Data/scientists.xsl` | XSL-шаблон для перетворення в HTML |
| `Models/Scientist.cs` | клас «науковець» |
| `Models/SearchFilter.cs` | параметри пошуку, які вибрав користувач |
| `Strategies/ISearchStrategy.cs` | інтерфейс стратегії |
| `Strategies/SaxSearchStrategy.cs` | пошук через SAX (`XmlReader`) |
| `Strategies/DomSearchStrategy.cs` | пошук через DOM (`XmlDocument` + XPath, що будується динамічно) |
| `Strategies/LinqSearchStrategy.cs` | пошук через LINQ to XML (`XDocument`) |
| `Strategies/Searcher.cs` | контекст стратегії |
| `Services/AttributeLoader.cs` | «підгрузка даних» — значення атрибутів для списків |
| `Services/HtmlTransformer.cs` | XML → HTML через XSL |

## Шаблон «Стратегія»
- `ISearchStrategy` — спільний інтерфейс з методом `Search`.
- `SaxSearchStrategy`, `DomSearchStrategy`, `LinqSearchStrategy` — три конкретні стратегії.
- `Searcher` — контекст: йому передають потрібну стратегію (`SetStrategy`), і він викликає її пошук.
  Сама сторінка не знає, як саме шукається інформація.

## Чому немає «магічної кнопки»
Обробники кнопок у `MainPage.xaml.cs` тільки збирають дані з форми і викликають інші класи.
Уся робота з XML винесена в окремі класи (`Strategies`, `Services`).
