FROM nginx:stable

COPY nginx.conf /etc/nginx/conf.d/default.conf

COPY html/webgl/ /usr/share/nginx/html

RUN chown -R nginx:nginx /etc/nginx/conf.d \
 && chmod -R 755 /etc/nginx/conf.d

RUN chown -R nginx:nginx /usr/share/nginx/html \
 && chmod -R 755 /usr/share/nginx/html


EXPOSE 80
