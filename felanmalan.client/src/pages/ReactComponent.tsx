import { useState } from 'react';

function TicketStatusActions({ ticketId }: { ticketId: number }) {

    const [updated, setUpdated] = useState(false);

    const handleStatusChange = async (action: 'StatusInProgress' | 'StatusResolved' | 'StatusClosed') => {
        try {
            const response = await fetch(`/api/felanmalan/${ticketId}/${action}`, {
                method: 'PUT',
            });

            if (!response.ok) {
                throw new Error('Kunde inte uppdatera status.');
            }

            setUpdated(true);
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <div className="status-actions">
            <button onClick={() => handleStatusChange('StatusInProgress')}>
                Påbörja
            </button>
            <button onClick={() => handleStatusChange('StatusResolved')}>
                Markera löst
            </button>
            <button onClick={() => handleStatusChange('StatusClosed')}>
                Stäng
            </button>

            {updated && (
                <div className="success-message">
                    Status uppdaterad.
                </div>
            )}
        </div>
    );
}

export default TicketStatusActions;