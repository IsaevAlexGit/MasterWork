using System.Collections.Generic;

namespace Optimum
{
    //! Класс "Полигон"
    public class Polygon
    {
        //! Идентификатор полигона
        public int idPolygon { get; set; }
        //! Количество граничных точек полигона
        public int countBoundaryPoints { get; set; }
        //! Список граничных точек полигона
        public List<Location> listBoundaryPoints { get; set; }
        //! Координа Х центра полигона
        public double xCenterOfPolygon { get; set; }
        //! Координата Y центра полигона
        public double yCenterOfPolygon { get; set; }
        //! Список значений для каждого критерия
        public List<double> valuesForEveryCriterionOfPolygon { get; set; }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="IdPolygon">Идентификатор полигона</param>
        /// <param name="CountBoundaryPoints">Количество граничных точек полигона</param>
        /// <param name="ListBoundaryPoints">Список граничных точек полигона</param>
        /// <param name="X">Координа Х центра полигона</param>
        /// <param name="Y">Координата Y центра полигона</param>
        /// <param name="ValuesForEveryCriterionOfPolygon">Список значений для каждого критерия</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public Polygon(int IdPolygon, int CountBoundaryPoints, List<Location> ListBoundaryPoints,
            double X, double Y, List<double> ValuesForEveryCriterionOfPolygon)
        {
            idPolygon = IdPolygon;
            countBoundaryPoints = CountBoundaryPoints;
            listBoundaryPoints = ListBoundaryPoints;
            xCenterOfPolygon = X;
            yCenterOfPolygon = Y;
            valuesForEveryCriterionOfPolygon = ValuesForEveryCriterionOfPolygon;
        }
    }
}