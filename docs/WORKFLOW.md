# Arbetsgång – User Story

Den här arbetsgången används av alla teammedlemmar när en User Story tas från backloggen och arbetas igenom till färdig funktionalitet.

Alla teammedlemmar följer samma arbetsgång.

## Statusflöde

```text
Backlog → Ready → In Progress → Review → Done
```

### Statusarnas betydelse

| Status          | Betydelse                                                                                    |
| --------------- | -------------------------------------------------------------------------------------------- |
| **Backlog**     | User Storyn finns i produktbackloggen men är inte vald för aktuell sprint.                   |
| **Ready**       | User Storyn är vald för aktuell sprint och är tillräckligt tydlig för att börja arbetas med. |
| **In Progress** | En teammedlem arbetar aktivt med User Storyn.                                                |
| **Review**      | Implementation är klar och en Pull Request väntar på eller genomgår code review.             |
| **Done**        | Pull Requesten är godkänd och mergad och User Storyn uppfyller Definition of Done.           |

---

# 1. Välj en User Story

Välj en User Story som ligger i **Ready**.

Innan arbetet påbörjas:

* Läs igenom User Storyn.
* Kontrollera att syftet och förväntat resultat är tydligt.
* Läs igenom samtliga acceptanskriterier.
* Kontrollera att User Storyn är tillräckligt tydlig för att börja arbeta med.
* Om något är oklart – fråga teamet innan arbetet påbörjas.

> Börja inte implementera en User Story som du inte förstår.

---

# 2. Ta ansvar för User Storyn

När du börjar arbeta med User Storyn:

* Tilldela User Storyn till dig själv (`Assignee`).
* Kontrollera att ingen annan redan arbetar med samma User Story.
* Flytta User Storyn från **Ready** till **In Progress**.

När en User Story är `In Progress` ska det vara tydligt vem som ansvarar för arbetet.

---

# 3. Skapa en branch

Skapa en egen branch för User Storyn.

Branchen ska utgå från den branch som teamet har bestämt som bas för utveckling.

Använd följande namngivning:

```text
dev/feature/US-XX-kort-beskrivning
```

Exempel:

```text
dev/feature/US-01-create-fault-report
```

Använd samma User Story-nummer som i GitHub Issue.

> Arbeta inte direkt på teamets gemensamma branch för en User Story.

---

# 4. Implementera User Storyn

Implementera det som krävs för att uppfylla User Storyn och dess acceptanskriterier.

Under arbetet:

* Följ projektets kodstandard och struktur.
* Commita arbetet löpande.
* Använd tydliga commit-meddelanden.
* Håll ändringarna fokuserade på den aktuella User Storyn.

Om du upptäcker att User Storyn behöver ändras eller utökas:

1. Ta upp det med teamet.
2. Kom överens om hur det ska hanteras.
3. Undvik att på egen hand utöka User Storyns omfattning.

> Om arbetet visar sig vara betydligt större än förväntat ska teamet informeras istället för att User Storyn växer okontrollerat.

---

# 5. Testa ditt arbete

Innan Pull Request skapas ska du själv kontrollera att arbetet fungerar.

Kontrollera:

* [ ] Alla acceptanskriterier är uppfyllda.
* [ ] Funktionen fungerar som förväntat.
* [ ] Relevant felhantering fungerar.
* [ ] Befintlig funktionalitet fortfarande fungerar.
* [ ] Relevanta tester är genomförda.
* [ ] Inga uppenbara fel har lämnats kvar.

Om något inte fungerar ska det åtgärdas innan User Storyn skickas till review.

---

# 6. Skapa Pull Request

När implementationen är färdig och du har testat ditt arbete:

1. Pusha branchen till GitHub.
2. Skapa en Pull Request mot teamets överenskomna målbranch.
3. Koppla Pull Requesten till rätt GitHub Issue.
4. Beskriv kort vad som har implementerats.
5. Beskriv hur funktionen har testats.

## Koppla Pull Request till Issue

Ange vilken Issue som Pull Requesten hör till.

Använd exempelvis:

```text
Closes #23
```

där `23` är numret på den aktuella GitHub Issue:n.

Exempel på PR-beskrivning:

```text
## Ändringar

- Lagt till möjlighet att skapa en felanmälan.
- Lagt till validering av obligatoriska fält.

## Testat

- Skapat en felanmälan med giltiga uppgifter.
- Kontrollerat validering av obligatoriska fält.

Closes #23
```

`Closes #23` kopplar PR:n till Issue #23 och gör att GitHub kan stänga Issue:n automatiskt när PR:n mergas.

> Kontrollera alltid att Issue-numret är korrekt.

### Flytta till Review

När Pull Requesten är skapad:

**In Progress → Review**

User Storyn ska nu vänta på code review.

> Att skapa en Pull Request betyder inte att User Storyn är klar.

---

# 7. Code Review

En annan teammedlem granskar Pull Requesten.

Granskaren kontrollerar bland annat:

* [ ] Implementation motsvarar User Storyn.
* [ ] Acceptanskriterierna är uppfyllda.
* [ ] Koden är begriplig.
* [ ] Koden följer projektets struktur och kodstandard.
* [ ] Inga uppenbara fel har introducerats.
* [ ] Relevanta tester finns eller har genomförts.
* [ ] PR:n innehåller endast ändringar som hör till User Storyn.

## Om ändringar behövs

Om granskaren hittar något som behöver ändras:

1. Utvecklaren gör ändringarna på samma branch.
2. Ändringarna pushas till GitHub.
3. Pull Requesten uppdateras.
4. Granskaren granskar ändringarna igen.

User Storyn ligger kvar i **Review** tills code review är godkänd.

---

# 8. Godkänd Pull Request

När code review är godkänd:

1. Kontrollera att eventuella review-kommentarer är hanterade.
2. Pull Requesten mergas enligt teamets Git-strategi.
3. Kontrollera att rätt GitHub Issue är kopplad till PR:n.

När PR:n mergas kan GitHub automatiskt:

* stänga den kopplade Issue:n via `Closes #XX`
* flytta User Storyn från **Review** till **Done**, om Project-automationen är konfigurerad för detta.

Det normala flödet blir då:

```text
In Progress
    ↓
Pull Request skapas
    ↓
Review
    ↓
Code Review
    ↓
Godkänd
    ↓
Pull Request mergas
    ↓
Done
```

> Om automationen inte fungerar ska teamet manuellt kontrollera att User Storyn hamnar i rätt status.

---

# 9. Done

En User Story är klar när samtliga delar i Definition of Done är uppfyllda.

* [ ] Alla acceptanskriterier är uppfyllda.
* [ ] Arbetet är testat.
* [ ] Code review är genomförd.
* [ ] Eventuella review-kommentarer är hanterade.
* [ ] Pull Requesten är godkänd.
* [ ] Pull Requesten är mergad.
* [ ] Kopplad GitHub Issue är korrekt hanterad.
* [ ] User Storyn är i **Done**.

> En User Story ska inte betraktas som `Done` enbart för att utvecklingen är färdig.

---

# Viktiga teamregler

## En User Story ska ha en ansvarig

När en person börjar arbeta med en User Story ska personen tilldelas som `Assignee`.

Andra teammedlemmar kan hjälpa till, men det ska vara tydligt vem som ansvarar för User Storyn.

## En User Story ska ha en egen branch

User Stories utvecklas på egna branches och går genom Pull Request.

## Code review görs av någon annan

Den som implementerat User Storyn ska normalt inte ensam godkänna sin egen Pull Request.

## Review är en egen status

När Pull Requesten har skapats flyttas User Storyn till **Review**.

Den ligger kvar där medan code review genomförs och eventuella ändringar görs.

## Merge innebär inte att review hoppas över

Review ska vara godkänd **innan** Pull Requesten mergas.

Det innebär:

```text
In Progress
    ↓
PR skapas
    ↓
Review
    ↓
Review godkänd
    ↓
Merge
    ↓
Done
```

## Håll GitHub Project uppdaterat

Project-statusen ska alltid spegla det faktiska arbetet.

Om någon arbetar aktivt med en User Story ska den inte ligga kvar i `Ready`.

Om en Pull Request väntar på review ska den inte ligga kvar i `In Progress`.

---

# Snabb checklista

När du tar en ny User Story:

```text
[ ] 1. Läs User Story och acceptanskriterier
[ ] 2. Kontrollera att allt är tydligt
[ ] 3. Tilldela User Storyn till dig själv
[ ] 4. Flytta Ready → In Progress
[ ] 5. Skapa feature branch
[ ] 6. Implementera
[ ] 7. Testa
[ ] 8. Pusha branch
[ ] 9. Skapa Pull Request
[ ] 10. Lägg till "Closes #XX"
[ ] 11. Flytta In Progress → Review
[ ] 12. Code review genomförs av annan teammedlem
[ ] 13. Hantera eventuella ändringar
[ ] 14. PR godkänns
[ ] 15. PR mergas
[ ] 16. Kontrollera att User Storyn hamnar i Done
```

## Övergripande flöde

```text
                    ┌──────────────┐
                    │   Backlog    │
                    └──────┬───────┘
                           │
                           ▼
                    ┌──────────────┐
                    │    Ready     │
                    └──────┬───────┘
                           │
                     Ta User Story
                           │
                           ▼
                    ┌──────────────┐
                    │ In Progress  │
                    └──────┬───────┘
                           │
                     Implementera
                           │
                      Testa själv
                           │
                    Skapa Pull Request
                           │
                           ▼
                    ┌──────────────┐
                    │    Review    │
                    └──────┬───────┘
                           │
                      Code Review
                           │
                     ┌─────┴─────┐
                     │           │
                  Ändringar    Godkänd
                     │           │
                     └──→ Review │
                                 │
                              Merge
                                 │
                                 ▼
                         ┌──────────────┐
                         │     Done     │
                         └──────────────┘
```
