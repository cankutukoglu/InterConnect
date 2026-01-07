import { useTranslation } from "react-i18next";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { login } from "./lib/api";
import {
    Box,
    Card,
    CardContent,
    Typography,
    TextField,
    InputAdornment,
    IconButton,
    Alert,
} from "@mui/material";
import { Visibility, VisibilityOff, Mail } from "@mui/icons-material";
import { LoadingButton } from "@mui/lab";
import ToggleLanguage from "./ToggleLanguage";

function LoginPage() {
    const { t } = useTranslation();
    const navigate = useNavigate();
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [showPw, setShowPw] = useState(false);
    const [error, setError] = useState<string>("");
    const [loading, setLoading] = useState(false);
    const onSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError("");
        setLoading(true);
        try {
            await login({ email, password });
            navigate("/profile");
        } catch (err: any) {
            setError(err?.message ?? "Login failed");
        } finally {
            setLoading(false);
        }
    };

    return (
        <Box
            sx={{
                minHeight: "100dvh",
                display: "grid",
                placeItems: "center",
                bgcolor: "#f7f7fb",
                p: 2,
            }}
        >
            <Card
                elevation={6}
                sx={{
                    width: "100%",
                    maxWidth: 450,
                    borderRadius: 3,
                    overflow: "hidden",
                }}
            >
                <CardContent sx={{ p: { xs: 3, sm: 4 } }}>
                    <Typography variant="h5" fontWeight={700}>
                        {t("auth.interconnect")}
                    </Typography>
                    <Typography variant="body2" color="text.secondary" sx={{ mt: 0.5 }}>
                        {t("auth.login")}
                    </Typography>
                    <Box component="form" onSubmit={onSubmit} sx={{ mt: 3 }}>
                        <TextField
                            id="email"
                            label={t("auth.email")}
                            type="email"
                            value={email}
                            autoComplete="email"
                            onChange={(e) => setEmail(e.target.value)}
                            required
                            fullWidth
                            variant="outlined"
                            margin="normal"
                            InputProps={{
                                startAdornment: (
                                    <InputAdornment position="start">
                                        <Mail fontSize="small" />
                                    </InputAdornment>
                                ),
                            }}
                        />
                        <TextField
                            id="password"
                            label={t("auth.password")}
                            type={showPw ? "text" : "password"}
                            value={password}
                            autoComplete="current-password"
                            onChange={(e) => setPassword(e.target.value)}
                            required
                            fullWidth
                            variant="outlined"
                            margin="normal"
                            sx={{ mb: 4 }}
                            InputProps={{
                                endAdornment: (
                                    <InputAdornment position="end">
                                        <IconButton
                                            aria-label={showPw ? "Hide password" : "Show password"}
                                            edge="end"
                                            onClick={() => setShowPw((s) => !s)}
                                        >
                                            {showPw ? <VisibilityOff /> : <Visibility />}
                                        </IconButton>
                                    </InputAdornment>
                                ),
                            }}
                        />
                        <ToggleLanguage />
                        {error && (
                            <Alert severity="error" sx={{ mt: 2 }}>
                                {error}
                            </Alert>
                        )}
                        <LoadingButton
                            type="submit"
                            loading={loading}
                            variant="contained"
                            fullWidth
                            sx={{ mt: 3, py: 1.2, borderRadius: 2, textTransform: "none", fontWeight: 700 }}
                        >
                            {t("auth.login")}
                        </LoadingButton>
                    </Box>
                </CardContent>
            </Card>
        </Box>
    );
};
export default LoginPage;
