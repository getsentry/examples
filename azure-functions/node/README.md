

## Testing Sentry SDK against Azure cloud functions

##### To test Sentry SDK integration with Node Azure cloud function, follow the below steps.
- Create development package from the source code
- Deploy it into Azure 'Function App' console
- Refer usage for use case specific handling

## Create a development package

##### 1. Clone the git repo on your development machine

##### 2. Cloned repo contains multiple folders, each of these folder contains an example of how we can capture errors/exceptions on sentry dashboard

##### 3. Go to any folder and edit the node source (_Index.js_) file . Edit below section of the file where you replace 'dsn' with your own DSN
```
Sentry.init({
dsn: "<your DSN>",
});
```
##### 4. Configure your [local environment](https://docs.microsoft.com/en-in/azure/azure-functions/functions-create-first-function-vs-code?pivots=programming-language-javascript#configure-your-environment).

##### 5. Install the dependencies using following command.
```html
npm install
```

##### 6. Run the following command to install the Core Tools package:

```
npm install -g azure-functions-core-tools
```

## Deplay Azure cloud function file to Azure Function App.

##### Create a Function App function in Azure Node, Deploy the Azure cloud function by selecting 'Function App' and deploy it. For more information refer to [Azure cloud function](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-function-vs-code?pivots=programming-language-javascript#publish-the-project-to-azure).

## Usage

#### 1. Initialization error:

This function contains code that instruments Azure cloud  function initialization error. This is instrumented by calling undefined function before invoking the cloud function.


#### 2. Handled exception:

This function contains code that instruments handled the exception. call undefined function and handle the exception using try-catch block.

