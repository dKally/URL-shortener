# URL Shortener API (C# .NET Core)

This repository contains a simple URL shortening API built with C# and ASP.NET Core, designed as a learning exercise. It demonstrates core concepts of web development, including API design, database interaction, and client-server communication.

## 🚀 Features

- **Shorten URLs:** Converts long URLs into short, easy-to-share links.
- **Redirection:** When accessing a shortened URL, it redirects to the original long URL.
- **Database Storage:** Uses SQLite (via Entity Framework Core) to persist URL mappings.
- **API Documentation:** Integrated Swagger UI for easy testing and documentation of API endpoints.
- **CORS Enabled:** Configured to allow requests from different origins (e.g., a separate frontend application).

## 🛠️ Technologies Used

- **Backend:** C# (.NET 8.0)
- **Web Framework:** ASP.NET Core Web API
- **Database:** SQLite
- **ORM:** Entity Framework Core
- **API Documentation:** Swashbuckle.AspNetCore (Swagger/OpenAPI)

## 💡 Learning Focus

This project was developed with a focus on understanding:

- **RESTful API Design:** Creating endpoints (`POST` for shortening, `GET` for redirection).
- **Dependency Injection:** Managing services like `DbContext`.
- **Database Migrations:** Using Entity Framework Core migrations to manage database schema.
- **Data Models (POCOs):** Defining simple classes to represent data.
- **Middleware Configuration:** Setting up CORS, Swagger, and routing in `Program.cs`.
- **Basic Security Concepts:** Handling URL validation and simple short code generation.

## ⚙️ How to Run Locally

### Prerequisites

- [.NET SDK 8.0 (or newer)](https://dotnet.microsoft.com/download)
- [dotnet-ef global tool](https://learn.microsoft.com/en-us/ef/core/cli/dotnet#installing-the-ef-core-tools) (if not already installed):

```bash
    dotnet tool install --global dotnet-ef
```

### Backend (API) Setup

1.  **Clone the repository:**

```bash
    git clone https://github.com/dKally/URL-shortener
    cd UrlShortenerApi
```

2.  **Restore NuGet packages:**

```bash
    dotnet restore
```

3.  **Apply database migrations:**
    This will create the `urls.db` SQLite database file.

```bash
    dotnet ef database update
```

4.  **Run the API:**

```bash
    dotnet run
```

The API will typically run on `http://localhost:5000` (HTTP) and `https://localhost:5001` (HTTPS). You can access the Swagger UI at `https://localhost:XXXX/swagger` (replace XXXX with your HTTPS port).

### Frontend (HTML/CSS/JS) Setup

This project uses a simple static HTML/CSS/JS frontend.

1.  Navigate to the `Frontend` directory.
2.  Open `index.html` in your web browser. For a better development experience (e.g., avoiding CORS issues with file:// protocol), you might want to use a simple local HTTP server (e.g., VS Code's Live Server extension, or `python -m http.server` if you have Python installed).
3.  **Important:** Ensure the `API_BASE_URL` in `script.js` matches the address where your API is running (e.g., `http://localhost:5000`).

## 🎯 How to Use

1.  With the API and Frontend running, open the `index.html` page in your browser.
2.  Enter a long URL into the input field.
3.  Click "Shorten Link".
4.  The shortened URL will appear below. Click it to be redirected to the original URL!
