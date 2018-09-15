using Newtonsoft.Json;

namespace Zarwin.Shared.Grader
{
    public class QualityGrade
    {    
        [JsonProperty("Nommages variables")]
        public double VariableNaming { get; set; }

        [JsonProperty("Nommages et types de membres")]
        public double MembersNamingAndTypes { get; set; }

        [JsonProperty("Structure méthodes")]
        public double MethodStructure { get; set; }

        [JsonProperty("Structure classes")]
        public double ClassStructure { get; set; }

        [JsonProperty("Pas de duplications de code")]
        public double NoCodeDupplication { get; set; }

        [JsonProperty("Séparation de responsabilités")]
        public double SeparationOfResponsibilities { get; set; }

        [JsonProperty("Encapsulation")]
        public double Encapsulation { get; set; }

        [JsonProperty("Tests unitaires")]
        public double UnitTests { get; set; }
    }
}
