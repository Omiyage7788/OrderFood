CREATE TABLE [dbo].[AdminAccPwds] (
    [AdminID] [int] NOT NULL,
    [AdminAccount] [nvarchar](max) NOT NULL,
    [AdminPassword] [nvarchar](max) NOT NULL,
    CONSTRAINT [PK_dbo.AdminAccPwds] PRIMARY KEY ([AdminID])
)
CREATE INDEX [IX_AdminID] ON [dbo].[AdminAccPwds]([AdminID])
CREATE TABLE [dbo].[Administrators] (
    [AdminID] [int] NOT NULL IDENTITY,
    [AdminName] [nvarchar](30) NOT NULL,
    [AdminAuthority] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.Administrators] PRIMARY KEY ([AdminID])
)
CREATE TABLE [dbo].[AdminMessages] (
    [MessageID] [int] NOT NULL IDENTITY,
    [AdminID] [int] NOT NULL,
    [AMessage] [nvarchar](max) NOT NULL,
    [MessageRecipient] [int] NOT NULL,
    [StartDate] [datetime] NOT NULL,
    [EndDate] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.AdminMessages] PRIMARY KEY ([MessageID])
)
CREATE INDEX [IX_AdminID] ON [dbo].[AdminMessages]([AdminID])
CREATE INDEX [IX_MessageRecipient] ON [dbo].[AdminMessages]([MessageRecipient])
CREATE TABLE [dbo].[AdminMemNotifications] (
    [MemberID] [int] NOT NULL,
    [MessageID] [int] NOT NULL,
    [AdminMemDisplay] [bit] NOT NULL,
    [Read] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.AdminMemNotifications] PRIMARY KEY ([MemberID], [MessageID])
)
CREATE INDEX [IX_MemberID] ON [dbo].[AdminMemNotifications]([MemberID])
CREATE INDEX [IX_MessageID] ON [dbo].[AdminMemNotifications]([MessageID])
CREATE TABLE [dbo].[Members] (
    [MemberID] [int] NOT NULL IDENTITY,
    [MName] [nvarchar](30) NOT NULL,
    [MCellphone] [nvarchar](max) NOT NULL,
    [MEmail] [nvarchar](30) NOT NULL,
    [MAuthority] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.Members] PRIMARY KEY ([MemberID])
)
CREATE TABLE [dbo].[MAccPwds] (
    [MemberID] [int] NOT NULL,
    [MAccount] [nvarchar](max) NOT NULL,
    [MPassword] [nvarchar](max) NOT NULL,
    CONSTRAINT [PK_dbo.MAccPwds] PRIMARY KEY ([MemberID])
)
CREATE INDEX [IX_MemberID] ON [dbo].[MAccPwds]([MemberID])
CREATE TABLE [dbo].[Orders] (
    [OrderID] [int] NOT NULL IDENTITY,
    [OrderTitle] [nvarchar](30),
    [MemberID] [int] NOT NULL,
    [OrderDate] [datetime] NOT NULL,
    [RequiredDate] [datetime] NOT NULL,
    [ReadyTime] [datetime],
    [ShipAddress] [nvarchar](50) NOT NULL,
    [Note] [nvarchar](max),
    [ShipVia] [int] NOT NULL,
    [PayVia] [int] NOT NULL,
    [OrderUrl] [nvarchar](max),
    [Rate] [smallint],
    [PayState] [bit] NOT NULL,
    [OrderDisplay] [bit] NOT NULL,
    [OrderState] [int] NOT NULL,
    [SupplierDecline] [nvarchar](max),
    CONSTRAINT [PK_dbo.Orders] PRIMARY KEY ([OrderID])
)
CREATE INDEX [IX_MemberID] ON [dbo].[Orders]([MemberID])
CREATE INDEX [IX_ShipVia] ON [dbo].[Orders]([ShipVia])
CREATE INDEX [IX_PayVia] ON [dbo].[Orders]([PayVia])
CREATE INDEX [IX_OrderState] ON [dbo].[Orders]([OrderState])
CREATE TABLE [dbo].[OrderDetails] (
    [OrderID] [int] NOT NULL,
    [ProductID] [int] NOT NULL,
    [Price] [decimal](18, 2) NOT NULL,
    [Discount] [decimal](18, 2) NOT NULL,
    [Quantity] [int] NOT NULL,
    [Total] [decimal](18, 2),
    CONSTRAINT [PK_dbo.OrderDetails] PRIMARY KEY ([OrderID], [ProductID])
)
CREATE INDEX [IX_OrderID] ON [dbo].[OrderDetails]([OrderID])
CREATE INDEX [IX_ProductID] ON [dbo].[OrderDetails]([ProductID])
CREATE TABLE [dbo].[Products] (
    [ProductID] [int] NOT NULL IDENTITY,
    [PName] [nvarchar](30) NOT NULL,
    [Price] [decimal](18, 2) NOT NULL,
    [PPhoto] [varbinary](max),
    [ImageFormat] [nvarchar](max),
    [Discount] [decimal](18, 2) NOT NULL,
    [Discontinuted] [bit] NOT NULL,
    [SupplierID] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Products] PRIMARY KEY ([ProductID])
)
CREATE INDEX [IX_SupplierID] ON [dbo].[Products]([SupplierID])
CREATE TABLE [dbo].[Suppliers] (
    [SupplierID] [int] NOT NULL IDENTITY,
    [SupplierName] [nvarchar](30) NOT NULL,
    [SCellphone] [nvarchar](max) NOT NULL,
    [SPhone] [nvarchar](max),
    [SEmail] [nvarchar](30) NOT NULL,
    [SCity] [nvarchar](5) NOT NULL,
    [SDistrict] [nvarchar](5) NOT NULL,
    [SRoad] [nvarchar](30) NOT NULL,
    [BusinessHour] [nvarchar](max),
    [SAuthority] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.Suppliers] PRIMARY KEY ([SupplierID])
)
CREATE TABLE [dbo].[AdminSupNotifications] (
    [SupplierID] [int] NOT NULL,
    [MessageID] [int] NOT NULL,
    [AdminSupDisplay] [bit] NOT NULL,
    [Read] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.AdminSupNotifications] PRIMARY KEY ([SupplierID], [MessageID])
)
CREATE INDEX [IX_SupplierID] ON [dbo].[AdminSupNotifications]([SupplierID])
CREATE INDEX [IX_MessageID] ON [dbo].[AdminSupNotifications]([MessageID])
CREATE TABLE [dbo].[SAccPwds] (
    [SupplierID] [int] NOT NULL,
    [SAccount] [nvarchar](max) NOT NULL,
    [SPassword] [nvarchar](max) NOT NULL,
    CONSTRAINT [PK_dbo.SAccPwds] PRIMARY KEY ([SupplierID])
)
CREATE INDEX [IX_SupplierID] ON [dbo].[SAccPwds]([SupplierID])
CREATE TABLE [dbo].[SCategories] (
    [CategoryID] [int] NOT NULL,
    [SupplierID] [int] NOT NULL,
    [Note] [nvarchar](30),
    CONSTRAINT [PK_dbo.SCategories] PRIMARY KEY ([CategoryID], [SupplierID])
)
CREATE INDEX [IX_CategoryID] ON [dbo].[SCategories]([CategoryID])
CREATE INDEX [IX_SupplierID] ON [dbo].[SCategories]([SupplierID])
CREATE TABLE [dbo].[Categories] (
    [CategoryID] [int] NOT NULL IDENTITY,
    [CategoryName] [nvarchar](10) NOT NULL,
    [IsSelected] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.Categories] PRIMARY KEY ([CategoryID])
)
CREATE TABLE [dbo].[SPhotoes] (
    [PhotoID] [int] NOT NULL IDENTITY,
    [Photo] [varbinary](max),
    [ImageFormat] [nvarchar](max),
    [SupplierID] [int] NOT NULL,
    CONSTRAINT [PK_dbo.SPhotoes] PRIMARY KEY ([PhotoID])
)
CREATE INDEX [IX_SupplierID] ON [dbo].[SPhotoes]([SupplierID])
CREATE TABLE [dbo].[SupMessages] (
    [MessageID] [int] NOT NULL IDENTITY,
    [SupplierID] [int] NOT NULL,
    [SMessage] [nvarchar](max) NOT NULL,
    [StartDate] [datetime] NOT NULL,
    [EndDate] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.SupMessages] PRIMARY KEY ([MessageID])
)
CREATE INDEX [IX_SupplierID] ON [dbo].[SupMessages]([SupplierID])
CREATE TABLE [dbo].[SupMemNotifications] (
    [MemberID] [int] NOT NULL,
    [MessageID] [int] NOT NULL,
    [SupMemDisplay] [bit] NOT NULL,
    [Read] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.SupMemNotifications] PRIMARY KEY ([MemberID], [MessageID])
)
CREATE INDEX [IX_MemberID] ON [dbo].[SupMemNotifications]([MemberID])
CREATE INDEX [IX_MessageID] ON [dbo].[SupMemNotifications]([MessageID])
CREATE TABLE [dbo].[OrderStatus] (
    [StatusID] [int] NOT NULL IDENTITY,
    [Status] [nvarchar](10) NOT NULL,
    CONSTRAINT [PK_dbo.OrderStatus] PRIMARY KEY ([StatusID])
)
CREATE TABLE [dbo].[PayMethods] (
    [PayID] [int] NOT NULL IDENTITY,
    [Method] [nvarchar](10) NOT NULL,
    CONSTRAINT [PK_dbo.PayMethods] PRIMARY KEY ([PayID])
)
CREATE TABLE [dbo].[ShipMethods] (
    [ShipID] [int] NOT NULL IDENTITY,
    [Method] [nvarchar](10) NOT NULL,
    CONSTRAINT [PK_dbo.ShipMethods] PRIMARY KEY ([ShipID])
)
CREATE TABLE [dbo].[ReceiveGroups] (
    [RecipientID] [int] NOT NULL IDENTITY,
    [Recipient] [nvarchar](10) NOT NULL,
    CONSTRAINT [PK_dbo.ReceiveGroups] PRIMARY KEY ([RecipientID])
)
ALTER TABLE [dbo].[AdminAccPwds] ADD CONSTRAINT [FK_dbo.AdminAccPwds_dbo.Administrators_AdminID] FOREIGN KEY ([AdminID]) REFERENCES [dbo].[Administrators] ([AdminID])
ALTER TABLE [dbo].[AdminMessages] ADD CONSTRAINT [FK_dbo.AdminMessages_dbo.Administrators_AdminID] FOREIGN KEY ([AdminID]) REFERENCES [dbo].[Administrators] ([AdminID]) ON DELETE CASCADE
ALTER TABLE [dbo].[AdminMessages] ADD CONSTRAINT [FK_dbo.AdminMessages_dbo.ReceiveGroups_MessageRecipient] FOREIGN KEY ([MessageRecipient]) REFERENCES [dbo].[ReceiveGroups] ([RecipientID]) ON DELETE CASCADE
ALTER TABLE [dbo].[AdminMemNotifications] ADD CONSTRAINT [FK_dbo.AdminMemNotifications_dbo.AdminMessages_MessageID] FOREIGN KEY ([MessageID]) REFERENCES [dbo].[AdminMessages] ([MessageID]) ON DELETE CASCADE
ALTER TABLE [dbo].[AdminMemNotifications] ADD CONSTRAINT [FK_dbo.AdminMemNotifications_dbo.Members_MemberID] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Members] ([MemberID]) ON DELETE CASCADE
ALTER TABLE [dbo].[MAccPwds] ADD CONSTRAINT [FK_dbo.MAccPwds_dbo.Members_MemberID] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Members] ([MemberID])
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [FK_dbo.Orders_dbo.Members_MemberID] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Members] ([MemberID]) ON DELETE CASCADE
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [FK_dbo.Orders_dbo.OrderStatus_OrderState] FOREIGN KEY ([OrderState]) REFERENCES [dbo].[OrderStatus] ([StatusID]) ON DELETE CASCADE
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [FK_dbo.Orders_dbo.PayMethods_PayVia] FOREIGN KEY ([PayVia]) REFERENCES [dbo].[PayMethods] ([PayID]) ON DELETE CASCADE
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [FK_dbo.Orders_dbo.ShipMethods_ShipVia] FOREIGN KEY ([ShipVia]) REFERENCES [dbo].[ShipMethods] ([ShipID]) ON DELETE CASCADE
ALTER TABLE [dbo].[OrderDetails] ADD CONSTRAINT [FK_dbo.OrderDetails_dbo.Orders_OrderID] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Orders] ([OrderID]) ON DELETE CASCADE
ALTER TABLE [dbo].[OrderDetails] ADD CONSTRAINT [FK_dbo.OrderDetails_dbo.Products_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ProductID]) ON DELETE CASCADE
ALTER TABLE [dbo].[Products] ADD CONSTRAINT [FK_dbo.Products_dbo.Suppliers_SupplierID] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[Suppliers] ([SupplierID]) ON DELETE CASCADE
ALTER TABLE [dbo].[AdminSupNotifications] ADD CONSTRAINT [FK_dbo.AdminSupNotifications_dbo.AdminMessages_MessageID] FOREIGN KEY ([MessageID]) REFERENCES [dbo].[AdminMessages] ([MessageID]) ON DELETE CASCADE
ALTER TABLE [dbo].[AdminSupNotifications] ADD CONSTRAINT [FK_dbo.AdminSupNotifications_dbo.Suppliers_SupplierID] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[Suppliers] ([SupplierID]) ON DELETE CASCADE
ALTER TABLE [dbo].[SAccPwds] ADD CONSTRAINT [FK_dbo.SAccPwds_dbo.Suppliers_SupplierID] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[Suppliers] ([SupplierID])
ALTER TABLE [dbo].[SCategories] ADD CONSTRAINT [FK_dbo.SCategories_dbo.Categories_CategoryID] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Categories] ([CategoryID]) ON DELETE CASCADE
ALTER TABLE [dbo].[SCategories] ADD CONSTRAINT [FK_dbo.SCategories_dbo.Suppliers_SupplierID] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[Suppliers] ([SupplierID]) ON DELETE CASCADE
ALTER TABLE [dbo].[SPhotoes] ADD CONSTRAINT [FK_dbo.SPhotoes_dbo.Suppliers_SupplierID] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[Suppliers] ([SupplierID]) ON DELETE CASCADE
ALTER TABLE [dbo].[SupMessages] ADD CONSTRAINT [FK_dbo.SupMessages_dbo.Suppliers_SupplierID] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[Suppliers] ([SupplierID]) ON DELETE CASCADE
ALTER TABLE [dbo].[SupMemNotifications] ADD CONSTRAINT [FK_dbo.SupMemNotifications_dbo.Members_MemberID] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Members] ([MemberID]) ON DELETE CASCADE
ALTER TABLE [dbo].[SupMemNotifications] ADD CONSTRAINT [FK_dbo.SupMemNotifications_dbo.SupMessages_MessageID] FOREIGN KEY ([MessageID]) REFERENCES [dbo].[SupMessages] ([MessageID]) ON DELETE CASCADE
CREATE TABLE [dbo].[__MigrationHistory] (
    [MigrationId] [nvarchar](150) NOT NULL,
    [ContextKey] [nvarchar](300) NOT NULL,
    [Model] [varbinary](max) NOT NULL,
    [ProductVersion] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202106200115277_AddAdminMessageModel', N'FoodProject.Migrations.Configuration',  0x1F8B0800000000000400ED5DDB72DC38927DDF88FD878A7ADA9DE8514976CC448F439A09B564F738A6CBD69872C7BE3968162C71874556932C8F151BFB65FBB09FB4BFB0E01D974C5C48F052EA0EBD94082091481C00894426F07FFFF3BF977FF9B68F565F499A85497CB5BE383B5FAF481C24BB307EB85A1FF32FBFFF7EFD973FFFEBBF5CBEDEEDBFAD7E6EF2BD2CF2D1927176B57ECCF3C3ABCD260B1EC9DECFCEF661902659F2253F0B92FDC6DF259B17E7E77FDA5C5C6C0825B1A6B456ABCB0FC7380FF7A4FC87FE7B93C40139E4473FDA263B1265F5779AE2955457EFFC3DC90E7E40AED66F9264779726FF4982FCACCABD5E5D47A14F39F148F465BDF2E338C9FD9CF2F9EA6346BC3C4DE207EF403FF8D1FDD381D07C5FFC282335FFAFBAECA64D397F513465D3156C4805C72C4FF696042F5ED6B2D988C57B4978DDCA8E4AEF359572FE54B4BA94E0D5FA7AB70FE3EB20B8FBE76EBD12EB7B7513A5455E48C6674CC9EF564CFA772D2828768ABFEF5637C7283FA6E42A26C73CF5A3EF5677C7CF5118FC8D3CDD27FF20F1557C8C22964DCA284DE33ED04F94FE81A4F9D307F28565FEEDED7AB5E10B6FC4D26D59B160D5BAB771FEF2C57AF58EB2E17F8E488B878D9E08154042A1DB50A2E0A2E364BDDAFADF7E22F143FE78B5A63FD7AB37E137B26BBED4D43FC6211D56B4509E1E49BFDAEFFC2CFB6792EE26A8FE9DFF357C285101311266B463F324A543EF0389CA6CD96378A846200B954F62F63769B2FF90443C14855C9FBCE4980694A7FB449BF5DE4F1F48CEB37FB9E970AF1F0D0C73F603A22DFC5CC704230C8FB693FC4862425B4C76777E9E93944E716F77A494AB11828B9F0AF4BE3C1F6DEC5C1FF3C7242DF9ACAAFF21A1D8F2E3A143A1994CC718070DB80DC6413364AC78DF922CF31F886214D73974ECC3D960FE91BC5003EC06724DB8CF30AE8BCE3588EBEAFB0C63A6E8A40379F06ADAF6D6D42B695DF10712848790746B79BF7678B99FE6B7548E0D99E2F77DB8B7E7EB75BCEB4767F0523D6C90838BB56642B09CA5F6EF923CFC120615F7AA8670393FF1D382D41C3C333673294AF49A80BDE3C1B069424E75D35499E1A6294BD8368D0E2D127E253FA6C9F1A0471D9F1BC11C9B498D382EE770ED50E8F37E8B0B4762BE4566FF99A4F21AE37E456AEA1932AF1AAF6B066B15E5E836CC0E916FAFFB49C8F6778EF5C7760C8F3FA921E3C6601A346D51D5F71633745B40D38E3A9F59139ACC83467FCB99D580AF4B2D6D8CBB1BB5EED4C8ED2C7BC1ED0D89A2C36312CFA179BEDEFB61347D93C7DAFA4A6BE358A3DE481B13A708E3394BB183AFD320868524894731DD96ADF7E90E9B49AB2488293E45E24948B665892A89265D2E678358C573496C2BB20EB21A6C7B19C2B7F31AC1C79FE275B3C95C26F0ED12CCDF2A0DC762B610F5186C36E985EB66EAB0827555682E5497B5F701755B702AB5A5ACF03ECC2307BA8B6E0BE462BC96FC3A31507D20BF1CC394F4B452013BA9A7AAA448496370A3E3ED7AB74BE90E45D1017F184593A24B900BB551DFC09F437F58AFDFF94F836994C8F998AA1456372DFEC0008AB27AF14783D6793953A8EF76BE1A1B6E4C03252D8EAB9E06E5E3E11085942D1244A1935D8A8BA5CD58E51417364423B5D2826F494EF74D0AC6EA0C9F9A854FE08E4F86B562214F2F6DBDE8FDA3527E5C3649864C2A2647368BAD2CE9A8D912BA1384B73A75054C2689BF360DE3AECB60CB5B31E9E999637349DC7589187B4C8EE1FA550B4A7B2DAB2EBA305D0BCC4BBFEC8E413EA266A699E9BBFA879109834ED32041B8F7E9B27697D25FB5DFDBF7EB9517F805417BEA7405E1B644CE2BF8FBD1AF75D32142B8A7008DCC797465A8B09B9CC1A18B4CE0C6135F0522031EBB9C08974D06359F6DAE41D34CC78CD514D3149B6B7A193065188F7677DBB9BB59ACD0E3CE4777778F499EB40A6D18FBE99399E6A8A6FB76EF3F903749BAF75D587F669E53CB0A2844E22385CD50E5BF51DAADD729478AAFF9BCA5547EA5D9CDC2325CB61F66B54D05F994532103B0986590E19761D76A6A6DCBCD35B7B238B39D5CCD31EA6E766DEA9C6592F5663CEAF3EE1CD5ABA9669E1345EF86514621EBDB2895DE167E6461A05A7BC6A9F843E2ABCE1CC691F10FC72C8C4996FD954E74E3E368AC2362C1AFCCDCBF8D99A235CE6DDDE260E4D9D665B75DE494DB08EB354EDC40289641E3555871925DA7C1729512E51558CA61AD22DCD015E42129345188BD261561504E965904F25833596BCE10876512C29E9026F32666E875F4AE70D6EFD2110E8174F89C5DCC34DC2F5F9A03EC5D280512CB53BF5C3B51F6DE508CE84649793A61374A770ED4A00FA291CBB59BED94B3F5D2A821F274D16FCBD5CBD7C69BD7D7669AED964E359BCBDFC65B82BF8DC6B260A7D5483A17AAF7F4C378A7E2D8A1BC293717CE9BFA0DD7B521C382ADCAC531F9503A1A978E7E8E3C289AEBD6879816D7AAB06C46480DEED265504399DCAE403DB4759C4B47838F9598D5E8EB0A2E6FF8B91C4FEEAC7A4D9D1AABDEC5287691B7198520ED9F1E46FBC17B538B5189EF4E81A1DB6FB5A9F7AA764B4D5968B6A3C1A2F25E07834DC1C98E054FFF04CDFD99946651B0319048CB016641E97BBED3EF3280AEE06F5701D89CEF0CD6E666BB0D60C9F1FBFDE36DD801A00CB9614C7E065137AC81D0F509B1BDF912B0A0E336CEFEF3C890F86F99C0D2C286DC4F420B8AFEAEC4BF48A3A5CAD97A70001D3832CC63A4CD0E22C69C8B0C1A00CC5CFD9D791B976C7B5FDEAAE46C06CAB2F65EE6C9B6E464BA422DE3B1F78B03226ACDDDF391C85AC883BF9FEF67E7866FE7FCD9949B6D8BE7F73263D4C526BB7BA096ED82B168168881E0508ED4E8A7FF30F116767A4F5B70B6999172D06B5EACCBFD8644EBA81B048B405C4E2F30F2F74459C1912D3A1720DBFBE6FAA0922B3C1534A51BF26640A7E53595F6778929AFA8046F1D3301EF7596254158B20A3444BC688F97C5EB78B7B2B875AFEA195112B493288E43BA090E287B57EBDF492237ABA6158F500D734B205FD1C55A1C0AEFE35B12919CACAE83BC74DBBFF1B3C0DFC920A0B2DCF15FE8E8216901563FBAA19D4EAB0CE35C1E6A614C81EA47E6CD114880E315B958B7E0B3AD514CB925071217A3CBBC0307B2D2D628885227B9CB0D035123E42A2E21D300CBE4463209C582D5C51ACD26773B9A0D9DC5205ADF241330A1B69D3EC8D677ED60962640B8786F0B062DF412970E48ED95423C86CECFCE2E149845EF926208370620119C22D55E08431830EB3CD4B0680E2744B243EB9F003BC2C508580F63B724741DDCE8E1E6331D76D9970168E69ED160D627831BDC1BA7823621A05A090F2CBA5AC05D1B6868893EEC660D1DAC178240987D131CE0772858A210EEA0812C4CA5152A7DAA950A9A9983B5A0A0498104965AA1D9B5D827A5159A346962ADD0A46B4F412BD4B8D95B210D38B41E19DB40944F572373C67E12A8961A63821F65B0CE404C4BFD399CA109100DC428629852052C4E8D23255700BABB004CDD18EA85505C3413A21297C46920518ADB40BB1C0FE26070D86BD7AD887A3582B81B34614C4C09264CC6A78125C83D1BED73650405D3ED9D47B8F942AC7604EF88B36EE44B5B7F556D3081833294C802928A7E1ACEC6A4A83498E354F132CE3079AA1AA1A20553CE91781F9DC62C297AE7A380415DF519B4D4E1111630446F48380D0C22EC4F0940A45F4E037DB847AB42D7D7BAB772D819706668E01F7B0A56757D3326B3B0EB7BEF04ACED6A67660B30419ECD2362170AEA10AA5BA84DD3A831D3D9338DBAF2146C99607C8D125168B08D0990B478456F223A0D6D40D184696D40683F9D8456005F618A0147730FB3E313CD3E06C6F991A96C820926EEB0CB9BAD90A9ECAAC16C4CE7D5C1C683285183BCDDE0C0BB030C3A11E836112B8B4423D804A3E909896DB24722D83B262CB06FBACC89C32E16448316E0850E071804C24D98C9B08B535928FE24F68D6620288EA90FF4A42E31ACBD7CBE694ED031611F1A7C402FAF38801D145AC2E8874C54CA42812737C068DE032397FA404FEE17D3FAE7039FF2BD580C30664F468FEC637E8A162393864C663332E9C513B01AE101491A3829628E5C47F9C021505D257C1CD542518B37C504258A78BC3EA8C53BCFC23EC444DCCD07DDDA19C026460D2C8282B797B386A29E1E316A4E5C37F42C9974BDB3383365270CE4C439F6AAB0495A26A72548CA6A9E5B5212CBC9B71C08F3FD98913AD237AB834B45C414843D92CB905EAFBA604D088F12F6004A0CA660624C06137A5D682B4CAE9DE8CD88091A0D4653C8A6A1DD6A2F12B9364547A1123148C250FACD2E4222D02498946F8D823095365943ABB3FF4974BA240D0DC6BC2D1161D24CBA5D721D86BB5DCAA6E3101D359E619F310E2B328D2E4D4385F59692C8B0893A6E6AA70599953A41DF63ED6884BACC74A842C77D303DBB61CA9902618037A93A7C77261D19E05D9AAE9DCC065D6E1F93A8A1C3EA5450CB785D5520C62C3FF0CC2BEA392BA60474C3804A2DC2F56D44336A9B2AAD06D2126C17AE2F12C657A50D2F2173E929E2C171199A0691EB77DB60C0902C4FE510B2AC05132ED66B3D642B4622039254062B732DC2C29519FEBB855921192C3A99A5832802F612101F1A9705A08AB885CC888AE6C3BA838AC8F86D179FCB4504A00A02951B8084818AA24014202D3D402A885AD673BA51061A62D38D7974A23C1118C5278A138146C3B2AC6582E946F7928CA1601197127D7B65F712E722957D4C982A7045DB5E96407015203F5D0816D720451096492374B40081A0DB971EF2909F6601C4A18E03E25B804602B1C280B7261A426343037C400090863692856F872A96856D0AB6D5D2530304A3D8710D918C1A26BA600AA4256AA8588A653AB84817E9031251BAF3F32DC01CFA59F6C12DB09ACCF8F3297A093238AF1A1D1A8A73A2EED0906F9CCD7EC2C0A77C142552EDB46C263ACCCB59D740C0CFD9A90001C766913E6C83E92946E9467A447A4ACF5AB951986FAD515BF4E4461E98C8C3F39A7D8A81BAA376F01CBC579948EF81AE924637B1A88321B407855C0C7B6D66219F429110622AEC2D0FC66E884A037173839A203BBAF59284ECD9C682023575F696026BF644C580395E410D005CAF7A0902F0B562A70FDC5AEBC87EA858DACD3D82F4F63CC5F2EEC66038C912AFB8F25663BDC6DD512C1D52865BAE41171486AC60F6772334D8B701939A812784AD2F84283783EDAA81E7830B737F7395717B26DFA65D6EBCE091ECFDFAC3E5866609C8213FFA5175BF7693B0F50F87307EC8BA92F5979577F083E228EFF7DE7AF56D1FC5D9D5FA31CF0FAF369BAC249D9DEDC3204DB2E44B7E1624FB8DBF4B362FCECFFFB4B9B8D8EC2B1A9B8013BDE841D0D6449B4DE125A4D2AA29A76FC2342BDE7AF23FFBC51DD437BBBD944DF24040CEA69AEA642703B9239BF3AAA64CF1BB2A873D770E93EAE4F98636715FB87F94377C2398924AD3F25EE0477E0ADDB15DF87DDC24D1711F6B9C413474DAD78A25626D8A25C5EE156289649724D3BCDC08C2929C5AA49E11868AD8D9E650504D16F66840A99902424160124C143F014AD5675B7C1DA91A9286C5EDF532C2BAB465E1A15D1D5DC001D9999B82012D8E899D895D6525AE08699D0056EDE69823846E99B5AD633C2F81462AFC3271CACCCB822C49E6B339ADD7CDDB822CA5F6E3D2C0CEEBD06E30AFA4698E7D0D191C258D4B398F0ECCD15C8B3777A3897DE04E1A556CA239DDEAA53B9658F5653138C3777F56D042C818A0092D390180E4D5746BBB926E6F48141D1E935824C47CB7A0F67AEF879140A9FE66410559D9B74B5CD55BBF99A108ECADE9A325274020A8E36F7BE8F75B58B7DF2E50AF47FD6CACFA1BA662D0DD58414CB2EDE5E0AC5CD11BC33574EEC33C2200A9FABBCDE2E70A8395655F528998CF36CBDD2FC730258086C5A7D82DA04FD5E3CFE22A5A7FB650231FC3C3F56E975205415024D904737A540112D8AABED8715406888ADC8051A3389D26C8992583053E6B90F0318D0020945F2D7A4DEE7FCB7EA7DCD7B705086D42EE10D0B40AD4EBF8144B8A0077AA3B0E1418A80F146F491085A21A21252E6B166F4E0E5DCCE5082DD3191D2D3EF6BCCE5CB0C24115BF7745452B0C44C8579FCC695040033A45F7D59CD2DF8F7E29649E52F7D59CD27D92FBC2B4527F5A0C9E15A7EA5658C6E818E0182F3A09F6E4DDD09DED6EC8057EEF6A072A8E08E254855379BBF71FC89B24DDFBC240E012E618556519DAE9F131273B805C9764BF86884050DD4B36DB485339F7580D359490C1585394752B641D3579D4F1291614115384D7CB145138294A94EEACA900060DCFDAA0E1DD48AB50FDC982C66D71761306C208663E5BD0FA908866BDFA93398D1F8E1955E8B2ECAFC931E549F129165C21661F6F89661F3024C1857D5B43D3D4BEAD2533CD2CE1DCC64DD9C06DDC6CE233B2717B6EFC091032268B8DAD85D1F152035A19BD1E56460FB6327A0BB43276310943BB1D2364D2F178594CC4EC731BAC8C55CF704C05247383D74C9DAE0CF6B1EA759C9241B7AB0A4FD3EF4D2959BBE4532C36549947222A2071CFC27E5F0C0EEA209CA1231FA46232EC9182E896B7C82E6DE29B8F165B6760E73CF7C6F9D4B6A678B48EEDDEB4BF7791AAF0149AA263F503F430F27A7818FD0AFD8080482F17C81CEA03644264FCA34BC798C7FC7F84A467B43352074DD99FE1C0A44C8F70B0D28AE9A0BED65C980D90CBCE7594203A0BEA2C554C97DD110546C8E48C022FAB38CA95741BF84A709C461332C50F73288C6AC60E52469BD9CDD1282593B95951181D00F54DD9A21FC233EC253E1A6A6047A9A2C00CBA4A5D1C5F5698FB76F9D5457111AF013D84DAE49D2705508959DADAEB2FEDFF6D00551DBCC4455595ED2F62A4CA766775209518CD546559AFA890BE86BB2292C97BCA72B23F2B329C79BF443751259426C3D68FC32F24CBEF937F90F86AFDE2FCE2C57A751D857E5645C0D5715AAFC40B588D02B72E5E16815B64B7DF88C5EDC3BF0A2A59B68B80E0AFA29B741158977F234F622F3748D25E7D7BB9114B5F0A286C0B56171286317467EDDB7847BE5DADFFAB2CF36AF5F63F3ED5C5BE5B952AC4ABD5F9EABFD7AB77C728F23F17217F5FFC28932F3150865B55F5C75FFD3478F4D37FDBFBDFFEBD1FC5CE2A6A4F921D3FFAEEC22E979EBEC7CA19EE4742C72CDD47EDEEFC3C272945FADBE2C6E3F204AA8F202B5B152FC4F56AEB7FFB89C40FF9E3D5FAE579CF3EEF4EC62AEA9FC3DC69C7C02F1C1A750BBA8BD1770C5374ECAE9969C0B6C6031783550E4DB26B91587E50D3186346C5C58EFECE4B4F574B4AAD31C38A8E2DBE154600539CA36F4C381E144D3DB6BD5B951BD4ABF08834C7968B058EB56198CE7632A9CA82E17CB684DFA51904A09E90703D4D6EC758BDD8F022275360ED93E398CB31D757306C6716C44C3689385551B793A8A750B48D592721AED8FA3E6A0B8E38A8D9A01DE33153BEB1A15D2866041813F33350F7E0C37D06136B837D704A26C2E5E27C14DDF607FBA9AE7284D00C24531ECBB81DBBFEAF8B0DEAFE2662C8AEE6AAD470DC9521460E04F881815CB6F7A3A86C881D8D2ECEA8BFA2C447170DA4C37163DA335DC9613B123106C9BA93EC560B309267D09A01E6458324FAAD30A6FD31708A66B8B61CA64DC181D587DD3B643BBAE1DD17413C7729FD959526CD8BEFE9621BF805C117D6D4BB708A912AE8629718E159D2A80397EC391C302EE0C820B3313100E730D85CEB5277636C90C6056A138F5491A72C7F0E633F7DEAB55E717E550ED6BFD1079110A5D47F6563DD892CB59DB6A4C56C663CDA90D820B3E186FB48E9C71B2210D7038E0F30723BEE3CC7868926E4C885663D8A8DA30E4452ED26EC697641496EE956414A6E05C0872BB9E8A7D14F6594C13D4387B96BBBF5C4B3E4326DD76C64D2E26CD76078CF6CABC574C0F09CDA23BD49EC9148448E596FA962311C772E5B955DE776259DECFA6743176451B332AE1A63020BD7190A8ADEBDEC5AE1E3637E1422BDB05FFED9F81FF7332B104863B8EB85A3670CF6BC4DC13177BCCBDD3D2E784FF69C7D69669F6D3DA71E3527E3C4A28B63F9CD856529DB00210867719B003482C570238044AD186C03DA9263CE4F7530CC10DDC1DCDA0DC798182EFC506889C1B2EF8FAD843581109388108B0131042318F96100C5BADC7391221E9E612647453C865E985CE111252AB9BDBA152AF34A0E6BD069DE2E52BAAF0B2FF7306FF1549C76EF0B3149DB63948754250A2833946F29C2E77D7C4B372C39595D075514C68D9F05FE4E96136DD04EC98FA032496F4489BCFC4EAA824285A445BF95AFE4146D08E5489ABB348C6917F9112C0B213B0844F45D88CB4D4B5D4CB925071217A0829A3CB0D296B620719D3CB847978C50C6BF29A6F0C91FD8B7E3E24C784A4DE649C8301EEE6C00A088C3B6C79D2E62DDB2FA0910583BA8C20FEC317DDCBE66C7746BFB4D4499D8930C99E60134964CF38D27737E762653EA8509FCDD07AB5D92391AF06BFEED2A9CA0FFEBF7209D76FF28934CF3B625C340F36994896472D0A06F052C1333CD33BBA0EF748F6E1B1336EDDBC122176DC22810B2E851DC27D01240F805E556954EA5FB0887DC27AAFB8847F5124F528667A7FBE86F345D9EEE032210F3ABE21E616F1E5767FA99F9FAAB039EEA8E6B4BAF940960A7AE7F02DCB5E26ADFA85F38D65A365926BA8FCF0753707F2C1647F5DE6D84290BDFC179C00ECE1B7B07370B18F0AB94978885C67DE213EA28D2F5219385ED46F6F324F34AE7E7C4A1A9FB3ACACCA2BC35D7D29FCA024D8A1B9A6D2B9D144FA7A210CD03A67966262B2CCD3F3795AE44A70324CEF3A962A1FEF48C20845D1BBD44FC48FE2F2760AF049C7604504F732032B91DD3E8EADEC5D934018CA1DE7C4227CB7628F6F3AF0A6CCA0BC5C7B23FF506DCBCD6A74E56A7B332DAC2FDA457483B2CCFBE4A72A7325AC3929549E7199ECD58597AD0E8742B40599FCF28AB9DEE6C18771A15BA92F7BFEC7AB2F9FE2CCF89D557CA9BFBD0DA43C9E2900F79EF794A1461FEB2CC94D4E6E0E6A4EEEBB3C48FEA9E7B43C7E1B1A0833DCE3E256C501F61462BE9B2705A09F3F959224779FFBEA9BFF458D8696F085B8E5FE5F26D064B72A49CDC6E30C0857256CB01E729AE70C6EFBA99CBC4762F9FF0EC7DC435EF5258852AF4819AB5A142F1D8C47450AB0F7DC78F49C0CF80D9D717248A639F05CF19596071223C6560C16BFE2192E60C517C34440249FDB6898CAEF5AA8B9381FAB67A7EE46ABDFB5C9C9354E1364C06E9412AB82E067B70754C06B446268F499DED8C06D7D826A3F5D5398CAB13D66AAC5A219BA27A2EA79E8D5683916A6E53A0CADA441D7914305B1558B6A640693468897C9300516FD24C68B76626B8863619ADA7CDA1A9AD33AE49357549502D5DAAA606C61C2C55C1A4417530C926B096DC0A61584BD950580B390DDA8AE2CE53E1CE33C51DE33720D7D0A58175B07E349A6AD8AC523D6C225491453DCD01B6DC963A016C489966429D39008090A79C55BB74C38A74332A9409ADD87236E5EC99F08CD1A4A21306FC66A33C5F74862F79C2E8D2C019A34936902863259125C92482126CD3F515F17B1BA92A3E19AA8C7F124FAC8ED18CE0155F5498574C09400350EAD74A0DBB6DB84E4151ECC3441AC833C49C4248739B8B4411718A0BC6344C7578C30CAC22322521C3605189A191806094D193B07189E11BD1B1F872FC3A57956BBE0D6EA210FD07B450151FE8A481BC82D7BDB32B0FF19ECD1302D5B036AAE2D9FA312C971274CDAE2CA643F61CDFCAA82A6C7C9B87628D31BE118DB2A324651847548C226C2827D4390176DF60DA852ADD8B100F10A502884417CBE24C0CE2F6A97AE819DB1AF568AE144C01B5561D71E1ACB11E30EB7BCE667D3056006AAD36A680635ADECB947CE3BB14BEC9E286AB6A74F7D561B3D57DACF37C77D7CBE33759F4BC86DAABF4CE76D7586E075A15AC3FB998AAB0C33E78CA323A1A74A2DEE03BD64676EE1557B52FAB994470EF37C41F506C947EDD9F4D34A2D3252611A573A6BB81612BC4A18AB06A39377022ECBF2ACFA210034E6CF83647E1CC8539BB899CC3D69D69B73CACB715DE58D4F308F6C9623BB8FB3A6F33590315DA4EDC4F067122624721F379A6A62A7D594C8D35E32D7073596770B70B9D654FE576807A69300DE2139662D383DD0230591838118C65E404B636DC776371349739B6E7DE6DDAE5A63220D71FE8BF94352AEB6DB22351567EBDDC7C38C6C525CDD57FB7240B1F3A129794664C02EE9CBDCDF336FE9234E7FD02474D16F93E507FE7E7FE754A87801FE43439A0BD1FC60FEBD5CF7E74A4595ED391B67B1BBF3FE687634E9B4C475EC4ED000AB70155FD971B89E7CBF787F254C34513289B6171AFF5FBF8876318ED5ABEDF00976C22240A7F84FA4AD0A22F0BC09087A796D2BBF209251342B5F85A378A7BB23F449458F63EF6FCAFA40F6F1F33F21379F08327FAFD6B48676E9C88BE2378B15FDE86FE43EAEFB39A46579EFE4B31BCDB7FFBF3FF03683E3AB3996F0100 , N'6.1.3-40302')
