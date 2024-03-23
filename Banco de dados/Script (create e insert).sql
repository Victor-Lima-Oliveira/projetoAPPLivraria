create database dbLivraria;
use dbLivraria;

create table tbStatus(
codStatus int primary key auto_increment,
sta varchar(50));

insert into tbstatus values (null, "Disponível"),(null,"Indisponível");

create table tbAutor(
codAutor int primary key auto_increment,
nomeAutor varchar(50),
sta int,
foreign key (sta) references tbstatus (codStatus));

create table tblivro (
codlivro int primary key auto_increment,
nomelivro varchar(50),
codautor int,
foreign key (codAutor) references tbautor (codAutor));

/*----------------- INSERTS --------------------*/

insert into tbautor (nomeAutor, sta) values 
	("Machadão de Assis", 1),
	("Érico Veríssimo", 1),
	("Guimarães Rosa", 1),
	("Carlos Drummond de Andrade", 1),
	("Clarice Lispector", 1),
	("Paulo Coelho", 1),
	("Manuel Bandeira", 1),
	("Viniciu de Moraes", 1);
    
    


