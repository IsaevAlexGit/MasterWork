using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyCalcLib.Tests
{
    [TestClass]
    public class OptimumTests
    {
        [TestMethod]
        // Найти оптимум из 3 точек-кандидатов
        public void TestSearchOptimumOfThreePoints()
        {
            // Arrange
            // Критерии и буферные зоны
            List<Criterion> criterion = new List<Criterion>
            {
                new Criterion("Критерий 1", true, 0.4),
                new Criterion("Критерий 2", false, 0.3),
                new Criterion("Критерий 3", false, 0.3)
            };
            List<BufferZone> zonesCandidate = new List<BufferZone>
            {
                new BufferZone(1, 49.29440052, -123.0235076, 1200, new List<double> {3438, 7, 1430 }),
                new BufferZone(2, 49.29273519, -123.0233574, 1200, new List<double> {2507, 11, 1073 }),
                new BufferZone(3, 49.29120976, -123.0234218, 1200, new List<double> {5622, 18, 1021 })
            };

            // Список оптимальных точек
            List<OptimalZone> expected = new List<OptimalZone>
            {
                new OptimalZone(zonesCandidate[0], new List<string>(){ "Свёртка Гермейера", "Свёртка на основе идеальной точки по метрике Чебышева"}),
                new OptimalZone(zonesCandidate[2], new List<string>(){ "Линейная свёртка", "Мультипликативная свёртка", "Свёртка агрегирования",
                    "Свёртка на основе идеальной точки по метрике Хемминга", "Свёртка на основе идеальной точки по метрике Евклида"})
            };

            Optimum point = new Optimum();
            // Act
            // Ищем оптимальные точки
            List<OptimalZone> actual = point.getOptimum(zonesCandidate, criterion);
            // Assert
            // Проверка, совпадает ли ожидаемый результат с полученным: списки должны быть одинаковы
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].optimalZone.idBufferZone, actual[i].optimalZone.idBufferZone);
                Assert.AreEqual(expected[i].optimalZone.x, actual[i].optimalZone.x);
                Assert.AreEqual(expected[i].optimalZone.y, actual[i].optimalZone.y);
                Assert.AreEqual(expected[i].namesConvolutiones.Count, actual[i].namesConvolutiones.Count);
            }
        }

        [TestMethod]
        // Найти оптимум из 10 точек-кандидатов
        public void TestSearchOptimumOfTenPoints()
        {
            // Arrange
            // Критерии и буферные зоны
            List<Criterion> criterion = new List<Criterion>
            {
                new Criterion("Критерий 1", true, 0.4),
                new Criterion("Критерий 2", false, 0.3),
                new Criterion("Критерий 3", false, 0.3)
            };
            List<BufferZone> zonesCandidate = new List<BufferZone>
            {
                new BufferZone(1, 47.24864468, 38.80555705, 2500, new List<double> { 2181, 7, 488 }),
                new BufferZone(2, 47.25495629, 38.81113604, 2500, new List<double> { 2080, 6, 1070 }),
                new BufferZone(3, 47.26082892, 38.8174446, 2500, new List<double> { 3227, 12, 1150 }),
                new BufferZone(4, 47.27704098, 38.96992281, 2500, new List<double> { 4629, 16, 671 }),
                new BufferZone(5, 47.2444219, 38.94131974, 2500, new List<double> { 2218, 6, 1329 }),
                new BufferZone(6, 47.22635805, 38.93655613, 2500, new List<double> { 4262, 10, 1419 }),
                new BufferZone(7, 47.20213165, 38.95460204, 2500, new List<double> { 5737, 8, 2625 }),
                new BufferZone(8, 47.19830003, 38.89329747, 2500, new List<double> { 1711, 1, 1069 }),
                new BufferZone(9, 47.18929022, 38.87370661, 2500, new List<double> { 2439, 7, 853 }),
                new BufferZone(10, 47.21932667, 38.81916121, 2500, new List<double> { 3438, 7, 1430 }),
            };

            // Список оптимальных точек
            List<OptimalZone> expected = new List<OptimalZone>
            {
                new OptimalZone(zonesCandidate[3], new List<string>(){ "Линейная свёртка", "Свёртка на основе идеальной точки по метрике Хемминга"}),
                new OptimalZone(zonesCandidate[5], new List<string>(){ "Свёртка на основе идеальной точки по метрике Чебышева",
                    "Свёртка на основе идеальной точки по метрике Евклида" }),
                new OptimalZone(zonesCandidate[6], new List<string>(){ "Мультипликативная свёртка", "Свёртка агрегирования"}),
                new OptimalZone(zonesCandidate[9], new List<string>(){ "Свёртка Гермейера"})
            };

            Optimum point = new Optimum();
            // Act
            // Ищем оптимальные точки
            List<OptimalZone> actual = point.getOptimum(zonesCandidate, criterion);
            // Assert
            // Проверка, совпадает ли ожидаемый результат с полученным: списки должны быть одинаковы
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].optimalZone.idBufferZone, actual[i].optimalZone.idBufferZone);
                Assert.AreEqual(expected[i].optimalZone.x, actual[i].optimalZone.x);
                Assert.AreEqual(expected[i].optimalZone.y, actual[i].optimalZone.y);
                Assert.AreEqual(expected[i].namesConvolutiones.Count, actual[i].namesConvolutiones.Count);
            }
        }

        [TestMethod]
        // Найдется один оптимум
        public void TestSearchOneOptimim()
        {
            // Arrange
            // Критерии и буферные зоны
            List<Criterion> criterion = new List<Criterion>
            {
                new Criterion("Критерий 1", true, 1),
                new Criterion("Критерий 2", false, 0),
                new Criterion("Критерий 3", false, 0)
            };
            List<BufferZone> zonesCandidate = new List<BufferZone>
            {
                new BufferZone(1, 47.23371003, 38.80504206, 300, new List<double> { 5419, 11, 1060 }),
                new BufferZone(2, 47.23707142, 38.80444125, 300, new List<double> { 2459, 10, 803 }),
                new BufferZone(3, 47.24332597, 38.80774573, 300, new List<double> { 2754, 14, 1332 })
            };

            // Список оптимальных точек
            List<OptimalZone> expected = new List<OptimalZone>
            {
                new OptimalZone(zonesCandidate[0], new List<string>(){ "Линейная свёртка", "Мультипликативная свёртка",
                "Свёртка агрегирования","Свёртка Гермейера","Метод главного критерия","Свёртка на основе идеальной точки по метрике Чебышева",
                "Свёртка на основе идеальной точки по метрике Хемминга","Свёртка на основе идеальной точки по метрике Евклида"})
            };

            Optimum point = new Optimum();
            // Act
            // Ищем оптимальные точки
            List<OptimalZone> actual = point.getOptimum(zonesCandidate, criterion);
            // Assert
            // Проверка, совпадает ли ожидаемый результат с полученным: списки должны быть одинаковы
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].optimalZone.idBufferZone, actual[i].optimalZone.idBufferZone);
                Assert.AreEqual(expected[i].optimalZone.x, actual[i].optimalZone.x);
                Assert.AreEqual(expected[i].optimalZone.y, actual[i].optimalZone.y);
                Assert.AreEqual(expected[i].namesConvolutiones.Count, actual[i].namesConvolutiones.Count);
            }
        }

        [TestMethod]
        // Найдется два оптимума
        public void TestSearchTwoOptimim()
        {
            // Arrange
            // Критерии и буферные зоны
            List<Criterion> criterion = new List<Criterion>
            {
                new Criterion("Критерий 1", true, 0.4),
                new Criterion("Критерий 2", false, 0.3),
                new Criterion("Критерий 3", false, 0.3)
            };
            List<BufferZone> zonesCandidate = new List<BufferZone>
            {
                new BufferZone(1, 55.656974, 37.841433, 2500, new List<double> { 4629, 16, 671 }),
                new BufferZone(2, 55.6565750965931, 37.8393173217773, 2500, new List<double> { 3116, 8, 1666 }),
                new BufferZone(3, 55.6505703634409, 37.8343391418457, 2500, new List<double> { 3353, 23, 2596 })
            };

            // Список оптимальных точек
            List<OptimalZone> expected = new List<OptimalZone>
            {
                new OptimalZone(zonesCandidate[0], new List<string>(){ "Линейная свёртка", "Свёртка агрегирования", "Свёртка Гермейера",
                "Свёртка на основе идеальной точки по метрике Чебышева","Свёртка на основе идеальной точки по метрике Хемминга",
                    "Свёртка на основе идеальной точки по метрике Евклида" }),
                new OptimalZone(zonesCandidate[2], new List<string>(){ "Мультипликативная свёртка" }),
            };

            Optimum point = new Optimum();
            // Act
            // Ищем оптимальные точки
            List<OptimalZone> actual = point.getOptimum(zonesCandidate, criterion);
            // Assert
            // Проверка, совпадает ли ожидаемый результат с полученным: списки должны быть одинаковы
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].optimalZone.idBufferZone, actual[i].optimalZone.idBufferZone);
                Assert.AreEqual(expected[i].optimalZone.x, actual[i].optimalZone.x);
                Assert.AreEqual(expected[i].optimalZone.y, actual[i].optimalZone.y);
                Assert.AreEqual(expected[i].namesConvolutiones.Count, actual[i].namesConvolutiones.Count);
            }
        }

        [TestMethod]
        // Найдется несколько оптимумов
        public void TestSearchSomeOptimim()
        {
            // Arrange
            // Критерии и буферные зоны
            List<Criterion> criterion = new List<Criterion>
            {
                new Criterion("Критерий 1", true, 0.4),
                new Criterion("Критерий 2", false, 0.3),
                new Criterion("Критерий 3", false, 0.3)
            };
            List<BufferZone> zonesCandidate = new List<BufferZone>
            {
                new BufferZone(1, 6.40401313, 3.421897888, 500, new List<double> { 4271, 9, 1028 }),
                new BufferZone(2, 6.410495517, 3.444213867, 500, new List<double> { 4966, 15, 1158 }),
                new BufferZone(3, 6.416295478, 3.479919434, 500, new List<double> { 3811, 9, 1001 }),
                new BufferZone(4, 6.417660165, 3.551330566, 500, new List<double> { 6995, 12, 1867 }),
                new BufferZone(5, 6.416295478, 3.698272705, 500, new List<double> { 3532, 5, 1192 }),
                new BufferZone(6, 6.429942185, 3.849334717, 500, new List<double> { 4089, 19, 1736 })
            };

            // Список оптимальных точек
            List<OptimalZone> expected = new List<OptimalZone>
            {
                new OptimalZone(zonesCandidate[0], new List<string>(){ "Линейная свёртка", "Свёртка на основе идеальной точки по метрике Хемминга",
                "Свёртка на основе идеальной точки по метрике Евклида"}),
                new OptimalZone(zonesCandidate[1], new List<string>(){ "Свёртка Гермейера",
                    "Свёртка на основе идеальной точки по метрике Чебышева" }),
                new OptimalZone(zonesCandidate[3], new List<string>(){ "Мультипликативная свёртка", "Свёртка агрегирования"}),
            };

            Optimum point = new Optimum();
            // Act
            // Ищем оптимальные точки
            List<OptimalZone> actual = point.getOptimum(zonesCandidate, criterion);
            // Assert
            // Проверка, совпадает ли ожидаемый результат с полученным: списки должны быть одинаковы
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].optimalZone.idBufferZone, actual[i].optimalZone.idBufferZone);
                Assert.AreEqual(expected[i].optimalZone.x, actual[i].optimalZone.x);
                Assert.AreEqual(expected[i].optimalZone.y, actual[i].optimalZone.y);
                Assert.AreEqual(expected[i].namesConvolutiones.Count, actual[i].namesConvolutiones.Count);
            }
        }

        [TestMethod]
        // Найти оптимум, задан 1 критерий
        public void TestSearchOptimumWithOneCriterion()
        {
            // Arrange
            // Критерии и буферные зоны
            List<Criterion> criterion = new List<Criterion>
            {
                new Criterion("Критерий 1", true, 1)
            };
            List<BufferZone> zonesCandidate = new List<BufferZone>
            {
                new BufferZone(1, 55.5276103359582, 37.5815677642822, 500, new List<double> { 1922 }),
                new BufferZone(2, 55.5278775302477, 37.5798511505127, 500, new List<double> { 2635 }),
                new BufferZone(3, 55.5253026733917, 37.5789070129395, 500, new List<double> { 2539 }),
                new BufferZone(4, 55.5221445999848, 37.5778341293335, 500, new List<double> { 2814 })
            };

            // Список оптимальных точек
            List<OptimalZone> expected = new List<OptimalZone>
            {
                new OptimalZone(zonesCandidate[3], new List<string>(){ "Линейная свёртка", "Мультипликативная свёртка",
                "Свёртка агрегирования", "Свёртка Гермейера", "Метод главного критерия", "Свёртка на основе идеальной точки по метрике Чебышева",
                "Свёртка на основе идеальной точки по метрике Хемминга","Свёртка на основе идеальной точки по метрике Евклида"})
            };

            Optimum point = new Optimum();
            // Act
            // Ищем оптимальные точки
            List<OptimalZone> actual = point.getOptimum(zonesCandidate, criterion);
            // Assert
            // Проверка, совпадает ли ожидаемый результат с полученным: списки должны быть одинаковы
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].optimalZone.idBufferZone, actual[i].optimalZone.idBufferZone);
                Assert.AreEqual(expected[i].optimalZone.x, actual[i].optimalZone.x);
                Assert.AreEqual(expected[i].optimalZone.y, actual[i].optimalZone.y);
                Assert.AreEqual(expected[i].namesConvolutiones.Count, actual[i].namesConvolutiones.Count);
            }
        }

        [TestMethod]
        // Найти оптимум, задано 3 критерия, радиус = 3000 м.
        public void TestSearchOptimumWithThreeCriterion()
        {
            // Arrange
            // Критерии и буферные зоны
            List<Criterion> criterion = new List<Criterion>
            {
                new Criterion("Критерий 1", true, 0.6),
                new Criterion("Критерий 2", false, 0.2),
                new Criterion("Критерий 3", false, 0.2)
            };
            List<BufferZone> zonesCandidate = new List<BufferZone>
            {
                new BufferZone(1, 49.20130795, -123.0233145, 3000, new List<double> { 213430, 186, 226510 }),
                new BufferZone(2, 49.19861594, -123.0231857, 3000, new List<double> { 169311, 152, 173980 }),
                new BufferZone(3, 49.20035455, -123.0285072, 3000, new List<double> { 133460, 141, 155024 }),
                new BufferZone(4, 49.20164444, -123.0332708, 3000, new List<double> { 104423, 100, 115933 })
            };

            // Список оптимальных точек
            List<OptimalZone> expected = new List<OptimalZone>
            {
                new OptimalZone(zonesCandidate[0], new List<string>(){ "Линейная свёртка", "Мультипликативная свёртка", "Свёртка агрегирования",
                    "Метод главного критерия","Свёртка на основе идеальной точки по метрике Чебышева","Свёртка на основе идеальной точки по метрике Хемминга"}),
                new OptimalZone(zonesCandidate[1], new List<string>(){ "Свёртка на основе идеальной точки по метрике Евклида" }),
                new OptimalZone(zonesCandidate[2], new List<string>(){ "Свёртка Гермейера"}),
            };

            Optimum point = new Optimum();
            // Act
            // Ищем оптимальные точки
            List<OptimalZone> actual = point.getOptimum(zonesCandidate, criterion);
            // Assert
            // Проверка, совпадает ли ожидаемый результат с полученным: списки должны быть одинаковы
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].optimalZone.idBufferZone, actual[i].optimalZone.idBufferZone);
                Assert.AreEqual(expected[i].optimalZone.x, actual[i].optimalZone.x);
                Assert.AreEqual(expected[i].optimalZone.y, actual[i].optimalZone.y);
                Assert.AreEqual(expected[i].namesConvolutiones.Count, actual[i].namesConvolutiones.Count);
            }
        }

        [TestMethod]
        // Найти оптимум, задано 7 критериев
        public void TestSearchOptimumWithSevenCriterion()
        {
            // Arrange
            // Критерии и буферные зоны
            List<Criterion> criterion = new List<Criterion>
            {
                new Criterion("Критерий 1", true, 0.3),
                new Criterion("Критерий 2", false, 0.15),
                new Criterion("Критерий 3", false, 0.2),
                new Criterion("Критерий 4", false, 0.1),
                new Criterion("Критерий 5", true, 0),
                new Criterion("Критерий 6", true, 0.1),
                new Criterion("Критерий 7", false, 0.15)
            };
            List<BufferZone> zonesCandidate = new List<BufferZone>
            {
                new BufferZone(1, 5.075213437, 8.728637695, 1650, new List<double> { 68929, 50, 52462, 23, 106, 15813, 49 }),
                new BufferZone(2, 5.109410202, 8.76159668, 1650, new List<double> { 54804, 61, 57172, 12, 68, 21035, 49 }),
                new BufferZone(3, 5.120352782, 8.785629272, 1650, new List<double> { 41184, 34, 49166, 16, 54, 17065, 39 }),
                new BufferZone(4, 5.151811655, 8.802108765, 1650, new List<double> { 77377, 85, 91613, 22, 104, 31075, 48 }),
                new BufferZone(5, 5.186004315, 8.817901611, 1650, new List<double> { 56629, 29, 47663, 22, 72, 21279, 35 })
            };

            // Список оптимальных точек
            List<OptimalZone> expected = new List<OptimalZone>
            {
                new OptimalZone(zonesCandidate[0], new List<string>(){ "Свёртка Гермейера", "Свёртка на основе идеальной точки по метрике Чебышева"}),
                new OptimalZone(zonesCandidate[3], new List<string>(){ "Мультипликативная свёртка" }),
                new OptimalZone(zonesCandidate[4], new List<string>(){ "Линейная свёртка", "Свёртка агрегирования",
                    "Свёртка на основе идеальной точки по метрике Хемминга", "Свёртка на основе идеальной точки по метрике Евклида"})
            };

            Optimum point = new Optimum();
            // Act
            // Ищем оптимальные точки
            List<OptimalZone> actual = point.getOptimum(zonesCandidate, criterion);
            // Assert
            // Проверка, совпадает ли ожидаемый результат с полученным: списки должны быть одинаковы
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].optimalZone.idBufferZone, actual[i].optimalZone.idBufferZone);
                Assert.AreEqual(expected[i].optimalZone.x, actual[i].optimalZone.x);
                Assert.AreEqual(expected[i].optimalZone.y, actual[i].optimalZone.y);
                Assert.AreEqual(expected[i].namesConvolutiones.Count, actual[i].namesConvolutiones.Count);
            }
        }

        [TestMethod]
        // Найти оптимум, заданы веса 0 1 0
        public void TestSearchOptimumWithZeroWeights()
        {
            // Arrange
            // Критерии и буферные зоны
            List<Criterion> criterion = new List<Criterion>
            {
                new Criterion("Критерий 1", true, 0),
                new Criterion("Критерий 2", false, 1),
                new Criterion("Критерий 3", false, 0)
            };
            List<BufferZone> zonesCandidate = new List<BufferZone>
            {
                new BufferZone(1, 11.9963384, 3.685913086, 3000, new List<double> { 4374, 22, 2366 }),
                new BufferZone(2, 11.94260107, 3.630981445, 3000, new List<double> { 4634, 11, 1778 }),
                new BufferZone(3, 11.82971803, 3.636474609, 3000, new List<double> { 3626, 7, 1026 })
            };

            // Список оптимальных точек
            List<OptimalZone> expected = new List<OptimalZone>
            {
                new OptimalZone(zonesCandidate[0], new List<string>(){ "Мультипликативная свёртка", "Свёртка Гермейера"}),
                new OptimalZone(zonesCandidate[2], new List<string>(){ "Линейная свёртка", "Свёртка агрегирования", "Метод главного критерия",
                "Свёртка на основе идеальной точки по метрике Чебышева","Свёртка на основе идеальной точки по метрике Хемминга",
                    "Свёртка на основе идеальной точки по метрике Евклида" }),
            };

            Optimum point = new Optimum();
            // Act
            // Ищем оптимальные точки
            List<OptimalZone> actual = point.getOptimum(zonesCandidate, criterion);
            // Assert
            // Проверка, совпадает ли ожидаемый результат с полученным: списки должны быть одинаковы
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].optimalZone.idBufferZone, actual[i].optimalZone.idBufferZone);
                Assert.AreEqual(expected[i].optimalZone.x, actual[i].optimalZone.x);
                Assert.AreEqual(expected[i].optimalZone.y, actual[i].optimalZone.y);
                Assert.AreEqual(expected[i].namesConvolutiones.Count, actual[i].namesConvolutiones.Count);
            }
        }

        [TestMethod]
        // Найти оптимум, заданы равные веса 0.25 0.25 0.25 0.25
        public void TestSearchOptimumWithEqualsWeights()
        {
            // Arrange
            // Критерии и буферные зоны
            List<Criterion> criterion = new List<Criterion>
            {
                new Criterion("Критерий 1", true, 0.25),
                new Criterion("Критерий 2", false, 0.25),
                new Criterion("Критерий 3", true, 0.25),
                new Criterion("Критерий 4", true, 0.25)
            };
            List<BufferZone> zonesCandidate = new List<BufferZone>
            {
                new BufferZone(1, 45.08939915, 35.43104768, 2000, new List<double> { 114611, 108, 122210, 46 }),
                new BufferZone(2, 45.08896739, 35.43153048, 2000, new List<double> { 66511, 37, 60915, 20 }),
                new BufferZone(3, 45.08746002, 35.42939544, 2000, new List<double> { 77842, 78, 89081, 26 }),
                new BufferZone(4, 45.08548295, 35.42632699, 2000, new List<double> { 41897, 42, 61040, 20 })
            };

            // Список оптимальных точек
            List<OptimalZone> expected = new List<OptimalZone>
            {
                new OptimalZone(zonesCandidate[0], new List<string>(){ "Линейная свёртка", "Мультипликативная свёртка", "Свёртка агрегирования",
                "Свёртка на основе идеальной точки по метрике Хемминга", "Свёртка на основе идеальной точки по метрике Евклида"}),
                new OptimalZone(zonesCandidate[2], new List<string>(){ "Свёртка Гермейера", "Свёртка на основе идеальной точки по метрике Чебышева"})
            };

            Optimum point = new Optimum();
            // Act
            // Ищем оптимальные точки
            List<OptimalZone> actual = point.getOptimum(zonesCandidate, criterion);
            // Assert
            // Проверка, совпадает ли ожидаемый результат с полученным: списки должны быть одинаковы
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].optimalZone.idBufferZone, actual[i].optimalZone.idBufferZone);
                Assert.AreEqual(expected[i].optimalZone.x, actual[i].optimalZone.x);
                Assert.AreEqual(expected[i].optimalZone.y, actual[i].optimalZone.y);
                Assert.AreEqual(expected[i].namesConvolutiones.Count, actual[i].namesConvolutiones.Count);
            }
        }

        [TestMethod]
        // Найти оптимум, заданы веса 0.8 0 0.2
        public void TestSearchOptimumWithDifferentWeights()
        {
            // Arrange
            // Критерии и буферные зоны
            List<Criterion> criterion = new List<Criterion>
            {
                new Criterion("Критерий 1", true, 0.8),
                new Criterion("Критерий 2", true, 0),
                new Criterion("Критерий 3", false, 0.2)
            };
            List<BufferZone> zonesCandidate = new List<BufferZone>
            {
                new BufferZone(1, 45.07258109, 35.348382, 500, new List<double> { 720, 6, 744 }),
                new BufferZone(2, 45.07179309, 35.34954071, 500, new List<double> { 4465, 9, 1480 }),
                new BufferZone(3, 45.07127786, 35.35022736, 500, new List<double> { 2754, 14, 1332 }),
                new BufferZone(4, 45.06991399, 35.35198689, 500, new List<double> { 4629, 16, 671 }),
                new BufferZone(5, 45.06879256, 35.35348892, 500, new List<double> { 2528, 8, 674 })
            };

            // Список оптимальных точек
            List<OptimalZone> expected = new List<OptimalZone>
            {
                new OptimalZone(zonesCandidate[0], new List<string>(){ "Свёртка Гермейера"}),
                new OptimalZone(zonesCandidate[1], new List<string>(){ "Мультипликативная свёртка" }),
                new OptimalZone(zonesCandidate[3], new List<string>(){ "Линейная свёртка", "Свёртка агрегирования", "Метод главного критерия",
                "Свёртка на основе идеальной точки по метрике Чебышева", "Свёртка на основе идеальной точки по метрике Хемминга",
                    "Свёртка на основе идеальной точки по метрике Евклида" })
            };

            Optimum point = new Optimum();
            // Act
            // Ищем оптимальные точки
            List<OptimalZone> actual = point.getOptimum(zonesCandidate, criterion);
            // Assert
            // Проверка, совпадает ли ожидаемый результат с полученным: списки должны быть одинаковы
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].optimalZone.idBufferZone, actual[i].optimalZone.idBufferZone);
                Assert.AreEqual(expected[i].optimalZone.x, actual[i].optimalZone.x);
                Assert.AreEqual(expected[i].optimalZone.y, actual[i].optimalZone.y);
                Assert.AreEqual(expected[i].namesConvolutiones.Count, actual[i].namesConvolutiones.Count);
            }
        }
    }
}