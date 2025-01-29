
-- Este escript podria ejecutrarse las veces que sean necesarias pero borraria la informacion que tiene la base de datos

-- Si existe la base de datos la borraria  en caso de no encotrarla
-- mandaria un mensaje de salida de que no la pudo borrar

DROP DATABASE IF EXISTS ethereum_data;

-- Creamos la base de datos 
CREATE DATABASE ethereum_data CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- usamos la base de datos creada
use ethereum_data;

-- comenzamos con la creacion de tablas 
create table transactions(
id varchar(66) not null primary key,
value BIGINT not null,
gasUsed BIGINT not null,
blockNumber BIGINT not null,
fromAddress varchar(42) not null,
toAdress varchar(42) not null
);
