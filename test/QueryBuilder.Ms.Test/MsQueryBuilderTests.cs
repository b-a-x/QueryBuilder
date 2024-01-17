using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using System.Xml.Linq;

namespace QueryBuilder.Ms.Test;

public class MsQueryBuilderTests
{
    #region 1
    /*
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
    left join Dict_AIS ais on ti.ATSAIS_ID = ais.ATSAIS_ID*/

    [Theory]
    [InlineData("\r\nselect ti.* ,tio.MRid ,ps.StringName as PSName ,rt.Name as RegistrationTypeName ,h.HierLev1_ID ,h.HierLev2_ID ,h.HierLev3_ID ,h.HierLev1Name ,h.HierLev2Name ,h.HierLev3Name ,cc.COUNTRY_ID ,c.NAME as CountryName ,areas.ATSAreaName ,ais.ATSAISName \r\nfrom dbo.Info_TI_Hist as ti\r\njoin dbo.Info_TI as tio on ti.TI_ID = tio.TI_ID\r\njoin dbo.Dict_PS as ps on ti.PS_ID = ps.PS_ID\r\njoin dbo.Dict_TI_RegistrationTypes as rt on ti.RegistrationType = rt.RegistrationType\r\njoin dbo.Dict_TI_Types as tt on ti.TIType = tt.TIType\r\njoin dbo.Dict_Areas as areas on ti.ATSArea_ID = areas.ATSArea_ID\r\njoin dbo.Dict_AIS as ais on ti.ATSAIS_ID = ais.ATSAIS_ID\r\nleft join dbo.MGLEP_TI_COUNTRIES as cc on ti.TI_ID = cc.TI_ID\r\nleft join dbo.MGLEP_SPR_COUNTRIES as c on cc.COUNTRY_ID = c.ID\r\njoin dbo.v_Dict_Hier as h on ps.HierLev3_ID = h.HierLev3_ID")]
    public void MsQueryBuilder_One_Build(string expected)
    {
        Action<IMsQueryBuilder> builder = b => b
        .Select<Info_TI_Hist>(x =>
        {
            x.All();
            x.Bind<Info_TI>().Column(x => x.MRid);
            x.Bind<Dict_PS>().Column(x => x.StringName).As("PSName");
            x.Bind<Dict_TI_RegistrationTypes>().Column(x => x.Name).As("RegistrationTypeName");
            x.Bind<v_Dict_Hier>().Column(x => x.HierLev1_ID)
                                 .Column(x => x.HierLev2_ID)
                                 .Column(x => x.HierLev3_ID)
                                 .Column(x => x.HierLev1Name)
                                 .Column(x => x.HierLev2Name)
                                 .Column(x => x.HierLev3Name);
            x.Bind<MGLEP_TI_COUNTRIES>().Column(x => x.COUNTRY_ID);
            x.Bind<MGLEP_SPR_COUNTRIES>().Column(x => x.NAME).As("CountryName");
            x.Bind<Dict_Areas>().Column(x => x.ATSAreaName);
            x.Bind<Dict_AIS>().Column(x => x.ATSAISName);
        })
		.From()
        .Join<Info_TI>(x => x.EqualTo(x => x.TI_ID, x => x.TI_ID))
        .Join<Dict_PS>(x => x.EqualTo(x => x.PS_ID, x => x.PS_ID))
        .Join<Dict_TI_RegistrationTypes>(x => x.EqualTo(x => x.RegistrationType, x => x.RegistrationType))
        .Join<Dict_TI_Types>(x => x.EqualTo(x => x.TIType, x => x.TIType))
        .Join<Dict_Areas>(x => x.EqualTo(x => x.ATSArea_ID, x => x.ATSArea_ID))
        .Join<Dict_AIS>(x => x.EqualTo(x => x.ATSAIS_ID, x => x.ATSAIS_ID))
        .LeftJoin<MGLEP_TI_COUNTRIES>(x => x.EqualTo(x => x.TI_ID, x => x.TI_ID))
        .LeftJoin<MGLEP_TI_COUNTRIES, MGLEP_SPR_COUNTRIES>(x => x.EqualTo(x => x.COUNTRY_ID, x => x.ID))
        .Join<Dict_PS, v_Dict_Hier>(x => x.EqualTo(x => x.HierLev3_ID, x => x.HierLev3_ID));


        builder(QBCore.Make<MsQueryBuilder>(out QueryBuilderContext context));

        Assert.Equal(expected, context.Query.ToString());
    }

    public class Info_TI_Hist : IHasTable
    {
        public int ATSAIS_ID { get; set; }
        public int ATSArea_ID { get; set; }
        public int TIType { get; set; }
        public int RegistrationType { get; set; }
        public int PS_ID { get; set; }
        public int TI_ID { get; set; }
        public static Table GetTable() => new Table("dbo", "Info_TI_Hist", "ti");
    }
    public class Info_TI : IHasTable
    {
        public int MRid { get; set; }
        public int TI_ID { get; set; }
        public static Table GetTable() => new Table("dbo", "Info_TI", "tio");
    }
    public class Dict_PS : IHasTable
    {
        public int PS_ID { get; set; }
        public int StringName { get; set; }
        public int HierLev3_ID { get; set; }
        public static Table GetTable() => new Table("dbo", "Dict_PS", "ps");
    }

    public class v_Dict_Hier : IHasTable
    {
        public int HierLev1_ID { get; set; }
        public int HierLev2_ID { get; set; }
        public int HierLev3_ID { get; set; }
        public int HierLev1Name { get; set; }
        public int HierLev2Name { get; set; }
        public int HierLev3Name { get; set; }
        public static Table GetTable() => new Table("dbo", "v_Dict_Hier", "h");
    }

    public class Dict_TI_RegistrationTypes : IHasTable
    {
        public int Name { get; set; }
        public int RegistrationType { get; set; }
        public static Table GetTable() => new Table("dbo", "Dict_TI_RegistrationTypes", "rt");
    }

    public class Dict_TI_Types : IHasTable
    {
        public int StringName { get; set; }
        public int TIType { get; set; }
        public static Table GetTable() => new Table("dbo", "Dict_TI_Types", "tt");
    }

    public class MGLEP_TI_COUNTRIES : IHasTable
    {
        public int COUNTRY_ID { get; set; }
        public int TI_ID { get; set; }
        public static Table GetTable() => new Table("dbo", "MGLEP_TI_COUNTRIES", "cc");
    }

    public class MGLEP_SPR_COUNTRIES : IHasTable
    {
        public int ID { get; set; }
        public int NAME { get; set; }
        public static Table GetTable() => new Table("dbo", "MGLEP_SPR_COUNTRIES", "c");
    }

    public class Dict_Areas : IHasTable
    {
        public int ATSArea_ID { get; set; }
        public int ATSAreaName { get; set; }
        public static Table GetTable() => new Table("dbo", "Dict_Areas", "areas");
    }

    public class Dict_AIS : IHasTable
    {
        public int ATSAIS_ID { get; set; }
        public int ATSAISName { get; set; }
        public static Table GetTable() => new Table("dbo", "Dict_AIS", "ais");
    }
    #endregion
    #region 2
    #region source

    /*
-- declare @startDate datetime = '2021-08-01 00:00:00';
-- declare @finishDate datetime = '2021-11-30 23:59:00';

select sub_dc_ec.*,
sub_tp_mtp.TP_ID
,sub_tp_mtp.MTPFlowType
,sub_tp_mtp.TPSD
,sub_tp_mtp.TPFD
,sub_tp_mtp.TPFederalSubject_ID
,sub_tp_mtp.TPName
,sub_tp_mtp.Section_ID
,sub_tp_mtp.TPNSIQualityStatus
,sub_tp_mtp.TP_MRid
,sub_tp_mtp.IsMTP
,sub_tp_mtp.TP2_ElectricConnection_ID
 from
(
	select sub_dc.*,
    ec.ElectricConnection_ID,
	ec.ElectricConnectionVersion_ID,
	ec.FederalSubject_ID as ECFederalSubject_ID,
    ec.Title as ECTitle,
	ec.StartDate as ECSD,
	ec.FinishDate as ECFD,
	ecr.NSIQualityStatus as ECNSIQualityStatus,
	ecr.MRid as EC_MRid,
	do2.Organization_ID as Consumer_ID,
    do2.Organization_Name as ConsumerName,
	do2.SAP_ID as Consumer_SAP_ID,
    do2.INN as ConsumerINN,
	qs.NSIQualityStatus as Consumer_NSIQualityStatus,
	ROW_NUMBER() over(partition by sub_dc.ConsumerContract_ID, ec.ElectricConnection_ID, ec.Organization_ID order by ec.StartDate desc) as rn_ec 
	from
	(
		select 
		dc1.ConsumerContract_ID,
		dc1.HierLev1_ID,
		dc1.ContractNumber,
		dc1.Organization_ID,
		dc1.SignDate,
		dc1.FinishDate,
		dc1.Perspective,
		dc1.TimeOffset,
		dc1.OLD_ID,
		dc1.Comment,
		dc1.ConsumerContract_SAP_ID,
		dc1.LastModifiedDate,
		dcct.ConsumerContract_Type_ID,
		dcct.ConsumerContract_Type_Name,
		dct.ContractorTypeVersion_ID,
		h1.StringName as HierLev1Name,
		do1.Organization_Name as ContractorName,
		h1.SAP_ID as HierLev1_SAP_ID,
		dc1.IsExistingContract,
		do1.SAP_ID as Organization_SAP_ID,
		h1.ShortCode as ShortCode
		,csap.ContractStatus as ConsumerContract_SAP_Status
		,isnull(csap.SAP_ID, '') as SAP_ID
		,csap.EDF
		,csap1.NSIQualityStatus as ConsumerContract_SAP_NSIQualityStatus
		,csap1.MRid as ConsumerContract_MRid
		,(case
			when csap.SAP_ID is null then 0
			else 1
		end) as HasSAP_ID
		, case when csap.ContractStatus = 40 then 1 when csap.ContractStatus is null and dc1.FinishDate >= @FinishDate then 1 else 0 end as IsActive
		,ROW_NUMBER() over(partition by dc1.ConsumerContract_ID, csap.ConsumerContract_SAP_ID order by csap.StartDate desc) as rn_sap
		,min(csap.ContractStatus) over (partition by csap.ConsumerContract_SAP_ID) as minStatus_40_70
		from Dict_ConsumerContract dc1
		join Dict_HierLev1 h1 on h1.HierLev1_ID = dc1.HierLev1_ID
		join Dict_Organization do1 on do1.Organization_ID = dc1.Organization_ID
		left join Dict_ContractorType_Hist dct on dct.ConsumerContract_ID = dc1.ConsumerContract_ID
		left join Dict_ConsumerContract_Type dcct on dcct.ConsumerContract_Type_ID = dct.ContractorType_ID
		left join Dict_ConsumerContract_SAP_Hist csap on dc1.ConsumerContract_SAP_ID = csap.ConsumerContract_SAP_ID
		left join Dict_ConsumerContract_SAP csap1 on dc1.ConsumerContract_SAP_ID = csap1.ConsumerContract_SAP_ID
		where (dc1.FinishDate >= @startDate and csap.ConsumerContract_SAP_ID is null) or (csap.ConsumerContract_SAP_ID is not null and csap.StartDate <= @finishdate and csap.FinishDate >= @startDate)
	) sub_dc
	left join Info_ElectricConnection_Hist ec on sub_dc.ConsumerContract_ID = ec.ConsumerContract_ID and ec.StartDate <= @finishdate and ec.FinishDate >= @startDate and ec.IsDeleted = 0
	left join Info_ElectricConnection_Registry ecr ON ecr.ElectricConnection_ID = ec.ElectricConnection_ID
	left join Dict_Organization do2 on ec.Organization_ID = do2.Organization_ID
	left join Dict_Organization_NSIQualityStatus qs on sub_dc.ConsumerContract_ID = qs.ConsumerContract_ID and ec.Organization_ID = qs.Organization_ID
	where rn_sap = 1 and isnull(minStatus_40_70, 40) = 40
) sub_dc_ec
join
(
	select 
	case when tpfs.TP_ID is null then null else tpec.TP_ID end as TP_ID
	,tpec.ElectricConnection_ID
	,tpec.MTPFlowType
	,case when tpfs.TP_ID is null then null else tpec.StartDate end as TPSD
	,case when tpfs.TP_ID is null then null else tpec.FinishDate end as TPFD
	,tpfs.FederalSubject_ID as TPFederalSubject_ID
	,case when tpfs.TP_ID is null then null else tp.StringName end as TPName
	,isd2.Section_ID
	,tpr.NSIQualityStatus as TPNSIQualityStatus
	,tpr.MRid as TP_MRid
	,0 as IsMTP
	,ROW_NUMBER() over(partition by tpec.TP_ID, tpec.ElectricConnection_ID, iech.ConsumerContract_ID, iech.Organization_ID order by tpec.StartDate desc, tp.StartDate desc, tpfs.DateStart desc) as rn_tpec
	,case when tpfs.TP_ID is null then null else tpec.TP2_ElectricConnection_ID end as TP2_ElectricConnection_ID
	,iech.ConsumerContract_ID
	,iech.Organization_ID
	from Info_TP2_ElectricConnection tpec 
	join Info_TP2_Hist tp on tpec.TP_ID = tp.TP_ID and tpec.StartDate <= tp.FinishDate and tpec.FinishDate >= tp.StartDate
	join Info_TP2 tpr on tp.TP_ID = tpr.TP_ID
	join Info_Section_Description2 isd2 on tpec.TP_ID = isd2.TP_ID
	left join Ref_TP_FederalSubject tpfs on tpec.TP_ID = tpfs.TP_ID and isnull(tpfs.DateEnd,'2099-12-31 23:59') >= @startDate and tpfs.DateStart <= @finishDate
	join Info_ElectricConnection_Hist iech on tpec.ElectricConnection_ID = iech.ElectricConnection_ID and tpec.FinishDate >= iech.StartDate and tpec.StartDate <= iech.FinishDate
	where tpec.StartDate <= @finishdate and tpec.FinishDate >= @startDate and tpec.MTPFlowType is null

	union all

	select 
	tpec.TP_ID
	,tpec.ElectricConnection_ID
	,tpec.MTPFlowType
	,tpec.StartDate as TPSD
	,case when mtp.FinishDate < tpec.FinishDate then mtp.FinishDate else tpec.FinishDate end as TPFD
	,mtp.FederalSubject_ID as TPFederalSubject_ID
	,mtp.MTP_Name as TPName
	,mtp.Section_ID
	,mtpr.NSIQualityStatus as TPNSIQualityStatus
	,mtpr.MRid as TP_MRid
	,1 as IsMTP
	,ROW_NUMBER() over(partition by tpec.TP_ID, tpec.ElectricConnection_ID, iech.ConsumerContract_ID, iech.Organization_ID order by tpec.StartDate desc, mtp.StartDate desc) as rn_tpec
	,tpec.TP2_ElectricConnection_ID
	,iech.ConsumerContract_ID
	,iech.Organization_ID
	from Info_TP2_ElectricConnection tpec 
	cross apply(select top 1 * from Info_Mediated_TP_Hist mtp where mtp.MTP_ID = tpec.TP_ID and mtp.StartDate <= @FinishDate and mtp.Deleted = 0 order by mtp.FinishDate desc) mtp
	join Info_ElectricConnection_Hist iech on tpec.ElectricConnection_ID = iech.ElectricConnection_ID and tpec.FinishDate >= iech.StartDate and tpec.StartDate <= iech.FinishDate
	join Info_Mediated_TP_Registry mtpr ON mtpr.MTP_ID=mtp.MTP_ID
	where tpec.StartDate <= @finishdate and tpec.FinishDate >= @startDate and tpec.MTPFlowType is not null

	union all

	select distinct
	null as TP_ID
	,iech.ElectricConnection_ID
	,null as MTPFlowType
	,null as TPSD
	,null as TPFD
	,null as TPFederalSubject_ID
	,null as TPName
	,null as Section_ID
	,null as TPNSIQualityStatus
	,null as TP_MRid
	,null as IsMTP
	,1 as rn_tpec
	,null as TP2_ElectricConnection_ID
	,iech.ConsumerContract_ID
	,iech.Organization_ID
	from Info_TP2_ElectricConnection tpec 
	right join Info_ElectricConnection_Hist iech on tpec.ElectricConnection_ID = iech.ElectricConnection_ID and tpec.StartDate <= @finishdate and tpec.FinishDate >= @startDate
	where tpec.ElectricConnection_ID is null

) sub_tp_mtp on sub_tp_mtp.ElectricConnection_ID = sub_dc_ec.ElectricConnection_ID and sub_dc_ec.ConsumerContract_ID = sub_tp_mtp.ConsumerContract_ID and sub_dc_ec.Consumer_ID = sub_tp_mtp.Organization_ID
where sub_dc_ec.rn_ec = 1 and sub_tp_mtp.rn_tpec = 1

union all

select 
	dc1.ConsumerContract_ID,
	dc1.HierLev1_ID,
	dc1.ContractNumber,
	dc1.Organization_ID,
	dc1.SignDate,
	dc1.FinishDate,
	dc1.Perspective,
	dc1.TimeOffset,
	dc1.OLD_ID,
	dc1.Comment,
	dc1.ConsumerContract_SAP_ID,
	dc1.LastModifiedDate,
	dcct.ConsumerContract_Type_ID,
	dcct.ConsumerContract_Type_Name,
	dct.ContractorTypeVersion_ID,
	h1.StringName as HierLev1Name,
	do1.Organization_Name as ContractorName,
	h1.SAP_ID as HierLev1_SAP_ID,
	dc1.IsExistingContract,
	do1.SAP_ID as Organization_SAP_ID,
	h1.ShortCode as ShortCode
    ,csap.ContractStatus as ConsumerContract_SAP_Status
	,isnull(csap.SAP_ID, '') as SAP_ID
	,csap.EDF
	,csap1.NSIQualityStatus as ConsumerContract_SAP_NSIQualityStatus
    ,csap1.MRid ConsumerContract_MRid
	,(case when csap.SAP_ID is null then 0 else 1 end) as HasSAP_ID
	,case when csap.ContractStatus = 40 then 1 when csap.ContractStatus is null and dc1.FinishDate >= @FinishDate then 1 else 0 end as IsActive
	,null as rn_sap
	,null as minStatus_40_70
	,
    null as ElectricConnection_ID,
	null as ElectricConnectionVersion_ID,
	null as ECFederalSubject_ID,
    null as ECTitle,
	null as ECSD,
	null as ECFD,
	null as ECNSIQualityStatus,
	null as EC_MRid,
	null as Consumer_ID,
    null as ConsumerName,
	null as Consumer_SAP_ID,
    null as ConsumerINN,
	null as Consumer_NSIQualityStatus,
	null as rn_ec,
	null as TP_ID
,null as MTPFlowType
,null as TPSD
,null as TPFD
,null as TPFederalSubject_ID
,null as TPName
,null as Section_ID
,null as TPNSIQualityStatus
,null as TP_MRid
,null as IsMTP
,null as TP2_ElectricConnection_ID
from
Dict_ConsumerContract dc1
    join Dict_HierLev1 h1 on h1.HierLev1_ID = dc1.HierLev1_ID
	join Dict_Organization do1 on do1.Organization_ID = dc1.Organization_ID
    left join Dict_ContractorType_Hist dct on dct.ConsumerContract_ID = dc1.ConsumerContract_ID and dct.FinishDate between dct.StartDate and dct.FinishDate
	left join Dict_ConsumerContract_Type dcct on dcct.ConsumerContract_Type_ID = dct.ContractorType_ID
	left join Dict_ConsumerContract_SAP_Hist csap on dc1.ConsumerContract_SAP_ID = csap.ConsumerContract_SAP_ID and @finishdate between csap.StartDate and csap.FinishDate
	left join Dict_ConsumerContract_SAP csap1 on dc1.ConsumerContract_SAP_ID = csap1.ConsumerContract_SAP_ID
	left join Info_ElectricConnection_Hist ec on dc1.ConsumerContract_ID = ec.ConsumerContract_ID and ec.StartDate <= @finishDate and ec.FinishDate >= @startDate and ec.IsDeleted = 0
where dc1.FinishDate >= @startDate and ec.ConsumerContract_ID is null
     */

    #endregion

    [Theory]
    [InlineData("")]
    public void MsQueryBuilder_Two_Build(string expected)
    {
		DateTime from = DateTime.Now, to = DateTime.Now;
		var str = string.Empty;

        Action<IMsQueryBuilder> builder = b => b
		.Select<Sub_dc_ec>(x =>
		{
			x.All();
		})
		.From(x => x.Select<Sub_dc>(x => 
					{ 
						x.All(); 
					})
					.From(x => x.Select<Dict_ConsumerContract>(x => 
								{ 
									x.Column(x => x.ConsumerContract_ID)
                                     .Column(x => x.HierLev1_ID)
                                     .Column(x => x.ContractNumber)
                                     .Column(x => x.Organization_ID)
                                     .Column(x => x.SignDate)
                                     .Column(x => x.FinishDate)
                                     .Column(x => x.Perspective)
                                     .Column(x => x.TimeOffset)
                                     .Column(x => x.OLD_ID)
                                     .Column(x => x.Comment)
                                     .Column(x => x.ConsumerContract_SAP_ID)
                                     .Column(x => x.LastModifiedDate)
                                     .Column(x => x.IsExistingContract);
									x.Bind<Dict_HierLev1>()
									 .Column(x => x.StringName).As("HierLev1Name")
									 .Column(x => x.SAP_ID).As("HierLev1_SAP_ID")
									 .Column(x => x.ShortCode);
									x.Bind<Dict_ContractorType_Hist>()
									 .Column(x => x.ContractorTypeVersion_ID);
									x.Bind<Dict_ConsumerContract_Type>()
									 .Column(x => x.ConsumerContract_Type_ID)
									 .Column(x => x.ConsumerContract_Type_Name);
									x.Bind<Dict_ConsumerContract_SAP_Hist>()
									 .Column(x => x.ContractStatus).As("ConsumerContract_SAP_Status")
									 .IsNullFunc(x => x.SAP_ID, str).As("SAP_ID")
									 .Column(x => x.EDF);
									x.Bind<Dict_ConsumerContract_SAP>()
									 .Column(x => x.NSIQualityStatus).As("ConsumerContract_SAP_NSIQualityStatus")
									 .Column(x => x.MRid).As("ConsumerContract_MRid");
                                    /*
									 ,(case
			when csap.SAP_ID is null then 0
			else 1
		end) as HasSAP_ID
									 */
                                })
								.From()
								.Join<Dict_HierLev1>(x => x.EqualTo(x => x.HierLev1_ID, x => x.HierLev1_ID))
								.Join<Dict_Organization>(x => x.EqualTo(x => x.Organization_ID, x => x.Organization_ID))
								.LeftJoin<Dict_ConsumerContract_SAP_Hist>(x => x.EqualTo(x => x.ConsumerContract_SAP_ID, x => x.ConsumerContract_SAP_ID))
                                .LeftJoin<Dict_ConsumerContract_SAP>(x => x.EqualTo(x => x.ConsumerContract_SAP_ID, x => x.ConsumerContract_SAP_ID))
                                .LeftJoin<Dict_ContractorType_Hist>(x => x.EqualTo(x => x.ConsumerContract_ID, x => x.ConsumerContract_ID))
								.LeftJoin<Dict_ConsumerContract_Type, Dict_ContractorType_Hist>(x => x.EqualTo(x => x.ConsumerContract_Type_ID, x => x.ConsumerContract_ID))
								.Where(x =>
								{
                                    var csap = x.Bind<Dict_ConsumerContract_SAP_Hist>();
                                    x.Bracket(() =>
                                    {
										x.MoreEqualTo(x => x.FinishDate, to)
										 .And();
                                        csap.IsNull(x => x.ConsumerContract_SAP_ID);
                                    })
                                    .Or()
                                    .Bracket(() =>
                                    {
                                        csap.IsNotNull(x => x.ConsumerContract_SAP_ID)
                                            .And()
                                            .LessEqualTo(x => x.StartDate, from)
											.And()
											.MoreEqualTo(x => x.FinishDate, to);
                                    });
								})
						  )
			  );



        builder(QBCore.Make<MsQueryBuilder>(out QueryBuilderContext context));

        Assert.Equal(expected, context.Query.ToString());
    }

    public class Sub_dc_ec : IHasTable
    {
        public static Table GetTable() => new Table("dbo", "Sub_dc_ec", "sub_dc_ec");
    }

	public class Sub_dc : IHasTable
	{
        public static Table GetTable() => new Table("dbo", "Sub_dc", "sub_dc");
    }

	public class Dict_ConsumerContract : IHasTable
    {
		public int ConsumerContract_ID { get; set; }
        public int HierLev1_ID { get; set; }
        public int ContractNumber { get; set; }
        public int Organization_ID { get; set; }
        public int SignDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int Perspective { get; set; }
        public int TimeOffset { get; set; }
        public int OLD_ID { get; set; }
        public int Comment { get; set; }
        public int ConsumerContract_SAP_ID { get; set; }
        public int LastModifiedDate { get; set; }
		public int IsExistingContract { get; set; }
        public static Table GetTable() => new Table("dbo", "Dict_ConsumerContract", "dc1");
    }

    public class Dict_HierLev1 : IHasTable
    {
		public int HierLev1_ID { get; set; }
        public int StringName { get; set; }
		public int SAP_ID { get; set; }
		public int ShortCode { get; set; }

        public static Table GetTable() => new Table("dbo", "Dict_HierLev1", "h1");
    }

	public class Dict_Organization : IHasTable
    {
		public int Organization_ID { get; set; }
        public static Table GetTable() => new Table("dbo", "Dict_Organization", "do1");
    }

    public class Dict_ConsumerContract_Type : IHasTable
    {
		public int ContractorType_ID { get; set; }
        public int ConsumerContract_Type_ID { get; set; }
		public int ConsumerContract_Type_Name { get; set; }
        public static Table GetTable() => new Table("dbo", "Dict_ConsumerContract_Type", "dcct");
    }

	public class Dict_ContractorType_Hist : IHasTable
    {
		public int ContractorTypeVersion_ID { get; set; }
		public int ConsumerContract_ID { get; set; }
        public static Table GetTable() => new Table("dbo", "Dict_ContractorType_Hist", "dct");
    }

	public class Dict_ConsumerContract_SAP_Hist : IHasTable
    {
		public string SAP_ID { get; set; }
        public int ContractStatus { get; set; }
        public DateTime StartDate { get; set; }
		public DateTime FinishDate { get; set; }
		public int ConsumerContract_SAP_ID { get; set; }
		public int EDF { get; set; }
        public static Table GetTable() => new Table("dbo", "Dict_ConsumerContract_SAP_Hist", "csap");
    }

	public class Dict_ConsumerContract_SAP : IHasTable
	{
		public int MRid { get; set; }
        public int NSIQualityStatus { get; set; }
        public int ConsumerContract_SAP_ID { get; set; }
        public static Table GetTable() => new Table("dbo", "Dict_ConsumerContract_SAP", "csap1");
    }

    #endregion
}
