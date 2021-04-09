#include <QtWidgets>
#include <sentry.h>

int main(int argc, char *argv[])
{
    sentry_options_t *options = sentry_options_new();
    sentry_init(options); // DSN key from environment

    auto sentryShutdown = qScopeGuard([] { sentry_shutdown(); });

    QApplication app(argc, argv);
    qDebug() << "Application initialized";

    QPushButton button("Boom");
    QObject::connect(&button, &QPushButton::clicked, []{
        qDebug() << "Button clicked";
        int *ptr = nullptr;
        *ptr = 123;
    });

    qDebug() << "Button created, now showing...";
    button.show();

    return app.exec();
}
