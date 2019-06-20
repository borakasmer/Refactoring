using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Refactoring
{
    class Program
    {
        static IDictionary<string, Course> courses;
        static Invoice invoice;
        static void Main(string[] args)
        {
            Console.WriteLine("Wellcome to Refactoring Example");
            courses = new Dictionary<string, Course>();
            courses.Add("dpattern", new Course() { Name = "Design Pattern", Type = Types.Software });
            courses.Add("hface", new Course() { Name = "Human Face", Type = Types.Art });
            courses.Add("redis", new Course() { Name = "Redis", Type = Types.Software });

            invoice = new Invoice();
            invoice.customerName = "Hasel Team";
            invoice.registers = new Register[]{
                new Register(){courseID="dpattern",student=20},
                new Register() { courseID = "hface", student = 15 },
                new Register() { courseID = "redis", student = 5 },
            };

            var result = $"{invoice.customerName} için Fatura Detayı: \n";

            /*CultureInfo trFormat = new CultureInfo("tr-TR", false);
            trFormat.NumberFormat.CurrencySymbol = "TL";
            trFormat.NumberFormat.NumberDecimalDigits = 2;*/

            /* decimal totalAmount = 0;
            foreach (Register reg in invoice.registers)
            {
                result += $"{findCourse(reg).Name}: {tr(getAmounth(reg) / 100)} ({reg.student} kişi)\n";
                totalAmount += getAmounth(reg);
            } */
            /* decimal volumeCredits = 0;
            foreach (Register reg in invoice.registers)
            {
                volumeCredits += calculateVolumeCredit(reg);
            } */
            //decimal volumeCredits = totalValumeCredits();

            //            foreach (Register reg in invoice.registers)
            //            {
            //Course lesson = courses[reg.courseID];
            /* Course lesson = findCourse(reg); */
            //var thisAmount = 0;
            //                var thisAmount = getAmounth(reg);
            /* switch (lesson.Type)
            {
                case Types.Art:
                    {
                        thisAmount = 3000;
                        if (reg.student > 15)
                        {
                            thisAmount += 1000 * (reg.student - 10);
                        }
                        break;
                    }
                case Types.Software:
                    {
                        thisAmount = 30000;
                        if (reg.student > 10)
                        {
                            thisAmount += 10000 + 500 * (reg.student - 5);
                        }
                        thisAmount += 300 * reg.student;
                        break;
                    }
            } */
            //kazanılan para puan
            //volumeCredits += Math.Max(reg.student - 15, 0);

            // extra bonus para puan her 5 yazılım öğrencisi için
            //decimal fiveStudentGroup = reg.student / 5;
            //if (Types.Software == findCourse(reg).Type) volumeCredits += Math.Floor(fiveStudentGroup);

            //                volumeCredits += calculateVolumeCredit(reg);

            // her bir şiparişin fiyatı
            //result += $"{findCourse(reg).Name}: {(thisAmount / 100).ToString("C", trFormat)} ({reg.student} kişi)\n";
            //                result += $"{findCourse(reg).Name}: {tr(thisAmount / 100)} ({reg.student} kişi)\n";
            //                totalAmount += thisAmount;
            //            }
            /* result += $"Toplam borç { (totalAmount / 100).ToString("C", trFormat)}\n";
            result += $"Kazancınız { volumeCredits.ToString("C", trFormat) } \n"; */
            foreach (Register reg in invoice.registers)
            {
                result += $"{findCourse(reg).Name}: {tr(getAmounth(reg) / 100)} ({reg.student} kişi)\n";
            }
            result += $"Toplam borç { tr(getTotalAmount() / 100) }\n";
            result += $"Kazancınız { tr(totalValumeCredits()) } \n";

            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static int getAmounth(Register register)
        {
            var result = 0;
            switch (findCourse(register).Type)
            {
                case Types.Art:
                    {
                        result = 3000;
                        if (register.student > 15)
                        {
                            result += 1000 * (register.student - 10);
                        }
                        break;
                    }
                case Types.Software:
                    {
                        result = 30000;
                        if (register.student > 10)
                        {
                            result += 10000 + 500 * (register.student - 5);
                        }
                        result += 300 * register.student;
                        break;
                    }
            }
            return result;
        }

        public static Course findCourse(Register register)
        {
            return courses[register.courseID];
        }

        public static decimal calculateVolumeCredit(Register register)
        {
            decimal volumeCredits = 0;
            //kazanılan para puan
            volumeCredits += Math.Max(register.student - 15, 0);

            // extra bonus para puan her 5 yazılım öğrencisi için
            decimal fiveStudentGroup = register.student / 5;
            if (Types.Software == findCourse(register).Type) volumeCredits += Math.Floor(fiveStudentGroup);
            return volumeCredits;
        }

        public static string tr(decimal value)
        {
            CultureInfo trFormat = new CultureInfo("tr-TR", false);
            trFormat.NumberFormat.CurrencySymbol = "TL";
            trFormat.NumberFormat.NumberDecimalDigits = 2;
            return value.ToString("C", trFormat);
        }

        public static decimal totalValumeCredits()
        {
            //decimal volumeCredits = 0;
            decimal result = 0;
            foreach (Register reg in invoice.registers)
            {
                //volumeCredits += calculateVolumeCredit(reg);
                result += calculateVolumeCredit(reg);
            }
            //return volumeCredits;
            return result;
        }

        public static decimal getTotalAmount()
        {
            // decimal totalAmount = 0;
            decimal result = 0;
            foreach (Register reg in invoice.registers)
            {
                //totalAmount += getAmounth(reg);
                result += getAmounth(reg);
            }
            //return totalAmount;
            return result;
        }
    }
}

