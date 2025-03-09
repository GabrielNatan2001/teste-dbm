FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Shared/Communication/Communication.csproj", "Shared/Communication/"]
COPY ["Shared/Exceptions/Exceptions.csproj", "Shared/Exceptions/"]
COPY ["Frontend/MVC/MVC.csproj", "Frontend/MVC/"]
RUN dotnet restore "Frontend/MVC/MVC.csproj"
COPY ["Shared/Communication/", "Shared/Communication/"]
COPY ["Shared/Exceptions/", "Shared/Exceptions/"]
COPY ["Frontend/MVC/", "Frontend/MVC/"]
WORKDIR "/src/Frontend/MVC"
RUN dotnet build "MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MVC.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "MVC.dll"]