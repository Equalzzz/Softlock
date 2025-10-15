namespace ConsoleSoftlock
{
    public struct Vec2(float x, float y) // Чтобы было
    {
        public float x = x, y = y;
        public Vec2(float a) : this(a, a) { }

        public readonly float Length => MathF.Sqrt(x * x + y * y);
        public readonly Vec2 Unit => (x == 0f && y == 0f) ? new() : this / Length;
        public static Vec2 Left => new(-1, 0);
        public static Vec2 Right => new(1, 0);
        public static Vec2 Up => new(0, 1);
        public static Vec2 Down => new(0, -1);
        public static Vec2 One => new(1);
        public static Vec2 Zero => new(0);
        
        public static Vec2 operator +(Vec2 v1, Vec2 v2) =>
            new(v1.x + v2.x, v1.y + v2.y);
        public static Vec2 operator -(Vec2 v1, Vec2 v2) =>
            new(v1.x - v2.x, v1.y - v2.y);
        public static Vec2 operator +(Vec2 v) => // Idk why i need this
            new(v.x, v.y);
        public static Vec2 operator -(Vec2 v) =>
            new(-v.x, -v.y);
        public static Vec2 operator *(Vec2 v, float s) =>
            new(v.x * s, v.y * s);
        public static Vec2 operator /(Vec2 v, float s) =>
            new(v.x / s, v.y / s);
        public static Vec2 operator *(float s, Vec2 v) => v * s;
        public static float operator *(Vec2 v1, Vec2 v2) =>
            v1.x * v2.x + v1.y * v2.y;
        public static bool operator ==(Vec2 v1, Vec2 v2) =>
            v1.x == v2.x && v1.y == v2.y;
        public static bool operator !=(Vec2 v1, Vec2 v2) =>
            v1.x != v2.x || v1.y != v2.y;
        public override readonly bool Equals(object? obj) =>
            obj != null && (Vec2)obj == this;
        public override readonly int GetHashCode() =>
            (int)(37199 * x + 39119 * y);
    }
    public static class Math
    {
        public static void ThereIsNothingHere() => throw new NotImplementedException();
    }
}
