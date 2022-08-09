using UnityEngine;

namespace MRD
{
    public readonly struct XY
    {
        public int X { get; }
        public int Y { get; }

        public XY(int x, int y)
        {
            X = x;
            Y = y;
        }

        public XY(int x, int y, bool firstAxis)
        {
            if (!firstAxis)
            {
                X = x;
                Y = y;
            }
            else
            {
                X = y;
                Y = x;
            }
        }

        public static implicit operator Vector2(XY pos) => new(pos.X, pos.Y);
        public static implicit operator Vector3(XY pos) => new(pos.X, pos.Y, 0);
        public static implicit operator XY(Vector2 pos) => new((int)pos.x, (int)pos.y);
        public static implicit operator XY((int x, int y) pos) => new(pos.x, pos.y);
        public static implicit operator XY(Vector3 pos) => new((int)pos.x, (int)pos.y);

        public static explicit operator XY(string str)
        {
            string[] split = str.Split(' ');
            return new XY(int.Parse(split[0]), int.Parse(split[1]));
        }

        public static XY operator +(XY a, XY b) => new(a.X + b.X, a.Y + b.Y);
        public static XY operator -(XY a) => new(-a.X, -a.Y);
        public static XY operator -(XY a, XY b) => a + -b;
        public static int operator *(XY a, XY b) => a.X * b.X + a.Y * b.Y;
        public static XY operator *(XY v, int a) => new(v.X * a, v.Y * a);
        public static XY operator *(int a, XY v) => v * a;
        public static XY operator *((XY row1, XY row2) mat, XY v) => (mat.row1 * v, mat.row2 * v);
        public static XY operator /(XY a, int b) => (a.X / b, a.Y / b);
        public static bool operator ==(XY a, XY b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(XY a, XY b) => !(a.X == b.X && a.Y == b.Y);
        public static XY Max(XY v, XY w) => (Mathf.Max(v.X, w.X), Mathf.Max(v.Y, w.Y));
        public static XY Min(XY v, XY w) => (Mathf.Min(v.X, w.X), Mathf.Min(v.Y, w.Y));
        public static XY Transpose(XY v) => new(v.Y, v.X);
        
        public override bool Equals(object obj)
        {
            if (obj is XY) return this == (XY)obj;
            return false;
        }
        public bool Equals(int x, int y) => (X == x && Y == y);

        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();

        public override string ToString() => X + " " + Y;
        public XY Up() => new(X,Y+1);
        public XY Down() => new(X,Y-1);
    }
}
