using TCGBase;


namespace Genshin3_3
{
    public class Ayaka : AbstractCardCharacter
    {
        public override int MaxMP => 3;
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[] {
            new CharacterSimpleA(0,2,1),
            new CharacterSimpleE(1,3),
            new CharacterSingleSummonQ(1,4,new SimpleSummon("genshin3_3","summon_ayaka",1,2,2)),
            new 神里流霰步(),
        };

        public override ElementCategory CharacterElement => ElementCategory.Cryo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Sword;

        public override CharacterRegion CharacterRegion => CharacterRegion.INAZUMA;

        public override string NameID => "ayaka";
        private class 神里流霰步 : AbstractPassiveSkill
        {
            public override string[] TriggerDic => new string[] { SenderTag.AfterSwitch.ToString() };

            public override bool TriggerOnce => false;

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                if (targetArgs[0] == me.TeamIndex && me.CurrCharacter == c.Index)
                {
                    me.AddPersistent(new Enchant(1, 1), c.Index);
                }
            }
        }
    }
    public class Talent_Ayaka : AbstractCardEquipmentOnlyTalent
    {
        public override string CharacterNameID => "ayaka";
        public override int[] Costs => new int[] { 0, 2 };
        public override CardPersistentTalent Effect => new E();

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.Characters[targetArgs[0]].Effects.TryRemove("equipment", "passive_genshin3_3_ayaka");
            base.AfterUseAction(me, targetArgs);
        }
        public class E : CardPersistentTalent
        {
            public override int MaxUseTimes => 1;
            public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes=1},
                { SenderTag.UseDiceFromSwitch,new PersistentDiceCostModifier<UseDiceFromSwitchSender>(
                    (me,p,s,v)=>s.Target==p.PersistentRegion,0,1)},
                { SenderTag.AfterSwitch,(me,p,s,v)=>
                {
                    if (s is AfterSwitchSender ss && s.TeamID == me.TeamIndex && ss.Target==p.PersistentRegion)
                    {
                        me.Characters[p.PersistentRegion].Effects.TryRemove("minecraft","enchant_cryo");
                        me.AddPersistent(new Enchant(1, 1,true,1), p.PersistentRegion);
                    }
                }
                }
            };
        }
    }
}
