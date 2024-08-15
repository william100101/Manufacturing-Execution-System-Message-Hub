SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetToSAPMessagesPendingProcessing] 

AS
BEGIN
SET NOCOUNT ON;

   select getdate() [asof]
   , [MessageName]
   , count([ToSAPID]) [NoPendingMessages]
   , min([QueuedTime]) [MinQueueDate]
   , DATEDIFF(second,min([QueuedTime]),getdate()) [secondsAwaitingSAP]
  FROM [BTE_Msg_Hub].[dbo].[ToSAP] with (Nolock)
  where QueuedTime >= GETDATE() - 7
  and MessageName <> 'BTEMessageAcknowledgment'
  and MessageName <> 'BTEMessageStatus'  
  and ProcessedTime is null
    group by [MessageName]
  order by [MessageName];



END

GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetFromSAPLastMessageReceived] 

AS
BEGIN
SET NOCOUNT ON;

    select getdate() [asof]
	, [MessageName]
	, max([QueuedTime]) [LastMsgDT]
	, DATEDIFF(minute,max([QueuedTime]), getdate()) [minutessincelastmsg]
  from [BTE_Msg_Hub].[dbo].[FromSAP] with (Nolock)
    where QueuedTime >= GETDATE() - 90
  group by [MessageName]
  order by  DATEDIFF(minute,max([QueuedTime]),getdate());


END

GO



