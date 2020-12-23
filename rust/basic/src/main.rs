/// An example app that sends a panic to Sentry.
fn main() {
    // Panics will be captured as long as this variable is alive.
    let _guard = sentry::init("https://<PUBLIC_DSN_KEY>:<PRIVATE_DSN_KEY>@sentry.io/<PROJECT_ID>");

    panic!("Something's wrong, I can feel it...");
}
