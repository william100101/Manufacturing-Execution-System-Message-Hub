
INSERT INTO [dbo].[EMailNotification]
           ([Sender]
           ,[Distribution]
           ,[Subject]
           ,[Application]
           ,[MessageBody]
           ,[CreatedTime]        
           ,[MessageStateID])           
     VALUES 
			('haasey@cartech.com'
            ,'chaas@cartech.com, haasey@hotmail.com'
            ,'test subject'
            ,null
            ,'test body'
            ,GETDATE()
            ,0)
GO


