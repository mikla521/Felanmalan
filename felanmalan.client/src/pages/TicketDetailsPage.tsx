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
    onBack: () => void;
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
                        <dd>{statusNames[ticket.status] ?? "Okänd status"}</dd>
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

                <button className="ticket-button" type="button" onClick={onBack}>
                    Tillbaka
                </button>
            </section>
        </main>
    );
}

export default TicketDetailsPage;
