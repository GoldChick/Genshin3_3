using TCGBase;

namespace Genshin3_7
{
    public class Shenhe : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Cryo(1).ToCostInit(),new DamageVariable(0,2)),
            new CharacterSimpleSkill(SkillCategory.E,new CostCreate().Cryo(3).ToCostInit(),(skill,me,c,args)=>me.AddPersistent(new Effect_Shenhe()),new DamageVariable(1,2)),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Cryo(3).MP(2).ToCostInit(),
                (skill,me,c,args)=>me.AddSummon(new Summon_Shenhe()),new DamageVariable(1,1)),
        };

        public override ElementCategory CharacterElement => ElementCategory.Cryo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Longweapon;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;
    }
    public class Effect_Shenhe : AbstractCardEffect
    {
        public override int MaxUseTimes => 2;
        public Effect_Shenhe(bool talent = false)
        {
            Variant = talent ? 1 : 0;
            TriggerDic = new()
            {
                { SenderTag.RoundStep,(me,p,s,v)=>p.Data=null},
                { SenderTag.DamageIncrease,(me,p,s,v)=>
                {
                    if (me.TeamIndex==s.TeamID && s is PreHurtSender hs && hs.RootSource is AbstractCardSkill skill && v is DamageVariable dv)
                    {
                        if (dv.Element==1)
                        {
                            dv.Damage++;
                            if (talent && p.Data==null && skill.Category==SkillCategory.A && dv.DirectSource==DamageSource.Character)
                            {
                                p.Data=114;
                            }else
                            {
                                p.AvailableTimes--;
                            }
                        }
                    }
                }
                }
            };
        }
        public override PersistentTriggerDictionary TriggerDic { get; }
    }
    public class Summon_Shenhe : AbstractCardSummon
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>me.Enemy.Hurt(new(1,1),this,()=>p.AvailableTimes--) },
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && v is DamageVariable dv && (dv.Element==0||dv.Element==1))
                {
                    dv.Damage++;
                }
            }
            }
        };
    }

    public class Talent_Shenhe : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 1;

        public override string CharacterNameID => "shenhe";

        public override CostInit Cost => new CostCreate().Cryo(3).ToCostInit();
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.Hurt(new(1, 2), c.Card.Skills[1], () => me.AddPersistent(new Effect_Shenhe(true)));
        }
    }
}
