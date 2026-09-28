import { useState } from "react";
import { formatTicketTime } from "../utils/formatTicketTime";

export type TicketDetails = {
    id: number;
    description: string;
    category: number;
    status: number;
    timeCreated: string;
    timeStarted: string | null;
};

type TicketDetailsPageProps = {
    ticket: TicketDetails;
    onBack: (newStatus: number) => void;
};

const categoryNames = [
    "Ej kategoriserad",
    "Hårdvara",
    "Mjukvara",
    "Nätverk",
    "Övrigt",
];

const statusNames = ["Ny", "Pågående", "Löst", "Stängd"];



function TicketDetailsPage({ ticket, onBack }: TicketDetailsPageProps) {

    const [category, setCategory] = useState(ticket.category);
    const [status, setStatus] = useState(ticket.status);

    async function handleStatusChange(newStatus: number) {
        let endpoint = "";

        switch (newStatus) {
            case 1:
                endpoint = `/api/felanmalan/${ticket.id}/StatusInProgress`;
                break;

            case 2:
                endpoint = `/api/felanmalan/${ticket.id}/StatusResolved`;
                break;

            case 3:
                endpoint = `/api/felanmalan/${ticket.id}/StatusClosed`;
                break;

            default:
                return;
        }

        const response = await fetch(endpoint, {
            method: "PUT",
        });

        if (!response.ok) {
            throw new Error("Kunde inte uppdatera status.");
        }

        setStatus(newStatus);
    }

    return (
        <main className="main-content">
            <section className="report-form">
                <div className="form-header">
                    <h2>Ärende #{ticket.id}</h2>
                    <p>Information om felanmälan</p>
                </div>

                <dl className="ticket-details">
                    <div className="ticket-detail">
                        <dt>Beskrivning</dt>
                        <dd className="ticket-description">{ticket.description}</dd>
                    </div>
                    <div className="ticket-detail">
                        <dt>Kategori</dt>
                        <dd>{categoryNames[ticket.category] ?? "Okänd kategori"}</dd>
                    </div>
                    <div className="ticket-detail">
                        <dt>Status</dt>
                        <dd>{statusNames[status] ?? "Okänd status"}</dd>
                    </div>
                    <div className="ticket-detail">
                        <dt>Skapad</dt>
                        <dd>{formatTicketTime(ticket.timeCreated)}</dd>
                    </div>
                    <div className="ticket-detail">
                        <dt>Påbörjad</dt>
                        <dd>{formatTicketTime(ticket.timeStarted)}</dd>
                    </div>
                </dl>

                <div className="form-group">
                    <label htmlFor="category">Välj kategori</label>
                    <select
                        id="category"
                        value={category}
                        onChange={(event) => setCategory(Number(event.target.value))}
                    >
                        {categoryNames.map((name, index) => (
                            <option key={index} value={index}>
                                {name}
                            </option>
                        ))}
                    </select>
                    {category !== ticket.category && (
                        <p role="status">Ändringen är inte sparad.</p>
                    )}
                </div>

                <div className="status-buttons">
                    <button
                        type="button"
                        className="status-button in-progress"
                        onClick={() => handleStatusChange(1)}
                    >
                        Påbörja
                    </button>

                    <button
                        type="button"
                        className="status-button resolved"
                        onClick={() => handleStatusChange(2)}
                    >
                        Löst
                    </button>

                    <button
                        type="button"
                        className="status-button closed"
                        onClick={() => handleStatusChange(3)}
                    >
                        Stäng
                    </button>
                </div>

                <div>
                    <button className="ticket-button" type="button" onClick={() => onBack(status)}>
                        Tillbaka
                    </button>
                </div>

                
            </section>
        </main>
    );
}

export default TicketDetailsPage;
