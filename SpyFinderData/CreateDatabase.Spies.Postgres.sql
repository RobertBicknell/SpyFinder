-- Database: Spies

-- DROP DATABASE "Spies";

CREATE DATABASE "Spies"
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'English_United Kingdom.1252'
    LC_CTYPE = 'English_United Kingdom.1252'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;


-- Table: public.spies

-- DROP TABLE public.spies;

CREATE TABLE public.spies
(
    name character varying(255) COLLATE pg_catalog."default" NOT NULL,
    code integer[] NOT NULL,
    CONSTRAINT spies_pkey PRIMARY KEY (name),
    CONSTRAINT spies_code_key UNIQUE (code)

)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.spies
    OWNER to postgres;