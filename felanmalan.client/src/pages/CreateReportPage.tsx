import { useState } from 'react';

function CreateReportPage() {

    const [description, setDescription] = useState('');
    const [submitted, setSubmitted] = useState(false);

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        setSubmitted(true);
    };

    return (
        <main className="main-content">
            <section className="report-form">
                <div className="form-header">
                    <h2>Skapa felanmälan</h2>
                    <p>Beskriv problemet så tydligt som möjligt.</p>
                </div>

                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label htmlFor="description">
                            Beskrivning <span className="required">*</span>
                        </label>

                        <textarea
                            id="description"
                            name="description"
                            placeholder="Beskriv vad som har hänt..."
                            rows={8}
                            value={description}
                            onChange={(event) => setDescription(event.target.value)}
                            required
                        />
                    </div>

                    <div className="form-actions">
                        <button type="submit">
                            Skicka felanmälan
                        </button>
                    </div>
                </form>

                {submitted && (
                    <div className="success-message">
                        Felanmälan har skickats.
                    </div>
                )}

            </section>
        </main>
    );
}

export default CreateReportPage;
