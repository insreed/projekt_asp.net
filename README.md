Instrukcja uruchomienia projektu:
1. Zainstaluj git for windows oraz Visual Studio
2. używając git-bash wpisz polecenie git clone https://github.com/insreed/projekt_asp.net.git
3. Jeśli w folderze Data/Migrations jest jakikolwiek plik - usuń go.
4. W Visual studio po wczytaniu projektu kliknij na narzędzia -> Menadżer pakietów NuGet -> Konsola menadżera pakietów
5. wklej kolejno polecenia:
							Add-Migration 001 -OutputDir "Data/Migrations"
							Update-Database
6. Uruchom projekt.