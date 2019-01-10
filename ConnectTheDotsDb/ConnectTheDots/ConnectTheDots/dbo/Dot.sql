CREATE TABLE dbo.Dot ( 
	DotId					int identity(1,1)				not null,
	DotName					varchar(100)					null,
	XCoordinate				decimal(18,6)					null,
	YCoordinate				decimal(18,6)					null,

	constraint PC_dbo_dot primary key clustered (DotId asc)
		with (data_compression = page),


)