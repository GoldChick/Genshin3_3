using TCGBase;

namespace Genshin3_3
{
    public class Collei : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,6),
            new CharacterSimpleE(6,3),
            new CharacterSingleSummonQ(6,2,new SimpleSummon("genshin3_3","summon_collei",6,2,2))
        };

        public override ElementCategory CharacterElement => ElementCategory.Dendro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override CharacterRegion CharacterRegion => CharacterRegion.SUMERU;

        public override string NameID => "collei";
    }

    public class Talent_Collei : AbstractCardEquipmentFightActionTalent
    {
        public override CardPersistentTalent Effect => new E();

        public override string CharacterNameID => "collei";

        public override int Skill => 1;

        public override int[] Costs => new int[] { 0, 0, 0, 0, 0, 0, 4 };

        private class E : CardPersistentTalent
        {
            public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes=1}
            };
            public override int MaxUseTimes => 1;
            public override int Skill => 1;
            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.Hurt(new(6, 3), c.Card.Skills[1], () =>
                {
                    var talent = c.Effects.Find(typeof(E));
                    if (talent != null && talent.AvailableTimes > 0)
                    {
                        me.AddPersistent(new 飞叶());
                        talent.AvailableTimes--;
                    }
                });
            }
        }
        private class 飞叶 : AbstractCardPersistentEffect
        {
            public override int MaxUseTimes => 1;
            public override string TextureNameID => PersistentTextures.Enchant_Dendro;
            public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes--},
                { SenderTag.AfterHurt,(me,p,s,v)=>
                {
                    if (me.TeamIndex!=s.TeamID && s is HurtSender hs && hs.RootSource is AbstractCardSkill)
                    {
                        int r=(int)hs.Reaction;
                        if (r>=8 && r<=10)
                        {
                            me.Enemy.Hurt(new(6,1),this);
                            p.AvailableTimes--;
                        }
                    }
                }
                }
            };
        }
    }
}
