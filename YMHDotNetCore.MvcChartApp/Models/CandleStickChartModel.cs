namespace YMHDotNetCore.MvcChartApp.Models;

public class CandleStickDataPoint
{
    public string x { get; set; }
    public List<int> y { get; set; }
}

public class CandleStickChartModel
{
    public List<CandleStickDataPoint> Data { get; set; }
}
