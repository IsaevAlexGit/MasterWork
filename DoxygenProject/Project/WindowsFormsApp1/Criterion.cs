namespace Optimum
{
    //! Класс "Частный критерий оптимальности"
    public class Criterion
    {
        //! Название критерия оптимальности
        public string nameOfCriterion { get; set; }
        //! Направление критерия, false - минимизация, true - максимизация
        public bool directionOfCriterion { get; set; }
        //! Весовой коэффициент критерия оптимальности
        public double weightOfCriterion { get; set; }

        /*!
         \version 1.0
        */
        /// <summary>
        /// Конструктор критерия
        /// </summary>
        /// <permission cref="">Доступ к функции: public</permission>
        public Criterion() { }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Конструктор критерия
        /// </summary>
        /// <permission cref="">Доступ к функции: public</permission>
        /// <param name="NameOfCriterion">Название критерия оптимальности</param>
        /// <param name="DirectionOfCriterion">Направление критерия</param>
        /// <param name="WeightOfCriterion">Весовой коэффициент</param>
        /// <remarks>
        /// Функция создаёт критерий
        /// </remarks>
        public Criterion(string NameOfCriterion, bool DirectionOfCriterion, double WeightOfCriterion)
        {
            nameOfCriterion = NameOfCriterion;
            directionOfCriterion = DirectionOfCriterion;
            weightOfCriterion = WeightOfCriterion;
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Клонирование объекта, но не ссылкой, а по значению
        /// </summary>
        /// <permission cref="">Доступ к функции: public</permission>
        /// <remarks>
        /// Функция клонирует объект класса Критерий
        /// </remarks>
        /// <returns>Объект класса Criterion</returns>
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