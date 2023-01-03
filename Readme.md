# ASP.NET Core Web API Serverless Application

This project is all about a simple wrapper POSTCODE API service implemented as an ASP.NET Core Web API project as an AWS Lambda exposed through Amazon API Gateway. 


### API Details ###


    I have deployed this API in AWS and below are the API Details.

    API Url : https://c8gur7sev1.execute-api.us-east-1.amazonaws.com/Prod

```
Methods :
  
## 1. Path : /Postcode/AutoComplete ##
       Type : GET
       Parameters: {

                    "name": "postcode",
                    "in": "query",
                    "schema": {
                    "type": "string"

                   }

        Response: Array of strings.

```

```
  
## 2. Path : /Postcode/Lookup ##
       Type : GET
       Parameters: {

                    "name": "postcode",
                    "in": "query",
                    "schema": {
                    "type": "string"

                   }

        Response: PostcodeInfo Object.

```



    I have enabled swagger to test API functionality.
    Swagger URl : https://c8gur7sev1.execute-api.us-east-1.amazonaws.com/Prod/swagger/index.html



### Steps to deploy application
```
    Open the command prompt and navigate to Postcode API solution folder where serverless template exists and run the below command.
    command : dotnet lambda deploy-serverless
```
