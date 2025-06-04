# שלב 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# העתק את כל הקבצים מראש – כולל csproj
COPY . ./

RUN dotnet restore
RUN dotnet publish -c Release -o out

# שלב 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "BookManagementSystem.dll"]
