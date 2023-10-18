using BenchmarkDotNet.Attributes;
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
