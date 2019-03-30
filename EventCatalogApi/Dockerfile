FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /app
EXPOSE 80

COPY . .
RUN dotnet restore EventCatalogApi.csproj


FROM build AS publish
RUN dotnet publish "EventCatalogApi.csproj" -c Release -o /app


WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "EventCatalogApi.dll"]