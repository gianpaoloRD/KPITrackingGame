using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Vector3D
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Vector3D(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public double Magnitude()
    {
        return Math.Sqrt(X * X + Y * Y + Z * Z);
    }

    public double DotProduct(Vector3D other)
    {
        return X * other.X + Y * other.Y + Z * other.Z;
    }

    public double AngleBetween(Vector3D other)
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
