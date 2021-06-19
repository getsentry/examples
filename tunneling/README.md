# Examples using the JavaScript `tunnel` option.

Using the new JavaScript option that allows events to pass through your own backend before being sent to Sentry. This allows bypassing Ad-Blockers that rely on matching Sentry by `ingest.sentry.io` or `sentry_key`.

To try things out, you can host the sample `index.html` in this directory and run one of the tunnels such as [dotnet](dotnet).


Serve the test html:

# Python

```
python -m SimpleHTTPServer 8132
```

# .NET

```sh
dotnet tool install --global dotnet-serve 
dotnet serve -o -p 8132
```