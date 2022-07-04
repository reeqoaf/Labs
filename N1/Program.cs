using N1.Entities;

namespace N1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var restaurunt = SeedData1();

            Console.WriteLine("Query 1: Select");

            var result1 = from x in restaurunt.Meals
                          select x;

            result1.ToList().ForEach(x =>
            {
                Console.WriteLine(x.Name);
                Console.WriteLine(x.Price);
                x.Products.ForEach(y =>
                {
                    Console.WriteLine($"{y.Item1.Name} {y.Item2}");
                });
            });
            Console.WriteLine("\nQuery 2: Select field name");

            var result2 = from x in restaurunt.Meals
                          select x.Name;
            result2.ToList().ForEach(x =>
            {
                Console.WriteLine(x);
            });

            Console.WriteLine("\nQuery 3: Select anonymous type");

            var result3 = restaurunt.Meals.FirstOrDefault().Products.Select(x => new
            {
                ProductName = x.Item1.Name,
                Calories = x.Item1.Calories,
                Price = x.Item2
            });
            result3.ToList().ForEach(x =>
            {
                Console.WriteLine(x.ProductName);
                Console.WriteLine(x.Calories);
                Console.WriteLine(x.Price);
            });

            Console.WriteLine("\nQuery 4: Where");

            var result4 = from x in restaurunt.Meals
                          where x.Name == "Soup"
                          select x;
            result4.ToList().ForEach(x =>
            {
                Console.WriteLine(x.Name);
                Console.WriteLine(x.Price);
            });

            Console.WriteLine("\nQuery 5: Order by desc");

            var result5 = from x in restaurunt.Meals
                          orderby x.Name descending
                          select x;
            result5.ToList().ForEach(x =>
            {
                Console.WriteLine(x.Name);
                Console.WriteLine(x.Price);
            });

            Console.WriteLine("\nQuery 6: Order by extension");

            var result6 = restaurunt.Meals.FirstOrDefault().Products.OrderByDescending(x => x.Item1.Calories);
            result6.ToList().ForEach(x =>
            {
                Console.WriteLine(x.Item1.Name);
                Console.WriteLine(x.Item1.Calories);
                Console.WriteLine(x.Item2);
            });
            Console.WriteLine("\nQuery 7: Except");

            var result7 = restaurunt.Meals.Except(restaurunt.Menus.FirstOrDefault().Meals);
            result7.ToList().ForEach(x =>
            {
                Console.WriteLine(x.Name);
                Console.WriteLine(x.Price);
            });
            Console.WriteLine("\nQuery 8: Inner join");

            var result8 = from x in restaurunt.Menus.FirstOrDefault().Meals.FirstOrDefault().Products
                          from y in restaurunt.Menus.LastOrDefault().Meals.LastOrDefault().Products
                          where x.Item2 == y.Item2
                          select new { x1 = x.Item1.Name, x2 = y.Item2 };
            result8.ToList().ForEach(x =>
            {
                Console.WriteLine(x.x1);
                Console.WriteLine(x.x2);
            });
            Console.WriteLine("\nQuery 9: Inner join");

            var result9 = from x in restaurunt.Menus.FirstOrDefault().Meals.FirstOrDefault().Products
                          from y in restaurunt.Menus.LastOrDefault().Meals.LastOrDefault().Products
                          where x.Item2 == y.Item2
                          select new { x1 = x.Item1.Name, x2 = y.Item2 };

            result9.ToList().ForEach(x =>
            {
                Console.WriteLine(x.x1);
                Console.WriteLine(x.x2);
            });

            Console.WriteLine("\nQuery 10: distinct by");


            var products = new List<Product>();
            restaurunt.Meals.ForEach(x => products.AddRange(x.Products.Select(x => x.Item1)));
            var result10 = products.DistinctBy(x => x.Name);

            result10.ToList().ForEach(x =>
            {
                Console.WriteLine(x.Name);
                Console.WriteLine(x.Calories);
            });

            Console.WriteLine("\nQuery 11: group by");

            var result11 = products.GroupBy(x => x.Name).Select(y => new { Key = y.Key, Count = y.Count() }).ToList();

            result11.ForEach(x =>
            {
                Console.WriteLine(x.Key);
                Console.WriteLine(x.Count);
            });
            Console.WriteLine("\nQuery 12: first");

            var result12 = products.FirstOrDefault();

            Console.WriteLine(result12?.Name);
            Console.WriteLine(result12?.Calories);
            Console.WriteLine("\nQuery 12: last");

            var result13 = products.LastOrDefault();

            Console.WriteLine(result13?.Name);
            Console.WriteLine(result13?.Calories);

            Console.WriteLine("\nQuery 13: Concat");

            foreach (var x in restaurunt.Meals.Concat(restaurunt.Menus.FirstOrDefault().Meals))
            {
                Console.WriteLine(x.Name);
                Console.WriteLine(x.Price);
            }

            Console.WriteLine("\nQuery 14: Union");

            foreach (var x in restaurunt.Meals.Union(restaurunt.Menus.FirstOrDefault().Meals))
            {
                Console.WriteLine(x.Name);
                Console.WriteLine(x.Price);
            }
            
            Console.WriteLine("\nQuery 15: interserect");

            foreach (var x in restaurunt.Meals.Intersect(restaurunt.Menus.FirstOrDefault().Meals))
            {
                Console.WriteLine(x.Name);
                Console.WriteLine(x.Price);
            }

        }
        public static Restaurant SeedData1()
        {
            var meals = new List<Meal>
            {
                new Meal
                {
                    Name = "Mash",
                    Price = "100",
                    Products = new List<(Product, string)>
                    {
                        (new Product
                        {
                            Name = "Potato",
                            Calories = "70"
                        }, "50"),
                        (new Product
                        {
                            Name = "Salt",
                            Calories = "0"
                        }, "20")
                    }
                },
                new Meal
                {
                    Name = "Soup",
                    Price = "150",
                    Products = new List<(Product, string)>
                    {
                        (new Product
                        {
                            Name = "Mushrooms",
                            Calories = "22"
                        }, "70"),
                        (new Product
                        {
                            Name = "Salt",
                            Calories = "0"
                        }, "20"),
                        (new Product
                        {
                            Name = "Potato",
                            Calories = "70"
                        }, "50"),
                    }
                },
                new Meal
                {
                    Name = "Salad",
                    Price = "200",
                    Products = new List<(Product, string)>
                    {
                        (new Product
                        {
                            Name = "Tomatoes",
                            Calories = "18"
                        }, "40"),
                        (new Product
                        {
                            Name = "Salt",
                            Calories = "0"
                        }, "20"),
                        (new Product
                        {
                            Name = "Cucumbers",
                            Calories = "16"
                        }, "35"),
                    }
                },
                new Meal
                {
                    Name = "Tea",
                    Price = "80",
                    Products = new List<(Product, string)>
                    {
                        (new Product
                        {
                            Name = "Green Tea",
                            Calories = "5"
                        }, "30"),
                        (new Product
                        {
                            Name = "Sugar",
                            Calories = "387"
                        }, "20"),
                    }
                }
            };
            var restaurunt = new Restaurant
            {
                Name = "My restaurunt",
                CreateDate = "29.06.2022",
                Meals = meals,
                Menus = new List<Menu>
                {
                    new Menu
                    {
                        Date = "29.06.2022",
                        Meals = new List<Meal>{ meals[0], meals[1] }
                    },
                    new Menu
                    {
                        Date = "30.06.2022",
                        Meals = new List<Meal> { meals[2], meals[3] }
                    }
                }
            };
            return restaurunt;
        }
        public static Restaurant SeedData2()
        {
            var meals = new List<Meal>
            {
                new Meal
                {
                    Name = "Mash2",
                    Price = "102",
                    Products = new List<(Product, string)>
                    {
                        (new Product
                        {
                            Name = "Potato2",
                            Calories = "72"
                        }, "52"),
                        (new Product
                        {
                            Name = "Salt2",
                            Calories = "0"
                        }, "22")
                    }
                },
                new Meal
                {
                    Name = "Soup2",
                    Price = "152",
                    Products = new List<(Product, string)>
                    {
                        (new Product
                        {
                            Name = "Mushroomss2",
                            Calories = "22"
                        }, "72"),
                        (new Product
                        {
                            Name = "Salt",
                            Calories = "0"
                        }, "22"),
                        (new Product
                        {
                            Name = "Potato",
                            Calories = "72"
                        }, "52"),
                    }
                },
                new Meal
                {
                    Name = "Salad",
                    Price = "202",
                    Products = new List<(Product, string)>
                    {
                        (new Product
                        {
                            Name = "Tomatoes",
                            Calories = "22"
                        }, "42"),
                        (new Product
                        {
                            Name = "Salt",
                            Calories = "0"
                        }, "22"),
                        (new Product
                        {
                            Name = "Cucumbers",
                            Calories = "22"
                        }, "32"),
                    }
                },
                new Meal
                {
                    Name = "Tea",
                    Price = "80",
                    Products = new List<(Product, string)>
                    {
                        (new Product
                        {
                            Name = "Green Tea",
                            Calories = "5"
                        }, "32"),
                        (new Product
                        {
                            Name = "Sugar",
                            Calories = "382"
                        }, "22"),
                    }
                }
            };
            var restaurunt = new Restaurant
            {
                Name = "My restaurunt2",
                CreateDate = "29.06.2022",
                Meals = meals,
                Menus = new List<Menu>
                {
                    new Menu
                    {
                        Date = "01.07.2022",
                        Meals = new List<Meal>{ meals[0], meals[1] }
                    },
                    new Menu
                    {
                        Date = "02.07.2022",
                        Meals = new List<Meal> { meals[2], meals[3] }
                    }
                }
            };
            return restaurunt;
        }
    }
}