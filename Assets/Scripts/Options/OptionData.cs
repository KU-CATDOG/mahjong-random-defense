using System;
using System.Collections.Generic;

namespace MRD
{
    public static class OptionData
    {
        private static readonly Dictionary<string, Func<TowerOption>> towerOptionBuilders = new()
        {
            { nameof(DoraStatOption), () => new DoraStatOption() },
            { nameof(ChanTaStatOption), () => new ChanTaStatOption() },
            { nameof(CheongIlSaekStatOption), () => new CheongIlSaekStatOption() },
            { nameof(CheongNoDuStatOption), () => new CheongNoDuStatOption() },
            { nameof(ChiToiStatOption), () => new ChiToiStatOption() },
            { nameof(DaeChilSeongStatOption), () => new DaeChilSeongStatOption() },
            { nameof(DaeSaHeeStatOption), () => new DaeSaHeeStatOption() },
            { nameof(DaeSamWonStatOption), () => new DaeSamWonStatOption() },
            { nameof(GukSaMuSangStatOption), () => new GukSaMuSangStatOption() },
            { nameof(GuRyeonBoDeungStatOption), () => new GuRyeonBoDeungStatOption() },
            { nameof(HonIlSaekStatOption), () => new HonIlSaekStatOption() },
            { nameof(HonNoDuStatOption), () => new HonNoDuStatOption() },
            { nameof(IlGiTongGwanStatOption), () => new IlGiTongGwanStatOption() },
            { nameof(JangPungPaeYeokPaeStatOption), () => new JangPungPaeYeokPaeStatOption() },
            { nameof(JunJJangStatOption), () => new JunJJangStatOption() },
            { nameof(NokIlSaekStatOption), () => new NokIlSaekStatOption() },
            { nameof(PingHuStatOption), () => new PingHuStatOption() },
            { nameof(RiChiStatOption), () => new RiChiStatOption() },
            { nameof(RyangPeKoStatOption), () => new RyangPeKoStatOption() },
            { nameof(SamSaekDongGakStatOption), () => new SamSaekDongGakStatOption() },
            { nameof(SamSaekDongSoonStatOption), () => new SamSaekDongSoonStatOption() },
            { nameof(SamWonPaeYeokPaeStatOption), () => new SamWonPaeYeokPaeStatOption() },
            { nameof(SanAnKouStatOption), () => new SanAnKouStatOption() },
            { nameof(SanKantSuStatOption), () => new SanKantSuStatOption() },
            { nameof(ShuAnKouStatOption), () => new ShuAnKouStatOption() },
            { nameof(ShuKantSuStatOption), () => new ShuKantSuStatOption() },
            { nameof(SoSaHeeStatOption), () => new SoSaHeeStatOption() },
            { nameof(SoSamWonStatOption), () => new SoSamWonStatOption() },
            { nameof(TangYaoStatOption), () => new TangYaoStatOption() },
            { nameof(ToiToiStatOption), () => new ToiToiStatOption() },
            { nameof(YiPeKoStatOption), () => new YiPeKoStatOption() },
            { nameof(JailSaekStatOption), () => new JailSaekStatOption() },
        };

        public static TowerOption GetOption(string name)
        {
            return towerOptionBuilders.TryGetValue(name, out var v) ? v() : null;
        }
    }
}
