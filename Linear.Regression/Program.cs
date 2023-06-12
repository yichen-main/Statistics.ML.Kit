//var array = new Point[5];
//array[0] = new Point(2, 2);
//array[1] = new Point(4, 0);
//array[2] = new Point(5, 4);
//array[3] = new Point(6, 3);
//array[4] = new Point(8, 6);

//var array = new Point[5];
//array[0] = new Point(14, 13);
//array[1] = new Point(12, 11);
//array[2] = new Point(8, 9);
//array[3] = new Point(10, 7);
//array[4] = new Point(6, 15);

var array = new Point[4];
array[0] = new Point(13, 14);
array[1] = new Point(11, 12);
array[2] = new Point(9, 8);
array[3] = new Point(7, 10);
//array[4] = new Point(15, 6);
LinearRegression(array);
Console.Read();
static void LinearRegression(Point[] points)
{
    //點數不能小於2
    if (points.Length < 2) return;
    //1.計算平均值
    var averageX = points.Select(item => item.X).Average();
    var averageY = points.Select(item => item.Y).Average();

    //2.計算標準差
    var standardDeviationX = StandardDeviation(points.Select(item => item.X));
    var standardDeviationY = StandardDeviation(points.Select(item => item.Y));

    //3.回歸係數
    var r = RegressionCoefficient(points);

    //4.截距
    double intercept = averageY - r * averageX;
    Console.WriteLine($"斜率： y = {r:0.0000}x + {intercept:0.0000}");

    double residualss = 0;    //殘差平方和(residual sum of squares)
    double regressionss = 0;  //回歸平方和(regression sum of squares)
    foreach (Point p in points)
    {
        residualss += (p.Y - intercept - r * p.X) * (p.Y - intercept - r * p.X);
        regressionss += (intercept + r * p.X - averageY) * (intercept + r * p.X - averageY);
    }
    Console.WriteLine("殘差平方和： " + residualss.ToString("0.0000"));
    Console.WriteLine("回歸平方和： " + regressionss.ToString("0.0000"));
}

//標準差
static double StandardDeviation(IEnumerable<double> arrays) => arrays.Any() ? Math.Sqrt(arrays.Sum(item => Math.Pow(item - arrays.Average(), 2)) / arrays.Count()) : default;
static double RegressionCoefficient(Point[] points)
{//回歸係數

    double numerator = 0, denominator = 0;
    var averageX = points.Select(item => item.X).Average();
    var averageY = points.Select(item => item.Y).Average();
    foreach (Point p in points)
    {
        numerator += (p.X - averageX) * (p.Y - averageY);
        denominator += (p.Y - averageY) * (p.Y - averageY);
    }
    return numerator / denominator;
}
readonly record struct Point(double X = 0, double Y = 0);