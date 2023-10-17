using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Vector2D
{
    public double X { get; set; }
    public double Y { get; set; }

    public Vector2D(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double Magnitude()
    {
        return Math.Sqrt(X * X + Y * Y);
    }

    public double DotProduct(Vector2D other)
    {
        return X * other.X + Y * other.Y;
    }

    public double AngleBetween(Vector2D other)
    {
        double dotProduct = DotProduct(other);
        double magnitude1 = Magnitude();
        double magnitude2 = other.Magnitude();

        if (magnitude1 == 0 || magnitude2 == 0)
        {
            throw new ArgumentException("A vector has zero magnitude.");
        }

        double cosTheta = dotProduct / (magnitude1 * magnitude2);
        double radians = Math.Acos(cosTheta);

        // Convert radians to degrees
        double degrees = radians * (180.0 / Math.PI);

        return degrees;
    }
}