# WebSocketAPI README

## Overview

This project is an ASP.NET Core WebSocket/SignalR API for real-time order and customer updates, backed by a SQL Server database using Entity Framework Core (EF Core).

---

## How to Run Locally

### Configure Database Connection

The connection string is specified in `appsettings.json`:

"ConnectionStrings": {
"DefaultConnection": "Server={MachineHostName};Database=WSDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"
}


- Replace `{MachineHostName}` with your SQL Server hostname or IP.
- Ensure the database `WSDB` exists or will be created by EF Core migrations.
- `Trusted_Connection=True` enables Windows Authentication.

### Restore and Build

From your project directory, run:

dotnet restore
dotnet build


### Apply EF Core Migrations

If migrations haven’t been applied yet, run:
dotnet ef database update



### Run the Application

Start the application with:

dotnet run



Your API will listen on the configured ports (check console output or `launchSettings.json`).

---

## Steps to Test the WebSocket API (SignalR)

### Access the Test Client

- Open `"https://localhost:3000"` in a modern web browser.

### Subscribe to Data

- Click the Subscribe buttons on the test page.
- When prompted, enter a valid `CustomerId` like `"123"`.
- Live updates will stream in and appear in the UI output area.
- Try changing data by sending request from postman, the Data will be UPDATED in the UI output area

### Stop the Connection

- Use the Stop buttons provided to cleanly disconnect from the SignalR hub.

---

## IIS Deployment Notes

### Publish the Project

- Use Visual Studio’s Publish wizard or the CLI command `dotnet publish` to generate the deployment-ready files.

### IIS Setup

- Install IIS and the ASP.NET Core Hosting Bundle.
- Create a new IIS website or application.
- Set the physical path to the published output folder.
- Configure the application pool to **No Managed Code**.
- Ensure appropriate filesystem permissions for the IIS user.

### Web.config

- Confirm the `web.config` file is present in your published folder; it is generated during the publish step for ASP.NET Core hosting.

### Enable WebSocket Support in IIS

- Go to “Turn Windows features on or off”.
- Under **Internet Information Services > WWW Services > Application Development Features**, enable **WebSocket Protocol**.
- Restart IIS to apply changes.

### Firewall Configuration

- Allow inbound traffic on the port IIS is listening on.

---

## Additional Tips

- Use browser developer tools to monitor network and WebSocket traffic for troubleshooting.
- Check IIS logs if SignalR connections fail behind IIS.

---

## Quick Reference

- Set up and run your app locally.
- Create and migrate the database.
- Open the UI test page.
- Query customers or orders by `CustomerId`.
- Use Postman to add or update customer/order entries.
- Watch live updates appear in the UI.

---

