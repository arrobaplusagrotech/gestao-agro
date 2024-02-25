using System.Text.RegularExpressions;

namespace AgroTech.DataAccessLayer.Models
{
    public class DataBase(int id, string name)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string CustomName => CustomNameDataBase(Name);
        public ICollection<Animal> Animals { get; set; } = [];

        #region Total number of days the animal has been in the phase
        public int TotalDaysActiveMaleAnimalsInPhase
        {
            get
            {
                return Animals.Where(x => x.Status && x.Sex.ToUpper() == "M")
                     .Sum(x => x.DaysInPhase);
            }
        }
        public int TotalDaysActiveFemaleAnimalsInPhase
        {
            get
            {
                return Animals.Where(x => x.Status && x.Sex.ToUpper() == "F")
                    .Sum(x => x.DaysInPhase);
            }
        }
        public int TotalDaysInactiveMaleAnimalsInPhase
        {
            get
            {
                return Animals.Where(x => !x.Status && x.Sex.ToUpper() == "M")
                .Sum(x => x.DaysInPhase);
            }
        }
        public int TotalDaysInactiveFemaleAnimalsInPhase
        {
            get
            {
                return Animals.Where(x => !x.Status && x.Sex.ToUpper() == "F")
                     .Sum(x => x.DaysInPhase);
            }
        }
        public int TotalDaysActiveAnimalsInPhase
        {
            get
            {
                return Animals.Where(x => x.Status)
                   .Sum(x => x.DaysInPhase);
            }
        }
        public int TotalDaysInactiveAnimalsInPhase
        {
            get
            {
                return Animals.Where(x => !x.Status)
                     .Sum(x => x.DaysInPhase);
            }
        }
        #endregion
        
        #region Total start weight
        public decimal TotalStartWeightActiveMaleAnimals
        {
            get
            {
                return Animals.Where(x => x.Status && x.Sex.ToUpper() == "M")
                    .Sum(x => x.StartWeight);
            }
        }
        public decimal TotalStartWeightActiveFemaleAnimals
        {
            get
            {
                return Animals.Where(x => x.Status && x.Sex.ToUpper() == "F")
                    .Sum(x => x.StartWeight);
            }
        }
        public decimal TotalStartWeightInactiveMaleAnimals
        {
            get
            {
                return Animals.Where(x => !x.Status && x.Sex.ToUpper() == "M")
                    .Sum(x => x.StartWeight);
            }
        }
        public decimal TotalStartWeightInactiveFemaleAnimals
        {
            get
            {
                return Animals.Where(x => !x.Status && x.Sex.ToUpper() == "F")
                    .Sum(x => x.StartWeight);
            }
        }
        public decimal TotalStartWeightActiveAnimals
        {
            get
            {
                return Animals.Where(x => x.Status)
                    .Sum(x => x.StartWeight);
            }
        }
        public decimal TotalStartWeightInactiveAnimals
        {
            get
            {
                return Animals.Where(x => !x.Status)
                    .Sum(x => x.StartWeight);
            }
        }
        #endregion

        #region Total current weight
        public decimal TotalWeightActiveMaleAnimals
        {
            get
            {
                return Animals.Where(x => x.Status && x.Sex.ToUpper() == "M")
                    .Sum(x => x.CurrentWeight);
            }
        }
        public decimal TotalWeightActiveFemaleAnimals
        {
            get
            {
                return Animals.Where(x => x.Status && x.Sex.ToUpper() == "F")
                    .Sum(x => x.CurrentWeight);
            }
        }
        public decimal TotalWeightInactiveMaleAnimals
        {
            get
            {
                return Animals.Where(x => !x.Status && x.Sex.ToUpper() == "M")
                    .Sum(x => x.CurrentWeight);
            }
        }
        public decimal TotalWeightInactiveFemaleAnimals
        {
            get
            {
                return Animals.Where(x => !x.Status && x.Sex.ToUpper() == "F")
                    .Sum(x => x.CurrentWeight);
            }
        }
        public decimal TotalWeightActiveAnimals
        {
            get
            {
                return Animals.Where(x => x.Status)
                    .Sum(x => x.CurrentWeight);
            }
        }
        public decimal TotalWeightInactiveAnimals
        {
            get
            {
                return Animals.Where(x => !x.Status)
                    .Sum(x => x.CurrentWeight);
            }
        }
        #endregion

        public int TotalActiveFemaleAnimals
        {
            get
            {
                return Animals.Count(x => x.Status && x.Sex.ToUpper() == "F");
            }
        }
        public int TotalActiveMaleAnimals
        {
            get
            {
                return Animals.Count(x => x.Status && x.Sex.ToUpper() == "M");
            }
        }
        public int TotalInactiveFemaleAnimals
        {
            get
            {
                return Animals.Count(x => !x.Status && x.Sex.ToUpper() == "F");
            }
        }
        public int TotalInactiveMaleAnimals
        {
            get
            {
                return Animals.Count(x => !x.Status && x.Sex.ToUpper() == "M");
            }
        }
        public int TotalActiveAnimals
        {
            get
            {
                return TotalActiveFemaleAnimals + TotalActiveMaleAnimals;
            }
        }
        public int TotalInactiveAnimals
        {
            get
            {
                return TotalInactiveFemaleAnimals + TotalInactiveMaleAnimals;
            }
        }

        private static string CustomNameDataBase(string name)
        {
            Regex pattern = new(@"(?:dbFaz|db)?([A-Z][a-z]+|[0-9]+)");
            MatchCollection matches = pattern.Matches(name);

            string[] resultArray = new string[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                if (i == 0)
                    resultArray[i] = $"Fazenda {matches[i].Groups[1].Value}";
                else
                    resultArray[i] = matches[i].Groups[1].Value;
            }

            string result = string.Join(" ", resultArray);
            return result;
        }
    }
}
