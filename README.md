# submission-service
Service listening for submission objects via an API endpoint and saving them to an external database

## Quickstart

You can quickly build & serve the app using the following commands

```
cd .\src
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


## Tests

The app is relying on a set of the integration tests. 
In order to execute the test, run the following commands

```
cd .\src
dotnet test
```