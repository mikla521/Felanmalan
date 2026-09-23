# Branching-strategi

> **Det här är ett förslag, inte ett facit.** Välj en strategi i sprint 0,
> skriv ner den här, och motivera valet. Att ni kan motivera valet väger
> tyngre än vilket val ni gör.

## Förslag: trunk-based med korta feature-grenar

- `main` är alltid deploybar. Ingen pushar direkt till `main`.
- Allt arbete sker på `feature/<kort-beskrivning>`, som lever max en dag.
- Ändringar går in i `main` via pull request med minst en granskare.
- CI måste vara grön innan merge.
- Grenen tas bort efter merge.

## Varför korta grenar i just det här projektet

Sprintarna är endagssprintar. En gren som lever längre än en sprint hinner
aldrig bli granskad inom sprinten, och då syns inte arbetet i demon.

## Skydda main

Settings → Branches → Add rule på `main`:

- Require a pull request before merging
- Require status checks to pass → välj `Bygg och testa`

Gör det i sprint 0. Efter det kan ni inte råka förstöra varandras arbete.
