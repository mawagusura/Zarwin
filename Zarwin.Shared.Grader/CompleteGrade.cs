using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Zarwin.Shared.Grader
{
    public class CompleteGrade
    {    
        [JsonProperty("Note de qualité")]
        public Dictionary<string, double> QualityGrades { get; set; }

        [JsonProperty("Résultats des tests")]
        public Dictionary<string, TestResult> TestResults { get; set; }

        [JsonProperty("Note actuelle")]
        public double Grade
        {
            get
            {
                var deliveryRates = TestResults
                    .Where(pair => pair.Key.StartsWith("Livraison "))
                    .Average(result => (double?)result.Value.SuccessRate)
                    ?? 1;

                var completeRate = TestResults["Complétude"].SuccessRate;

                var qualityRate = QualityGrades.Values.Average();

                return (deliveryRates + completeRate) * 10 * qualityRate;
            }
        }
    }
}
