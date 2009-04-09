CREATE TABLE #ContentTypes (ActiveObjectType nvarchar(255) NULL)

INSERT INTO #ContentTypes(ActiveObjectType) VALUES('Mubble.Models.Controllers.Blog, Mubble.Models')
INSERT INTO #ContentTypes(ActiveObjectType) VALUES('Mubble.Models.Controllers.Section, Mubble.Models')
INSERT INTO #ContentTypes(ActiveObjectType) VALUES('Mubble.Models.Controllers.Article, Mubble.Models')
INSERT INTO #ContentTypes(ActiveObjectType) VALUES('Mubble.Models.Controllers.Query, Mubble.Models')
INSERT INTO #ContentTypes(ActiveObjectType) VALUES('Mubble.Models.Post, Mubble.Models')
INSERT INTO #ContentTypes(ActiveObjectType) VALUES(NULL)

INSERT INTO ContentTypes (ActiveObjectType) SELECT ActiveObjectType FROM #ContentTypes WHERE ActiveObjectType 
NOT IN (SELECT ActiveObjectType FROM ContentTypes)

DROP TABLE #ContentTypes