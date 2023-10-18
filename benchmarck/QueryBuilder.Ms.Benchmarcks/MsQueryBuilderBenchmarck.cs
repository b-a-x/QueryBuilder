﻿using BenchmarkDotNet.Attributes;
using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using System.Text;

namespace QueryBuilder.Ms.Benchmarcks;

[RankColumn]
[MemoryDiagnoser]
public class MsQueryBuilderBenchmarck
{
    [Benchmark]
    public string QueryBuilder()
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

        return source.Query.ToString();
    }

    [Benchmark]
    public string QueryBuilderNameOf()
    {
        Action<IMsQueryBuilder> builder = b => b
        .Select<Info_TI_Hist>(x =>
        {
            x.All();
            x.Bind<Info_TI>().Field(nameof(Info_TI.MRid));
            x.Bind<Dict_PS>().Field(nameof(Dict_PS.StringName)).As("PSName");
            x.Bind<Dict_TI_RegistrationTypes>().Field(nameof(Dict_TI_RegistrationTypes.Name)).As("RegistrationTypeName");
            x.Bind<v_Dict_Hier>().Field(nameof(v_Dict_Hier.HierLev1_ID))
                                 .Field(nameof(v_Dict_Hier.HierLev2_ID))
                                 .Field(nameof(v_Dict_Hier.HierLev3_ID))
                                 .Field(nameof(v_Dict_Hier.HierLev1Name))
                                 .Field(nameof(v_Dict_Hier.HierLev2Name))
                                 .Field(nameof(v_Dict_Hier.HierLev3Name));
            x.Bind<MGLEP_TI_COUNTRIES>().Field(nameof(MGLEP_TI_COUNTRIES.COUNTRY_ID));
            x.Bind<MGLEP_SPR_COUNTRIES>().Field(nameof(MGLEP_SPR_COUNTRIES.NAME)).As("CountryName");
            x.Bind<Dict_Areas>().Field(nameof(Dict_Areas.ATSAreaName));
            x.Bind<Dict_AIS>().Field(nameof(Dict_AIS.ATSAISName));
        })
        .Join<Info_TI>(x => x.EqualTo(nameof(Info_TI_Hist.TI_ID), nameof(Info_TI.TI_ID)))
        .Join<Dict_PS>(x => x.EqualTo(nameof(Info_TI_Hist.PS_ID), nameof(Dict_PS.PS_ID)))
        .Join<Dict_TI_RegistrationTypes>(x => x.EqualTo(nameof(Info_TI_Hist.RegistrationType), nameof(Dict_TI_RegistrationTypes.RegistrationType)))
        .Join<Dict_TI_Types>(x => x.EqualTo(nameof(Info_TI_Hist.TIType), nameof(Dict_TI_Types.TIType)))
        .Join<Dict_Areas>(x => x.EqualTo(nameof(Info_TI_Hist.ATSArea_ID), nameof(Dict_Areas.ATSArea_ID)))
        .Join<Dict_AIS>(x => x.EqualTo(nameof(Info_TI_Hist.ATSAIS_ID), nameof(Dict_AIS.ATSAIS_ID)))
        .LeftJoin<MGLEP_TI_COUNTRIES>(x => x.EqualTo(nameof(Info_TI_Hist.TI_ID), nameof(MGLEP_TI_COUNTRIES.TI_ID)))
        .LeftJoin<MGLEP_TI_COUNTRIES, MGLEP_SPR_COUNTRIES>(x => x.EqualTo(nameof(MGLEP_TI_COUNTRIES.COUNTRY_ID), nameof(MGLEP_SPR_COUNTRIES.ID)))
        .Join<Dict_PS, v_Dict_Hier>(x => x.EqualTo(nameof(Dict_PS.HierLev3_ID), nameof(v_Dict_Hier.HierLev3_ID)));

        var source = new QueryBuilderSource();
        builder(new MsQueryBuilder(source));

        return source.Query.ToString();
    }

    [Benchmark]
    public string QueryBuilderString()
    {
        Action<IMsQueryBuilder> builder = b => b
        .Select<Info_TI_Hist>(x =>
        {
            x.All();
            x.Bind<Info_TI>().Field("MRid");
            x.Bind<Dict_PS>().Field("StringName").As("PSName");
            x.Bind<Dict_TI_RegistrationTypes>().Field("Name").As("RegistrationTypeName");
            x.Bind<v_Dict_Hier>().Field("HierLev1_ID")
                                 .Field("HierLev2_ID")
                                 .Field("HierLev3_ID")
                                 .Field("HierLev1Name")
                                 .Field("HierLev2Name")
                                 .Field("HierLev3Name");
            x.Bind<MGLEP_TI_COUNTRIES>().Field("COUNTRY_ID");
            x.Bind<MGLEP_SPR_COUNTRIES>().Field("NAME").As("CountryName");
            x.Bind<Dict_Areas>().Field("ATSAreaName");
            x.Bind<Dict_AIS>().Field("ATSAISName");
        })
        .Join<Info_TI>(x => x.EqualTo("TI_ID", "TI_ID"))
        .Join<Dict_PS>(x => x.EqualTo("PS_ID", "PS_ID"))
        .Join<Dict_TI_RegistrationTypes>(x => x.EqualTo("RegistrationType", "RegistrationType"))
        .Join<Dict_TI_Types>(x => x.EqualTo("TIType", "TIType"))
        .Join<Dict_Areas>(x => x.EqualTo("ATSArea_ID", "ATSArea_ID"))
        .Join<Dict_AIS>(x => x.EqualTo("ATSAIS_ID", "ATSAIS_ID"))
        .LeftJoin<MGLEP_TI_COUNTRIES>(x => x.EqualTo("TI_ID", "TI_ID"))
        .LeftJoin<MGLEP_TI_COUNTRIES, MGLEP_SPR_COUNTRIES>(x => x.EqualTo("COUNTRY_ID", "ID"))
        .Join<Dict_PS, v_Dict_Hier>(x => x.EqualTo("HierLev3_ID", "HierLev3_ID"));

        var source = new QueryBuilderSource();
        builder(new MsQueryBuilder(source));

        return source.Query.ToString();
    }

    [Benchmark]
    public string LoadFile()
    {
        var result = new StringBuilder();
        string path = "Files/TestOne.sql";
        using (StreamReader reader = new StreamReader(path))
        {
            string text = reader.ReadToEnd();
            result.Append(text);
        }
        return result.ToString();
    }

    [Benchmark]
    public string StringBuilder()
    {
        string name = null;
        int age = 10;
        var builder = new StringBuilder();
        builder.AppendLine("update BenchmarkClass");
        builder.AppendLine("set ");
        builder.Append("Id = ");
        builder.Append("'").Append(Guid.Empty).Append("'").Append(", ");
        builder.Append("Name = ").Append(name).Append(", ");
        builder.Append("Age = ").Append(age).Append(", ");
        builder.Append("Timespan = ").Append("'").Append(new DateTime(2023, 04, 23)).Append("'");

        builder.AppendLine("update BenchmarkClass");
        builder.AppendLine("set ");
        builder.Append("Id = ");
        builder.Append("'").Append(Guid.Empty).Append("'").Append(", ");
        builder.Append("Name = ").Append(name).Append(", ");
        builder.Append("Age = ").Append(age).Append(", ");
        builder.Append("Timespan = ").Append("'").Append(new DateTime(2023, 04, 23)).Append("'");

        builder.AppendLine("update BenchmarkClass");
        builder.AppendLine("set ");
        builder.Append("Id = ");
        builder.Append("'").Append(Guid.Empty).Append("'").Append(", ");
        builder.Append("Name = ").Append(name).Append(", ");
        builder.Append("Age = ").Append(age).Append(", ");
        builder.Append("Timespan = ").Append("'").Append(new DateTime(2023, 04, 23)).Append("'");
        return builder.ToString();
    }
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
