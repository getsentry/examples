const Fastify = require("fastify");

// Allow body up to 100 MB
const fastify = Fastify({ bodyLimit: 100 * 1048576 });


//TODO: Change this to suit your needs
const SENTRY_HOST = "oXXXXXX.ingest.sentry.io";
const SENTRY_KNOWN_PROJECTS = ["123456"]

//TODO: Put your routes here, make sure to keep the /bugs route at the end of your file


//Handle replay packets where no content-type header is available
fastify.addContentTypeParser("*", { parseAs: "buffer" }, function (req, body, done) {
    done(null, body);
});
fastify.post("/bugs", async (request, reply) => {
    try {
        const envelope = request.body;

        const piece = envelope.toString().split("\n")[0];
        const header = JSON.parse(piece);

        const dsn = new URL(header.dsn);
        if (dsn.hostname !== SENTRY_HOST) {
            return reply.code(400).send({ message: `Invalid Sentry host: ${dsn.hostname}` });
        }

        const project_id = dsn.pathname.substring(1);
        if (!SENTRY_KNOWN_PROJECTS.includes(project_id)) {
            return reply.code(400).send({ message: `Invalid Project ID: ${project_id}` });
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
        return reply.code(204).send();
    }
    return reply.code(204).send();
});
fastify.listen({ port: 3000 })
