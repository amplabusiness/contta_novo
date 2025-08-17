FROM node:18-alpine

ENV NODE_ENV=production
WORKDIR /app

# App code (no external deps)
COPY server.js ./

EXPOSE 8080
CMD ["node", "server.js"]
