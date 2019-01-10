CREATE TABLE dbo.Connection(
	ConnectionId				int				identity(1,1),
	DotSource					int				not null,
	DotSync						int				not null,
	ConnectionDateTime			datetime		not null default getutcdate(),

	constraint PC_dbo_Connection primary key clustered (ConnectionId asc)
		with (data_compression = page),

	constraint FK_dbo_Connection_DotSourceId_dbo_Dot
		foreign key (DotSource)
		references dbo.Dot (DotId),

	constraint FK_dbo_Connection_DotSyncId_dbo_Dot
		foreign key (DotSource)
		references dbo.Dot (DotId)

)