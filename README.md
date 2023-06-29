# Aplikacja z ogłoszeniami samochodów używanych.

https://vcarrr.herokuapp.com/cars

## Skład zespołu: 

1. Witold Andreasik 64042 w64042@student.wsiz.edu.pl
2. Rafał Paśko 64882 w64882@student.wsiz.edu.pl

## Wykorzystane technologie: 

• C# 
• ASP.NET Core
• Entity Framework
• MSSQL
• RxJS
• Angular
• TypeScript

## Cele projektu: 

Celem projektu jest stworzenie aplikacji z ogłoszeniami samochodów. Będzie on napisany w 
C# przy użyciu ASP.NET Core i połączony z MSSQL przy użyciu Entity Framework. Frontend 
aplikacji zostanie napisany w Angularze. Serwis będzie umożliwiał logowanie, wystawienie
ogłoszenia sprzedaży samochodu oraz wyświetlenie listy dostępnych ofert. Użytkownik będzie 
miał możliwość eksportu listy ofert oraz importu ogłoszenia.

## Założenia funkcjonalne: 

1. Logowanie
2. Rejestracja
3. Przegląd dostępnych ofert sprzedaży
4. Wyświetlanie danych użytkownika oraz jego ogłoszeń

## Dokumentacja użytkownika

![](https://i.imgur.com/UiBPFy2.png)

Użytkownik jako stronę główną widzi listę dostępnych samochodów wraz z możliwością filtrowania wedle potrzeb. Może z niej przejść do logowania (jeśli nie jest zalogowany), strony na której może wystawić ogłoszenie oraz do listy wystawionych pojazdów.

![](https://i.imgur.com/7YFBEzP.png)

Na tej stronie użytkownik uzupełnia swoje dane oraz informacje o samochodzie po czym ogłoszenie zostaje wystawione na tablicę ogłoszeń.

![](https://i.imgur.com/0jIMHU9.png)

W zakładce "your cars" możemy zobaczyć listę samochodów, które wystawiliśmy na sprzedaż.

![](https://i.imgur.com/TFuveXo.png)

Po wejściu w nasze ogłoszenie, możemy sprawdzić dane, edytować lub usunąć.

![](https://i.imgur.com/VZFsbTA.png)

Podstrona ma 2 zakładki: Info oraz Photos.

![](https://i.imgur.com/Vk3pP52.png)

W bramce logowania możemy zalogować się przez google lub githuba

## Ograniczenia
![](https://i.imgur.com/6KDmHty.png)
Dodawać ogłoszenia mogą tylko zalogowani użytkownicy.

