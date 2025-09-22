# WalletFlow

A full-stack personal finance tracker built with **React.js**, **ASP.NET Core**, and **PostgreSQL**.  
It allows users to record incomes and expenses, categorize transactions, and visualize cash flow with a clean and responsive interface.

---

## Tech Stack

- **Frontend:** React 18 (Vite) + TypeScript + Tailwind CSS + React Query + Axios
- **Backend:** ASP.NET Core 8 Web API + Entity Framework Core + JWT Authentication
- **Database:** PostgreSQL

---

## Features (Planned)

- User authentication (JWT)
- Add, edit, and delete income/expense transactions
- Categories and notes for transactions
- Summary dashboard (total income, expenses, balance)
- Data visualization with charts
- Responsive UI

---

### Clone the repository

```bash
git clone https://github.com/<your-username>/walletflow.git
cd walletflow
```

Backend (server)
```bash
cd server
dotnet restore
dotnet run
```

Frontend (client)
```bash
cd client
npm install
npm run dev
```