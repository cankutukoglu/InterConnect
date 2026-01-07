import React, { useEffect, useState, type JSX } from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import ProfilePage from "./ProfilePage";
import LoginPage from "./LoginPage";
import { validate } from "./lib/api";

type PrivateRouteProps = {
    children: JSX.Element;
};

const PrivateRoute: React.FC<PrivateRouteProps> = ({ children }) => {
    const [valid, setValid] = useState<boolean | null>(null);

    useEffect(() => {
        validate()
            .then(() => setValid(true))
            .catch(() => setValid(false));
    }, []);

    if (valid === null) {
        return null;
    }
    else if (valid) {
        return children;
    }
    else {
        return <Navigate to="/" replace />;
    }
};

function App() {
    return (
        <Routes>
            <Route path="/" element={<LoginPage />} />
            <Route
                path="/profile"
                element={
                    <PrivateRoute>
                        <ProfilePage />
                    </PrivateRoute>
                }
            />
            <Route path="*" element={<Navigate to="/" replace />} />
        </Routes>
    );
}
export default App;

