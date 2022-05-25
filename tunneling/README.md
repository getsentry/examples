# Examples using the JavaScript `tunnel` option.

Using the new JavaScript option that allows events to pass through your own backend before being sent to Sentry. This allows bypassing Ad-Blockers that rely on matching Sentry by `ingest.sentry.io` or `sentry_key`.

To try things out, you can host the sample `index.html` in this directory and run one of the tunnels such as [dotnet](dotnet).


Serve the test html:

# Python

```
# python 3.x
python3 -m http.server 8132

# python 2.x
python -m SimpleHTTPServer 8132
```

# .NET

```sh
dotnet tool install --global dotnet-serve 
dotnet serve -o -p 8132
```

# Using a docker image

From the project [sentry_tunnel](https://github.com/gbip/sentry_tunnel), run [the docker image](https://hub.docker.com/repository/docker/sigalen/sentry_tunnel) :

```
docker run -e 'TUNNEL_REMOTE_HOST=https://sentry.example.com' -e 'TUNNEL_PROJECT_IDS=1,5' -e 'TUNNEL_IP=0.0.0.0'  sigalen/sentry_tunnel
```
