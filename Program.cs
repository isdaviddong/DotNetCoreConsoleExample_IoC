﻿using System;

namespace consoleIoC
{
    class Program
    {
        static void Main(string[] args)
        {
            //一般員工 SalaryFormula
            SalaryCalculator SC = new SalaryCalculator(new SalaryFormula());
            //注意參數完全相同
            float amount = SC.Calculate(8 * 19, 200, 8);
            Console.Write("\nSalaryFormula--->amount:" + amount);

            //老闆 BossSalaryFormula
            SC = new SalaryCalculator(new BossSalaryFormula());
            //注意參數完全相同
            amount = SC.Calculate(8 * 19, 200, 8); //即便與員工相同
                                                   //但計算出的結果不同
            Console.Write("\nBoss SalaryFormula--->amount:" + amount);
            Console.Write("\n");
            Console.ReadKey();
        }

        /// <summary>
        /// 計算薪資的類別
        /// </summary>
        class SalaryCalculator
        {
            /// <summary>
            /// 計算薪資的公式物件
            /// </summary>
            private ISalaryFormula _SalaryFormula;
            /// <summary>
            /// 建構子
            /// </summary>
            /// <param name="SalaryFormula"></param>
            public SalaryCalculator(ISalaryFormula SalaryFormula)
            {
                _SalaryFormula = SalaryFormula;
            }
            /// <summary>
            /// 實際計算薪資
            /// </summary>
            /// <param name="WorkHours">工時</param>
            /// <param name="HourlyWage">時薪</param>
            /// <param name="PrivateDayOff">請假天數</param>
            /// <returns></returns>
            public float Calculate(float WorkHours, int HourlyWage, int PrivateDayOffHours)
            {
                return _SalaryFormula.Execute(WorkHours, HourlyWage, PrivateDayOffHours);
            }
        }

        /// <summary>
        /// 預設的計算薪資的公式的類別
        /// </summary>
        class SalaryFormula : ISalaryFormula
        {
            /// <summary>
            /// 實際計算薪資
            /// </summary>
            /// <param name="WorkHours"></param>
            /// <param name="HourlyWage"></param>
            /// <param name="PrivateDayOffHours"></param>
            /// <returns></returns>
            public float Execute(float WorkHours, int HourlyWage, int PrivateDayOffHours)
            {
                //薪資=工時*時薪-(事假時數*時薪)
                return WorkHours * HourlyWage - (PrivateDayOffHours * HourlyWage);
            }
        }

        /// <summary>
        /// 計算薪資的公式的介面
        /// </summary>
        public interface ISalaryFormula
        {
            //薪資=工時*時薪-(事假時數*時薪)
            float Execute(float WorkHours, int HourlyWage, int PrivateDayOffHours);
        }

        //老闆薪資計算公式
        class BossSalaryFormula : ISalaryFormula
        {
            public float Execute(float WorkHours, int HourlyWage, int PrivateDayOffHours)
            {
                //老闆請假不扣薪!!!!!!!
                return WorkHours * HourlyWage - (PrivateDayOffHours * HourlyWage * 0);
            }
        }

    }
}