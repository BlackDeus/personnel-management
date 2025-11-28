Programmiersprache: C#

Maske erstellen: 
Person anlegen 
- Anrede (Combobox) 
- Name 
- Vorname 
- Geschlecht (Combobox) 
- Geburtsdatum 
  - zusätzlich Berechnung des Alters (Label) Bsp. Anzeige wenn Geburtsdatum = 01.01.1999: "24 Jahre und 2 Monate" 
- Straße 
- HausNr 
- PLZ 
- Ort 
- Bundesland (Dropdown) 
- E-Mail
- Telefonnummer
- Button "Alle Felder leeren", "Person speichern"
 
Grundlegende Validation: 
- Sind alle Felder ausgefüllt 
  - Wenn nicht werden die nicht ausgefüllten Gelb markiert (Backcolor) 
 
Erweiterte Validation: 
- E-Mail
  - Z.B. Prüfung auf @ und .
- Telefonnummer !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
  - Nur Zahleneingabe möglich (Regex) + Max. Länge von 10) 
- Altersberechnung 
  - Label wird Rot wenn Alter <= 18 & >= 100 
  - Grün wenn Alter >= 18 & < 100

Datenbank:
- Entweder in MSSQL oder ACCDB
- Tabelle erstellen via DataGridView
- Select, insert into, update Statements werden benötigt
- Personen werden ausgelesen und in der Tabelle angezeigt
  - Bei einem Doppelklick auf ein Eintrag werden die Personendatenfelder automatisch befüllt
- Speichern Button unterscheidet ob der Datensatz neu angelegt werden muss oder aktualisiert wird (Prüfung auf ID - Primärschlüssel, Autoinkrement)


CREATE TABLE Personen
(
    Id INT IDENTITY PRIMARY KEY,
    Vorname NVARCHAR(50),
    Nachname NVARCHAR(50),
    Email NVARCHAR(100),
    Geburtsdatum DATE,
    PLZ NVARCHAR(5),
    Ort NVARCHAR(50),
    Straße NVARCHAR(100),
    Hausnummer NVARCHAR(10),
    Telefonnummer NVARCHAR(20)
)
