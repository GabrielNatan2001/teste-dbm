FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Shared/Communication/Communication.csproj", "Shared/Communication/"]
COPY ["Shared/Exceptions/Exceptions.csproj", "Shared/Exceptions/"]
COPY ["Backend/Application/Application.csproj", "Backend/Application/"]
COPY ["Backend/Domain/Domain.csproj", "Backend/Domain/"]
COPY ["Backend/Infraestructure/Infraestructure.csproj", "Backend/Infraestructure/"]
COPY ["Backend/WebAPI/API.csproj", "Backend/WebAPI/"]
RUN dotnet restore "Backend/WebAPI/API.csproj"
COPY ["Shared/Communication/", "Shared/Communication/"]
COPY ["Shared/Exceptions/", "Shared/Exceptions/"]
COPY ["Backend/Application/", "Backend/Application/"]
COPY ["Backend/Domain/", "Backend/Domain/"]
COPY ["Backend/Infraestructure/", "Backend/Infraestructure/"]
COPY ["Backend/WebAPI/", "Backend/WebAPI/"]
WORKDIR "/src/Backend/WebAPI"
RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "API.dll"]