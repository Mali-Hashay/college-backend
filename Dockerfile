# כדי לבנות את האפליקציה. משתמש בתמונה הבסיסית של NET SDK.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# מעתיק את קבצי הפרויקט ומבצע restore
COPY *.sln ./
COPY College.collegeAPI/*.csproj College.collegeAPI/
COPY College.Core/*.csproj College.Core/
COPY College.Data/*.csproj College.Data/
COPY College.service/*.csproj College.service/
RUN dotnet restore

# מעתיק את שאר הקבצים ובונה את האפליקציה
COPY . ./
RUN dotnet publish College.collegeAPI -c Release -o out

# כדי להריץ את האפליקציה. משתמש בתמונה של NET Runtime.
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# מגדיר את הפורט שהאפליקציה תאזין עליו
EXPOSE 8080

# מפעיל את האפליקציה
ENTRYPOINT ["dotnet", "College.collegeAPI.dll"]