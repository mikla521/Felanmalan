import { createContext, useContext, useEffect, useState } from "react";

export type AuthUser = {
    id: string;
    email: string;
    role: string;
};

type AuthContextType = {
    user: AuthUser | null;
    loading: boolean;
    login: () => Promise<AuthUser | null>;
    logout: () => Promise<void>;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: React.ReactNode }) {
    const [user, setUser] = useState<AuthUser | null>(null);
    const [loading, setLoading] = useState(true);

    async function loadUser(): Promise<AuthUser | null> {
        try {
            const response = await fetch("/api/auth/me", {
                credentials: "include",
            });

            if (!response.ok) {
                setUser(null);
                return null;
            }

            const currentUser: AuthUser = await response.json();
            setUser(currentUser);

            return currentUser;
        } catch {
            setUser(null);
            return null;
        } finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        loadUser();
    }, []);

    async function login() {
        return await loadUser();
    }

    async function logout() {
        await fetch("/api/auth/logout", {
            method: "POST",
            credentials: "include",
        });

        setUser(null);
    }

    return (
        <AuthContext.Provider value={{ user, loading, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
}

export function useAuth() {
    const context = useContext(AuthContext);

    if (!context) {
        throw new Error("useAuth must be used within an AuthProvider");
    }

    return context;
}
