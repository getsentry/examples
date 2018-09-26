$sentry_org="testorg-az"
$sentry_project="dotnet-demo"
$version=sentry-cli releases propose-version

#Create Release
sentry-cli releases -o $sentry_org new -p $sentry_project $version

#Associate Commits
sentry-cli releases -o $sentry_org -p $sentry_project set-commits --auto $version

#Set new value for release version environment variable
$Env:RELEASE_VERSION=$version

#Launch app 
dotnet run