CREATE TABLE [UserProfile] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [firebaseUserId] nvarchar(255),
  [full_name] nvarchar(255),
  [created_at] timestamp
)
GO

CREATE TABLE [Note] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userProfileId] int,
  [content] nvarchar(255),
  [created_at] timestamp
)
GO

CREATE TABLE [Category] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(255),
  [userProfileId] int
)
GO

CREATE TABLE [CategoryNote] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [categoryId] int,
  [noteId] int,
  [userProfileId] int
)
GO

CREATE TABLE [NoteComment] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [noteId] int,
  [userProfileId] int,
  [content] nvarchar(255),
  [created_at] timestamp
)
GO

CREATE TABLE [NoteLike] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [noteId] int,
  [userProfileId] int
)
GO

ALTER TABLE [Note] ADD FOREIGN KEY ([userProfileId]) REFERENCES [UserProfile] ([id])
GO

ALTER TABLE [Category] ADD FOREIGN KEY ([userProfileId]) REFERENCES [UserProfile] ([id])
GO

ALTER TABLE [CategoryNote] ADD FOREIGN KEY ([noteId]) REFERENCES [Note] ([id]) ON DELETE CASCADE
GO

ALTER TABLE [CategoryNote] ADD FOREIGN KEY ([categoryId]) REFERENCES [Category] ([id])
GO

ALTER TABLE [CategoryNote] ADD FOREIGN KEY ([userProfileId]) REFERENCES [UserProfile] ([id])
GO

ALTER TABLE [NoteComment] ADD FOREIGN KEY ([noteId]) REFERENCES [Note] ([id])
GO

ALTER TABLE [NoteComment] ADD FOREIGN KEY ([userProfileId]) REFERENCES [UserProfile] ([id])
GO

ALTER TABLE [NoteLike] ADD FOREIGN KEY ([userProfileId]) REFERENCES [UserProfile] ([id])
GO

ALTER TABLE [NoteLike] ADD FOREIGN KEY ([noteId]) REFERENCES [Note] ([id])
GO
