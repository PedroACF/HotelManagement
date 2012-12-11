create table habitacion(
	numero int not null primary key,
	tipo varchar(30),
	disponibilidad bit not null,
	precio float not null
)
create table servicio(
	id int identity(1,1) not null primary key,
	nombre varchar(30) not null,
	categoria varchar(30) not null,
	descripcion varchar(120) not null,
	precio float
)
create table cliente(
	id uniqueidentifier not null primary key,
	ciudad varchar(30) not null,
	estado varchar(30) not null,
	pais varchar(20) not null,
	nit varchar (10),
	telefono varchar(10) not null,
	direccion varchar(50) not null,
	comentarios varchar(500),
	foreign key(id)references Users(UserId)
)
create table persona(
	id uniqueidentifier not null primary key,
	ci int not null,
	apellido varchar(50) not null,
	cumpleaños date not null,
	pasaporte int,
	foreign key (id) references cliente(id)
)
create table empresa(
	id uniqueidentifier not null primary key,
	contacto varchar(120) not null,
	metodopago varchar(100) not null,
	foreign key (id) references cliente(id)
)
create table agencia(
	id uniqueidentifier not null primary key,
	contacto varchar(120) not null,
	foreign key (id) references cliente(id)
)
create table numerohabitaciones(
	id int identity (1,1) not null primary key,
	numhabitacion int not null,
	foreign key(numhabitacion)references habitacion(numero)
)
create table reserva(
	id int identity(1,1) not null primary key,
	cant_dias int not null,
	fecha_ini date not null,
	fecha_fin date not null,
	pago float,
	estado varchar(12) not null,
	num_pasajeros int not null,
	idHab int not null,
	idcli uniqueidentifier not null,
	foreign key(idHab) references habitacion(numero),
	foreign key(idcli) references cliente(id)
)
create table serviciousado(
	id int identity(1,1) primary key,
	idser int not null,
	idcli uniqueidentifier not null,
	foreign key (idser) references servicio(id),
	foreign key(idcli) references cliente(id)
)

create table factura(
	nro int not null primary key,
	fecha date not null,
	idR int not null,
	idS int not null,
	foreign key(idR) references reserva(id),
	foreign key (idS) references serviciousado (id),
)

create table mantenciones(
	id int not null identity(1,1) primary key,
	numerodias int not null,
	idhab int not null,
	foreign key (idhab) references habitacion(numero)
)
create table archivo(
id int identity(1,1) primary key not null,
ruta_fisica varchar(256) not null,
fecha datetime not null
)