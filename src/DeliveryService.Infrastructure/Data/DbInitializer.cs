using System.Linq;
using DeliveryService.Core.Entities;

namespace DeliveryService.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DeliveryContext context)
        {
            if (context.Points.Any())
            {
                return;
            }

            var points = new Point[]
            {
                new Point{Name = "A"},                      // 1
                new Point{Name = "B"},                      // 2
                new Point{Name = "C"},                      // 3
                new Point{Name = "D"},                      // 4
                new Point{Name = "E"},                      // 5
                new Point{Name = "F"},                      // 6
                new Point{Name = "G"},                      // 7
                new Point{Name = "H"},                      // 8
                new Point{Name = "I"},                      // 9
                new Point{Name = "Arad"},                   // 10
                new Point{Name = "Bucareste"},              // 11
                new Point{Name = "Craiova"},                // 12
                new Point{Name = "Drobeta"},                // 13
                new Point{Name = "Eforie"},                 // 14
                new Point{Name = "Fagaras"},                // 15
                new Point{Name = "Giurgiu"},                // 16
                new Point{Name = "Hirsova"},                // 17
                new Point{Name = "Iasi"},                   // 18
                new Point{Name = "Lugoj"},                  // 19
                new Point{Name = "Mehadia"},                // 20
                new Point{Name = "Neamt"},                  // 21
                new Point{Name = "Oradea"},                 // 22
                new Point{Name = "Pitesti"},                // 23
                new Point{Name = "Rimnicu Vilcea"},         // 24
                new Point{Name = "Sibiu"},                  // 25
                new Point{Name = "Timisoara"},              // 26
                new Point{Name = "Urziceni"},               // 27
                new Point{Name = "Vaslui"},                 // 28
                new Point{Name = "Zerind"}                  // 29
            };

            context.Points.AddRange(points);
            context.SaveChanges();

            var routes = new Route[]
            {
                new Route{ FromId=1,  ToId=3,   Time=1,  Cost=20  },  // A -> C
                new Route{ FromId=1,  ToId=5,   Time=30, Cost=5   },  // A -> E
                new Route{ FromId=1,  ToId=8,   Time=10, Cost=1   },  // A -> H
                new Route{ FromId=3,  ToId=2,   Time=1,  Cost=12  },  // C -> B
                new Route{ FromId=4,  ToId=6,   Time=4,  Cost=50  },  // D -> F
                new Route{ FromId=5,  ToId=4,   Time=3,  Cost=5   },  // E -> D
                new Route{ FromId=6,  ToId=7,   Time=40, Cost=50  },  // F -> G
                new Route{ FromId=6,  ToId=9,   Time=45, Cost=50  },  // F -> I
                new Route{ FromId=7,  ToId=2,   Time=64, Cost=73  },  // G -> B
                new Route{ FromId=8,  ToId=5,   Time=30, Cost=1   },  // H -> E
                new Route{ FromId=9,  ToId=2,   Time=65, Cost=5   },  // I -> B
                new Route{ FromId=10, ToId=29,  Time=0,  Cost=75  },  // Arad -> Zerind
                new Route{ FromId=10, ToId=26,  Time=0,  Cost=118 },  // Arad -> Timisoara
                new Route{ FromId=10, ToId=25,  Time=0,  Cost=140 },  // Arad -> Sibiu
                new Route{ FromId=11, ToId=15,  Time=0,  Cost=211 },  // Bucareste -> Fagaras
                new Route{ FromId=11, ToId=23,  Time=0,  Cost=101 },  // Bucareste -> Pitesti
                new Route{ FromId=11, ToId=16,  Time=0,  Cost=90  },  // Bucareste -> Giurgiu
                new Route{ FromId=11, ToId=27,  Time=0,  Cost=85  },  // Bucareste -> Urziceni
                new Route{ FromId=12, ToId=13,  Time=0,  Cost=120 },  // Craiova -> Drobeta
                new Route{ FromId=12, ToId=24,  Time=0,  Cost=146 },  // Craiova -> Rimnicu Vilcea
                new Route{ FromId=12, ToId=23,  Time=0,  Cost=138 },  // Craiova -> Pitesti
                new Route{ FromId=13, ToId=12,  Time=0,  Cost=120 },  // Drobeta -> Craiova
                new Route{ FromId=13, ToId=20,  Time=0,  Cost=75  },  // Drobeta -> Mehadia
                new Route{ FromId=14, ToId=17,  Time=0,  Cost=86  },  // Eforie -> Hirsova
                new Route{ FromId=15, ToId=11,  Time=0,  Cost=211 },  // Fagaras -> Bucareste
                new Route{ FromId=15, ToId=25,  Time=0,  Cost=99  },  // Fagaras -> Sibiu
                new Route{ FromId=16, ToId=11,  Time=0,  Cost=90  },  // Giurgiu -> Bucareste
                new Route{ FromId=17, ToId=14,  Time=0,  Cost=86  },  // Hirsova -> Eforie
                new Route{ FromId=17, ToId=27,  Time=0,  Cost=98  },  // Hirsova -> Urziceni
                new Route{ FromId=18, ToId=21,  Time=0,  Cost=87  },  // Iasi -> Neamt
                new Route{ FromId=18, ToId=28,  Time=0,  Cost=92  },  // Iasi -> Vaslui
                new Route{ FromId=19, ToId=20,  Time=0,  Cost=70  },  // Lugoj -> Mehadia
                new Route{ FromId=19, ToId=26,  Time=0,  Cost=111 },  // Lugoj -> Timisoara
                new Route{ FromId=20, ToId=19,  Time=0,  Cost=70  },  // Mehadia -> Lugoj
                new Route{ FromId=20, ToId=13,  Time=0,  Cost=75  },  // Mehadia -> Drobeta
                new Route{ FromId=21, ToId=18,  Time=0,  Cost=87  },  // Neamt -> Iasi
                new Route{ FromId=22, ToId=29,  Time=0,  Cost=71  },  // Oradea -> Zerind
                new Route{ FromId=22, ToId=25,  Time=0,  Cost=151 },  // Oradea -> Sibiu
                new Route{ FromId=23, ToId=24,  Time=0,  Cost=97  },  // Pitesti -> Rimnicu Vilcea
                new Route{ FromId=23, ToId=12,  Time=0,  Cost=138 },  // Pitesti -> Craiova
                new Route{ FromId=23, ToId=11,  Time=0,  Cost=101 },  // Pitesti -> Bucareste
                new Route{ FromId=24, ToId=25,  Time=0,  Cost=80  },  // Rimnicu Vilcea -> Sibiu
                new Route{ FromId=24, ToId=12,  Time=0,  Cost=146 },  // Rimnicu Vilcea -> Craiova
                new Route{ FromId=24, ToId=23,  Time=0,  Cost=97  },  // Rimnicu Vilcea -> Pitesti
                new Route{ FromId=25, ToId=10,  Time=0,  Cost=140 },  // Sibiu -> Arad
                new Route{ FromId=25, ToId=22,  Time=0,  Cost=151 },  // Sibiu -> Oradea
                new Route{ FromId=25, ToId=15,  Time=0,  Cost=99  },  // Sibiu -> Fagaras
                new Route{ FromId=25, ToId=24,  Time=0,  Cost=80  },  // Sibiu -> Rimnicu Vilcea
                new Route{ FromId=26, ToId=10,  Time=0,  Cost=118 },  // Timisoara -> Arad
                new Route{ FromId=26, ToId=19,  Time=0,  Cost=111 },  // Timisoara -> Lugoj
                new Route{ FromId=27, ToId=11,  Time=0,  Cost=85  },  // Urziceni -> Bucareste
                new Route{ FromId=27, ToId=17,  Time=0,  Cost=98  },  // Urziceni -> Hirsova
                new Route{ FromId=27, ToId=28,  Time=0,  Cost=142 },  // Urziceni -> Vaslui
                new Route{ FromId=28, ToId=18,  Time=0,  Cost=92  },  // Vaslui -> Iasi
                new Route{ FromId=28, ToId=27,  Time=0,  Cost=142 },  // Vaslui -> Urziceni
                new Route{ FromId=29, ToId=10,  Time=0,  Cost=75  },  // Zerind -> Arad
                new Route{ FromId=29, ToId=22,  Time=0,  Cost=71  }   // Zerind -> Oradea
            };

            context.Routes.AddRange(routes);
            context.SaveChanges();
        }
    }
}