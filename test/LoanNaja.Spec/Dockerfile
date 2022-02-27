FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app
COPY test/LoanNaja.Spec .
RUN dotnet restore
RUN dotnet test