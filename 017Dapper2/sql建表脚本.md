
```sql
CREATE TABLE [dbo].[User]
(
    [Id] [INT] PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    [Name] [NVARCHAR](50) NULL,
    [Age] [INT] NULL,
    [Gender] [BIT] NULL,
    [CreateTime] [DATETIME] NULL,
    [UpdateTime] [DATETIME] NULL
);
```


```sql
CREATE TABLE [dbo].[Department]
(
	[Id] [INT] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Name] [NVARCHAR](50) NULL,
	[Address] [NVARCHAR](50) NULL,
)

```