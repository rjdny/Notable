CREATE TABLE [UserProfile] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [FirebaseUserId] nvarchar(255),
  [Username] nvarchar(255),
  [Email] nvarchar(255),
  [CreatedAt] datetime
)
GO

CREATE TABLE [Note] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [UserProfileId] int,
  [Content] nvarchar(255),
  [CreatedAt] datetime
)
GO

CREATE TABLE [Category] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255),
  [UserProfileId] int
)
GO

CREATE TABLE [CategoryNote] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [CategoryId] int,
  [NoteId] int,
  [UserProfileId] int
)
GO

CREATE TABLE [NoteComment] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [NoteId] int,
  [UserProfileId] int,
  [Content] nvarchar(255),
  [CreatedAt] datetime
)
GO

CREATE TABLE [NoteLike] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [NoteId] int,
  [UserProfileId] int
)
GO

ALTER TABLE [Note] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id])
GO

ALTER TABLE [Category] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id])
GO

ALTER TABLE [CategoryNote] ADD FOREIGN KEY ([NoteId]) REFERENCES [Note] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [CategoryNote] ADD FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id])
GO

ALTER TABLE [CategoryNote] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id])
GO

ALTER TABLE [NoteComment] ADD FOREIGN KEY ([NoteId]) REFERENCES [Note] ([Id])
GO

ALTER TABLE [NoteComment] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id])
GO

ALTER TABLE [NoteLike] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id])
GO

ALTER TABLE [NoteLike] ADD FOREIGN KEY ([NoteId]) REFERENCES [Note] ([Id])
GO
