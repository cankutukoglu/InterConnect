# InterConnect (ASP.NET + TypeScript & React)

## Overview
InterConnect is a small LinkedIn-style profile app where you can add, edit and delete profile features, with JWT auth, SQLite, EF Core, and a React (Vite + MUI) frontend.

## Tech Stack
- Backend: .NET 8, ASP.NET Minimal APIs, EF Core, SQLite, JWT, CORS configuration
- Frontend: React + Vite + MUI, i18next

## Prerequisites
- .NET 8 SDK
- Node 18+ / npm
- SQLite

## Setup

1. From project repository root, install root dependencies (for concurrently)
```
npm i
```
2. Navigate to the frontend directory, and install frontend dependencies
```
cd frontend
npm install
```
3. Navigate to the backend/WebApplication1 directory, and setup backend
```
cd ../backend/WebApplication1
dotnet restore
dotnet ef database update
```
4. Navigate to the project repository root, and start both API and Web by "npm run dev" command
```
cd ../../
npm run dev
```
5. Access the localhost port printed on the terminal. Example output:
```
web  |   VITE v7.1.2  ready in 159 ms
web  |
web  |   ➜  Local:   http://localhost:5173/
web  |   ➜  Network: http://172.18.0.3:5173/
```

Defaults to http://localhost:5173/

If you want, you can also reach the swagger from https://localhost:5044/swagger/index.html

If there happens an issue about HTTPS certs:
```
dotnet dev-certs https --trust
```

### Secrets
To avoid publishing secrets, do not commit `Jwt:Key`. In development, store it using .NET User Secrets:
```
cd backend/WebApplication1
dotnet user-secrets init
dotnet user-secrets set "Jwt:Key" "<your-strong-secret>"
```
For production, set the secret via environment variables (e.g., `Jwt__Key`) or your secrets manager.

### Default Users
You can reach the default user accounts by the following mail and passwords, you can also register new users from the swagger yourself too.

- User1
E-mail: user1@mail.com
Password: user1

- User2
E-mail: user2@mail.com
Password: user2
