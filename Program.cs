using System;
namespace ConsoleApp1
{
    class Program

    {
        static double D;
        public static double f(double x)
        {
            return x * x - 4;
        }
        public static double fp(double x)
        {
            return (f(x + D) - f(x)) / D;
        }
        public static double fp2(double x)
        {
            return (fp(x + D) - fp(x)) / D;
        }

        public static (double root, int iterations, string msg, bool isRootFound) MDN(double a, double b, double Eps)
        {
            double c;
            int Lich = 0;

            if (f(a) * f(b) > 0) // перевіряємо, чи є корінь на інтервалі [a, b]
            {
                return (0, 0, "No root in the interval", false);
            }
            if (Math.Abs(f(a)) < Eps) // перевіряємо, чи ліва межа не є коренем
            {
                return (a, Lich, "Root was found", true);
            }
            else
            if (Math.Abs(f(b)) < Eps) // перевіряємо, чи права межа не є коренем
            {
                return (b, Lich, "Root was found", true);
            }
            else
            {
                while (Math.Abs(b - a) > Eps) // цикл Методу ділення навпіл
                {
                    c = 0.5 * (a + b);
                    Lich++;
                    if (Math.Abs(f(c)) < Eps) // перевіряємо, чи точка с не є коренем
                    {
                        return (c, Lich, "Root was found", true);
                    }
                    else if (f(a) * f(c) < 0) b = c; // звуження інтервалу пошуку кореня
                    else a = c;
                }
                return (a + b / 2, Lich, "Root was found", true);
            }
        }
        public static void ShowResults(double result, int Lich, string msg, bool isRootFound)
        {
            Console.WriteLine(msg);
            if (isRootFound)
            {
                Console.WriteLine("x = " + result + " Lich = " + Lich);
            }
            Console.ReadLine();
        }
        public static (double root, int iterations, string msg, bool isRootFound) MN(double a, double b, double Eps, int Kmax)
        {
            double x, Dx;
            int i;

            D = Eps / 100.0;

            x = b;

            if (f(x) * fp2(x) < 0)
            {
                x = a;
            }

            if (f(x) * fp2(x) <= 0)
            {
                return (0, 0, "For the given equation, convergence of Newton's method is not guaranteed", false);
            }

            bool found = false;
            for (i = 1; i <= Kmax; i++)
            {
                Dx = f(x) / fp(x);
                x = x - Dx;
                if (Math.Abs(Dx) <= Eps)
                {
                    found = true;
                    break;
                }
            }

            if (found)
            {
                return (x, i, "Root was found", true);
            }
            else
            {
                return (0, i, "For the given number of iterations, the root with accuracy Eps was not found", false);
            }
        }
        public static (double a, double b) FindLocalizationInterval(
            double rangeStart = -1000,
            double rangeEnd = 1000,
            double step = 3)
        {
            double previousPoint = rangeStart;
            double previousValue = f(previousPoint);

            for (double currentPoint = rangeStart + step; currentPoint <= rangeEnd + 1e-10; currentPoint += step)
            {
                double currentValue = f(currentPoint);

                if (previousValue * currentValue < 0 || Math.Abs(currentValue) < 1e-10)
                {
                    return (previousPoint, currentPoint);
                }

                previousPoint = currentPoint;
                previousValue = currentValue;
            }

            return (0, 0); 
        }

        static void Main()
        {
            double a, b, Eps;
            int Kmax;

            int methodN;
            Console.WriteLine("Choose method 1 - Method Dilenia Navpil 2 - Method Newtona");
            methodN = Convert.ToInt32(Console.ReadLine());


            Console.WriteLine("Use auto finding a and b? Type 1 - Yes or 2 - No");
            int autoIntervalFinding = Convert.ToInt32(Console.ReadLine());

            if (autoIntervalFinding == 1) {
                Console.WriteLine(" Input eps");
                Eps = Convert.ToDouble(Console.ReadLine());
                var result = FindLocalizationInterval();
                (a, b) = result;
                Console.WriteLine("Your a = " + a + " and b =" + b);
            }
            else
            {
                Console.WriteLine(" Input a, b, eps");
                a = Convert.ToDouble(Console.ReadLine());
                b = Convert.ToDouble(Console.ReadLine());
                Eps = Convert.ToDouble(Console.ReadLine());
            }

            if (methodN == 2)
            {
                Console.WriteLine("Input Kmax");
                Kmax = Convert.ToInt32(Console.ReadLine());
                var result  = MN(a, b, Eps, Kmax);
                var (root, iterations, msg, isRootFound) = result;
                ShowResults(root, iterations, msg, isRootFound);

            } else 
            {
                var result = MDN(a, b, Eps);
                var (root, iterations, msg, isRootFound) = result;
                ShowResults(root, iterations, msg, isRootFound);

            }


        }
    }
}
