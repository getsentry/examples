# Sentry PHP Laravel Example

This shows how to use Sentry in Laravel to capture errors/exceptions

# Setup
1. `$ compose install`
2. Set your DSN key + projectID in `.env`
3. Run server. `$ php artisan serve`
4. Go to http://localhost:8000 to trigger error. You should see issue/event within Sentry project

# Resources:
- https://sentry.io/for/laravel/
- https://docs.sentry.io/clients/php/
- Sentry config is in `app/Exceptions/Handler.php`
