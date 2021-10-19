/**
 * @format
 */

import {Navigation} from 'react-native-navigation';
import * as Sentry from '@sentry/react-native';

import FirstScreen from './src/FirstScreen';
import SecondScreen from './src/SecondScreen';

Sentry.init({
  dsn: '__DSN__',
  // The sample rate being set to 1.0 ensures all traces are logged, set to a lower value in your production apps.
  tracesSampleRate: 1.0,
  integrations: [
    new Sentry.ReactNativeTracing({
      // Pass instrumentation to be used as `routingInstrumentation`
      routingInstrumentation: new Sentry.ReactNativeNavigationInstrumentation(
        Navigation,
      ),
      idleTimeout: 5000,
    }),
  ],
  // Setting debug to true means Sentry will log valuable information to the console.
  debug: true,
});

Navigation.registerComponent('FirstScreen', () => FirstScreen);
Navigation.registerComponent('SecondScreen', () => SecondScreen);

Navigation.events().registerAppLaunchedListener(() => {
  Navigation.setRoot({
    root: {
      stack: {
        children: [
          {
            component: {
              name: 'FirstScreen',
            },
          },
        ],
      },
    },
  });
});
