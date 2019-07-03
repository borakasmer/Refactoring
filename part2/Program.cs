using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Refactoring
{
    public class ArtRegisterGenerator : RegisterGenerator
    {
        public ArtRegisterGenerator(Register _register, Course _course) : base(_register, _course)
        {

        }

        public override int getAmounth
        {
            get
            {
                var resultAmounth = 3000;
                if (this.register.student > 15)
                {
                    resultAmounth += 1000 * (this.register.student - 10);
                }
                return resultAmounth;
            }
        }
    }

    public class SoftwareRegisterGenerator : RegisterGenerator
    {
        public SoftwareRegisterGenerator(Register _register, Course _course) : base(_register, _course)
        {
        }
        public override int getAmounth
        {
            get
            {
                var resultAmounth = 30000;
                if (this.register.student > 10)
                {
                    resultAmounth += 10000 + 500 * (this.register.student - 5);
                }
                resultAmounth += 300 * this.register.student;
                return resultAmounth;
            }
        }

        public override decimal getValumeCredit
        {
            get
            {
                decimal fiveStudentGroup = this.register.student / 5;
                return base.getValumeCredit + Math.Floor(fiveStudentGroup);
            }
        }
    }

    public class RegisterGenerator
    {
        public Register register { get; set; }
        public Course course { get; set; }
        public virtual int getAmounth
        {
            get
            {
                throw new Exception("Subclass Responsibility");
                //                var resultAmounth = 0;
                //                switch (this.course.Type)
                //                {
                //                    case Types.Art:
                //                        {
                /*  resultAmounth = 3000;
                 if (this.register.student > 15)
                 {
                     resultAmounth += 1000 * (this.register.student - 10);
                 }
                 break; */
                //                            throw new Exception("Subclass Responsibility");
                //                        }
                //                    case Types.Software:
                //                        {
                /*  resultAmounth = 30000;
                 if (this.register.student > 10)
                 {
                     resultAmounth += 10000 + 500 * (this.register.student - 5);
                 }
                 resultAmounth += 300 * this.register.student;
                 break; */
                //                            throw new Exception("Subclass Responsibility");
                //                        }
                //                }
                //                return resultAmounth;
            }
        }

        public virtual decimal getValumeCredit
        {
            get
            {
                return Math.Max(this.register.student - 15, 0);
                //decimal volumeCredits = 0;
                //kazanılan para puan
                //volumeCredits += Math.Max(this.register.student - 15, 0);

                // extra bonus para puan her 5 yazılım öğrencisi için
                //decimal fiveStudentGroup = this.register.student / 5;
                //if (Types.Software == this.course.Type) volumeCredits += Math.Floor(fiveStudentGroup);
                //return volumeCredits;
            }
        }

        public RegisterGenerator(Register _register, Course _course)
        {
            this.register = _register;
            this.course = _course;
        }
    }
    static class Program
    {
        public static RegisterGenerator createRegisterGenerator(Register reg, Course course)
        {
            //return new RegisterGenerator(reg, course);
            switch (course.Type)
            {
                case Types.Art: return new ArtRegisterGenerator(reg, course);
                case Types.Software: return new SoftwareRegisterGenerator(reg, course);
                default:
                    throw new Exception($"Unknown type: { course.Type }");
            }
        }

        public static Register[] CloneRegister(this Register[] arrayToClone)
        {
            return arrayToClone.Select(item =>
            {
                RegisterGenerator regGenerator = createRegisterGenerator(item, findCourse(item));
                //RegisterGenerator regGenerator = new RegisterGenerator(item, findCourse(item));
                Register reg = (Register)item.Clone();
                //reg.course = findCourse(reg);
                reg.course = regGenerator.course;
                //reg.amount = getAmounth(reg);
                reg.amount = regGenerator.getAmounth;
                //reg.valumeCredit = calculateVolumeCredit(reg);
                reg.valumeCredit = regGenerator.getValumeCredit;
                return reg;
            }).ToArray();
        }
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
            invoice.registers = new Register[] {
                new Register () { courseID = "dpattern", student = 20 },
                new Register () { courseID = "hface", student = 15 },
                new Register () { courseID = "redis", student = 5 },
            };

            PassingData passingData = new PassingData();
            passingData.customerName = invoice.customerName;
            /* passingData.registers = invoice.registers; */
            passingData.registers = invoice.registers.CloneRegister();
            /* CalculateAmounth(passingData, invoice, courses); */
            passingData.TotalAmount = getTotalAmount(passingData);
            passingData.TotalValumeCredits = totalValumeCredits(passingData);
            CalculateAmounth(passingData, courses);
        }

        public static Course findCourse(Register register)
        {
            return courses[register.courseID];
        }

        public static int getAmounth(Register register)
        {
            return new RegisterGenerator(register, findCourse(register)).getAmounth;
        }

        //public static int getAmounth(Register register)
        //{
        //var resultAmounth = 0;
        /* switch (findCourse(register).Type) */
        /*switch (register.course.Type)
        {
            case Types.Art:
                {
                    resultAmounth = 3000;
                    if (register.student > 15)
                    {
                        resultAmounth += 1000 * (register.student - 10);
                    }
                    break;
                }
            case Types.Software:
                {
                    resultAmounth = 30000;
                    if (register.student > 10)
                    {
                        resultAmounth += 10000 + 500 * (register.student - 5);
                    }
                    resultAmounth += 300 * register.student;
                    break;
                }
        }
        return resultAmounth;
    }*/

        public static decimal calculateVolumeCredit(Register register)
        {
            return new RegisterGenerator(register, findCourse(register)).getValumeCredit;
        }
        //public static decimal calculateVolumeCredit(Register register)
        //{
        //    decimal volumeCredits = 0;
        //kazanılan para puan
        //    volumeCredits += Math.Max(register.student - 15, 0);

        // extra bonus para puan her 5 yazılım öğrencisi için
        //    decimal fiveStudentGroup = register.student / 5;
        /* if (Types.Software == findCourse(register).Type) volumeCredits += Math.Floor(fiveStudentGroup); */
        //    if (Types.Software == register.course.Type) volumeCredits += Math.Floor(fiveStudentGroup);
        //    return volumeCredits;
        //}

        public static decimal totalValumeCredits(PassingData data)
        {
            //decimal volumeCredits = 0;
            decimal resultValume = 0;
            /* foreach (Register reg in invoice.registers) */
            foreach (Register reg in data.registers)
            {
                //volumeCredits += calculateVolumeCredit(reg);
                /* resultValume += calculateVolumeCredit (reg); */
                resultValume += reg.valumeCredit;
            }
            //return volumeCredits;
            return resultValume;
        }

        public static decimal getTotalAmount(PassingData data)
        {
            // decimal totalAmount = 0;
            decimal resultTotalAmounth = 0;
            /* foreach (Register reg in invoice.registers) */
            foreach (Register reg in data.registers)
            {
                //totalAmount += getAmounth(reg);
                /* resultTotalAmounth += getAmounth (reg); */
                resultTotalAmounth += reg.amount;
            }
            //return totalAmount;
            return resultTotalAmounth;
        }

        /* public static void CalculateAmounth(PassingData data, Invoice invoice, IDictionary<string, Course> courses) */
        public static void CalculateAmounth(PassingData data, IDictionary<string, Course> courses)
        {
            /* var result = $"{invoice.customerName} için Fatura Detayı: \n"; */
            var result = $"{data.customerName} için Fatura Detayı: \n";
            /* foreach (Register reg in invoice.registers) */
            foreach (Register reg in data.registers)
            {
                /* result += $"{reg.course.Name}: {tr(getAmounth(reg) /100)} ({reg.student} kişi)\n"; */
                result += $"{reg.course.Name}: {tr(reg.amount / 100)} ({reg.student} kişi)\n";
                /* result += $"{findCourse(reg).Name}: {tr(getAmounth(reg) / 100)} ({reg.student} kişi)\n"; */
            }
            /*  result += $"Toplam borç { tr(getTotalAmount() / 100) }\n";
             result += $"Kazancınız { tr(totalValumeCredits()) } \n"; */

            result += $"Toplam borç { tr(data.TotalAmount / 100) }\n";
            result += $"Kazancınız { tr(data.TotalValumeCredits) } \n";

            Console.WriteLine(result);
            Console.ReadLine();

            string tr(decimal value)
            {
                CultureInfo trFormat = new CultureInfo("tr-TR", false);
                trFormat.NumberFormat.CurrencySymbol = "TL";
                trFormat.NumberFormat.NumberDecimalDigits = 2;
                return value.ToString("C", trFormat);
            }
        }
    }
}