USE [Gestion]
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [NombreCompleto]) VALUES (N'973c1e83-0776-411e-9f60-cb5a7e6e4261', N'renato.cheva', N'RENATO.CHEVA', N'renato_120397@gmail.com', N'RENATO_120397@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEJyaWrFFmBOPY6yoAvwnb+vohUfmNNyyG104UX7Y1V3OEZbXAFw+DC4dFr1okIUhdg==', N'V6YMQHVQEFHXF3PAFVW2FIEPQRTJNLXD', N'eb720a29-84ad-40e6-ae4e-db491a2bc4fc', NULL, 0, 0, NULL, 1, 0, N'Renato Chevarria')
GO
SET IDENTITY_INSERT [dbo].[Categoria] ON 

INSERT [dbo].[Categoria] ([Id_categoria], [nombre_categoria]) VALUES (1, N'PAGO NUEVO EDITA')
SET IDENTITY_INSERT [dbo].[Categoria] OFF
GO
SET IDENTITY_INSERT [dbo].[Pago] ON 

INSERT [dbo].[Pago] ([Id_pago], [Id_usuario], [id_categoria], [nombre_pago], [fechaingreso], [fechapago], [alerta], [estado]) VALUES (1, N'aebdbddb-3968-494b-80d4-6b0cc1a5699e', 1, N'PAGO AGUA SEDAPAL EDITAR', N'10/02/2022', N'10/02/2022', 1, 1)
SET IDENTITY_INSERT [dbo].[Pago] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220707072754_AddItems', N'3.1.2')
GO
SET IDENTITY_INSERT [dbo].[Documento] ON 

INSERT [dbo].[Documento] ([id_documento], [id_usuario], [nombre_documento], [documento], [fechacreacion]) VALUES (1, N'973c1e83-0776-411e-9f60-cb5a7e6e4261', N'documento_prueba', N'documento_prueba', N'25-07-2022')
SET IDENTITY_INSERT [dbo].[Documento] OFF
GO
