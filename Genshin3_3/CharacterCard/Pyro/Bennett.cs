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

        public override string NameID => "xiangling";

        public override ElementCategory CharacterElement => ElementCategory.Pyro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Longweapon;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;
        private class 鼓舞领域 : AbstractCardPersistentEffect
        {
            public override int MaxUseTimes => 2;
            public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes-- },
                { SenderTag.DamageIncrease,(me,p,s,v)=>
                    {
                        if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv))
	                    {
                            dv.Damage+=2;
	                    } 
                    }
                },
                { SenderTag.AfterUseSkill,(me, p, s, v) =>
                    {
                        if (s is UseSkillSender uss)
                        {
                            me.Heal(this,new DamageVariable(0,2,uss.CharIndex-me.CurrCharacter));
	                    }
                    }
                }
            };
            public override string TextureNameSpace => "genshin3_3";
            public override string TextureNameID => "effect_bennett";
        }
    }
    public class Talent_Bennett : AbstractCardTalent
    {
        public override string CharacterNameID => "bennett";

        public override int Skill => 1;

        public override int[] Costs => new int[] { 0, 0, 0, 4 };

        public override string NameID => "talent_bennett";

        public override CardPersistentTalent Effect => throw new NotImplementedException();
    }
}
