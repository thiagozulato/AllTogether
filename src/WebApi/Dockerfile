FROM microsoft/dotnet:2.1-sdk-alpine AS build-publish
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet build --no-restore -c Release
RUN dotnet publish --no-restore -c Release -o pub_folder

FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine AS publish
WORKDIR /app
COPY --from=build-publish /app/pub_folder ./
EXPOSE 80
ENTRYPOINT [ "dotnet", "WebApi.dll" ]