import { useTranslation } from "react-i18next";
import { Select, MenuItem, FormControl, InputLabel } from "@mui/material";

function ToggleLanguage() {
    const { i18n } = useTranslation();

    return (
        <FormControl size="small" sx={{ minWidth: 120 }}>
            <InputLabel id="lang-label">Language</InputLabel>
            <Select
                labelId="lang-label"
                label="Language"
                value={i18n.resolvedLanguage}
                onChange={(e) => i18n.changeLanguage(e.target.value)}
            >
                <MenuItem value="en">English</MenuItem>
                <MenuItem value="tr">Türkçe</MenuItem>
            </Select>
        </FormControl>
    );
}
export default ToggleLanguage;