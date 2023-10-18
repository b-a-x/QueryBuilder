select ti.*, 
h.HierLev1_ID, h.HierLev1Name as HierLev1Name,
h.HierLev2_ID, h.HierLev2Name as HierLev2Name,
h.HierLev3_ID, h.HierLev3Name as HierLev3Name,
ps.StringName as PSName,
cc.COUNTRY_ID,
c.NAME as CountryName,
rt.Name as RegistrationTypeName,
tt.StringName as TITypeName,
areas.ATSAreaName, 
ais.ATSAISName,
tio.MRid
from Info_TI_Hist ti
join Info_TI tio on ti.TI_ID = tio.TI_ID
join Dict_PS ps on ti.PS_ID = ps.PS_ID
join v_Dict_Hier h on h.HierLev3_ID = ps.HierLev3_ID
join Dict_TI_RegistrationTypes rt on rt.RegistrationType = ti.RegistrationType
join Dict_TI_Types tt on tt.TIType = ti.TIType
left outer join MGLEP_TI_COUNTRIES cc on ti.TI_ID = cc.TI_ID
left outer join MGLEP_SPR_COUNTRIES c on c.ID = cc.COUNTRY_ID
left join Dict_Areas areas on ti.ATSArea_ID = areas.ATSArea_ID
left join Dict_AIS ais on ti.ATSAIS_ID = ais.ATSAIS_ID