using N1.Entities;
using System.Xml;
using System.Xml.Linq;

namespace N2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var restaurant = SeedData1();
            using (var writer = XmlWriter.Create("restaurant.xml", settings: new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartElement("restaurant");
                writer.WriteElementString("name", restaurant.Name);
                writer.WriteElementString("createdate", restaurant.CreateDate);
                writer.WriteStartElement("meals");
                foreach (var meal in restaurant.Meals)
                {
                    writer.WriteStartElement("meal");
                    writer.WriteElementString("name", meal.Name);
                    writer.WriteElementString("price", meal.Price);
                    writer.WriteStartElement("products");
                    foreach (var product in meal.Products)
                    {
                        writer.WriteStartElement("product");
                        writer.WriteElementString("name", product.Item1.Name);
                        writer.WriteElementString("calories", product.Item1.Calories);
                        writer.WriteElementString("price", product.Item2);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteStartElement("menus");
                foreach (var menu in restaurant.Menus)
                {
                    writer.WriteStartElement("menu");
                    writer.WriteElementString("name", menu.Date);
                    writer.WriteStartElement("meals");
                    foreach (var meal in menu.Meals)
                    {
                        writer.WriteStartElement("meal");
                        writer.WriteElementString("name", meal.Name);
                        writer.WriteElementString("price", meal.Price);
                        writer.WriteStartElement("products");
                        foreach (var product in meal.Products)
                        {
                            writer.WriteStartElement("product");
                            writer.WriteElementString("name", product.Item1.Name);
                            writer.WriteElementString("calories", product.Item1.Calories);
                            writer.WriteElementString("price", product.Item2);
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndElement();
            }

            XDocument xmlEmp = XDocument.Load("restaurant.xml");

            XElement rest = xmlEmp.Element("restaurant");

            Console.WriteLine($"restaurant name - {rest.Element("name").Value}");
            Console.WriteLine($"createdate - {rest.Element("createdate").Value}");
            Console.WriteLine("meals:");
            foreach (XElement meal in rest.Element("meals").Elements("meal"))
            {
                Console.WriteLine($"\tname - {meal.Element("name").Value}");
                Console.WriteLine($"\tprice - {meal.Element("price").Value}");
                Console.WriteLine("\tproducts:");
                foreach (var product in meal.Element("products").Elements("products"))
                {
                    Console.WriteLine($"\t\tname - {product.Element("name").Value}");
                    Console.WriteLine($"\t\tcalories - {product.Element("calories").Value}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("menus");
            foreach (var menu in rest.Element("menus").Elements("menu"))
            {
                Console.WriteLine($"\tdate - {menu.Element("name").Value}");
                Console.WriteLine("\tmeals:");
                foreach (XElement meal in menu.Element("meals").Elements("meal"))
                {
                    Console.WriteLine($"\t\tname - {meal.Element("name").Value}");
                    Console.WriteLine($"\t\tprice - {meal.Element("price").Value}");
                    Console.WriteLine("\t\tproducts:");

                    foreach (var product in meal.Element("products").Elements("products"))
                    {
                        Console.WriteLine($"\t\t\tname - {product.Element("name").Value}");
                        Console.WriteLine($"\t\t\tcalories - {product.Element("calories").Value}");
                    }
                }
            }
            Console.WriteLine();

            Console.WriteLine("Query 1: Select");

            var result1 = rest.Element("meals")?.Elements("meal").Select(x => new
            {
                Name = x.Element("name").Value,
                Price = x.Element("price").Value
            });
            result1.ToList().ForEach(x =>
            {
                Console.WriteLine(x.Name);
                Console.WriteLine(x.Price);
            });

            Console.WriteLine("\nQuery 2: Select with where");
            var result2 = rest.Element("meals")?.Elements("meal").Where(x => x.Element("name").Value == "Soup").Select(x => new
            {
                Name = x.Element("name").Value,
                Price = x.Element("price").Value
            });
            result2.ToList().ForEach(x =>
            {
                Console.WriteLine(x.Name);
                Console.WriteLine(x.Price);
            });

            Console.WriteLine("\nQuery 3: order by desc");
            var result3 = rest.Element("meals")?.Elements("meal").OrderBy(x => x.Element("name").Value).Select(x => new
            {
                Name = x.Element("name").Value,
                Price = x.Element("price").Value
            });
            result3.ToList().ForEach(x =>
            {
                Console.WriteLine(x.Name);
                Console.WriteLine(x.Price);
            });
            Console.WriteLine("Query 3: except");
            var result4 = rest.Element("meals")?.Elements("meal").Select(x => new
            {
                Name = x.Element("name").Value,
                Price = x.Element("price").Value
            }).Except(restaurant.Menus.FirstOrDefault().Meals.Select(x => new
            {
                Name = x.Name,
                Price = x.Price
            }));

            result4.ToList().ForEach(x =>
            {
                Console.WriteLine(x.Name);
                Console.WriteLine(x.Price);
            });
            Console.WriteLine("\nQuery 5: Inner join");

            var result5 = from x in rest.Element("menus").Elements("menu").FirstOrDefault().Element("meals").Elements("meal").FirstOrDefault().Element("products").Elements("product")
                          from y in rest.Element("menus").Elements("menu").LastOrDefault().Element("meals").Elements("meal").LastOrDefault().Element("products").Elements("product")
                          where x.Element("price").Value == y.Element("price").Value
                          select new { x1 = x.Element("price").Value, x2 = x.Element("name").Value };
            result5.ToList().ForEach(x =>
            {
                Console.WriteLine(x.x2);
                Console.WriteLine(x.x1);
            });
            Console.WriteLine("\nQuery 6: Inner join");

            var result6 = from x in rest.Element("menus").Elements("menu").FirstOrDefault().Element("meals").Elements("meal").FirstOrDefault().Element("products").Elements("product")
                          from y in rest.Element("menus").Elements("menu").LastOrDefault().Element("meals").Elements("meal").LastOrDefault().Element("products").Elements("product")
                          where x.Element("price").Value == y.Element("price").Value
                          select new { x1 = x.Element("price").Value, x2 = x.Element("name").Value };
            result6.ToList().ForEach(x =>
            {
                Console.WriteLine(x.x2);
                Console.WriteLine(x.x1);
            });
            Console.WriteLine("\nQuery 7: Distinct by");

            var products = new List<Product>();
            rest.Element("meals").Elements("meal").ToList().ForEach(x => products.AddRange(x.Element("products").Elements("product").Select(x => new Product { Name = x.Element("name").Value, Calories = x.Element("calories").Value })));
            var result7 = products.DistinctBy(x => x.Name);

            result7.ToList().ForEach(x =>
            {
                Console.WriteLine(x.Name);
                Console.WriteLine(x.Calories);
            });

            Console.WriteLine("\nQuery 8: group by");

            var result8 = products.GroupBy(x => x.Name).Select(y => new { Key = y.Key, Count = y.Count() }).ToList();

            result8.ForEach(x =>
            {
                Console.WriteLine(x.Key);
                Console.WriteLine(x.Count);
            });

            Console.WriteLine("\nQuery 9: first");

            var result9 = rest.Element("menus").Elements("menu").FirstOrDefault().Element("meals").Elements("meal").FirstOrDefault().Element("products").Elements("product").FirstOrDefault();
            Console.WriteLine(result9.Element("name").Value);
            Console.WriteLine(result9.Element("calories").Value);

            Console.WriteLine("\nQuery 10: last");

            var result10 = rest.Element("menus").Elements("menu").LastOrDefault().Element("meals").Elements("meal").LastOrDefault().Element("products").Elements("product").LastOrDefault();
            Console.WriteLine(result9.Element("name").Value);
            Console.WriteLine(result9.Element("calories").Value);

            Console.WriteLine("\nQuery 11: elementat");

            var result11 = rest.Element("menus").Elements("menu").LastOrDefault().Element("meals").Elements("meal").ElementAt(1).Element("products").Elements("product").LastOrDefault();
            Console.WriteLine(result11.Element("name").Value);
            Console.WriteLine(result11.Element("calories").Value);

            Console.WriteLine("\nQuery 12: select and reverse");

            var result12 = rest.Element("meals")?.Elements("meal").Select(x => new
            {
                Name = x.Element("name").Value,
                Price = x.Element("price").Value
            }).Reverse();

            result12.ToList().ForEach(x =>
            {
                Console.WriteLine(x.Name);
                Console.WriteLine(x.Price);
            });

            Console.WriteLine("\nQuery 14: Concat");

            foreach (var x in rest.Element("meals").Elements("meal").Select(x => new { Name = x.Element("name").Value , Price = x.Element("price").Value }).Concat(rest.Element("menus").Elements("menu").FirstOrDefault().Element("meals").Elements("meal").Select(x => new { Name = x.Element("name").Value, Price = x.Element("price").Value })))
            {
                Console.WriteLine(x.Name);
                Console.WriteLine(x.Price);
            }

            Console.WriteLine("\nQuery 14: Unionby");

            foreach (var x in rest.Element("meals").Elements("meal").Select(x => new { Name = x.Element("name").Value, Price = x.Element("price").Value }).UnionBy(rest.Element("menus").Elements("menu").FirstOrDefault().Element("meals").Elements("meal").Select(x => new { Name = x.Element("name").Value, Price = x.Element("price").Value }), x => x.Name))
            {
                Console.WriteLine(x.Name);
                Console.WriteLine(x.Price);
            }

            Console.WriteLine("\nQuery 15: interserect");

            foreach (var x in rest.Element("meals").Elements("meal").Select(x => new { Name = x.Element("name").Value, Price = x.Element("price").Value }).Intersect(rest.Element("menus").Elements("menu").FirstOrDefault().Element("meals").Elements("meal").Select(x => new { Name = x.Element("name").Value, Price = x.Element("price").Value })))
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