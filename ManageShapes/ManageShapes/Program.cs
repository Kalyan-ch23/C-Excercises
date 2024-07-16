using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void j(string[] args)
        {
            ShapeManager shapeManager = new ShapeManager();
            bool continueInput = true;

            while (continueInput)
            {
                Console.WriteLine("Enter shape type (Circle, Rectangle, Triangle):");
                string shapeType = Console.ReadLine();

                switch (shapeType.ToLower())
                {
                    case "circle":
                        double radius;
                    inputRadius:
                        Console.WriteLine("Enter radius:");
                        if (!double.TryParse(Console.ReadLine(), out radius) || radius <= 0)
                        {
                            Console.WriteLine("Invalid radius input. Radius must be a positive number.");
                            goto inputRadius; // Go back to inputRadius label
                        }
                        Circle circle = new Circle { radius = radius };
                        shapeManager.AddShape(circle);
                        break;

                    case "rectangle":
                        double length, width;
                    inputLength:
                        Console.WriteLine("Enter length:");
                        if (!double.TryParse(Console.ReadLine(), out length) || length <= 0)
                        {
                            Console.WriteLine("Invalid length input. Length must be a positive number.");
                            goto inputLength;
                        }

                    inputWidth:
                        Console.WriteLine("Enter width:");
                        if (!double.TryParse(Console.ReadLine(), out width) || width <= 0)
                        {
                            Console.WriteLine("Invalid width input. Width must be a positive number.");
                            goto inputWidth;
                        }

                        Rectangle rectangle = new Rectangle { length = length, width = width };
                        shapeManager.AddShape(rectangle);
                        break;

                    case "triangle":
                        double triangleBase, height;
                    inputBase:
                        Console.WriteLine("Enter base:");
                        if (!double.TryParse(Console.ReadLine(), out triangleBase) || triangleBase <= 0)
                        {
                            Console.WriteLine("Invalid base input. Base must be a positive number.");
                            goto inputBase;
                        }

                    inputHeight:
                        Console.WriteLine("Enter height:");
                        if (!double.TryParse(Console.ReadLine(), out height) || height <= 0)
                        {
                            Console.WriteLine("Invalid height input. Height must be a positive number.");
                            goto inputHeight;
                        }

                        Triangle triangle = new Triangle { Base = triangleBase, Height = height };
                        shapeManager.AddShape(triangle);
                        break;

                    default:
                        Console.WriteLine("Invalid shape type. Please enter Circle, Rectangle, or Triangle.");
                        break;
                }

                Console.WriteLine();
                shapeManager.PrintDetails();
            }
        }
    }
    public abstract class shape
    {
        // Used an abstract class common interface for all shapes
        public abstract double CalculateArea();
        public abstract double CalculatePerimeter();
    }
    public class Circle : shape
    {
        public double radius { get; set; }
        public override double CalculateArea()
        {
            return Math.PI * radius * radius;
        }
        public override double CalculatePerimeter()
        {
            return 2 * Math.PI * radius;
        }
    }
    public class Rectangle : shape
    {
        public double length { get; set; }
        public double width { get; set; }
        public override double CalculateArea()
        {
            return length * width;
        }
        public override double CalculatePerimeter()
        {
            return 2 * (length + width);
        }
    }
    public class Triangle : shape
    {
        public double Base { get; set; }
        public double Height { get; set; }
        public override double CalculateArea()
        {
            return 0.5 * Base * Height;
        }
        public override double CalculatePerimeter()
        {
            return Base + Height + Math.Sqrt(Base * Base + Height * Height);
        }
    }
    public class ShapeManager
    {
        private List<shape> shapes = new List<shape>();
        public void AddShape(shape shape)
        {
            if (shape != null)
            {
                shapes.Add(shape);
            }
            else
            {
                Console.WriteLine("Cannot add a null shape.");
            }
        }
        public void PrintDetails()
        {
            double TotalArea = 0;
            double TotalPerimeter = 0;
            foreach (var shape in shapes)
            {
                if (shape is Circle)
                {
                    Circle circle = (Circle)shape;
                    Console.WriteLine($"Circle:\nRadius:{circle.radius}\nArea:{circle.CalculateArea():F1}\\nPerimeter: {{circle.CalculatePerimeter():F1}}\\n");
                }
                else if (shape is Rectangle)
                {
                    Rectangle rectangle = (Rectangle)shape;
                    Console.WriteLine($"Rectangle:\nLength: {rectangle.length}\nWidth: {rectangle.width}\nArea: {rectangle.CalculateArea():F1}\nPerimeter: {rectangle.CalculatePerimeter():F1}\n");
                }
                else if (shape is Triangle)
                {
                    Triangle triangle = (Triangle)shape;
                    Console.WriteLine($"Triangle:\nBase: {triangle.Base}\nHeight: {triangle.Height}\nArea: {triangle.CalculateArea():F1}\nPerimeter: {triangle.CalculatePerimeter():F1}\n");
                }
                TotalArea += shape.CalculateArea();
                TotalPerimeter += shape.CalculatePerimeter();
            }
            Console.WriteLine($"Total Shapes: {shapes.Count}");
            Console.WriteLine($"Total Area: {TotalArea:F1}");
            Console.WriteLine($"Total Perimeter: {TotalPerimeter:F1}");
        }

    }
}
