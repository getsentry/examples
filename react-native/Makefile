
clean-project: #this is meant to be called from make react-native appium make file
	@echo "--> Creating new react-native project"
	rm -rf AwesomeProject/ || true
	react-native init AwesomeProject --version react-native@0.46.4
	(cd AwesomeProject; yarn add react-native-sentry)
	(cd AwesomeProject; yarn remove babel-preset-react-native && yarn add babel-preset-react-native@3.0.1)
	rm -rf AwesomeProject/node_modules/react-native-sentry/scripts/*
	# rm -rf AwesomeProject/node_modules/react-native-sentry/lib/*
	cp -r ../../scripts/* AwesomeProject/node_modules/react-native-sentry/scripts/
	# cp -r ../../react-native-sentry/lib/* AwesomeProject/node_modules/react-native-sentry/lib/
	cp sentry.properties AwesomeProject/ios/.
	cp sentry.properties AwesomeProject/android/.
	cp index.js AwesomeProject/index.ios.js
	cp index.js AwesomeProject/index.android.js
	(cd AwesomeProject; react-native link react-native-sentry)
	rm AwesomeProject/android/app/build.gradle
	cp build.gradle AwesomeProject/android/app/build.gradle
