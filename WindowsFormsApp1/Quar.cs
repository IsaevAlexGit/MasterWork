using System.Collections.Generic;

namespace Optimum
{
    public class Quar
    {
        public int idQuartet { get; set; }
        public int countBoundaryPoints { get; set; }
        public List<Location> listBoundaryPoints { get; set; }
        public double xCentreOfQuartet { get; set; }
        public double yCentreOfQuartet { get; set; }
        public List<double> valuesEveryCriterionForQuartet { get; set; }

        public Quar() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id">ID полигона</param>
        /// <param name="Count">Количество граничных точек</param>
        /// <param name="List">Список граничных точек</param>
        /// <param name="X">Х центра полигона</param>
        /// <param name="Y">Y центра полигона</param>
        /// <param name="values">Все значения для каждого критерия полигона</param>
        public Quar(int Id, int Count, List<Location> List, double X, double Y, List<double> values)
        {
            idQuartet = Id;
            countBoundaryPoints = Count;
            listBoundaryPoints = List;
            xCentreOfQuartet = X;
            yCentreOfQuartet = Y;
            valuesEveryCriterionForQuartet = values;
        }
    }
}