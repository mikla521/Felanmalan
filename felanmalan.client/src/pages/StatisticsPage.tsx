import { useEffect, useState } from "react";

 type AverageResponseTime = {
     category: number;
     averageResponseTimeInMinutes: number;
};

const categoryNames = [
    "Ej kategoriserad",
    "Hårdvara",
    "Mjukvara",
    "Nätverk",
    "Övrigt",
];

function StatisticsPage() {
    const [averageTimes, setAverageTimes] = useState<AverageResponseTime[]>([]);

    useEffect(() => {
        async function loadStatistics() {
            try {
                const response = await fetch("/api/felanmalan/statistics/response-time");

                if (!response.ok) {
                    throw new Error("Kunde inte hämta statistik");
                }

                const data: AverageResponseTime[] = await response.json();
                setAverageTimes(data);
                console.log(data);
            } catch (error) {
                console.error("Kunde inte hämta statistik:", error);
            }
        }

        loadStatistics();
    }, []);


    return (
        <main className="main-content">
            <h2>Statistik</h2>
            <div>
                <h3>Genomsnittlig svarstid</h3>
                <ul>
                    {averageTimes.map((item) => (
                        <li key={item.category}>
                            {categoryNames[item.category]}: {item.averageResponseTimeInMinutes.toFixed(1)} min
                        </li>
                    ))} 
                </ul>
            </div>
        </main>
    );
}

export default StatisticsPage;