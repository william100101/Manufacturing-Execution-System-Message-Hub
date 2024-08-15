select distinct MessageName from ToSAP

select MessageName, count(*) 
from ToSAP
group by MessageName


--select Tosap.TransactionId, FromSAP.FromSAPID, Fromsap.MessageName, FromSap.*
select distinct FromSAP.MessageName
from ToSAP 
left join FromSAP on ToSap.TransactionID = FromSap.TransactionID
where ToSap.MessageName in ('MainframeMessageAcknowledgement', 'MainframeMessageStatus')
and isnull(ToSap.TransactionId, '') <> ''

select * from tosap
where ToSap.MessageName in ('MainframeMessageAcknowledgement', 'MainframeMessageStatus')
and isnull(TransactionID, '') = ''
order by tosapid desc

select * 
from fromsap
where MessageName = 'QPPD_MG_MES'


select * 
from fromsap
where MessageName = 'MT_DEV0626B_QPPD_MG_MES'


select * from FromSAP
where FromSAPID = '64905'

select * from ToSAP
where TransactionID = '10003008'

select * from ToSAP
where TransactionID = '1000306'


select * from FromSAP
where MessageName = 'MT_DEV0626B_QPPD_MG_MES' and isnull(TransactionId, '') <> ''
order by Fromsapid desc


select * from ToSap
where TransactionID = '10003008'


--select Tosap.TransactionId, FromSAP.FromSAPID, Fromsap.MessageName, FromSap.*

-- Look for ack/status for a particular messagename (type)
select FromSAP.*
from ToSAP 
left join FromSAP on ToSap.TransactionID = FromSap.TransactionID
where ToSap.MessageName in ('MainframeMessageAcknowledgement', 'MainframeMessageStatus')
and isnull(ToSap.TransactionId, '') <> '' and  Fromsap.MessageName = 'MT_DEV0782_SOCharacteristic_SAP'
order by FromSAPID desc


select distinct TargetSystemID
from FromSAP
where MessageName = 'MT_DEV0782_SOCharacteristic_SAP'


select * from ToSAP
where MessageName in ('MainframeMessageAcknowledgement', 'MainframeMessageStatus')
and TransactionID in ('1000306', '10003006')


select * from ToSAP
where MessageName in ('MainframeMessageAcknowledgement', 'MainframeMessageStatus')





-- Update Transaction Id for Ack's and Status Only
Update ToSAP
Set TransactionId = Message.value('(//TransactionId)[1]', 'VARCHAR(12)') 
from ToSAP
where MessageName in ('MainframeMessageAcknowledgement', 'MainframeMessageStatus')
and ISNULL(TransactionId, '') = ''