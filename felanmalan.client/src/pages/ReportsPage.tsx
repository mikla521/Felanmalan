import { useEffect, useState } from "react";
import { formatTicketTime } from "../utils/formatTicketTime";
import TicketDetailsPage from "./TicketDetailsPage";

type Ticket = {
    id: number;
    description: string;
    category: number;
    status: number;
    timeStarted: string | null;
    timeCreated: string;
};

function getStatusText(status: number) {
    switch (status) {
        case 0:
            return "Ny";
        case 1:
            return "Pågående";
        case 2:
            return "Löst";
        case 3:
            return "Stängd";
        default:
            return "Okänd";
    }
}

function getCategoryText(category: number) {
    switch (category) {
        case 0:
            return "Ej kategoriserad";
        case 1:
            return "Hårdvara";
        case 2:
            return "Mjukvara";
        case 3:
            return "Nätverk";
        case 4:
            return "Övrigt";
        default:
            return "Okänd";
    }
}

function ReportsPage() {
    const [tickets, setTickets] = useState<Ticket[]>([]);
    const [selectedTicket, setSelectedTicket] = useState<Ticket | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        async function loadTickets() {
            try {
                const response = await fetch("/api/felanmalan");

                if (!response.ok) {
                    throw new Error("Kunde inte hämta ärenden.");
                }

                const data: Ticket[] = await response.json();
                setTickets(data);
            } catch {
                setError("Kunde inte hämta inkomna ärenden.");
            } finally {
                setLoading(false);
            }
        }

        loadTickets();
    }, []);

    if (loading) {
        return <main className="main-content">Laddar ärenden...</main>;
    }

    if (error) {
        return <main className="main-content">{error}</main>;
    }

    if (selectedTicket) {
        return (
            <TicketDetailsPage
                ticket={selectedTicket}
                onBack={() => setSelectedTicket(null)}
            />
        );
    }

    return (
        <main className="main-content">
            <h2>Inkomna ärenden</h2>

            <p>Antal ärenden: {tickets.length}</p>

            {tickets.length === 0 ? (
                <p>Det finns inga inkomna ärenden.</p>
            ) : (
                <div className="ticket-list">
                    {tickets.map((ticket) => (
                        <article className="ticket-card" key={ticket.id}>
                            <h3>Ärende #{ticket.id}</h3>

                            <p>
                                <strong>Beskrivning:</strong>{" "}
                                {ticket.description}
                            </p>

                            <p>
                                <strong>Kategori:</strong>{" "}
                                {getCategoryText(ticket.category)}
                            </p>

                            <p>
                                <strong>Status:</strong>{" "}
                                {getStatusText(ticket.status)}
                            </p>

                            <p>
                                <strong>Skapad:</strong>{" "}
                                {formatTicketTime(ticket.timeCreated)}
                            </p>

                            <p>
                                <strong>Påbörjad:</strong>{" "}
                                {formatTicketTime(ticket.timeStarted)}
                            </p>

                            <button
                                className="ticket-button"
                                type="button"
                                onClick={() => setSelectedTicket(ticket)}
                            >
                                Visa detaljer
                            </button>
                        </article>
                    ))}
                </div>
            )}
        </main>
    );

}

export default ReportsPage;
