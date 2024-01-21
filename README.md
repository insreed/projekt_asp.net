Instrukcja uruchomienia projektu:
1. Zainstaluj git for windows oraz Visual Studio
2. Klikając prawym przyciskiem w ekplorerze windows wybierz opcję otwórz w terminalu i wpisz następującą komendę
3. git clone https://github.com/insreed/projekt_asp.net.git
4. Jeśli w folderze Data/Migrations jest jakikolwiek plik - usuń go.
5. W Visual studio po wczytaniu projektu kliknij na narzędzia -> Menadżer pakietów NuGet -> Konsola menadżera pakietów
6. Wklej kolejno polecenia:
 Add-Migration 001 -OutputDir "Data/Migrations"
 
 
 
 Update-Database
7. Uruchom projekt.
8. W celu przetestowania działania aplikacji zarejestruj użytkownika, po rejestracju trzeba kliknąć:
9. Click here to confirm your account
10. Następnie logujemy się na zalogowanego użytkownika.
11. Dodajemy kolejno sesje, typ ćwiczeń i ćwiczenia
12. Można edytować i usuwać dane, po dodaniu danych w ćwiczeniach pojawią się też statystyki
