const express = require("express");

// Allow body up to 100 MB
const app = express();


//TODO: Change this to suit your needs
const SENTRY_HOST = "oXXXXXX.ingest.sentry.io";
const SENTRY_KNOWN_PROJECTS = ["123456"]

//TODO: Put your routes here, make sure to keep the /bugs route at the end of your file
app.post("/bugs", async (req, res) => {
    try {
        const envelope = req.body;

        const piece = envelope.toString().split("\n")[0];
        const header = JSON.parse(piece);

        const dsn = new URL(header.dsn);
        if (dsn.hostname !== SENTRY_HOST) {
            return res.status(400).send({ message: `Invalid Sentry host: ${dsn.hostname}` });
        }

        const project_id = dsn.pathname.substring(1);
        if (!SENTRY_KNOWN_PROJECTS.includes(project_id)) {
            return res.status(400).send({ message: `Invalid Project ID: ${project_id}` });
        }

        const url = `https://${SENTRY_HOST}/api/${project_id}/envelope/`;
        await fetch(url, {
            method: "POST",
            body: envelope,
            headers: {
                "Content-Type": "application/x-sentry-envelope"
            }
        });
    }
    catch {
        return res.sendStatus(204);
    }
    return res.sendStatus(204);
});
app.listen(3000)
