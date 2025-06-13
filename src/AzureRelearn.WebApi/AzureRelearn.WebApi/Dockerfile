# See https://aka.ms/customizecontainer to lear how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
# For more information, please see https://aka.ms/containercompat

# This stage is used when running from VS in fast mode (Default for Debug configuration)
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore
COPY AzureRelearn.WebApi.csproj ./
RUN dotnet restore ./AzureRelearn.WebApi.csproj

# Copy everything and publish
COPY . . 
RUN dotnet publish -c Release -o /app/out /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "AzureRelearn.WebApi.dll"]

#PS D:\Amit\azure-relearn-2025\src\AzureRelearn.WebApi\AzureRelearn.WebApi> docker build -t amitjoshipune/azure-relearn-webapi:v1 .
#PS D:\Amit\azure-relearn-2025\src\AzureRelearn.WebApi\AzureRelearn.WebApi> docker run -p 8083:80 -e ASPNETCORE_URLS=http://+:80 amitjoshipune/azure-relearn-webapi:v1
#PS D:\Amit\azure-relearn-2025\src\AzureRelearn.WebApi\AzureRelearn.WebApi> docker login
#PS D:\Amit\azure-relearn-2025\src\AzureRelearn.WebApi\AzureRelearn.WebApi> docker push amitjoshipune/azure-relearn-webapi:v1
# Best way run docker image is 
# PS D:\Amit\azure-relearn-2025\src\AzureRelearn.WebApi\AzureRelearn.WebApi> docker run -p 8084:80 -e ASPNETCORE_URLS=http://+:80 amitjoshipune/azure-relearn-webapi:v1 --name=azure-relearn-13June2025

