import React, { useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";

import {
    getUserById,
    logout,
    upsertProfile,
    createExperience,
    updateExperience,
    createEducation,
    updateEducation,
    createSkill,
    updateSkill,
    createAchievement,
    updateAchievement,
    createLanguage,
    updateLanguage,
    deleteProfile,
    deleteExperience,
    deleteEducation,
    deleteSkill,
    deleteAchievement,
    deleteLanguage,
} from "./lib/api";

import type {
    UserTemplate,
    ProfileTemplate,
    ExperienceTemplate,
    EducationTemplate,
    SkillTemplate,
    AchievementTemplate,
    LanguageTemplate,
} from "./lib/types";

import {
    Box,
    Container,
    Card,
    CardContent,
    CardHeader,
    Typography,
    Avatar,
    Stack,
    Button,
    Divider,
    List,
    ListItem,
    ListItemAvatar,
    ListItemText,
    CircularProgress,
    IconButton,
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    TextField,
    Alert,
} from "@mui/material";

import {
    Edit as EditIcon,
    Add as AddIcon,
    Logout as LogoutIcon,
    Work as WorkIcon,
    School as SchoolIcon,
    EmojiEvents as EmojiEventsIcon,
    Translate as TranslateIcon,
    Star as StarIcon,
    Person as PersonIcon,
    Close as CloseIcon,
} from "@mui/icons-material";
import ToggleLanguage from "./ToggleLanguage";

type Mode = "create" | "edit";
type Section = "profile" | "experience" | "education" | "skill" | "achievement" | "language";

function ProfilePage() {
    const { t, i18n } = useTranslation();
    const [user, setUser] = useState<UserTemplate | null>(null);
    const [error, setError] = useState("");
    const [busy, setBusy] = useState(false);

    const [open, setOpen] = useState(false);
    const [section, setSection] = useState<Section>("profile");
    const [mode, setMode] = useState<Mode>("create");
    const [formErr, setFormErr] = useState<string>("");

    const [profile, setProfile] = useState<ProfileTemplate>({
        fullName: "",
        city: "",
        country: "",
        description: "",
        profilePic: "",
        connectionCount: null,
    });
    const [profileId, setProfileId] = useState<number | null>(null);

    const [experience, setExperience] = useState<ExperienceTemplate>({
        company: "",
        title: "",
        startDate: "",
        endDate: null,
        city: "",
        country: "",
        description: "",
        logoPicUrl: "",
    });
    const [experienceId, setExperienceId] = useState<number | null>(null);

    const [education, setEducation] = useState<EducationTemplate>({
        school: "",
        degree: "",
        city: "",
        country: "",
        startYear: null,
        endYear: null,
        activities: "",
        logoPicUrl: "",
    });
    const [educationId, setEducationId] = useState<number | null>(null);

    const [skill, setSkill] = useState<SkillTemplate>({ name: "" });
    const [skillId, setSkillId] = useState<number | null>(null);

    const [achievement, setAchievement] = useState<AchievementTemplate>({
        title: "",
        year: null,
        description: "",
    });
    const [achievementId, setAchievementId] = useState<number | null>(null);

    const [language, setLanguage] = useState<LanguageTemplate>({ name: "" });
    const [languageId, setLanguageId] = useState<number | null>(null);

    const navigate = useNavigate();

    const userId = useMemo(() => {
        const id = sessionStorage.getItem("userId");
        return id ? Number(id) : null;
    }, []);

    useEffect(() => {
        if (!userId) {
            setError(t("auth.login") + " required");
            navigate("/login");
            return;
        }
        refresh();
    }, [userId]);

    const refresh = async () => {
        if (!userId) return;
        try {
            const data = await getUserById(userId);
            setUser(data);
        } catch (e: any) {
            setError(String(e));
        }
    };

    const handleLogout = () => {
        logout();
        navigate("/login");
    };

    const openCreate = (s: Section) => {
        setSection(s);
        setMode("create");
        setFormErr("");

        if (s === "profile") {
            setProfile({
                fullName: user?.profile?.fullName ?? "",
                city: user?.profile?.city ?? "",
                country: user?.profile?.country ?? "",
                description: user?.profile?.description ?? "",
                profilePic: user?.profile?.profilePic ?? "",
                connectionCount: user?.profile?.connectionCount ?? null,
            });
            setProfileId(null);
        }
        if (s === "experience") {
            setExperience({
                company: "",
                title: "",
                startDate: "",
                endDate: null,
                city: "",
                country: "",
                description: "",
                logoPicUrl: "",
            });
            setExperienceId(null);
        }
        if (s === "education") {
            setEducation({
                school: "",
                degree: "",
                city: "",
                country: "",
                startYear: null,
                endYear: null,
                activities: "",
                logoPicUrl: "",
            });
            setEducationId(null);
        }
        if (s === "skill") {
            setSkill({ name: "" });
            setSkillId(null);
        }
        if (s === "achievement") {
            setAchievement({ title: "", year: null, description: "" });
            setAchievementId(null);
        }
        if (s === "language") {
            setLanguage({ name: "" });
            setLanguageId(null);
        }
        setOpen(true);
    };

    const openEdit = (s: Section, data: any) => {
        setSection(s);
        setMode("edit");
        setFormErr("");

        if (s === "profile") {
            setProfile({
                fullName: data?.fullName ?? "",
                city: data?.city ?? "",
                country: data?.country ?? "",
                description: data?.description ?? "",
                profilePic: data?.profilePic ?? "",
                connectionCount: data?.connectionCount ?? null,
            });
            setProfileId(data?.id ?? null);
        }
        if (s === "experience") {
            setExperience({
                company: data.company ?? "",
                title: data.title ?? "",
                startDate: (data.startDate ?? "").toString(),
                endDate: data.isCurrent ? null : data.endDate ?? null,
                city: data.city ?? "",
                country: data.country ?? "",
                description: data.description ?? "",
                logoPicUrl: data.logoPicUrl ?? "",
            });
            setExperienceId(data.id);
        }
        if (s === "education") {
            setEducation({
                school: data.school ?? "",
                degree: data.degree ?? "",
                city: data.city ?? "",
                country: data.country ?? "",
                startYear: data.startYear ?? null,
                endYear: data.endYear ?? null,
                activities: data.activities ?? "",
                logoPicUrl: data.logoPicUrl ?? "",
            });
            setEducationId(data.id);
        }
        if (s === "skill") {
            setSkill({ name: data.name ?? "" });
            setSkillId(data.id);
        }
        if (s === "achievement") {
            setAchievement({
                title: data.title ?? "",
                year: data.year ?? null,
                description: data.description ?? "",
            });
            setAchievementId(data.id);
        }
        if (s === "language") {
            setLanguage({ name: data.name ?? "" });
            setLanguageId(data.id);
        }

        setOpen(true);
    };

    const handleSave = async () => {
        if (!userId) return;
        setBusy(true);
        setFormErr("");
        try {
            switch (section) {
                case "profile": {
                    await upsertProfile(userId, profile);
                    break;
                }
                case "experience": {
                    const body: ExperienceTemplate = {
                        company: experience.company,
                        title: experience.title,
                        startDate: experience.startDate,
                        endDate: experience.endDate ? experience.endDate : null,
                        city: experience.city,
                        country: experience.country,
                        description: experience.description ?? null,
                        logoPicUrl: experience.logoPicUrl ?? null,
                    };
                    if (mode === "create") await createExperience(userId, body);
                    else await updateExperience(userId, experienceId!, body);
                    break;
                }
                case "education": {
                    const body: EducationTemplate = {
                        school: education.school,
                        degree: education.degree,
                        city: education.city,
                        country: education.country,
                        startYear: education.startYear ?? null,
                        endYear: education.endYear ?? null,
                        activities: education.activities ?? null,
                        logoPicUrl: education.logoPicUrl ?? null,
                    };
                    if (mode === "create") await createEducation(userId, body);
                    else await updateEducation(userId, educationId!, body);
                    break;
                }
                case "skill": {
                    const body: SkillTemplate = { name: skill.name };
                    if (mode === "create") await createSkill(userId, body);
                    else await updateSkill(userId, skillId!, body);
                    break;
                }
                case "achievement": {
                    const body: AchievementTemplate = {
                        title: achievement.title,
                        year: achievement.year ?? null,
                        description: achievement.description ?? null,
                    };
                    if (mode === "create") await createAchievement(userId, body);
                    else await updateAchievement(userId, achievementId!, body);
                    break;
                }
                case "language": {
                    const body: LanguageTemplate = { name: language.name };
                    if (mode === "create") await createLanguage(userId, body);
                    else await updateLanguage(userId, languageId!, body);
                    break;
                }
            }
            setOpen(false);
            await refresh();
        } catch (e: any) {
            setFormErr(e?.message ?? t("common.save") + " failed");
        } finally {
            setBusy(false);
        }
    };

    const handleDelete = async () => {
        setBusy(true);
        setFormErr("");
        try {
            switch (section) {
                case "profile":
                    if (profileId != null) await deleteProfile(profileId);
                    break;
                case "experience":
                    if (experienceId != null) await deleteExperience(experienceId);
                    break;
                case "education":
                    if (educationId != null) await deleteEducation(educationId);
                    break;
                case "skill":
                    if (skillId != null) await deleteSkill(skillId);
                    break;
                case "achievement":
                    if (achievementId != null) await deleteAchievement(achievementId);
                    break;
                case "language":
                    if (languageId != null) await deleteLanguage(languageId);
                    break;
            }
            setOpen(false);
            await refresh();
        } catch (e: any) {
            setFormErr(e?.message ?? t("common.delete") + " failed");
        } finally {
            setBusy(false);
        }
    };

    if (error) {
        return <Alert severity="error">{error}</Alert>;
    }

    if (!user) {
        return (
            <Box sx={{ minHeight: "60vh", display: "grid", placeItems: "center" }}>
                <CircularProgress />
            </Box>
        );
    }

    const fullName = user.profile?.fullName ?? user.email;

    return (
        <Box sx={{ bgcolor: "#f7f7fb" }}>
            <Container maxWidth="md" sx={{ py: 6 }}>
                <Card sx={{ mb: 3, borderRadius: 3 }}>
                    <CardContent>
                        <Stack
                            direction={{ xs: "column", sm: "row" }}
                            alignItems={{ xs: "flex-start", sm: "center" }}
                            justifyContent="space-between"
                            gap={2}
                        >
                            <Stack direction="row" spacing={2} alignItems="center">
                                <Avatar sx={{ width: 72, height: 72 }}>
                                    {user.profile?.profilePic ? (
                                        <img
                                            src={user.profile.profilePic}
                                            alt="Profile"
                                            style={{ width: "100%", height: "100%", objectFit: "cover" }}
                                        />
                                    ) : (
                                        <PersonIcon />
                                    )}
                                </Avatar>
                                <Box>
                                    <Typography variant="h5" fontWeight={700}>
                                        {fullName}
                                    </Typography>
                                    <Typography variant="body2" color="text.secondary">
                                        {user.email}
                                    </Typography>
                                    <Typography variant="body2" color="text.secondary">
                                        {t("app.joined")} {new Date(user.createdAt).toLocaleDateString(i18n.language)}
                                    </Typography>
                                </Box>
                            </Stack>
                            <Stack direction="row" spacing={1}>
                                <ToggleLanguage />
                                <Button
                                    variant="outlined"
                                    startIcon={<EditIcon />}
                                    onClick={() => openEdit("profile", user.profile ?? {})}
                                    sx={{ textTransform: "none" }}
                                >
                                    {t("profile.edit")}
                                </Button>
                                <Button
                                    color="inherit"
                                    variant="text"
                                    startIcon={<LogoutIcon />}
                                    onClick={handleLogout}
                                    sx={{ textTransform: "none" }}
                                >
                                    {t("app.logout")}
                                </Button>
                            </Stack>
                        </Stack>
                        {user.profile && (
                            <Box sx={{ mt: 2 }}>
                                {user.profile.description && (
                                    <Typography variant="body1" sx={{ mt: 1.5 }}>
                                        <strong>{user.profile.description}</strong>
                                    </Typography>
                                )}
                                <Typography variant="body1">
                                    {user.profile.city}, {user.profile.country}
                                </Typography>
                                <Typography variant="body2" color="text.secondary" sx={{ mt: 1 }}>
                                    {t("profile.connections")}: {user.profile.connectionCount}
                                </Typography>
                            </Box>
                        )}
                    </CardContent>
                </Card>

                <Section title={t("profile.experiences")} icon={<WorkIcon />} onAdd={() => openCreate("experience")}>
                    {user.experiences?.length ? (
                        <List disablePadding>
                            {user.experiences.map((e) => (
                                <React.Fragment key={e.id}>
                                    <ListItem
                                        alignItems="flex-start"
                                        sx={{ px: 0 }}
                                        secondaryAction={
                                            <IconButton edge="end" aria-label={t("common.edit")} onClick={() => openEdit("experience", e)}>
                                                <EditIcon />
                                            </IconButton>
                                        }
                                    >
                                        <ListItemAvatar>
                                            <Avatar
                                                src={e.logoPicUrl ?? undefined}
                                                variant="square"
                                                sx={{
                                                    bgcolor: "transparent",
                                                    "& img": {
                                                        objectFit: "contain",
                                                        objectPosition: "center",
                                                    },
                                                }}
                                            >
                                                {!e.logoPicUrl && <WorkIcon />}
                                            </Avatar>
                                        </ListItemAvatar>
                                        <ListItemText
                                            primary={<Typography fontWeight={600}>{e.title}</Typography>}
                                            secondary={
                                                <>
                                                    <Typography fontWeight={600}>{e.company}</Typography>
                                                    <Typography variant="body2" color="text.secondary">
                                                        {e.city}, {e.country}
                                                    </Typography>
                                                    <Typography variant="body2" color="text.secondary">
                                                        {e.startDate} – {e.isCurrent ? t("common.present") : e.endDate}
                                                    </Typography>
                                                    {e.description && <Typography variant="body2" sx={{ mt: 0.5 }}>{e.description}</Typography>}
                                                </>
                                            }
                                        />
                                    </ListItem>
                                    <Divider sx={{ my: 1.5 }} />
                                </React.Fragment>
                            ))}
                        </List>
                    ) : (
                        <EmptyMessage text={t("common.noData")} />
                    )}
                </Section>

                <Section title={t("profile.education")} icon={<SchoolIcon />} onAdd={() => openCreate("education")}>
                    {user.educations?.length ? (
                        <List disablePadding>
                            {user.educations.map((ed) => (
                                <React.Fragment key={ed.id}>
                                    <ListItem
                                        alignItems="flex-start"
                                        sx={{ px: 0 }}
                                        secondaryAction={
                                            <IconButton edge="end" aria-label={t("common.edit")} onClick={() => openEdit("education", ed)}>
                                                <EditIcon />
                                            </IconButton>
                                        }
                                    >
                                        <ListItemAvatar>
                                            <Avatar
                                                src={ed.logoPicUrl ?? undefined}
                                                variant="square"
                                                sx={{
                                                    bgcolor: "transparent",
                                                    "& img": {
                                                        objectFit: "contain",
                                                        objectPosition: "center",
                                                    },
                                                }}
                                            >
                                                {!ed.logoPicUrl && <SchoolIcon />}
                                            </Avatar>
                                        </ListItemAvatar>
                                        <ListItemText
                                            primary={<Typography fontWeight={600}>{ed.degree}</Typography>}
                                            secondary={
                                                <>
                                                    <Typography fontWeight={600}>{ed.school}</Typography>
                                                    <Typography variant="body2" color="text.secondary">
                                                        {ed.city}, {ed.country}
                                                    </Typography>
                                                    <Typography variant="body2" color="text.secondary">
                                                        {ed.startYear} – {ed.endYear}
                                                    </Typography>
                                                    {ed.activities && <Typography variant="body2" sx={{ mt: 0.5 }}>{t("education.activities")}: {ed.activities}</Typography>}
                                                </>
                                            }
                                        />
                                    </ListItem>
                                    <Divider sx={{ my: 1.5 }} />
                                </React.Fragment>
                            ))}
                        </List>
                    ) : (
                        <EmptyMessage text={t("common.noData")} />
                    )}
                </Section>

                <Section title={t("profile.skills")} icon={<StarIcon />} onAdd={() => openCreate("skill")}>
                    {user.skills?.length ? (
                        <List disablePadding>
                            {user.skills.map((s) => (
                                <ListItem
                                    key={s.id}
                                    sx={{ px: 0 }}
                                    secondaryAction={
                                        <IconButton edge="end" aria-label={t("common.edit")} onClick={() => openEdit("skill", s)}>
                                            <EditIcon />
                                        </IconButton>
                                    }
                                >
                                    <ListItemText primary={<Typography fontWeight={600}>{s.name}</Typography>} />
                                </ListItem>
                            ))}
                        </List>
                    ) : (
                        <EmptyMessage text={t("common.noData")} />
                    )}
                </Section>

                <Section title={t("profile.achievements")} icon={<EmojiEventsIcon />} onAdd={() => openCreate("achievement")}>
                    {user.achievements?.length ? (
                        <List disablePadding>
                            {user.achievements.map((a) => (
                                <React.Fragment key={a.id}>
                                    <ListItem
                                        alignItems="flex-start"
                                        sx={{ px: 0 }}
                                        secondaryAction={
                                            <IconButton edge="end" aria-label={t("common.edit")} onClick={() => openEdit("achievement", a)}>
                                                <EditIcon />
                                            </IconButton>
                                        }
                                    >
                                        <ListItemAvatar>
                                            <Avatar>
                                                <EmojiEventsIcon />
                                            </Avatar>
                                        </ListItemAvatar>
                                        <ListItemText
                                            primary={<Typography fontWeight={600}>{a.title} {a.year ? `(${a.year})` : ""}</Typography>}
                                            secondary={a.description}
                                        />
                                    </ListItem>
                                    <Divider sx={{ my: 1.5 }} />
                                </React.Fragment>
                            ))}
                        </List>
                    ) : (
                        <EmptyMessage text={t("common.noData")} />
                    )}
                </Section>

                <Section title={t("profile.languages")} icon={<TranslateIcon />} onAdd={() => openCreate("language")}>
                    {user.languages?.length ? (
                        <List disablePadding>
                            {user.languages.map((l) => (
                                <ListItem
                                    key={l.id}
                                    sx={{ px: 0 }}
                                    secondaryAction={
                                        <IconButton edge="end" aria-label={t("common.edit")} onClick={() => openEdit("language", l)}>
                                            <EditIcon />
                                        </IconButton>
                                    }
                                >
                                    <ListItemText primary={<Typography fontWeight={600}>{l.name}</Typography>} />
                                </ListItem>
                            ))}
                        </List>
                    ) : (
                        <EmptyMessage text={t("common.noData")} />
                    )}
                </Section>
            </Container>

            <Dialog open={open} onClose={() => setOpen(false)} fullWidth maxWidth="sm">
                <DialogTitle sx={{ pr: 5 }}>
                    {mode === "create" ? t("common.new") : t("common.edit")}
                    <IconButton onClick={() => setOpen(false)} sx={{ position: "absolute", right: 8, top: 8 }} aria-label={t("common.cancel")}>
                        <CloseIcon />
                    </IconButton>
                </DialogTitle>
                <DialogContent dividers>
                    {formErr && (
                        <Alert severity="error" sx={{ mb: 2 }}>
                            {formErr}
                        </Alert>
                    )}
                    {section === "profile" && (
                        <Stack spacing={2}>
                            <TextField
                                label={t("profile.fullName")}
                                fullWidth
                                required
                                value={profile.fullName}
                                onChange={(e) => setProfile({ ...profile, fullName: e.target.value })}
                            />
                            <Stack direction={{ xs: "column", sm: "row" }} spacing={2}>
                                <TextField
                                    label={t("profile.city")}
                                    fullWidth
                                    value={profile.city ?? ""}
                                    onChange={(e) => setProfile({ ...profile, city: e.target.value })}
                                />
                                <TextField
                                    label={t("profile.country")}
                                    fullWidth
                                    value={profile.country ?? ""}
                                    onChange={(e) => setProfile({ ...profile, country: e.target.value })}
                                />
                            </Stack>
                            <TextField
                                label={t("profile.description")}
                                fullWidth
                                multiline
                                minRows={3}
                                value={profile.description ?? ""}
                                onChange={(e) => setProfile({ ...profile, description: e.target.value })}
                            />
                            <TextField
                                label={t("profile.profilePic")}
                                fullWidth
                                value={profile.profilePic ?? ""}
                                onChange={(e) => setProfile({ ...profile, profilePic: e.target.value })}
                            />
                            {!!profile.profilePic && (
                                <Box mt={1} display="flex" justifyContent="center">
                                    <img
                                        src={profile.profilePic}
                                        alt={t("profile.profilePic")}
                                        style={{ maxWidth: 120, height: 120, borderRadius: "50%", objectFit: "cover" }}
                                    />
                                </Box>
                            )}
                        </Stack>
                    )}
                    {section === "experience" && (
                        <Stack spacing={2}>
                            <Stack direction={{ xs: "column", sm: "row" }} spacing={2}>
                                <TextField
                                    label={t("experience.company")}
                                    fullWidth
                                    required
                                    value={experience.company}
                                    onChange={(e) => setExperience({ ...experience, company: e.target.value })}
                                />
                                <TextField
                                    label={t("experience.title")}
                                    fullWidth
                                    required
                                    value={experience.title}
                                    onChange={(e) => setExperience({ ...experience, title: e.target.value })}
                                />
                            </Stack>
                            <Stack direction={{ xs: "column", sm: "row" }} spacing={2}>
                                <TextField
                                    label={t("experience.startDate")}
                                    type="date"
                                    fullWidth
                                    required
                                    InputLabelProps={{ shrink: true }}
                                    value={experience.startDate || ""}
                                    onChange={(e) => setExperience({ ...experience, startDate: e.target.value })}
                                />
                                <TextField
                                    label={t("experience.endDate") + ` (${t("common.present")}=Ø)`}
                                    type="date"
                                    fullWidth
                                    InputLabelProps={{ shrink: true }}
                                    value={experience.endDate ?? ""}
                                    onChange={(e) => setExperience({ ...experience, endDate: e.target.value ? e.target.value : null })}
                                />
                            </Stack>
                            <Stack direction={{ xs: "column", sm: "row" }} spacing={2}>
                                <TextField
                                    label={t("profile.city")}
                                    fullWidth
                                    value={experience.city}
                                    onChange={(e) => setExperience({ ...experience, city: e.target.value })}
                                />
                                <TextField
                                    label={t("profile.country")}
                                    fullWidth
                                    value={experience.country}
                                    onChange={(e) => setExperience({ ...experience, country: e.target.value })}
                                />
                            </Stack>
                            <TextField
                                label={t("profile.description")}
                                fullWidth
                                multiline
                                minRows={3}
                                value={experience.description ?? ""}
                                onChange={(e) => setExperience({ ...experience, description: e.target.value })}
                            />
                            <TextField
                                label={t("experience.logoUrl")}
                                fullWidth
                                value={experience.logoPicUrl ?? ""}
                                onChange={(e) => setExperience({ ...experience, logoPicUrl: e.target.value })}
                            />
                        </Stack>
                    )}
                    {section === "education" && (
                        <Stack spacing={2}>
                            <Stack direction={{ xs: "column", sm: "row" }} spacing={2}>
                                <TextField
                                    label={t("education.school")}
                                    fullWidth
                                    required
                                    value={education.school}
                                    onChange={(e) => setEducation({ ...education, school: e.target.value })}
                                />
                                <TextField
                                    label={t("education.degree")}
                                    fullWidth
                                    required
                                    value={education.degree}
                                    onChange={(e) => setEducation({ ...education, degree: e.target.value })}
                                />
                            </Stack>
                            <Stack direction={{ xs: "column", sm: "row" }} spacing={2}>
                                <TextField
                                    label={t("profile.city")}
                                    fullWidth
                                    value={education.city}
                                    onChange={(e) => setEducation({ ...education, city: e.target.value })}
                                />
                                <TextField
                                    label={t("profile.country")}
                                    fullWidth
                                    value={education.country}
                                    onChange={(e) => setEducation({ ...education, country: e.target.value })}
                                />
                            </Stack>
                            <Stack direction={{ xs: "column", sm: "row" }} spacing={2}>
                                <TextField
                                    label={t("education.startYear")}
                                    type="number"
                                    fullWidth
                                    value={education.startYear ?? ""}
                                    onChange={(e) =>
                                        setEducation({
                                            ...education,
                                            startYear: e.target.value === "" ? null : Number(e.target.value),
                                        })
                                    }
                                />
                                <TextField
                                    label={t("education.endYear")}
                                    type="number"
                                    fullWidth
                                    value={education.endYear ?? ""}
                                    onChange={(e) =>
                                        setEducation({
                                            ...education,
                                            endYear: e.target.value === "" ? null : Number(e.target.value),
                                        })
                                    }
                                />
                            </Stack>
                            <TextField
                                label={t("education.activities")}
                                fullWidth
                                value={education.activities ?? ""}
                                onChange={(e) => setEducation({ ...education, activities: e.target.value })}
                            />
                            <TextField
                                label={t("experience.logoUrl")}
                                fullWidth
                                value={education.logoPicUrl ?? ""}
                                onChange={(e) => setEducation({ ...education, logoPicUrl: e.target.value })}
                            />
                        </Stack>
                    )}
                    {section === "skill" && (
                        <TextField
                            label={t("skill.name")}
                            fullWidth
                            required
                            value={skill.name}
                            onChange={(e) => setSkill({ ...skill, name: e.target.value })}
                        />
                    )}
                    {section === "achievement" && (
                        <Stack spacing={2}>
                            <TextField
                                label={t("achievement.title")}
                                fullWidth
                                required
                                value={achievement.title}
                                onChange={(e) => setAchievement({ ...achievement, title: e.target.value })}
                            />
                            <TextField
                                label={t("achievement.year")}
                                type="number"
                                fullWidth
                                value={achievement.year ?? ""}
                                onChange={(e) =>
                                    setAchievement({
                                        ...achievement,
                                        year: e.target.value === "" ? null : Number(e.target.value),
                                    })
                                }
                            />
                            <TextField
                                label={t("profile.description")}
                                fullWidth
                                multiline
                                minRows={2}
                                value={achievement.description ?? ""}
                                onChange={(e) => setAchievement({ ...achievement, description: e.target.value })}
                            />
                        </Stack>
                    )}
                    {section === "language" && (
                        <TextField
                            label={t("language.name")}
                            fullWidth
                            required
                            value={language.name}
                            onChange={(e) => setLanguage({ ...language, name: e.target.value })}
                        />
                    )}
                </DialogContent>
                <DialogActions sx={{ justifyContent: "space-between" }}>
                    <Box>
                        {mode === "edit" && (
                            <Button variant="outlined" color="error" onClick={handleDelete} disabled={busy}>
                                {busy ? t("common.delete") + "…" : t("common.delete")}
                            </Button>
                        )}
                    </Box>
                    <Box>
                        <Button onClick={() => setOpen(false)} disabled={busy} sx={{ mr: 1 }}>
                            {t("common.cancel")}
                        </Button>
                        <Button variant="contained" onClick={handleSave} disabled={busy}>
                            {busy ? t("common.save") + "…" : t("common.save")}
                        </Button>
                    </Box>
                </DialogActions>
            </Dialog>
        </Box>
    );
}
export default ProfilePage;

const Section = ({
    title,
    icon,
    onAdd,
    children,
}: {
    title: string;
    icon: React.ReactNode;
    onAdd: () => void;
    children: React.ReactNode;
}) => {
    const { t } = useTranslation();
    return (
        <Card sx={{ mb: 3, borderRadius: 3 }}>
            <CardHeader
                title={
                    <Stack direction="row" alignItems="center" spacing={1}>
                        {icon}
                        <Typography variant="h6" fontWeight={700}>
                            {title}
                        </Typography>
                    </Stack>
                }
                action={
                    <Button variant="contained" size="small" startIcon={<AddIcon />} onClick={onAdd} sx={{ textTransform: "none" }}>
                        {t("common.new")}
                    </Button>
                }
                sx={{ pb: 0, pt: 2.5 }}
            />
            <CardContent>{children}</CardContent>
        </Card>
    );
};

const EmptyMessage = ({ text }: { text: string }) => (
    <Typography variant="body2" color="text.secondary">
        {text}
    </Typography>
);
