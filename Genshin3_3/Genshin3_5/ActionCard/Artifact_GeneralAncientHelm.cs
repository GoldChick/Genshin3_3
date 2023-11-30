using TCGBase;

namespace Genshin3_5
{
    internal class Artifact_GeneralAncientHelm : AbstractCardArtifact
    {
        public override int MaxUseTimes => 0;
        public override CostInit Cost => new CostCreate().Same(2).ToCostInit();

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStart,(me,p,s,v)=>me.AddPersistent(new Effect_GeneralAncientHelm(),p.PersistentRegion)}
        };
    }
    public class Effect_GeneralAncientHelm : AbstractPersistentShieldYellow
    {
        public override int MaxUseTimes => 2;
    }
}
