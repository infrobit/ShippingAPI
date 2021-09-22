CREATE SCHEMA dbo;

CREATE TABLE shipping.dbo.status_carga (
	id int IDENTITY(0,1) NOT NULL,
	status varchar(15) COLLATE Latin1_General_CI_AS NOT NULL,
	CONSTRAINT status_carga_PK PRIMARY KEY (id)
);

CREATE TABLE shipping.dbo.tipo_veiculo (
	id int IDENTITY(0,1) NOT NULL,
	tipo varchar(50) COLLATE Latin1_General_CI_AS NULL,
	CONSTRAINT tipo_veiculo_PK PRIMARY KEY (id)
);
 CREATE  UNIQUE NONCLUSTERED INDEX tipo_veiculo_id_IDX ON dbo.tipo_veiculo (  id ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
	 	 
CREATE TABLE shipping.dbo.veiculo (
	id int IDENTITY(0,1) NOT NULL,
	nome_modelo varchar(50) COLLATE Latin1_General_CI_AS NOT NULL,
	id_tipo_veiculo int NOT NULL,
	valor_mcubico decimal(38,0) DEFAULT 0 NOT NULL,
	CONSTRAINT veiculo_PK PRIMARY KEY (id),
	CONSTRAINT veiculo_FK FOREIGN KEY (id_tipo_veiculo) REFERENCES shipping.dbo.tipo_veiculo(id)
);

CREATE TABLE shipping.dbo.carga (
	id int IDENTITY(0,1) NOT NULL,
	responsavel varchar(100) COLLATE Latin1_General_CI_AS NOT NULL,
	id_veiculo int NOT NULL,
	data_saida datetime NOT NULL,
	valor_carga decimal(38,0) NOT NULL,
	altura decimal(38,0) NOT NULL,
	largura decimal(38,0) NOT NULL,
	comprimento decimal(38,0) NOT NULL,
	id_status_carga int NOT NULL,
	CONSTRAINT carga_PK PRIMARY KEY (id),
	CONSTRAINT carga_UN UNIQUE (id_veiculo),
	CONSTRAINT carga_FK FOREIGN KEY (id_veiculo) REFERENCES shipping.dbo.veiculo(id),
	CONSTRAINT carga_FK2 FOREIGN KEY (id_status_carga) REFERENCES shipping.dbo.status_carga(id)
);
CREATE UNIQUE NONCLUSTERED INDEX carga_UN ON shipping.dbo.carga (id_veiculo);
 CREATE  UNIQUE NONCLUSTERED INDEX carga_id_status_carga_IDX ON dbo.carga (  id_status_carga ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;

--INSERTS
INSERT INTO shipping.dbo.tipo_veiculo (tipo) VALUES
	 (N'Terrestre'),
	 (N'Aéreo'),
	 (N'Náutico');
	 
INSERT INTO shipping.dbo.status_carga (status) VALUES
	 (N'Cancelada'),
	 (N'Em Percurso'),
	 (N'Entregue');
	 
INSERT INTO shipping.dbo.veiculo (nome_modelo,id_tipo_veiculo,valor_mcubico) VALUES
	 (N'Cargueiro Cruiser Ship',3,22),
	 (N'Caminhão Bitrem',1,35);
