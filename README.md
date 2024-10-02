# submission-service
Service listening for submission objects via an API endpoint and saving them to an external database

## Quickstart

You can quickly build & serve the app using the following commands

```
docker build ./src/flamflam.SubmissionService -t submission-service-image
docker run -it --rm -p 5000:80 submission-service-image
```

Once done you can look at the API definitions on `http://localhost:5000/swagger/index.html`

## Prerequisites

In order to work the on the application you'd need the following: 
* [dotnet 7.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
    * [dotnet CLI](https://learn.microsoft.com/en-us/dotnet/core/tools/) is installed as part of the SDK
* [VS Code](https://code.visualstudio.com/) or an alternative editor
* [Docker Desktop](https://www.docker.com/products/docker-desktop/)
* [mongodb](https://www.mongodb.com/) connection string


## Tests

The app is relying on a set of the integration tests. 
In order to execute the test, run the following commands

```
cd .\src
dotnet test
```

## Configuration

In order to get the application to properly function, you'd need to specify the DB configruation. 

This can be done via [appsettings.json](./src/flamflam.SubmissionService/appsettings.json) file, or alternatively via environment variables, example below:

```
environment:
    FlamFlamDb:ConnectionString: [Connection string]
    FlamFlamDb:DatabaseName: [Db name]
    FlamFlamDb:SubmissionsCollectionName: submissions
```

> NOTE: if both values in `appsettings.json` & environment variables are supplied, the environment variables will be prioritized. 