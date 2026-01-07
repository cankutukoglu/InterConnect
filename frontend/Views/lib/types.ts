export type LoginRequest = { email: string; password: string };

export type RegisterRequest = { email: string; password: string };

export type RefreshRequest = { refreshToken: string };

export type AuthResponse = { accessToken: string; refreshToken: string; userId: number };

export type ProfileTemplate = {
    fullName: string;
    city: string;
    country: string;
    description?: string | null;
    profilePic?: string | null;
    connectionCount?: number | null;
};

export type SkillTemplate = { name: string };

export type LanguageTemplate = { name: string };

export type ExperienceTemplate = {
    company: string;
    title: string;
    startDate: string;
    endDate?: string | null;
    city: string;
    country: string;
    description?: string | null;
    logoPicUrl?: string | null;
};

export type EducationTemplate = {
    school: string;
    degree: string;
    city: string;
    country: string;
    startYear?: number | null;
    endYear?: number | null;
    activities?: string | null;
    logoPicUrl?: string | null;
};

export type AchievementTemplate = {
    title: string;
    year?: number | null;
    description?: string | null;
};

export type UserTemplate = {
    id: number;
    email: string;
    createdAt: string;
    profile: {
        id: number;
        fullName: string;
        city: string;
        country: string;
        description: string | null;
        connectionCount: number;
        profilePic: string | null;
    } | null;
    experiences: Array<{
        id: number;
        title: string;
        company: string;
        city: string;
        country: string;
        description: string | null;
        startDate: string;
        endDate: string | null;
        isCurrent: boolean;
        logoPicUrl: string | null;
    }> | null;
    educations: Array<{
        id: number;
        school: string;
        degree: string;
        city: string;
        country: string;
        startYear: number | null;
        endYear: number | null;
        activities: string | null;
        logoPicUrl?: string | null;
    }> | null;
    skills: Array<{ id: number; name: string }> | null;
    achievements: Array<{ id: number; title: string; description: string | null; year: number | null }> | null;
    languages: Array<{ id: number; name: string }> | null;
};

export type RawUserTemplate = {
    id: number;
    email: string;
    createdAt: string;
};
