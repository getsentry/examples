## Testing Sentry  SDK against Lambda functions

##### To test Sentry SDK integration with Python lambda function, follow the below steps.

 - Create development package from the source code
 - Upload it into Lambda console
 - Refer usage for use case specific handling 


## Create a development package

##### 1. Clone the git repo on your development machine

##### 2. Cloned repo contains multiple folders, each of these folder contains an example of how we can capture errors/exceptions on sentry dashboard 

##### 3. Go to any folder and edit the python source file. Edit below section of the file where you replace 'dsn' with your own DSN 
```python
sentry_sdk.init(
    dsn="<your DSN>",
    integrations=[AwsLambdaIntegration()]
)
```
##### 4. Install the dependencies using following command. 

```html
pip install -r requirements.txt
```

##### 5. Zip the contents of the folder so that it can be uploaded to lambda 
```html
$zip filename.zip *.py *.txt
```

### Uploading the Lambda function zip file from the local machine and execution.

#####  Create a Lambda function in Python by uploading the zip file created in 'creating development package' section with LambdaAdminAccess and test event configured. For more information refer to [Python Lambda function](https://docs.aws.amazon.com/lambda/latest/dg/lambda-python.htm).


## Usage

#### 1. Out of memory:

This AWS Lambda function contains code that consumes memory limit equal to set memory limit in the configuration.

##### Configuration: Set the following parameters in Lambda dashboard for Out of memory:
```html
a) Handler : out_of_memory.lambda_handler
b) Memory (MB) : 128 MB 
c) Timeout : 1 min
```

#### 2. Invalid Handler error:

This Lambda function contains code that instruments Lambda invalid handler error.

##### Configuration: Set the following parameters in Lambda dashboard for Invalid handler error:
```html
a) Handler : invalid_handler.lambda_handler_changed
```

#### 3. Handled exception:

This function contains code that instruments handled exception.

##### Configuration: Set the following parameters in Lambda dashboard for Handled exception:
```html
a) Handler : handled_exception.lambda_handler
```

#### 4. Unhandled exception:

This function contains code that instruments unhandled exception.

##### Configuration: Set the following parameters in Lambda dashboard for Unhandled exception:
```html
a) Handler : unhandled_exception.lambda_handler
```

#### 5. Network error:
This function contains code that instruments the network error using different scenarios.

>**Scenario: Wrong IP address**
 
In this scenario, we put the wrong IP address.

##### Configuration: Set the following parameters in Lambda dashboard for wrong IP address scenario :

```html
a) Handler : network_error_wrong_address.handler 
b) Timeout : 2 min 30 sec
```

>**Scenario: Wrong PORT number**
 
In this scenario, we put the wrong PORT number.

##### Configuration: Set the following parameters in Lambda dashboard for wrong PORT number scenario :

```html
a) Handler : network_error_wrong_port.handler 
b) Timeout : 2 min 30 sec
```

#### 6. Custom tags & Context:
This function contains code that instruments the handled exception and creates custom tag & context.

##### Configuration: Set the following parameters in Lambda dashboard for Custom tags & Context:
```html
a) Handler : custom_tag_and_context.lambda_handler
b) Environment Variables :-
   Key : ENV_VAR
   Value : Test Value
```
