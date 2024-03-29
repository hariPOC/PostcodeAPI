# Postcode Web API Serverless Application

This project is all about a simple wrapper POSTCODE API service implemented as an AWS Lambda exposed through Amazon API Gateway. 


### API Details ###


   - I have deployed this Postcode API in AWS and below are the API Details.

   - API Base Url : https://c8gur7sev1.execute-api.us-east-1.amazonaws.com/Prod
   - Sample API Calls :
     1.  https://c8gur7sev1.execute-api.us-east-1.amazonaws.com/Prod/Postcode/AutoComplete?postcode=s 
     2.  https://c8gur7sev1.execute-api.us-east-1.amazonaws.com/Prod/Postcode/Lookup?postcode=S10%201AE


## API Methods :
```

## 1. Path : /Postcode/AutoComplete 
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
  
## 2. Path : /Postcode/Lookup 
       Type : GET
       Parameters: {

                    "name": "postcode",
                    "in": "query",
                    "schema": {
                    "type": "string"

                   }

        Response: PostcodeInfo Object.

```

   - I have enabled swagger to test API functionality.
   - Swagger URl : https://c8gur7sev1.execute-api.us-east-1.amazonaws.com/Prod/swagger/index.html

### Steps to deploy application

   - Open the command prompt and navigate to Postcode API solution folder where serverless template exists and run the below command.
   - Command : "dotnet lambda deploy-serverless"

