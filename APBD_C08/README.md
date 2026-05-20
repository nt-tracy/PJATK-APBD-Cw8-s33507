Treść zadania znajduje się w folderze TASK

Zad1.
Utwórz bazę danych z użyciem dostarczonego skryptu create.sql. Następnie korzystając z podejścia
DatabaseFirst z frameworka EntityFrameworkCore, przeprowadź operację scaffold, która utworzy
kontekst bazy danych bazujący na tabelach, które istnieją już w bazie.


Zad2.
Przygotuj koncówkę /api/patients odpowiadającą na zapytania HTTP GET. Końcówka powinna
zwrócić dane na temat wszystkich obecnych w bazie pacjentach. Dodatkowo powinna ona
reagować na opcjonalny parametr żądania “search”, który umożliwia filtrowanie kolekcji
wynikowej na podstawie stringa przekazanego w zapytaniu. Filtrowanie kolekcji ma się odbywać
poprzez sprawdzanie wartości firstname oraz lastname, w poszukiwaniu pasujących wartości.
Np. dla rekordów:
1. FirstName=Jan, LastName=Kowalski
2. FirstName=Jakub, LastName=Jankowski
3. FirstName=Joanna, LastName=Doe
   Wysłanie zapytania pod ares /api/patients?search=an powinno zwrócić wszystkie powyższe
   rekordy.



Zad3.
Przygotuj końcówkę /api/patients/{pesel}/bedassignments odpowiadającą na zapytania HTTP
POST. Końcówka powinna umożliwić przypisanie pacjentowi łóżka. W ramach zapytania serwer
powinien znaleźć łóżko o danym typie, w danym oddziale, które we wskazanym okresie czasu nie
jest przez nikogo zajęte. Jeżeli takiego łóżka nie ma, serwer powinien zwrócić kod 404 (Zwracane
komunikaty powinny być czytelne i jednoznaczne. Nieakceptowalne jest zastosowanie jednej
długiej wiadomości, która nie mówi dokładnie, co się stało).
Wymagany format przesyłanych danych znajdziesz w pliku POST.json