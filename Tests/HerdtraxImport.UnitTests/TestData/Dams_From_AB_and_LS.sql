select tblDam.Dam_SN, Herd_Code, tblDam.DamYr_Code, tblDam.DamTag_Str, tblCalf.CCID

from [dbo].[tblDam]

inner join [dbo].[tblCalf] 
	inner join dbo.tblHerdCode
	on tblHerdCode.Herd_SN = tblCalf.CalfHerd_SN
on tblDam.Calf_SN = tblCalf.Calf_SN

--where Herd_Code in ('AB','LS')
where Herd_Code in ('AB')
and DamDisp_SN is null

order by Herd_Code, DamBirthYr_Num , DamTag_Num
