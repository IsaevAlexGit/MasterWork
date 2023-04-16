namespace Optimum
{
    // Критерий
    public class Criterion
    {
        public string nameOfCriterion { get; set; }
        // Направление критерия: false - минимизация, true - максимизация
        public bool direction { get; set; }
        // Вес критерия
        public double weightOfCriterion { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Criterion() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Name">Название критерия оптимальности</param>
        /// <param name="Weight">Весовой коэффициент критерия оптимальности</param>
        /// <param name="Direction">Направление критерия (минимазация или максимизация)</param>
        public Criterion(string Name, bool Direction, double Weight)
        {
            nameOfCriterion = Name;
            direction = Direction;
            weightOfCriterion = Weight;            
        }
    }
}