using BDA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDA.Domain
{
    public class LocalityTypeConsts
    {
        public static readonly IReadOnlyDictionary<LocalityType, string> Dict = new Dictionary<LocalityType, string>()
        {
            { LocalityType.City, "Город" },
            { LocalityType.UrbanVillage, "Поселок городского типа"},
            { LocalityType.AgroCity, "Агрогород"}
        };
    }
}
