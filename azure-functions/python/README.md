

## Testing Sentry SDK against Azure Function app

##### To test Sentry SDK integration with Azure Function app, follow the below steps.

 - Create development package from the source code
 - Add the file into the Azure Function app
 - Refer usage for use case specific handling 


## Create a development package 

##### 1. Clone the git repo on your development machine.

##### 2. Cloned repo contains multiple folders, each of these folder contains an example of how we can capture errors/exceptions on sentry dashboard.

##### 3. Go to any folder and edit the python source file. Edit below section of the file where you replace 'dsn' with your own DSN. 
```
sentry_sdk.init(
    dsn="<your DSN>"
) 
```


## Add file to Functions on Azure Function app.

#####  Create a function app in Python Runtime stack and add Functions by selecting "HTTP" Trigger and add file created in 'creating development package' section. For more information refer to [Azure Function app](https://docs.microsoft.com/en-us/azure/azure-functions/functions-reference-python).


## Usage


#### 1. Handled exception:

This function contains code that instruments function handled exception. This is instrumented by capturing an Exception in the function app.

##### Configuration: Set the following parameters in host.json file  in Function app for Handled exception:

```html
a) File to execute : __init__.py
b) "functionTimeout": "00:01:00"
```

#### 2. Unhandled exception:

This function contains code that instruments function unhandled exception. This is instrumented by not capturing an Exception in the function app.

##### Configuration: Set the following parameters in host.json file  in Function app for Unhandled exception:

```html
a) File to execute : __init__.py
b) "functionTimeout": "00:01:00"
```

#### 3. Custom tags & Context:
This function contains code that instruments the handled exception and creates custom tag & context.

##### Configuration: Set the following parameters in Application settings inside Configuration Function app dashboard for Custom tags & Context:
```html
a) Function to execute : __init__.py
b) Environment Variables :-
   Key : ENV_VAR
   Value : Test Value
```