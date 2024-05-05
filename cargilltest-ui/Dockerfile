FROM node:latest as build-step

WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

EXPOSE 13000
CMD ["npm","start"]