#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Web/EShop.Web/EShop.Web.csproj", "Web/EShop.Web/"]
COPY ["Services/EShop.Services/EShop.Services.csproj", "Services/EShop.Services/"]
COPY ["Models/EShop.Models.ViewModels/EShop.Models.ViewModels.csproj", "Models/EShop.Models.ViewModels/"]
COPY ["Data/EShop.Data/EShop.Data.csproj", "Data/EShop.Data/"]
COPY ["EShop.Common/EShop.Common.csproj", "EShop.Common/"]
COPY ["Data/EShop.Data.Models/EShop.Data.Models.csproj", "Data/EShop.Data.Models/"]
COPY ["Data/EShop.Data.Common/EShop.Data.Common.csproj", "Data/EShop.Data.Common/"]
COPY ["Models/EShop.Models.ServiceModels/EShop.Models.ServiceModels.csproj", "Models/EShop.Models.ServiceModels/"]
RUN dotnet restore "Web/EShop.Web/EShop.Web.csproj"
COPY . .
WORKDIR "/src/Web/EShop.Web"
RUN dotnet build "EShop.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EShop.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShop.Web.dll"]