import "./App.css";

import { AuthProvider } from "./auth/AuthContext";
import AppRoutes from "./AppRoutes";

function App() {
  return (
    <AuthProvider>
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

        <AppRoutes />

        <footer className="footer">
          <span>Felanmälan</span>
          <span>IT-supportsystem</span>
        </footer>
      </div>
    </AuthProvider>
  );
}

export default App;
