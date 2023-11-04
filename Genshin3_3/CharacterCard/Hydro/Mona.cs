using TCGBase;

namespace Genshin3_3
{
    public class Mona : AbstractCardCharacter
    {
        public override int MaxMP => 3;

        public override AbstractCardSkill[] Skills => new AbstractCardSkill[] {
            new CharacterSimpleA(2,1),
            new CharacterSingleSummonE(2,1,new 虚影()),
            new 星命定轨()};

        public override string NameID => "mona";

        public override ElementCategory CharacterElement => ElementCategory.Hydro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Catalyst;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;

        private class 虚影 : AbstractCardPersistentSummon
        {
            public override bool CustomDesperated => true;
            public override int MaxUseTimes => 1;
            public override PersistentTriggerDictionary TriggerDic => new() {
                { SenderTag.HurtDecrease.ToString(), new PersistentPurpleShield(1) },
                { SenderTag.RoundOver.ToString(), (me,p,s,v)=>
                {
                    p.Active = false;
                    me.Enemy.Hurt(new(2, 1,  0),this);
                }}
            };

            public override string TextureNameSpace => "genshin3_3";
            public override string TextureNameID => "summon_mona";
        }
        private class 星命定轨 : AbstractCardSkill
        {
            public override int[] Costs => new int[] { 0, 0, 3 };
            public override SkillCategory Category => SkillCategory.Q;

            public override void AfterUseAction(PlayerTeam me, Character c, int[]? targetArgs = null)
            {
                me.Enemy.Hurt(new DamageVariable(2, 4, 0), this);
                me.AddPersistent(new 泡影());
            }
            private class 泡影 : AbstractCardPersistentEffect
            {
                public override int MaxUseTimes => 1;

                public override PersistentTriggerDictionary TriggerDic => new() 
                {
                    { SenderTag.HurtMul,(me,p,s,v)=>
                        {
                            if (v is DamageVariable dv)
                            {
                                dv.Damage *= 2;
                                p.Active = false;
                            }
                        }
                    } 
                };

                public override string TextureNameSpace => "genshin3_3";
                public override string TextureNameID => "effect_mona";
            }
        }
    }
}
