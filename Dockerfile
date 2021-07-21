# base image
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

# baby of mkdir and cd
WORKDIR /app

COPY *.sln ./
COPY BL/*.csproj BL/
COPY DL/*.csproj DL/
COPY WebUI/*.csproj WebUI/
COPY Models/*.csproj Models/

#Restores any deps that we would need
# if the csproj files have not changed since the last build
# on this laptop, then, the above layers will be recovered from cache,
# and we don't need to run restore again.
RUN cd WebUI && dotnet restore

# we use .dockerignore to hide files from being copied with
# the build context, so COPY here won't see them either.
# which files? bin/, obj/, etc.

# copy the source code
COPY . ./
# CMD /bin/bash

# Publishes the application and its dependencies to a folder for deployment to a hosting system.
RUN dotnet publish WebUI -c Release -o publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime

WORKDIR /app
# From the build stage, I wanna get the published version of my app
COPY --from=build /app/publish ./

CMD ["dotnet", "WebUI.dll"]
#this final image does not have the SDK, nor the src code, only
# 1 what's in the base image (the runtime)
# 2 my published build output