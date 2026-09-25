export function formatTicketTime(value: string | null) {
    if (!value) return "Inte påbörjad";

    // Databasen sparar UTC-tider, men de kan skickas utan tidszonsmarkering.
    const utcValue = /Z$|[+-]\d{2}:\d{2}$/.test(value) ? value : `${value}Z`;
    return new Date(utcValue).toLocaleString("sv-SE");
}
