export const API_BASE = import.meta.env.VITE_API_BASE as string;

import type {
    AchievementTemplate,
    AuthResponse,
    EducationTemplate,
    ExperienceTemplate,
    LanguageTemplate,
    LoginRequest,
    ProfileTemplate,
    RegisterRequest,
    SkillTemplate,
    UserTemplate,
    RawUserTemplate
} from "./types";

async function request<T>(
    path: string,
    initialize: RequestInit = {},
    options: { auth?: boolean } = { auth: true }
): Promise<T> {
    const url = `${API_BASE}${path}`;
    const headers = new Headers(initialize.headers);

    if (!headers.has("Content-Type") && initialize.body && !(initialize.body instanceof FormData)) {
        headers.set("Content-Type", "application/json");
    }

    const accessToken = sessionStorage.getItem("accessToken");
    if (options.auth && accessToken) {
        headers.set("Authorization", `Bearer ${accessToken}`);
    }

    let payload = await fetch(url, { ...initialize, headers });

    if (!payload.ok) {
        throw new Error(`Error, invalid fields`);
    }

    if (payload.status === 204) {
        return undefined as unknown as T;
    }

    const contentType = payload.headers.get("content-type") || "";
    if (!contentType.includes("application/json")) {
        return undefined as unknown as T;
    }

    return payload.json() as Promise<T>;
}

// Authentication Functions

export async function register(req: RegisterRequest) {
    return request<{ id: number; email: string }>(`/auth/register`, {
        method: "POST",
        body: JSON.stringify(req),
    }, { auth: false });
}

export async function login(req: LoginRequest) {
    const tokens = await request<AuthResponse>(`/auth/login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(req),
    }, { auth: false });
    sessionStorage.setItem("accessToken", tokens.accessToken);
    sessionStorage.setItem("refreshToken", tokens.refreshToken);
    sessionStorage.setItem("userId", String(tokens.userId));
    return tokens;
}

export async function validate(): Promise<RawUserTemplate> {
    return request<RawUserTemplate>(`/auth/validation`, { method: "GET" });
}

export function logout() {
    sessionStorage.clear();
    localStorage.clear();
}

// User Functions

export async function getAllUsers() { // getAllUsers is only for development process -CK
    const res = await fetch(`${API_BASE}/users`, {
        headers: sessionStorage.getItem("accessToken") ? { Authorization: `Bearer ${sessionStorage.getItem("accessToken")}` } : undefined,
    });
    if (!res.ok) {
        throw new Error(`API ${res.status}`);
    }
    return res.json() as Promise<UserTemplate[]>;
}

export function getUserById(id: number) {
    return request<UserTemplate>(`/users/${id}`);
}

// Profile Functions

export function upsertProfile(userId: number, body: ProfileTemplate) {
    return request(`/users/${userId}/profile`, {
        method: "POST",
        body: JSON.stringify(body),
    });
}

export function deleteProfile(id: number) {
    return request<void>(`/profile/${id}`, { method: "DELETE" });
}

// Experience Functions

export function createExperience(userId: number, body: ExperienceTemplate) {
    return request(`/users/${userId}/experiences`, {
        method: "POST",
        body: JSON.stringify(body),
    });
}

export function updateExperience(userId: number, expId: number, body: ExperienceTemplate) {
    return request(`/users/${userId}/experiences/${expId}`, {
        method: "PUT",
        body: JSON.stringify(body),
    });
}

export function deleteExperience(id: number) {
    return request<void>(`/experiences/${id}`, { method: "DELETE" });
}

// Skill Functions

export function createSkill(userId: number, body: SkillTemplate) {
    return request(`/users/${userId}/skills`, {
        method: "POST",
        body: JSON.stringify(body),
    });
}

export function updateSkill(userId: number, skillId: number, body: SkillTemplate) {
    return request(`/users/${userId}/skills/${skillId}`, {
        method: "PUT",
        body: JSON.stringify(body),
    });
}

export function deleteSkill(id: number) {
    return request<void>(`/skills/${id}`, { method: "DELETE" });
}

// Education Functions 

export function createEducation(userId: number, body: EducationTemplate) {
    return request(`/users/${userId}/educations`, {
        method: "POST",
        body: JSON.stringify(body),
    });
}

export function updateEducation(userId: number, eduId: number, body: EducationTemplate) {
    return request(`/users/${userId}/educations/${eduId}`, {
        method: "PUT",
        body: JSON.stringify(body),
    });
}

export function deleteEducation(id: number) {
    return request<void>(`/educations/${id}`, { method: "DELETE" });
}

// Language Functions

export function createLanguage(userId: number, body: LanguageTemplate) {
    return request(`/users/${userId}/languages`, {
        method: "POST",
        body: JSON.stringify(body),
    });
}

export function updateLanguage(userId: number, langId: number, body: LanguageTemplate) {
    return request(`/users/${userId}/languages/${langId}`, {
        method: "PUT",
        body: JSON.stringify(body),
    });
}

export function deleteLanguage(id: number) {
    return request<void>(`/languages/${id}`, { method: "DELETE" });
}

// Achievement Functions

export function createAchievement(userId: number, body: AchievementTemplate) {
    return request(`/users/${userId}/achievements`, {
        method: "POST",
        body: JSON.stringify(body),
    });
}

export function updateAchievement(userId: number, achieveId: number, body: AchievementTemplate) {
    return request(`/users/${userId}/achievements/${achieveId}`, {
        method: "PUT",
        body: JSON.stringify(body),
    });
}

export function deleteAchievement(id: number) {
    return request<void>(`/achievements/${id}`, { method: "DELETE" });
}
