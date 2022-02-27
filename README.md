# LoanNaja

This is a exmaple solution for TDD training. A C# port of this [sea-tdd-workshop-09-2021](https://github.com/ThoughtWorksInc/sea-tdd-workshop-09-2021)

## Structure

|  Project | Description  |
|---|---|
|  LoanNaja.Core |  Business logic and infrastucture |
|  LoanNaja |  Web |
|  LoanNaja.Core.Test | Test for business logic  |
|  LoanNaja.Test | Test for controller using mock  |
|  LoanNaja.IntegrationTest |  Integration Test using WebApplicationFactory |
|  LoanNaja.Spec   |  Black box test  |
|  LoanNaja.ContractTest   | Contact test   |


# Useful command

1. Add a coverlet to your test project

    ```text
    dotnet add package coverlet.msbuild
    ```

1. Check for code coverage percentage

    ```text
    dotnet test /p:CollectCoverage=true
    ```

1. Add a mock for a parent class

    ```text
    Mock<IDerived> mock = new Mock<IDerived>( MockBehavior.Strict );
    mock.Setup( obj => obj.Value ).Returns( "Derived" );
    mock.As<IBase>().Setup( obj => obj.Value ).Returns( "Base" );
    ```

1. Format your C# code using [csharpier](https://github.com/belav/csharpier)

    ```txt
    dotnet-csharpier 
    ```

1. Run a server for acceptance tests

    ```txt
    docker build -t loan-naja -f ./src/LoanNaja/Dockerfile .
    docker run -d --name loannaja -p 8080:80 loan-naja:latest
    ```

1. See server logs

    ```txt
    docker logs loannaja
    ```

1. Close a server

    ```txt
    docker stop loannaja
    docker rm loannaja
    ```

1. Check access to your server

    ```txt
    curl http://docker.local:8080
    ```

1. Run test except an acceptance test

    ```txt
    dotnet test --filter "Category!=Spec"
    ```

1. Run acceptance tests

    ```txt
    docker-compose up --exit-code-from acceptancetests
    ```
    
    ```txt
    docker-compose up --exit-code-from acceptancetests --build
    docker-compose up --exit-code-from acceptancetests --no-cache
    ```

## Need help?

Go check [Setup](doc/SETUP.md)
