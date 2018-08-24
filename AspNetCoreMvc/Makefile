# Must have `sentry-cli` installed globally
# Following variable must be passed in
#  SENTRY_AUTH_TOKEN

SENTRY_ORG=testorg-az
SENTRY_PROJECT=asp-net-core-mvc
VERSION=`sentry-cli releases propose-version`

setup_release: reference_release create_release associate_commits

create_release:
	sentry-cli releases -o $(SENTRY_ORG) new -p $(SENTRY_PROJECT) $(VERSION)

associate_commits:
	sentry-cli releases -o $(SENTRY_ORG) -p $(SENTRY_PROJECT) set-commits --auto $(VERSION)

reference_release:
	sed -i -e "s/i.Release = .*;/\i.Release = \"${VERSION}\";/g" Program.cs
