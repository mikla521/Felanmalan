import { Outlet } from "react-router-dom";
import { useAuth } from "../../auth/AuthContext";

function AppLayout() {
    const { user, logout } = useAuth();

    return (
        <>
            <div className="user-info">
                <div>
                    <span>Inloggad som: {user?.email}</span>
                    <span>Roll: {user?.role}</span>
                </div>

                <button className="logout-button" onClick={logout}>
                    Logga ut
                </button>
            </div>

            <Outlet />
        </>
    );
}

export default AppLayout;
