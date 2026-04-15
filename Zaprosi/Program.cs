using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ToyStoreQueries
{
    internal class Program
    {
        public class Material
        {
            public int MaterialCode { get; set; }
            public string Name { get; set; }
            public string AgeGroup { get; set; }

            public Material(int code, string name, string ageGroup)
            {
                MaterialCode = code;
                Name = name;
                AgeGroup = ageGroup;
            }
        }

        public class Toy
        {
            public string Name { get; set; }
            public int MaterialCode { get; set; }
            public DateTime ReleaseDate { get; set; }

            public Toy(string name, int materialCode, DateTime releaseDate)
            {
                Name = name;
                MaterialCode = materialCode;
                ReleaseDate = releaseDate;
            }
        }

        static void Main(string[] args)
        {
            List<Material> materials = new List<Material>
            {
                new Material(1, "Пластмасса", "до 3-х лет"),
                new Material(2, "Ткань", "от 3-х до 10 лет"),
                new Material(3, "Металл", "от 10 лет")
            };

            List<Toy> toys = new List<Toy>
            {
                new Toy("Медведь", 1, new DateTime(2022, 10, 11)),
                new Toy("Кролик", 2, new DateTime(2021, 5, 3)),
                new Toy("Машинка", 3, new DateTime(2020, 1, 20)),
                new Toy("Кубики", 1, new DateTime(2023, 7, 7)),
                new Toy("Кукла", 2, new DateTime(2021, 2, 8)),
                new Toy("Лего", 1, new DateTime(2023, 5, 20)),
                new Toy("Мячик", 2, new DateTime(2021, 3, 6))
            };

            var allToys = from t in toys select t;

            var sortedByName = from t in toys orderby t.Name descending select t;

            var sortedByTwo = from t in toys
                              orderby t.Name descending, t.ReleaseDate descending
                              select t;

            var beforeJune = from t in toys where t.ReleaseDate.Month < 6 select t;

            var summerToys = from t in toys
                             where t.ReleaseDate.Month >= 5 && t.ReleaseDate.Month <= 8
                             select t;

            var joinedData = from t in toys
                             join m in materials on t.MaterialCode equals m.MaterialCode
                             select new
                             {
                                 ToyName = t.Name,
                                 MaterialType = m.Name,
                                 TargetAge = m.AgeGroup
                             };

            string output = "";

            output += "a) Все игрушки:\n";
            foreach (var t in allToys) output += $"{t.Name} (Код: {t.MaterialCode})\n";

            output += "\nb) По имени (убывание):\n";
            foreach (var t in sortedByName) output += $"{t.Name}\n";

            output += "\nc) По имени и дате:\n";
            foreach (var t in sortedByTwo) output += $"{t.Name} - {t.ReleaseDate:yyyy-MM-dd}\n";

            output += "\nd) Выпущены до июня:\n";
            foreach (var t in beforeJune) output += $"{t.Name} ({t.ReleaseDate:MMMM})\n";

            output += "\ne) С мая по август:\n";
            foreach (var t in summerToys) output += $"{t.Name} ({t.ReleaseDate:MMMM})\n";

            output += "\nf) Объединенные данные (JOIN):\n";
            foreach (var item in joinedData)
            {
                output += $"Игрушка: {item.ToyName} | Материал: {item.MaterialType} | Возраст: {item.TargetAge}\n";
            }

            File.WriteAllText("zaprosi_output.txt", output);
            Console.WriteLine(output);
            Console.WriteLine("\nГотово! Результаты в файле zaprosi_output.txt");
        }
    }
}