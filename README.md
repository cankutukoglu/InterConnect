# InterConnect

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![React](https://img.shields.io/badge/React-19-61DAFB?logo=react)](https://react.dev/)
[![TypeScript](https://img.shields.io/badge/TypeScript-5.8-3178C6?logo=typescript)](https://www.typescriptlang.org/)
[![Vite](https://img.shields.io/badge/Vite-7-646CFF?logo=vite)](https://vitejs.dev/)
[![MUI](https://img.shields.io/badge/MUI-7-007FFF?logo=mui)](https://mui.com/)

A LinkedIn-style profile management case-study where users can manage their professional profiles including experience, education, skills, achievements, and languages.

## Features

- **JWT Authentication** - Secure user registration and login
- **Profile Management** - Create and edit professional profiles
- **Experience** - Add work history and professional experience
- **Education** - Track educational background
- **Skills** - Showcase technical and soft skills
- **Achievements** - Highlight honors and accomplishments
- **Languages** - List language proficiencies
- **i18n Support** - Multi-language interface (i18next)
- **Responsive Design** - Material UI components

## Tech Stack

### Backend
- **.NET 9** - ASP.NET Core Web API
- **Entity Framework Core** - ORM with Code-First migrations
- **SQLite** - Lightweight database
- **JWT Bearer** - Authentication
- **Swagger/OpenAPI** - API documentation

### Frontend
- **React 19** - UI library
- **TypeScript** - Type safety
- **Vite 7** - Build tool
- **Material UI 7** - Component library
- **React Router 7** - Client-side routing
- **i18next** - Internationalization

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js 18+](https://nodejs.org/) (with npm)
- [Git](https://git-scm.com/)

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/cankutukoglu/InterConnect.git
cd InterConnect
```

### 2. Install dependencies

```bash
# Install root dependencies (concurrently)
npm install

# Install frontend dependencies
cd frontend
npm install
cd ..
```

### 3. Configure the backend

```bash
cd backend/WebApplication1

# Restore .NET packages
dotnet restore

# Initialize user secrets and set JWT key
dotnet user-secrets init
dotnet user-secrets set "Jwt:Key" "YourSuperSecretKeyThatIsAtLeast32CharactersLong!"

# Apply database migrations
dotnet ef database update

cd ../..
```

### 4. Run the application

```bash
npm run dev
```

This starts both the API and frontend concurrently:
- **Frontend**: http://localhost:5173
- **API/Swagger**: https://localhost:5044/swagger

> **Note:** If you encounter HTTPS certificate issues, run: `dotnet dev-certs https --trust`

## Default Users

The database is seeded with test accounts:

| Email | Password |
|-------|----------|
| user1@mail.com | user1 |
| user2@mail.com | user2 |

You can also register new users via the application or Swagger UI.

## Project Structure

```
InterConnect/
├── backend/
│   └── WebApplication1/
│       ├── Controllers/     # API endpoints
│       ├── Models/          # Entity models
│       ├── Services/        # Business logic
│       ├── Migrations/      # EF Core migrations
│       └── data/            # DbContext
├── frontend/
│   ├── Views/               # React components
│   │   ├── lib/             # Utilities
│   │   └── locales/         # i18n translations
│   └── public/              # Static assets
└── package.json             # Root scripts
```

## Environment & Secrets

**Never commit secrets to version control.**

## Available Scripts

| Command | Description |
|---------|-------------|
| `npm run dev` | Start both API and frontend in development mode |
| `npm run dev` (in frontend/) | Start only the frontend |
| `dotnet watch run` (in backend/WebApplication1/) | Start only the API with hot reload |

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## Acknowledgments

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [React Documentation](https://react.dev)
- [Material UI](https://mui.com)
- [Vite](https://vitejs.dev)
