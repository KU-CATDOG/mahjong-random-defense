using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD
{
    public struct XY
    {
        public int x;
        public int y;

        public XY(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public XY(int x, int y, bool firstAxis)
        {
            if (!firstAxis)
            {
                this.x = x;
                this.y = y;
            }
            else
            {
                this.x = y;
                this.y = x;
            }
        }

        public int this[int index]
        {
            get => index == 0 ? x : y;
            set
            {
                if (index == 0)
                {
                    x = value;
                }
                else
                {
                    y = value;
                }
            }
        }
        public int this[bool index]
        {
            get => !index ? x : y;
            set
            {
                if (!index)
                {
                    x = value;
                }
                else
                {
                    y = value;
                }
            }
        }

        public static implicit operator Vector2(XY pos) => new Vector2(pos.x, pos.y);
        public static implicit operator Vector3(XY pos) => new Vector3(pos.x, pos.y, 0);
        public static implicit operator XY(Vector2 pos) => new XY((int)pos.x, (int)pos.y);
        public static implicit operator XY((int x, int y) pos) => new XY(pos.x, pos.y);
        public static implicit operator XY(Vector3 pos) => new XY((int)pos.x, (int)pos.y);
        public static explicit operator XY(string str)
        {
            string[] split = str.Split(' ');
            return new XY(int.Parse(split[0]), int.Parse(split[1]));
        }
        public static XY operator +(XY a, XY b) => new XY(a.x + b.x, a.y + b.y);
        public static XY operator -(XY a) => new XY(-a.x, -a.y);
        public static XY operator -(XY a, XY b) => a + (-b);
        public static int operator *(XY a, XY b) => a.x * b.x + a.y * b.y;
        public static XY operator *(XY v, int a) => new XY(v.x * a, v.y * a);
        public static XY operator *(int a, XY v) => v * a;
        public static XY operator *((XY row1, XY row2) mat, XY v) => (mat.row1 * v, mat.row2 * v);
        public static XY operator /(XY a, int b) => (a.x / b, a.y / b);
        public static bool operator ==(XY a, XY b) => a.x == b.x && a.y == b.y;
        public static bool operator !=(XY a, XY b) => !(a.x == b.x && a.y == b.y);
        public static XY Max(XY v, XY w) => (Mathf.Max(v.x, w.x), Mathf.Max(v.y, w.y));
        public static XY Min(XY v, XY w) => (Mathf.Min(v.x, w.x), Mathf.Min(v.y, w.y));
        public static XY Transpose(XY v) => new XY(v.y, v.x);

        public override bool Equals(object obj)
        {
            if (obj is XY)
            {
                return this == ((XY)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }

        public override string ToString()
        {
            return x.ToString() + " " + y.ToString();
        }
    }

}