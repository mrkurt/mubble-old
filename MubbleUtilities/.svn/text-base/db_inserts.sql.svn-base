/*INSERT INTO Modules (Id, Name, Path) VALUES('de691346-a714-489f-9741-d5aea60c3de7','Basic Controller', '~/Modules/BasicController/')
INSERT INTO Modules (Id, Name, Path) VALUES('d95340c7-2310-49a6-8e8e-f0fb203331c3','Featured Controller', '~/Modules/FeaturedController/')
INSERT INTO Modules (Id, Name, Path) VALUES('68ac8b8f-fd1d-4f90-967c-88ce975b8a2c','User Controls', '~/Modules/Users/')*/

INSERT INTO Templates(Id, Name, Path, UpdatedAt) VALUES('64351B54-3A36-45AF-8F3A-959E33A79984','Default', '~/Templates/Default/', getdate())
INSERT INTO Templates(Id, Name, Path, UpdatedAt) VALUES('602b9110-855e-4888-a721-2d61804fd981','Audioholics', '~/Templates/Audioholics/', getdate())

/* Some play Controller */

INSERT INTO Controllers(ID,ModuleControlId, TemplateId, TemplateControl, Name, FileName, Status)
VALUES('51799860-8138-43B4-9584-183AF29503E4','152D7361-5CFD-4A76-B2C9-4FB230F3773C','602b9110-855e-4888-a721-2d61804fd981', 'Default.master', 'Home page', 'default', 1)

INSERT INTO Controllers(ID,ControllerId,ModuleControlId, TemplateId, TemplateControl, Name, FileName, Status)
VALUES('4D2355FC-D079-4125-811A-F15B921F0D01','51799860-8138-43B4-9584-183AF29503E4','152D7361-5CFD-4A76-B2C9-4FB230F3773C','602b9110-855e-4888-a721-2d61804fd981', 'Default.master', 'Login', 'users', 1)

INSERT INTO Controllers(ID,ControllerId, ModuleControlID, TemplateId, TemplateControl, Name, FileName, Status)
VALUES('6B879B43-88E0-4556-AC24-76BDB23F909E','51799860-8138-43B4-9584-183AF29503E4','152D7361-5CFD-4A76-B2C9-4FB230F3773C','602b9110-855e-4888-a721-2d61804fd981', 'Default.master', 'Register', 'register', 1)

INSERT INTO UrlRedirects(ID, Url, ControllerId, PathExtra)
VALUES('68ac8b8f-fd1d-4f90-967c-88ce975b8a2c','/home.php','51799860-8138-43b4-9584-183af29503e4', null) 