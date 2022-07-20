namespace MRD
{
    public class ChiToiTowerInfo : YakuHolderInfo
    {
        public ChiToiTowerInfo(ToitsuInfo t1, ToitsuInfo t2, ToitsuInfo t3, ToitsuInfo t4, ToitsuInfo t5, ToitsuInfo t6, ToitsuInfo t7)
        {
            mentsus.Add(t1);
            mentsus.Add(t2);
            mentsus.Add(t3);
            mentsus.Add(t4);
            mentsus.Add(t5);
            mentsus.Add(t6);
            mentsus.Add(t7);

            hais.AddRange(t1.Hais);
            hais.AddRange(t2.Hais);
            hais.AddRange(t3.Hais);
            hais.AddRange(t4.Hais);
            hais.AddRange(t5.Hais);
            hais.AddRange(t6.Hais);
            hais.AddRange(t7.Hais);
        }
    }
}
