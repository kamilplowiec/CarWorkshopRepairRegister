## Warsztat samochodowy - rejestr napraw

![Warsztat samochodowy - rejestr napraw](https://kamilplowiec.tk/img/portfolio/csdesktop5.jpg)

Dane logowania: 
- login: a
- hasło: a

## Opis programu

1. Lista osób (pracownicy, klienci) dodawanie, edycja, usuwanie
2. Cennik usług warsztatu dodawanie edycja usuwanie
3. Lista pojazdów dodawanie, edycja, usuwanie
4. Rejestr napraw:
	- data przyjecia
	- data zwrotu
	- czy naprawa sie powiodla
	- lista wykonanych uslug
	- pojazd
	- kto przyjal
	- kto oddal

## Tabele

1. Pracownik
- id
- nazwa
- login
- haslo

2. Osoba
- id
- nazwa
- adres

3. Pojazd
- id
- marka
- model
- rok_prod
- nr_rej
- vin
- wlasciciel_id

4. Usluga
- id
- nazwa
- cena

5. RejestrNapraw
- id
- pojazd_id
- data_przyjecia
- data_odbioru
- czy_wykonane
- przyjmujacy_pracownik_id
- oddajacy_pracownik_id

6. RejestrNaprawUslugi
- id
- rejestrnapraw_id
- usluga_id


## Okna w programie

1. Glowne okno
- Logowanie (tylko pracownicy)
- Lista pojazdow
  - Lista pojazdow w systemie
  - Dodawanie/edycja pojazdow
  - Usuwanie pojazdow na liscie (usuwa uslugi dla pojazdu)
- Lista klientow
  - Lista klientow w systemie
  - Dodawanie/edycja klientow
  - Usuwanie klientow (usuwa rowniez jego pojazdy, a dalej uslugi dla pojazdow)
- Lista uslug
  - Lista i cennik uslug w systemie
  - Dodawanie/edycja uslug
  - Usuwanie uslug (usuwa uslugi wykonane w pojazdach)
- Lista napraw
  - Lista wykonanych napraw
  - Dodawanie/edycja naprawy
  - Usuwanie naprawy
- Lista pracownikow
  - Lista pracownikow w systemie
  - Dodawanie/edycja pracownika
  - Usuwanie pracownika (usuwa wszystkie naprawy wykonane przez niego)
- Zaplanowane naprawy na dziś
