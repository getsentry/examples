# Example with React Native Navigation

This sample showcases how to use Sentry with routing instrumentation for React Native Navigation.

## Running this example

### Install Dependencies

```bash
yarn install
npx pod-install
```

### Set the DSN

You will then need to replace the DSN in `index.js` with your own:

```javascript
// index.js
Sentry.init({
  dsn: '__DSN__',
  //...
})
```

### Launch the App

You can then launch the app:

```bash
# Launch iOS simulator
yarn run ios
# Launch Android simulator
yarn run android
```

### Using the Example

You can navigate to a screen by pressing on "Open Screen", and the navigation transaction should show up on your Sentry Dashboard.

You can throw an error by pressing "Throw Error" and the error should be logged on your Sentry Dashboard.