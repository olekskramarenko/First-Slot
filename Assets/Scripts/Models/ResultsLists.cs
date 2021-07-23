using System.Collections.Generic;

public class ResultsLists
{
    public List<Symbol> WinSymbolsLineList { get; set; }
    public List<Symbol> OtherSymbolsLineList { get; set; }

    public ResultsLists()
    {
        WinSymbolsLineList = new List<Symbol>();
        OtherSymbolsLineList = new List<Symbol>();
    }
}
