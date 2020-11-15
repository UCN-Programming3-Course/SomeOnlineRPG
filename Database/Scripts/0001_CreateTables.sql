CREATE TABLE [Characters](
	[Id] int not null primary key identity,
	[WebId] uniqueidentifier not null default(newid()),
	[Name] nvarchar(max) not null
);

CREATE TABLE [Items](
	[Id] int not null primary key identity, 
	[Name] nvarchar(max) not null,
	[CharacterId] int foreign key references [Characters](Id)
);