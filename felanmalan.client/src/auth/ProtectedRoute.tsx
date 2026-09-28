import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../auth/AuthContext";

type ProtectedRouteProps = {
    roles?: string[];
};

function ProtectedRoute({ roles }: ProtectedRouteProps) {
    const { user, loading } = useAuth();

    if (loading) {
        return <p>Laddar...</p>;
    }

    if (!user) {
        return <Navigate to="/login" replace />;
    }

    if (roles && !roles.includes(user.role)) {
        return <Navigate to="/access-denied" replace />;
    }

    return <Outlet />;
}

export default ProtectedRoute;
