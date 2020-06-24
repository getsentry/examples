



## Testing Sentry  SDK against GCP Cloud functions

##### To test Sentry SDK integration with Node GCP Cloud function, follow the below steps.

 - Create development package from the source code
 - Upload it into the GCP Cloud console
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


##### 4. Zip the contents of the folder so that it can be uploaded to GCP cloud function 
```html
$zip -r filename.zip *
```

## Upload Zip file to Cloud functions on GCP.

#####  Create a cloud function in Node by uploading the zip file created in 'creating development package' section, select "Cloud Pub/Sub" in Trigger dropdown, select 'Topic' in topic section and test cloud function. For more information refer to [GCP cloud function](https://cloud.google.com/functions/docs/deploying/console).


## Usage

#### 1. Initialization error:

This function contains code that instruments function initialization error. This is instrumented by calling an undefined function before invoking the handler.

##### Configuration: Set the following parameters in cloud function  dashboard for Initialization error :

```html
a) Function to execute : initialization
b) Timeout : 1 min
```
#### 2. Handled  exception:

This function contains code that instruments handle the exception.

##### Configuration: Set the following parameters in cloud function  dashboard for handled exception :

```html
a) Function to execute : handled_exception
```