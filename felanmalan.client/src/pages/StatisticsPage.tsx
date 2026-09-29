import { useEffect, useState } from "react";

type AverageResponseTime = {
    category: number | string;
    averageResponseTimeInMinutes: number;
};

function StatisticsPage() {
    const [averageTimes, setAverageTimes] = useState<AverageResponseTime[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        async function loadStatistics() {
            try {
                const response = await fetch(
                    "http://localhost:5145/api/felanmalan/statistics/response-time"
                );
                if (!response.ok) {
                    throw new Error(`Servern svarade med ${response.status}`);
                }
                const data: AverageResponseTime[] = await response.json();
                setAverageTimes(data);
            } catch (err) {
                setError(err instanceof Error ? err.message : "Något gick fel");
            } finally {
                setLoading(false);
            }
        }

        loadStatistics();
    }, []);

    if (loading) return <p>Laddar...</p>;
    if (error) return <p>Kunde inte hämta statistik: {error}</p>;

    return (
        <main className="main-content">
            <h2>Statistik</h2>
            <div>
                <h3>Genomsnittlig svarstid</h3>
                <ul>
                    {averageTimes.map((item) => (
                        <li key={item.category}>
                            {item.category}: {item.averageResponseTimeInMinutes.toFixed(1)} min
                        </li>
                    ))}
                </ul>
            </div>
        </main>
    );
}

export default StatisticsPage;