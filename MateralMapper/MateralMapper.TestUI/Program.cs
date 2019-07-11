using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using MateralMapper.Core;

namespace MateralMapper.TestUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var customClass1 = new CustomClass1
            {
                Name = "Materal",
                Age = 12
            };
            var customClass2 = new CustomClass2
            {
                Name = "qwe",
                Age = 444,
                Remark = "MMMM"
            };
            var customClass1List = new List<CustomClass1>();
            for (var i = 0; i < 10000000; i++)
            {
                customClass1List.Add(new CustomClass1
                {
                    Name = $"Materal{i}",
                    Age = i
                });
            }
            {
                Mapper.Initialize(m => m.CreateMap<CustomClass1, CustomClass2>());
                var watch = new Stopwatch();
                watch.Start();
                CustomClass2 customClass202 = Mapper.Instance.Map<CustomClass2>(customClass1);
                List<CustomClass2> customClass202s = Mapper.Instance.Map<List<CustomClass2>>(customClass1List);
                CustomClass2 customClass203 = Mapper.Instance.Map(customClass1, customClass2);
                watch.Stop();
                Console.WriteLine($"AutoMapper:{watch.ElapsedMilliseconds}ms");
            }
            {
                var watch = new Stopwatch();
                watch.Start();
                IMateralMapper mapper = new MateralMapperImpl();
                CustomClass2 customClass202 = mapper.Map<CustomClass1, CustomClass2>(customClass1);
                List<CustomClass2> customClass202s = mapper.Map<CustomClass1, CustomClass2>(customClass1List).ToList();
                CustomClass2 customClass203 = mapper.Map(customClass1, customClass2);
                watch.Stop();
                Console.WriteLine($"MateralMapper:{watch.ElapsedMilliseconds}ms");
            }
        }
    }

    public class CustomClass1
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public class CustomClass2
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Remark { get; set; }
    }
}
