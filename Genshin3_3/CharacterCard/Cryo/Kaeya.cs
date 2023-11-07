using TCGBase;

namespace Genshin3_3
{
    public class Kaeya : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,1),
            new CharacterSimpleE(1,3),
            new CharacterEffectQ(1,1,new 寒冰之棱(),false,1,4)
        };

        public override ElementCategory CharacterElement => ElementCategory.Cryo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Sword;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;

        public override string NameID => "kaeya";
        private class 寒冰之棱 : AbstractCardPersistentEffect
        {
            public override string TextureNameSpace => "genshin3_3";
            public override string TextureNameID => "effect_kaeya";
            public override int MaxUseTimes => 3;
            public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.AfterSwitch,(me,p,s,v)=>
                    {
                        if (me.TeamIndex==s.TeamID)
                        {
                            me.Enemy.Hurt(new DamageVariable(1,2),this);
                        }
                    }
                }
            };
        }
    }
    public class Talent_Kaeya : AbstractCardTalent
    {
        public override CardPersistentTalent Effect => throw new NotImplementedException();

        public override string CharacterNameID => "kaeya";

        public override int Skill => 1;

        public override int[] Costs => new int[] { 0, 4 };

        private class 冷血之剑_Effect : CardPersistentTalent
        {
            public override int Skill => 1;
            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                base.AfterUseAction(me, c, targetArgs);
                me.Heal(c.Card.Skills[Skill], new DamageVariable(0, 2));
            }
        }
    }

}
