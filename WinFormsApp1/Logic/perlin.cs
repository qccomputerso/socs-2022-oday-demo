using System;

namespace Logic
{
    public class Perlin2d
    {
        int seed;
        Dictionary<string, Vec2> points = new Dictionary<string, Vec2>();

        private static double PRNG(double a, double b, int seed)
        {
            double d = (Math.Abs(a) * 100 + Math.Abs(b) * 100 + 2000) * seed * 15485863;
            return (d * d * d % 2038074743) / 2038074743;
        }

        private static double fade(double t) => t*t*t*(t*(t*6 - 15) + 10);

        public Perlin2d(int seed)
        {
            this.seed = seed;
        }

        public Vec2 grad(Vec2 p)
        {
            string key = $"{p.x}/{p.y}";
            if (!this.points.ContainsKey(key))
            {
                this.points.Add(key, new Vec2(
                    PRNG(p.x, p.y, this.seed) * 2 - 1,
                    PRNG(p.x, p.y, this.seed + 100) * 2 - 1
                ));
            }
            return this.points.GetValueOrDefault(key, new Vec2(0, 0));
        }

        public double valueAt(double x, double y)
        {
            return this.valueAt(new Vec2(x, y));
        }
        public double valueAt(Vec2 p)
        {
            Vec2 p0 = new Vec2(Math.Floor(p.x), Math.Floor(p.y));
            Vec2 p1 = p0 + new Vec2(1, 0);
            Vec2 p2 = p0 + new Vec2(0, 1);
            Vec2 p3 = p0 + new Vec2(1, 1);

            Vec2 g0 = grad(p0);
            Vec2 g1 = grad(p1);
            Vec2 g2 = grad(p2);
            Vec2 g3 = grad(p3);

            double t0 = fade(p.x - p0.x);
            double t1 = fade(p.y - p0.y);

            double p0p1 = (1 - t0) * g0.dot(p - p0) + t0 * g1.dot(p - p1);
            double p2p3 = (1 - t0) * g2.dot(p - p2) + t0 * g3.dot(p - p3);

            return (1 - t1) * p0p1 + t1 * p2p3;
        }
    }


    public class Perlin3d
    {
        int seed;
        Dictionary<string, Vec3> points = new Dictionary<string, Vec3>();

        private static double PRNG(double a, double b, double c, int seed)
        {
            double d = (Math.Abs(a) * 100 + Math.Abs(b) * 100 + Math.Abs(c) * 100 + 3000) * seed * 15485863;
            return (d * d * d % 2038074743) / 2038074743;
        }

        private static double fade(double t) => t*t*t*(t*(t*6 - 15) + 10);

        public Perlin3d(int seed)
        {
            this.seed = seed;
        }

        public Vec3 grad(Vec3 p)
        {
            string key = $"{p.x}/{p.y}/{p.z}";
            if (!this.points.ContainsKey(key))
            {
                this.points.Add(key, new Vec3(
                    PRNG(p.x, p.y, p.z, this.seed) * 2 - 1,
                    PRNG(p.x, p.y, p.z, this.seed + 100) * 2 - 1,
                    PRNG(p.x, p.y, p.z, this.seed + 200) * 2 - 1
                ));
            }
            return this.points.GetValueOrDefault(key, new Vec3(0, 0, 0));
        }

        public double valueAt(double x, double y, double z)
        {
            return this.valueAt(new Vec3(x, y, z));
        }
        public double valueAt(Vec3 p)
        {
            Vec3 p0 = new Vec3(Math.Floor(p.x), Math.Floor(p.y), Math.Floor(p.z));
            Vec3 p1 = p0 + new Vec3(1, 0, 0);
            Vec3 p2 = p0 + new Vec3(0, 1, 0);
            Vec3 p3 = p0 + new Vec3(1, 1, 0);
            Vec3 p4 = p0 + new Vec3(0, 0, 1);
            Vec3 p5 = p0 + new Vec3(1, 0, 1);
            Vec3 p6 = p0 + new Vec3(0, 1, 1);
            Vec3 p7 = p0 + new Vec3(1, 1, 1);

            Vec3 g0 = grad(p0);
            Vec3 g1 = grad(p1);
            Vec3 g2 = grad(p2);
            Vec3 g3 = grad(p3);
            Vec3 g4 = grad(p4);
            Vec3 g5 = grad(p5);
            Vec3 g6 = grad(p6);
            Vec3 g7 = grad(p7);

            double t0 = fade(p.x - p0.x);
            double t1 = fade(p.y - p0.y);
            double t2 = fade(p.z - p0.z);

            double p0p1 = (1 - t0) * g0.dot(p - p0) + t0 * g1.dot(p - p1);
            double p2p3 = (1 - t0) * g2.dot(p - p2) + t0 * g3.dot(p - p3);
            double p4p5 = (1 - t0) * g4.dot(p - p4) + t0 * g5.dot(p - p5);
            double p6p7 = (1 - t0) * g6.dot(p - p6) + t0 * g7.dot(p - p7);

            double y1 = (1 - t1) * p0p1 + t1 * p2p3;
            double y2 = (1 - t1) * p4p5 + t1 * p6p7;

            return (1 - t2) * y1 + t2 * y2;
        }
    }
    public struct Accessor<T>
    {
        public T perlin;
        public double interval;
        public double weight;

        public Accessor(T perlin, double interval, double weight)
        {
            this.perlin = perlin;
            this.interval = interval;
            this.weight = weight;
        }
    }
    public class PerlinAccessor2d
    {
        Accessor<Perlin2d>[] accessors;

        public PerlinAccessor2d(Accessor<Perlin2d>[] accessors)
        {
            this.accessors = accessors;
        }

        public double valueAt(double x, double y)
        {
            return this.valueAt(new Vec2(x, y));
        }
        public double valueAt(Vec2 p)
        {
            double v = 0;
            for (int i = 0; i < accessors.Length; i++)
            {
                Accessor<Perlin2d> accessor = accessors[i];
                v += accessor.perlin.valueAt(p / accessor.interval) * accessor.weight;
            }
            return v;
        }
    }
    public class PerlinAccessor3d
    {
        Accessor<Perlin3d>[] accessors;

        public PerlinAccessor3d(Accessor<Perlin3d>[] accessors)
        {
            this.accessors = accessors;
        }

        public double valueAt(double x, double y, double z)
        {
            return this.valueAt(new Vec3(x, y, z));
        }
        public double valueAt(Vec3 p)
        {
            double v = 0;
            for (int i = 0; i < accessors.Length; i++)
            {
                Accessor<Perlin3d> accessor = accessors[i];
                v += accessor.perlin.valueAt(p / accessor.interval) * accessor.weight;
            }
            return v;
        }
    }
}
