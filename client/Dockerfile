FROM node:18 AS build

WORKDIR /app
COPY package.json ./
RUN npm install

COPY .. .
RUN npm run build --prod

# Stage 2: Serve Angular app with nginx
FROM nginx:alpine

COPY --from=build /app/dist/client /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
