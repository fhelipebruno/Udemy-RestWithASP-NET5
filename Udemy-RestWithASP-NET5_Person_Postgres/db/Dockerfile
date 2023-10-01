FROM mysql:5.7.22
EXPOSE 3306
COPY ./Udemy-RestWithASP-NET5/db/migrations/ /home/database/
COPY ./Udemy-RestWithASP-NET5/db/dataset/ /home/database/
COPY ./Udemy-RestWithASP-NET5/CI/init_database.sh/ /docker-entrypoint-initdb.d/init_databse.sh