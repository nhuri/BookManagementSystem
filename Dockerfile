FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app

# לא עושים COPY מראש כי אנחנו משתמשים ב-volume mount

EXPOSE 80

CMD ["dotnet", "watch", "run", "--urls=http://+:80"]
