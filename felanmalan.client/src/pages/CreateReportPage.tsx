function CreateReportPage() {
    return (
        <main className="main-content">
            <section className="report-form">
                <h2>Skapa felanmälan</h2>
                <p>Beskriv problemet så tydligt som möjligt.</p>

                <form>
                    <div className="form-group">
                        <label htmlFor="description">Beskrivning</label>

                        <textarea
                            id="description"
                            name="description"
                            placeholder="Beskriv problemet..."
                            rows={7}
                            required
                        />
                    </div>

                    <button type="submit">
                        Skicka felanmälan
                    </button>
                </form>
            </section>
        </main>
    );
}

export default CreateReportPage;
