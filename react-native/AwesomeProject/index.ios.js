/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 * @flow
 */

import React, {Component} from 'react';

import {AppRegistry, StyleSheet, Text, View, Button, TextInput} from 'react-native';

import {Sentry, SentrySeverity, SentryLog} from 'react-native-sentry';

Sentry.config(
  'https://6890c2f6677340daa4804f8194804ea2:8ce0e61531284b1c8f39318c974ad264@sentry.io/148053',
  {
    deactivateStacktraceMerging: true
  }
).install();

Sentry.setExtraContext({
  a_thing: 3,
  some_things: {green: 'red'},
  foobar: ['a', 'b', 'c'],
  react: true,
  float: 2.43
});

Sentry.setTagsContext({
  environment: 'production',
  react: true
});

Sentry.setUserContext({
  email: 'john@apple.com',
  userID: '12341',
  username: 'username',
  extra: {
    is_admin: false
  }
});

Sentry.captureBreadcrumb({
  message: 'Item added to shopping cart',
  category: 'action',
  data: {
    isbn: '978-1617290541',
    cartSize: '3'
  }
});

export default class AwesomeProject extends Component {
  constructor() {
    super();
    this.state = {text: ''};
    Sentry.setEventSentSuccessfully(event => {
      this.setState({text: JSON.stringify(event)});
    });
  }

  _sendMessage() {
    Sentry.captureMessage('TEST message', {
      level: SentrySeverity.Warning
    });
  }
  _throwError() {
    throw new Error('Sentry: Test throw error');
  }
  _nativeCrash() {
    Sentry.nativeCrash();
  }
  render() {
    return (
      <View style={styles.container}>
        <Text style={styles.welcome}>Welcome to React Native Sentry example</Text>
        <Button
          style={{fontSize: 20, color: 'green'}}
          styleDisabled={{color: 'red'}}
          onPress={() => this._throwError()}
          accessibilityLabel={'throw error'}
          title="throw error!"
        />
        <Button
          style={{fontSize: 20, color: 'green'}}
          styleDisabled={{color: 'red'}}
          onPress={() => this._nativeCrash()}
          accessibilityLabel={'native crash'}
          title="native crash!"
        />
        <Button
          style={{fontSize: 20, color: 'green'}}
          styleDisabled={{color: 'red'}}
          onPress={() => this._sendMessage()}
          accessibilityLabel={'send message'}
          title="send message"
        />
        <TextInput
          style={{height: 40, borderColor: 'gray', borderWidth: 1, width: '80%'}}
          accessibilityLabel={'textarea'}
          value={this.state.text}
        />
      </View>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#F5FCFF'
  },
  welcome: {
    fontSize: 20,
    textAlign: 'center',
    margin: 10
  },
  instructions: {
    textAlign: 'center',
    color: '#333333',
    marginBottom: 5
  }
});

AppRegistry.registerComponent('AwesomeProject', () => AwesomeProject);
