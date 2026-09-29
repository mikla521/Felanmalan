# Överlämningsnot

> Två av demons tio minuter är överlämning. Skriv noten först, prata sedan
> utifrån den. Tänk er ett team som tar över koden på måndag utan att ha
> träffat er.

## Var koden finns

Källkoden finns i projektets GitHub-repository.

Solution-struktur: 
Innehåller två projekt, ett för frontend (felanmalan.client) och ett för backend (Felanmalan.Server)

## Hur man kör den

1. Klona GitHub-repositoryt.
2. Öppna Felanmalan.slnx i Visual Studio.
3. Kontrollera att rätt Startup Project är konfigurerat.
4. Kontrollera att nödvändiga User Secrets finns konfigurerade lokalt.
5. Kontrollera att SQL Server LocalDB är tillgänglig och att rätt databas finns.
6. Tryck F5.
7. Visual Studio startar både backend och frontend samtidigt.
8. Applikationen öppnas i webbläsaren.

#### Förutsättningar:
Projektet använder User Secrets för lokala konfigurationsvärden som inte ska ligga i Git-repositoryt och har formatet:
```{
  "ConnectionStrings": {
    "Felanmalan": "Data Source=(your_data_source;Initial Catalog=Felanmalan;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Command Timeout=0"
  }
}
```
#### User Secrets
**Viktigt:** User Secrets ska inte läggas in i repositoryt.


## Vad som är känt men inte åtgärdat

| Vad | Hur allvarligt | Var i koden |
|---|---|---|
|  |  |  |

## Vad ett mottagande team bör ta först
Dessa flyttades från sprint 2 till sprint 3 efter kravförändring.
1. US08 - Filtrera efter kategori
2. US09 - Filtrera efter status
3. US 10 - Bekräftelse efter inskick

## Vad vi skulle göra om vi fick en vecka till
