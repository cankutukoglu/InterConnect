import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import LanguageDetector from "i18next-browser-languagedetector";

import en from "./locales/en/common.json";
import tr from "./locales/tr/common.json";

void i18n
    .use(LanguageDetector)
    .use(initReactI18next)
    .init({
        resources: {
            en: { translation: en },
            tr: { translation: tr }
        },
        supportedLngs: ["en", "tr"],
        fallbackLng: "en",
        detection: {
            order: ["querystring", "localStorage", "navigator", "htmlTag"],
            caches: ["localStorage"]
        },
        interpolation: {
            escapeValue: false
        }
    });

i18n.on("languageChanged", (lng) => {
    if (typeof document !== "undefined") {
        document.documentElement.lang = lng;
    }
});
export default i18n;
