using BDA.Domain;
using System.Collections;
using System.Collections.Generic;

namespace BDA.Domain
{
    public class CitizenshipConsts
    {
        public static readonly IReadOnlyDictionary<Citizenship, string> Dict = new Dictionary<Citizenship, string>()
        {
            { Citizenship.PB, "Гражданин Республики Беларусь" },
            { Citizenship.BA, "Лицо без гражданства, постоянно проживающее в республике"},
            { Citizenship.BI, "Иностранный гражданин, постоянно проживающий в стране"}
        };
    }
}
