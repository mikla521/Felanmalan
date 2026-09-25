import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../auth/AuthContext";

function LoginPage() {
    const { login } = useAuth();
    const navigate = useNavigate();

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
        event.preventDefault();
        setError("");

        try {
            const response = await fetch("/api/auth/login", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                credentials: "include",
                body: JSON.stringify({
                    email,
                    password,
                }),
            });

            if (!response.ok) {
                setError("Felaktig e-postadress eller lösenord.");
                return;
            }

            const loggedInUser = await login();

            if (loggedInUser?.role === "Support") {
                navigate("/support/reports");
            } else {
                navigate("/user/report");
            }
        } catch {
            setError("Kunde inte logga in.");
        }
    }

    return (
        <main className="main-content">
            <section className="login-form">
                <div className="form-header">
                    <h2>Logga in</h2>
                    <p>Logga in för att använda Felanmälan.</p>
                </div>

                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label htmlFor="email">E-post</label>
                        <input
                            id="email"
                            type="email"
                            value={email}
                            onChange={(event) => setEmail(event.target.value)}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="password">Lösenord</label>
                        <input
                            id="password"
                            type="password"
                            value={password}
                            onChange={(event) => setPassword(event.target.value)}
                            required
                        />
                    </div>

                    <div className="form-actions">
                        <button type="submit" className="login-button">
                            Logga in
                        </button>
                    </div>
                </form>

                {error && (
                    <div className="success-message">
                        {error}
                    </div>
                )}
            </section>
        </main>
    );
}

export default LoginPage;
