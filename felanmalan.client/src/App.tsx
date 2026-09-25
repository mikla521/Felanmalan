import { useEffect, useState } from "react";
import "./App.css";

import CreateReportPage from "./pages/CreateReportPage";
import LoginPage from "./pages/LoginPage";

type AuthUser = {
  id: string;
  email: string;
};

function App() {
  const [user, setUser] = useState<AuthUser | null>(null);

  async function getCurrentUser(): Promise<AuthUser | null> {
    const response = await fetch("/api/auth/me", {
      credentials: "include",
    });

    if (!response.ok) {
      return null;
    }

    return await response.json();
  }

  async function loadUser() {
    const currentUser = await getCurrentUser();
    setUser(currentUser);
  }

  useEffect(() => {
    getCurrentUser().then((currentUser) => {
      setUser(currentUser);
    });
  }, []);

  async function handleLogout() {
    await fetch("/api/auth/logout", {
      method: "POST",
      credentials: "include",
    });

    setUser(null);
  }

  return (
    <div className="app">
      <header className="topbar">
        <div className="brand">
          <div className="brand-icon">F</div>
          <div>
            <h1>Felanmälan</h1>
            <span>IT-support</span>
          </div>
        </div>
      </header>

      {user ? (
        <>
          <p>Inloggad som: {user.email}</p>
          <button onClick={handleLogout}>Logga ut</button>

          <CreateReportPage />
        </>
      ) : (
        <LoginPage onLogin={loadUser} />
      )}

      <footer className="footer">
        <span>Felanmälan</span>
        <span>IT-supportsystem</span>
      </footer>
    </div>
  );
}

export default App;
