using BenchmarkDotNet.Attributes;
using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using System.Text;

namespace QueryBuilder.Ms.Benchmarcks;

/*
 BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Windows 10 (10.0.19045.3570/22H2/2022Update)
AMD Ryzen 7 3800X, 1 CPU, 16 logical and 8 physical cores
.NET SDK 7.0.401
  [Host]     : .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2 [AttachedDebugger]
  DefaultJob : .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2


| Method             | Mean       | Error     | StdDev    | Rank | Gen0   | Gen1   | Allocated |
|------------------- |-----------:|----------:|----------:|-----:|-------:|-------:|----------:|
| QueryBuilder       |  20.810 μs | 0.1022 μs | 0.0956 μs |    3 | 2.5330 | 0.0305 |  20.73 KB |
| QueryBuilderNameOf |   2.162 μs | 0.0390 μs | 0.0345 μs |    2 | 0.5722 | 0.0038 |    4.7 KB |
| QueryBuilderString |   2.156 μs | 0.0425 μs | 0.0455 μs |    2 | 0.5722 | 0.0038 |    4.7 KB |
| LoadFile           | 110.908 μs | 1.4929 μs | 1.3965 μs |    4 | 1.7090 |      - |  14.92 KB |
| StringBuilder      |   1.066 μs | 0.0153 μs | 0.0143 μs |    1 | 0.3433 |      - |    2.8 KB |
 */

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
            x.Bind<Info_TI>().Column(nameof(Info_TI.MRid));
            x.Bind<Dict_PS>().Column(nameof(Dict_PS.StringName)).As("PSName");
            x.Bind<Dict_TI_RegistrationTypes>().Column(nameof(Dict_TI_RegistrationTypes.Name)).As("RegistrationTypeName");
            x.Bind<v_Dict_Hier>().Column(nameof(v_Dict_Hier.HierLev1_ID))
                                 .Column(nameof(v_Dict_Hier.HierLev2_ID))
                                 .Column(nameof(v_Dict_Hier.HierLev3_ID))
                                 .Column(nameof(v_Dict_Hier.HierLev1Name))
                                 .Column(nameof(v_Dict_Hier.HierLev2Name))
                                 .Column(nameof(v_Dict_Hier.HierLev3Name));
            x.Bind<MGLEP_TI_COUNTRIES>().Column(nameof(MGLEP_TI_COUNTRIES.COUNTRY_ID));
            x.Bind<MGLEP_SPR_COUNTRIES>().Column(nameof(MGLEP_SPR_COUNTRIES.NAME)).As("CountryName");
            x.Bind<Dict_Areas>().Column(nameof(Dict_Areas.ATSAreaName));
            x.Bind<Dict_AIS>().Column(nameof(Dict_AIS.ATSAISName));
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
            x.Bind<Info_TI>().Column("MRid");
            x.Bind<Dict_PS>().Column("StringName").As("PSName");
            x.Bind<Dict_TI_RegistrationTypes>().Column("Name").As("RegistrationTypeName");
            x.Bind<v_Dict_Hier>().Column("HierLev1_ID")
                                 .Column("HierLev2_ID")
                                 .Column("HierLev3_ID")
                                 .Column("HierLev1Name")
                                 .Column("HierLev2Name")
                                 .Column("HierLev3Name");
            x.Bind<MGLEP_TI_COUNTRIES>().Column("COUNTRY_ID");
            x.Bind<MGLEP_SPR_COUNTRIES>().Column("NAME").As("CountryName");
            x.Bind<Dict_Areas>().Column("ATSAreaName");
            x.Bind<Dict_AIS>().Column("ATSAISName");
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
