using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

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
            x.Bind<Info_TI>().Field(x => x.MRid);
            x.Bind<Dict_PS>().Field(x => x.StringName).As("PSName");
            x.Bind<Dict_TI_RegistrationTypes>().Field(x => x.Name).As("RegistrationTypeName");
            x.Bind<v_Dict_Hier>().Field(x => x.HierLev1_ID)
                                 .Field(x => x.HierLev2_ID)
                                 .Field(x => x.HierLev3_ID)
                                 .Field(x => x.HierLev1Name)
                                 .Field(x => x.HierLev2Name)
                                 .Field(x => x.HierLev3Name);
            x.Bind<MGLEP_TI_COUNTRIES>().Field(x => x.COUNTRY_ID);
            x.Bind<MGLEP_SPR_COUNTRIES>().Field(x => x.NAME).As("CountryName");
            x.Bind<Dict_Areas>().Field(x => x.ATSAreaName);
            x.Bind<Dict_AIS>().Field(x => x.ATSAISName);
        })
        .Join<Info_TI>(x => x.EqualTo(x => x.TI_ID, x => x.TI_ID))
        .Join<Dict_PS>(x => x.EqualTo(x => x.PS_ID, x => x.PS_ID))
        .Join<Dict_TI_RegistrationTypes>(x => x.EqualTo(x => x.RegistrationType, x => x.RegistrationType))
        .Join<Dict_TI_Types>(x => x.EqualTo(x => x.TIType, x => x.TIType))
        .Join<Dict_Areas>(x => x.EqualTo(x => x.ATSArea_ID, x => x.ATSArea_ID))
        .Join<Dict_AIS>(x => x.EqualTo(x => x.ATSAIS_ID, x => x.ATSAIS_ID))
        .LeftJoin<MGLEP_TI_COUNTRIES>(x => x.EqualTo(x => x.TI_ID, x => x.TI_ID))
        .LeftJoin<MGLEP_TI_COUNTRIES, MGLEP_SPR_COUNTRIES>(x => x.EqualTo(x => x.COUNTRY_ID, x => x.ID))
        .Join<Dict_PS, v_Dict_Hier>(x => x.EqualTo(x => x.HierLev3_ID, x => x.HierLev3_ID));
        
        
        var source = new QueryBuilderSource();
        builder(new MsQueryBuilder(source));
        
        Assert.Equal(expected, source.Query.ToString());
    }

    public class Info_TI_Hist : ITableBuilder
    {
        public int ATSAIS_ID { get; set; }
        public int ATSArea_ID { get; set; }
        public int TIType { get; set; }
        public int RegistrationType { get; set; }
        public int PS_ID { get; set; }
        public int TI_ID { get; set; }
        public static TableBuilder GetTable() => new TableBuilder("dbo", "Info_TI_Hist", "ti");
    }
    public class Info_TI : ITableBuilder
    {
        public int MRid { get; set; }
        public int TI_ID { get; set; }
        public static TableBuilder GetTable() => new TableBuilder("dbo", "Info_TI", "tio");
    }
    public class Dict_PS : ITableBuilder
    {
        public int PS_ID { get; set; }
        public int StringName { get; set; }
        public int HierLev3_ID { get; set; }
        public static TableBuilder GetTable() => new TableBuilder("dbo", "Dict_PS", "ps");
    }

    public class v_Dict_Hier : ITableBuilder
    {
        public int HierLev1_ID { get; set; }
        public int HierLev2_ID { get; set; }
        public int HierLev3_ID { get; set; }
        public int HierLev1Name { get; set; }
        public int HierLev2Name { get; set; }
        public int HierLev3Name { get; set; }
        public static TableBuilder GetTable() => new TableBuilder("dbo", "v_Dict_Hier", "h");
    }

    public class Dict_TI_RegistrationTypes : ITableBuilder
    {
        public int Name { get; set; }
        public int RegistrationType { get; set; }
        public static TableBuilder GetTable() => new TableBuilder("dbo", "Dict_TI_RegistrationTypes", "rt");
    }

    public class Dict_TI_Types : ITableBuilder
    {
        public int StringName { get; set; }
        public int TIType { get; set; }
        public static TableBuilder GetTable() => new TableBuilder("dbo", "Dict_TI_Types", "tt");
    }

    public class MGLEP_TI_COUNTRIES : ITableBuilder
    {
        public int COUNTRY_ID { get; set; }
        public int TI_ID { get; set; }
        public static TableBuilder GetTable() => new TableBuilder("dbo", "MGLEP_TI_COUNTRIES", "cc");
    }

    public class MGLEP_SPR_COUNTRIES : ITableBuilder
    {
        public int ID { get; set; }
        public int NAME { get; set; }
        public static TableBuilder GetTable() => new TableBuilder("dbo", "MGLEP_SPR_COUNTRIES", "c");
    }

    public class Dict_Areas : ITableBuilder
    {
        public int ATSArea_ID { get; set; }
        public int ATSAreaName { get; set; }
        public static TableBuilder GetTable() => new TableBuilder("dbo", "Dict_Areas", "areas");
    }

    public class Dict_AIS : ITableBuilder
    {
        public int ATSAIS_ID { get; set; }
        public int ATSAISName { get; set; }
        public static TableBuilder GetTable() => new TableBuilder("dbo", "Dict_AIS", "ais");
    }
    #endregion
}
