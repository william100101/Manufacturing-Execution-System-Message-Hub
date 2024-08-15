select top 10 * from FromSAP
where MessageStateID = 4
order by FromSAPId desc


select MessageName, count(*)
from fromsap 
group by MessageName


select 