using System;

namespace DevFitness.API.Models.inputModels
{
    public class UpdateMealInputModel
    {
        public string Description { get; set; }
        public int Calories { get; set; }
        public DateTime Date { get; set; }
    }
}
