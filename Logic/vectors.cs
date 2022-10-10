using System;
using System.Drawing;

namespace Logic
{
    public struct Vec2
    {
        public double x;
        public double y;

        public Vec2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double mag2()
        {
            return this.x * this.x + this.y * this.y;
        }

        public double mag()
        {
            return Math.Sqrt(this.mag2());
        }

        public static Vec2 operator +(Vec2 a) => a;
        public static Vec2 operator -(Vec2 a) => new Vec2(-a.x, -a.y);
        public static Vec2 operator +(Vec2 a, Vec2 b)
            => new Vec2(a.x + b.x, a.y + b.y);
        public static Vec2 operator -(Vec2 a, Vec2 b)
            => new Vec2(a.x - b.x, a.y - b.y);
        public static Vec2 operator +(Vec2 a, Vec2Int b)
            => new Vec2(a.x + b.x, a.y + b.y);
        public static Vec2 operator -(Vec2 a, Vec2Int b)
            => new Vec2(a.x - b.x, a.y - b.y);
        public static Vec2 operator +(Vec2Int a, Vec2 b)
            => new Vec2(a.x + b.x, a.y + b.y);
        public static Vec2 operator -(Vec2Int a, Vec2 b)
            => new Vec2(a.x - b.x, a.y - b.y);
        public static Vec2 operator *(Vec2 a, double b)
            => new Vec2(a.x * b, a.y * b);
        public static Vec2 operator *(Vec2 a, int b)
            => new Vec2(a.x * b, a.y * b);
        public static Vec2 operator /(Vec2 a, double b)
            => a * (1 / b);
        public static Vec2 operator /(Vec2 a, int b)
            => a * (1 / Convert.ToDouble(b));

        public Vec2 pow(double exp)
        {
            return new Vec2(Math.Pow(this.x, exp), Math.Pow(this.y, exp));
        }
        public double dot(Vec2 b)
        {
            return this.x * b.x + this.y * b.y;
        }
        public double dot(Vec2Int b)
        {
            return this.x * b.x + this.y * b.y;
        }

        public Vec2 normalize()
        {
            return this / this.mag();
        }
    }
    public struct Vec2Int
    {
        public int x;
        public int y;

        public Vec2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int mag2()
        {
            return this.x * this.x + this.y * this.y;
        }

        public double mag()
        {
            return Math.Sqrt(this.mag2());
        }

        public static Vec2Int operator +(Vec2Int a) => a;
        public static Vec2Int operator -(Vec2Int a) => new Vec2Int(-a.x, -a.y);
        public static Vec2Int operator +(Vec2Int a, Vec2Int b)
            => new Vec2Int(a.x + b.x, a.y + b.y);
        public static Vec2Int operator -(Vec2Int a, Vec2Int b)
            => new Vec2Int(a.x - b.x, a.y - b.y);
        public static Vec2Int operator *(Vec2Int a, int b)
            => new Vec2Int(a.x * b, a.y * b);
        public static Vec2Int operator /(Vec2Int a, int b)
            => new Vec2Int(a.x / b, a.y / b);

        public int dot(Vec2Int b)
        {
            return this.x * b.x + this.y * b.y;
        }

        public double dot(Vec2 b)
        {
            return this.x * b.x + this.y * b.y;
        }
    }

    public struct Vec3
    {
        public double x;
        public double y;
        public double z;

        public Vec3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double mag2()
        {
            return this.x * this.x + this.y * this.y + this.z * this.z;
        }

        public double mag()
        {
            return Math.Sqrt(this.mag2());
        }

        public static Vec3 operator +(Vec3 a) => a;
        public static Vec3 operator -(Vec3 a) => new Vec3(-a.x, -a.y, -a.z);
        public static Vec3 operator +(Vec3 a, Vec3 b)
            => new Vec3(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vec3 operator -(Vec3 a, Vec3 b)
            => new Vec3(a.x - b.x, a.y - b.y, a.z - b.z);
        public static Vec3 operator +(Vec3 a, Vec3Int b)
            => new Vec3(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vec3 operator -(Vec3 a, Vec3Int b)
            => new Vec3(a.x - b.x, a.y - b.y, a.z - b.z);
        public static Vec3 operator +(Vec3Int a, Vec3 b)
            => new Vec3(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vec3 operator -(Vec3Int a, Vec3 b)
            => new Vec3(a.x - b.x, a.y - b.y, a.z - b.z);
        public static Vec3 operator *(Vec3 a, double b)
            => new Vec3(a.x * b, a.y * b, a.z * b);
        public static Vec3 operator *(Vec3 a, int b)
            => new Vec3(a.x * b, a.y * b, a.z * b);
        public static Vec3 operator /(Vec3 a, double b)
            => a * (1 / b);
        public static Vec3 operator /(Vec3 a, int b)
            => a * (1 / Convert.ToDouble(b));

        public Vec3 pow(double exp)
        {
            return new Vec3(Math.Pow(this.x, exp), Math.Pow(this.y, exp), Math.Pow(this.z, exp));
        }

        public double dot(Vec3 b)
        {
            return this.x * b.x + this.y * b.y + this.z * b.z;
        }
        public double dot(Vec3Int b)
        {
            return this.x * b.x + this.y * b.y + this.z * b.z;
        }

        public Vec3 normalize()
        {
            return this / this.mag();
        }

        public Vec3 clampToColor()
        {
            return new Vec3(
                Math.Max(Math.Min(Math.Floor(this.x * 255), 255), 0),
                Math.Max(Math.Min(Math.Floor(this.y * 255), 255), 0),
                Math.Max(Math.Min(Math.Floor(this.z * 255), 255), 0)
            );
        }

        public Color toColour()
        {
            return Color.FromArgb(
                Convert.ToInt32(Math.Max(Math.Min(Math.Floor(this.x * 255), 255), 0)),
                Convert.ToInt32(Math.Max(Math.Min(Math.Floor(this.y * 255), 255), 0)),
                Convert.ToInt32(Math.Max(Math.Min(Math.Floor(this.z * 255), 255), 0))
            );
        }
    }

    public struct Vec3Int
    {
        public int x;
        public int y;
        public int z;

        public Vec3Int(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public int mag2()
        {
            return this.x * this.x + this.y * this.y + this.z * this.z;
        }

        public double mag()
        {
            return Math.Sqrt(this.mag2());
        }

        public static Vec3Int operator +(Vec3Int a) => a;
        public static Vec3Int operator -(Vec3Int a) => new Vec3Int(-a.x, -a.y, -a.z);
        public static Vec3Int operator +(Vec3Int a, Vec3Int b)
            => new Vec3Int(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vec3Int operator -(Vec3Int a, Vec3Int b)
            => new Vec3Int(a.x - b.x, a.y - b.y, a.z - b.z);
        public static Vec3Int operator *(Vec3Int a, int b)
            => new Vec3Int(a.x * b, a.y * b, a.z * b);
        public static Vec3Int operator /(Vec3Int a, int b)
            => new Vec3Int(a.x / b, a.y / b, a.z / b);

        public int dot(Vec3Int b)
        {
            return this.x * b.x + this.y * b.y + this.z * b.z;
        }

        public double dot(Vec3 b)
        {
            return this.x * b.x + this.y * b.y + this.z * b.z;
        }
    }
}