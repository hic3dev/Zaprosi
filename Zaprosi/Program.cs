using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zaprosi
{
    internal class Program
    {
        public class Material
        {
            public int KodMaterial { get; set; }
            public string Materiali { get; set; }
            public string Gruppa { get; set; }

            public Material(int kodMaterial, string materiali, string gruppa)
            {
                KodMaterial = kodMaterial;
                Materiali = materiali;
                Gruppa = gruppa;
            }
        }

        public class Igrushka
        {
            public string Name { get; set; }
            public int KodMaterial { get; set; }
            public DateTime Mesyac { get; set; }

            public Igrushka(string name, int kodMaterial, DateTime mesyac)
            {
                Name = name;
                KodMaterial = kodMaterial;
                Mesyac = mesyac;
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

            List<Igrushka> igruski = new List<Igrushka>
            {
                new Igrushka("Медведь", 1, new DateTime(2022, 10, 11)),
                new Igrushka("Кролик", 2, new DateTime(2021, 5, 3)),
                new Igrushka("Машинка", 3, new DateTime(2020, 1, 20)),
                new Igrushka("Кубики", 4, new DateTime(2023, 7, 7)),
                new Igrushka("Кукла", 5, new DateTime(2021, 2, 8)),
                new Igrushka("Лего", 6, new DateTime(2023, 5, 20)),
                new Igrushka("Мячик", 7, new DateTime(2021, 3, 6))
            };


            var allToys = from t in igruski select t;

            var sortedByName = from t in igruski orderby t.Name descending select t;

            var sortedByNameAndDate = from t in igruski orderby t.Name descending, t.Mesyac descending select t;

            var beforeJune = from t in igruski
                             where t.Mesyac.Month < 6
                             select t;

            var mayToAugust = from t in igruski
                              where t.Mesyac.Month >= 5 && t.Mesyac.Month <= 8
                              select t;

            string output = "";
            output += "Все игрушки\n";
            foreach (var t in allToys)
            {
                output += $"{t.Name} (код: {t.KodMaterial}, выпуск: {t.Mesyac:yyyy-MM-dd})\n";
            }
            output += "По убыванию названия\n";
            foreach (var t in sortedByName)
            {
                output += $"{t.Name} (код: {t.KodMaterial}, выпуск: {t.Mesyac:yyyy-MM-dd})\n";
            }

            output += "По убыванию названия и даты\n";
            foreach (var t in sortedByNameAndDate)
            {
                output += $"{t.Name} (код: {t.KodMaterial}, выпуск: {t.Mesyac:yyyy-MM-dd})\n";
            }

            output += "Игрушки, выпущенные ДО июня\n";
            foreach (var t in beforeJune)
            {
                output += $"{t.Name} (выпуск: {t.Mesyac:yyyy-MM-dd})\n";
            }

            output += "Игрушки, выпущенные с МАЯ по АВГУСТ\n";
            foreach (var t in mayToAugust)
            {
                output += $"{t.Name} (выпуск: {t.Mesyac:yyyy-MM-dd})\n";
            }
            string  fileSaving= "zaprosi_output.txt";
            File.WriteAllText(fileSaving, output);

            Console.WriteLine("Результаты сохранены в файл: " + fileSaving);
            Console.WriteLine("\n" + output);
        }
    }
}