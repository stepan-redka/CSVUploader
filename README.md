# TestApp

TestApp is a web application designed to manage contacts and documents efficiently. It provides features such as uploading CSV files, inline editing, and a user-friendly interface for managing data.

## Features
- **Contact Management**: Add, edit, and delete contacts with ease.
- **Document Upload**: Upload and organize documents in CSV format.
- **Inline Editing**: Quickly update data directly in the table.
- **Search and Filter**: Advanced search functionality to find data quickly.
- **Responsive Design**: Optimized for all devices.

## Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/)
- A modern web browser

## Setting Up SQL Server with Docker
This project uses SQL Server running in a Docker container. Follow these steps to set it up:

1. Pull the SQL Server Docker image:
   ```bash
   docker pull mcr.microsoft.com/mssql/server:2025-latest
   ```

2. Run the SQL Server container:
   ```bash
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" \
      -p 1433:1433 --name sql1 --hostname sql1 \
      -d \
      mcr.microsoft.com/mssql/server:2025-latest
   ```
   Replace `<password>` with a strong password.

3. Verify the container is running:
   ```bash
   docker ps
   ```

## Configuration
Update the connection string in `Program.cs` to match your SQL Server setup:
```csharp
builder.Services.AddDbContext<DbAppContext>(options =>
{
    options.UseSqlServer(
        "Server=localhost,1433;Database=TestDb;User Id=sa;Password=<password>;TrustServerCertificate=True");
});
```
Replace `<password>` with the password you set for the SQL Server container.

## Running the Application
1. Clone the repository:
   ```bash
   git clone https://github.com/stepan-redka/testApp.git
   ```

2. Navigate to the project directory:
   ```bash
   cd testApp/TestApp
   ```

3. Restore dependencies:
   ```bash
   dotnet restore
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. Open your browser and navigate to `http://localhost:5000`.

## Usage
- Navigate to the **Home** page to see an overview of the application.
- Use the **Upload** page to upload CSV files.
- Manage your contacts and documents on the **Documents** page.

## Technologies Used
- **ASP.NET Core**: Backend framework
- **Entity Framework Core**: ORM for database operations
- **SQL Server**: Database
- **Docker**: Containerized SQL Server
- **Bootstrap**: Frontend styling

## License
This project is licensed under the MIT License. See the LICENSE file for details.

## Contributing
Contributions are welcome! Feel free to open issues or submit pull requests.
