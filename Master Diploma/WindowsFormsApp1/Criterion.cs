namespace Optimum
{
    // ICloneable позволяет копировать список объектов
    // Класс "Частный критерий оптимальности"
    public class Criterion
    {
        // Название критерия оптимальности
        public string nameOfCriterion { get; set; }
        // Направление критерия
        // false - минимизация, true - максимизация
        public bool directionOfCriterion { get; set; }
        // Весовой коэффициент критерия оптимальности
        public double weightOfCriterion { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Criterion() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="NameOfCriterion">Название критерия оптимальности</param>
        /// <param name="DirectionOfCriterion">Направление критерия</param>
        /// <param name="WeightOfCriterion">Весовой коэффициент</param>
        public Criterion(string NameOfCriterion, bool DirectionOfCriterion, double WeightOfCriterion)
        {
            nameOfCriterion = NameOfCriterion;
            directionOfCriterion = DirectionOfCriterion;
            weightOfCriterion = WeightOfCriterion;            
        }

        /// <summary>
        /// Клонирование объекта, но не ссылкой, а по значению
        /// </summary>
        /// <returns>объект класса BufferZone</returns>
        public object Clone()
        {
            return new Criterion
            {
                nameOfCriterion = this.nameOfCriterion,
                directionOfCriterion = this.directionOfCriterion,
                weightOfCriterion = this.weightOfCriterion
            };
        }
    }
}