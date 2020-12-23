/// An actix-web 3 application that raises an error when visiting http://127.0.0.1:3001/.
use std::io;

use actix_web::{get, App, Error, HttpRequest, HttpServer};

#[get("/")]
async fn failing(_req: HttpRequest) -> Result<String, Error> {
    Err(io::Error::new(io::ErrorKind::Other, "An error happens here").into())
}

#[actix_web::main]
async fn main() -> io::Result<()> {
    let _guard =
        sentry::init("https://<PUBLIC_DSN_KEY>:<PRIVATE_DSN_KEY>@sentry.io/<PROJECT_ID>");

    // Required for capturing errors with actix-web
    std::env::set_var("RUST_BACKTRACE", "1");

    HttpServer::new(|| {
        App::new()
            .wrap(sentry_actix::Sentry::new())
            .service(failing)
    })
    .bind("127.0.0.1:3001")?
    .run()
    .await?;

    Ok(())
}
