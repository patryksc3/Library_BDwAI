# System Zarządania biblioteką
Użytkownik może przeglądnąć wirtualną bibliotekę w poszukiwaniu interesującą go książkę. Administrator systemu, kiedy osoba zarejestrowana odnajdzie książkę może jej ją wypożyczyć. Książka trafia do tabeli z wypożyczeniami, w której to zawarta jest informacja kiedy została wypożyczona i do kiedy ma zostać oddana.
Administrator również może dodawać nowe pozycje książek, edytować je, a także usuwać. Może śledzić kto ostatnio wypożyczył daną książkę. Użytkownik może przeglądać swoje wypożyczenia, aby móc dowiedzieć się do kiedy może cieszyć się swoją książką.

## Wymagania systemowe

- **.NET SDK 10.0** lub nowszy
- **SQLite** (wbudowane w aplikację, nie wymaga instalacji)
- **Visual Studio 2022** (wersja 17.12 lub nowsza) lub **Visual Studio Code** z rozszerzeniem C#

## Używane technologie
- **ASP.NET Core 10.0** (Razor Pages / MVC)
- **Entity Framework Core 10.0.1** (ORM)
- **SQLite** (baza danych)
- **Microsoft.Data.Sqlite 9.0.0** (provider SQLite)
- **Bootstrap 5.3** (framework frontendu)
- **jQuery 3.7** (biblioteka javascript)

---

### Funkcje dla zwykłych użytkowników

#### Przeglądanie katalogu książek
Po zalogowaniu kliknij kafelek **"Lista dostępnych książek"** na stronie głównej. Kliknij "Szczegóły", aby zobaczyć pełne informacje o książce

#### Wypożyczanie książki
W katalogu książek znajdź tytuł, który Cię interesuje. Jeśli książka jest dostępna, kliknij przycisk **"Wypożycz"**. Książka zostanie automatycznie przypisana do Twojego konta. Otrzymasz potwierdzenie.

#### Sprawdzanie swoich wypożyczeń
Na stronie głównej kliknij kafelek **"Moje wypożyczenia"**. Zobaczysz listę wszystkich swoich wypożyczeń:  

#### Spóźnione zwroty
Wypożyczenia trwają standardowo **30 dni**. Zwróć książki jak najszybciej, aby uniknąć kar!

### Zmiana ścieżki bazy danych

Edytuj `DatabaseHelper.cs` lub `appsettings.json` (w zależności od konfiguracji)

## Przykładowe dane logowania:

### Administrator
- Email: a@example.com
- Hasło: AdminPass123

### Użytkownik
- Email: john24@example.com
- Hasło: UserPass456

## 👥 Autorzy

- Filip Bujakowski
- Patryk Czekaj
