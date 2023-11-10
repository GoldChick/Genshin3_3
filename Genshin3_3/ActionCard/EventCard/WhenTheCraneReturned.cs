using TCGBase;
 

namespace Genshin3_3
{
    public class WhenTheCraneReturned : AbstractCardEvent
    {
        public override string NameID => "event_whenthecranereturned";

        public override int[] Costs => new int[] { 1 };

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.AddPersistent(new E());
        }
        public class E : AbstractCardPersistentEffect
        {
            public override int MaxUseTimes => 1;

            public override string TextureNameID => PersistentTextures.Buff;

            public override PersistentTriggerDictionary TriggerDic => new() 
            { 
                { SenderTag.AfterUseSkill, (me,p,s,v)=>
                {
                    if (me.TeamIndex==s.TeamID)
                    {
                        p.AvailableTimes--;
                        me.SwitchToNext();
	                }
                }
                }
            };
        }
    }
}
