using TCGBase;

namespace Genshin3_3
{
    public class Bennett : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
        new CharacterSimpleA(0,2,3),
        new CharacterSimpleE(3,3),
        new CharacterEffectQ(3,2,new 鼓舞领域(),false,3,4)
        };

        public override string NameID => "bennett";

        public override ElementCategory CharacterElement => ElementCategory.Pyro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Longweapon;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;
        public class 鼓舞领域 : AbstractCardPersistentEffect
        {
            private readonly bool _talent;
            public 鼓舞领域(bool talent = false)
            {
                _talent = talent;
            }
            public override int MaxUseTimes => 2;
            public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes-- },
                { SenderTag.DamageIncrease,(me,p,s,v)=>
                    {
                        if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv))
                        {
                            if (_talent || me.Characters[me.CurrCharacter].HP>=7)
                            {
                                dv.Damage+=2;
                            }
                        }
                    }
                },
                { SenderTag.AfterUseSkill,(me, p, s, v) =>
                    {
                        if (me.TeamIndex==s.TeamID && s is AfterUseSkillSender uss && me.Characters[uss.CharIndex].HP<7)
                        {
                            me.Heal(this,new DamageVariable(0,2,uss.CharIndex-me.CurrCharacter));
                        }
                    }
                }
            };
            public override string TextureNameSpace => "genshin3_3";
            public override string TextureNameID => "effect_bennett";
            public override int[] Info(AbstractPersistent p) => new int[] { p.AvailableTimes, _talent ? 1 : 0 };
        }
    }
    public class Talent_Bennett : AbstractCardEquipmentFightActionTalent
    {
        public override string CharacterNameID => "bennett";

        public override int Skill => 1;

        public override int[] Costs => new int[] { 0, 0, 0, 4 };

        public override CardPersistentTalent Effect => new Talent_E();
        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            var p = me.Effects.Find("effect_bennett");
            if (p != null)
            {
                p.Active = false;
            }
            base.AfterUseAction(me, targetArgs);
        }
        private class Talent_E : CardPersistentTalent
        {
            public override int Skill => 2;
            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.Hurt(new(3, 2), c.Card.Skills[2], () => me.AddPersistent(new Bennett.鼓舞领域(true), c.Index));
            }
        }
    }
}
