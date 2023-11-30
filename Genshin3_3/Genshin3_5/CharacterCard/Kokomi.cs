using TCGBase;
namespace Genshin3_5
{
    public class Kokomi : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Hydro(1).ToCostInit(),new DamageVariable(2,1)),
            new E(),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Hydro(3).MP(2).ToCostInit(),
                (skill,me,c,args)=>me.AddPersistent(new Effect_Kokomi(),c.Index),new DamageVariable(2,2)),
        };

        public override ElementCategory CharacterElement => ElementCategory.Hydro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Catalyst;

        public override CharacterRegion CharacterRegion => CharacterRegion.INAZUMA;
        private class E : AbstractCardSkill
        {
            public override SkillCategory Category => SkillCategory.E;

            public override CostInit Cost => new CostCreate().Hydro(3).ToCostInit();

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.AttachElement(this, 2, 0);
                me.AddSummon(new Summon_Kokomi());
            }
        }
        private class Q : AbstractCardSkill
        {
            public override SkillCategory Category => SkillCategory.Q;

            public override CostInit Cost => new CostCreate().Hydro(3).MP(2).ToCostInit();

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.Hurt(new(2, 2), c.Card.Skills[2], () =>
                {
                    me.Heal(c.Card.Skills[2], new(1), new(1, 0, true));
                    me.AddPersistent(new Effect_Kokomi(), c.Index);
                });
            }
        }
    }
    public class Effect_Kokomi : AbstractCardPersistent
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv) && s is PreHurtSender hs && hs.RootSource is AbstractCardSkill skill && skill.Category==SkillCategory.A)
                {
                    dv.Damage++;
                    p.Data=114;
                }
            }
            },
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (p.Data!=null)
                {
                    me.Heal(this,new(1),new(1,0,true));
                    p.Data=null;
                }
            }
            }
        };
    }
    public class Summon_Kokomi : AbstractCardPersistentSummon
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>
            {
                var kkm=me.Characters[p.PersistentRegion];
                me.Enemy.Hurt(new(2,(kkm.Effects.Contains(typeof(Talent_Kokomi))&&kkm.Effects.Contains(typeof(Effect_Kokomi)))?2:1),this,()=>me.Heal(this,new HealVariable(1)));
                p.AvailableTimes--;
            }
            }
        };
    }
    public class Talent_Kokomi : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 2;

        public override string CharacterNameID => "kokomi";

        public override CostInit Cost => new CostCreate().Hydro(3).MP(2).ToCostInit();
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.Hurt(new(2, 2), c.Card.Skills[2], () =>
            {
                me.Heal(c.Card.Skills[2], new(1), new(1, 0, true));
                me.AddPersistent(new Effect_Kokomi(), c.Index);
                var summon = me.Summons.Find(typeof(Summon_Kokomi));
                if (summon != null)
                {
                    summon.AvailableTimes++;
                }
                else
                {
                    me.AddSummon(new Summon_Kokomi());
                }
            });
        }
    }
}
