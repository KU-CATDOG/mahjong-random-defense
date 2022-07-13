namespace MRD
{
    public class BulletInfo
    {
        public float SpeedMultiplier { get; } = 1f;
        public float Angle { get; }

        public BulletInfo(float speedMultiplier, float angle)
        {
            SpeedMultiplier = speedMultiplier;
            Angle = angle;
        }
    }
}