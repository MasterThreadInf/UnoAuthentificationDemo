FROM node:8.9.3-alpine
RUN mkdir -p /usr/src/authen/authen.Shared
COPY -/authen/authen.Shared/* /usr/src/authen/authen.Shared/
WORKDIR /usr/src/authen/authen.Shared
RUN npm install
CMD node /usr/src/authen/authen.Shared/MainPage.xaml.cs
