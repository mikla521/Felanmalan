import "./App.css";

import CreateReportPage from "./pages/CreateReportPage";
import LoginPage from "./pages/LoginPage";

function App() {
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

      <LoginPage />

      <CreateReportPage />

      <footer className="footer">
        <span>Felanmälan</span>
        <span>IT-supportsystem</span>
      </footer>
    </div>
  );
}

export default App;
