

## Testing Sentry SDK against GCP Cloud functions

##### To test Sentry SDK integration with Python GCP Cloud function, follow the below steps.

 - Create development package from the source code
 - Upload the file into the GCP Cloud console
 - Refer usage for use case specific handling 


## Create a development package 

##### 1. Clone the git repo on your development machine

##### 2. Cloned repo contains multiple folders, each of these folder contains an example of how we can capture errors/exceptions on sentry dashboard 

##### 3. Go to any folder and edit the python source file. Edit below section of the file where you replace 'dsn' with your own DSN. 
```
sentry_sdk.init(
    dsn="<your DSN>",
    integrations=GcpIntegration()]
)
```

##### 4. Zip the contents of the folder so that it can be uploaded to cloud function. 
```html
$zip -r filename.zip *
```

## Upload Zip file to Cloud functions on GCP.

#####  Create a cloud function in Python by uploading the zip file created in 'creating development package' section with select "Cloud Pub/Sub" Trigger dropdown, select 'Topic' in topic section and test cloud function. For more information refer to [GCP cloud function](https://cloud.google.com/functions/docs/deploying/console).


## Usage

#### 1. Handled exception:

This function contains code that instruments function handled exception. This is instrumented by capturing an Exception in the cloud handler.

##### Configuration: Set the following parameters in cloud function dashboard for Handled exception:

```html
a) Function to execute : cloud_handler
b) Timeout : 1 min
```

#### 2. Unhandled exception:

This function contains code that instruments function unhandled exception. This is instrumented by not capturing an Exception in the cloud handler.

##### Configuration: Set the following parameters in cloud function dashboard for Unhandled exception:

```html
a) Function to execute : cloud_handler
b) Timeout : 1 min
```

#### 3. Network error:
This function contains code that instruments the network error using different scenarios.

>**Scenario: Wrong IP address**
 
In this scenario, we put the wrong IP address.

##### Configuration: Set the following parameters in cloud function dashboard for wrong IP address scenario:

```html
a) Function to execute : cloud_handler
b) Timeout : 2 min 30 sec
```

>**Scenario: Wrong PORT number**
 
In this scenario, we put the wrong PORT number.

##### Configuration: Set the following parameters in cloud function dashboard for wrong PORT number scenario:

```html
a) Function to execute : cloud_handler 
b) Timeout : 2 min 30 sec
```

#### 4. Custom tags & Context:
This function contains code that instruments the handled exception and creates custom tag & context.

##### Configuration: Set the following parameters in cloud function dashboard for Custom tags & Context:
```html
a) Function to execute : cloud_handler
b) Environment Variables :-
   Key : ENV_VAR
   Value : Test Value
```