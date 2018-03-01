import io.sentry.Sentry

object Application {
  def main(args: Array[String]): Unit = {
    Sentry.init("YOUR DSN")

    try
      1/0 //caught exception that will be sent to Sentry
    catch {
      case e: Exception =>
        Sentry.capture(e)
    }

    1/0 //uncaught exception that will be sent to sentry
  }
}