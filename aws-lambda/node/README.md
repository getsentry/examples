

## Testing Sentry  SDK against Lambda functions

##### To test Sentry SDK integration with Node lambda function, follow the below steps.

 - Create development package from the source code
 - Upload it into Lambda console
 - Refer usage for use case specific handling 

## Create a development package

##### 1. Clone the git repo on your development machine

##### 2. Cloned repo contains multiple folders, each of these folder contains an example of how we can capture errors/exceptions on sentry dashboard 

##### 3. Go to any folder and edit the node source file. Edit below section of the file where you replace 'dsn' with your own DSN 
```
Sentry.init({
  dsn: "<your DSN>",
}); 
```
##### 4. Install the dependencies using following command. 

```html
npm install
```

##### 5. Zip the contents of the folder so that it can be uploaded to lambda 
```html
$zip -r filename.zip *
```

## Upload Zip file to Lambda function on AWS.

#####  Create a Lambda function in Node by uploading the zip file created in 'creating development package' section with LambdaAdminAccess and test event configured. For more information refer to [Node Lambda function](https://docs.aws.amazon.com/lambda/latest/dg/lambda-nodejs.htm).


## Usage

#### 1. Initialization error:

This function contains code that instruments lambda initialization error. This is instrumented by calling undefined function before invoking the Lambda handler.

##### Configuration: Set the following parameters in Lambda dashboard for Initialization error :

```html
a) Handler : initialization.handler 
b) Timeout : 1 min
```

#### 2. Handled exception:

This function contains code that instruments handled the exception.

##### Configuration: Set the following parameters in Lambda dashboard for handled exception :

```html
a) Handler : handled_exception.handler
```

#### 3. Network error:

This function contains code that instruments the network error using different scenarios.

> **Scenario: Wrong IP address**

In this scenario, we put the wrong IP address.

##### Configuration: Set the following parameters in Lambda dashboard for wrong IP address scenario :

```html
a) Handler : network_error_wrong_address.handler
b) Timeout : 10 sec
```

> **Scenario: Wrong PORT number**

In this scenario, we put the wrong PORT number.

##### Configuration: Set the following parameters in Lambda dashboard for wrong PORT number scenario :

```html
a) Handler : network_error_wrong_port.handler 
b) Timeout : 10 sec
```

#### 4. Custom tags & Context:
This function contains code that instruments the handled exception and creates custom tag & context.

##### Configuration: Set the following parameters in Lambda dashboard for Custom tags & Context:
```html
a) Handler : custom_tag_and_context.handler
b) Environment Variables :-
   Key : ENV_VAR
   Value : env_variable_value
```