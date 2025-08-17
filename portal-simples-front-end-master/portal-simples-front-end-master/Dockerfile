FROM node:14.17.1-alpine as build
WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH
COPY package.json /app/package.json
RUN npm install && npm audit fix
COPY . /app
RUN npm run-script build

# stage: 2 — the production environment
FROM nginx:1.16.0-alpine
COPY --from=build /app/build /usr/share/nginx/html
RUN rm -R /etc/nginx/conf.d/
COPY nginx.conf /etc/nginx
EXPOSE 80
CMD ["nginx","-g","daemon off;"]
