using TCGBase;

namespace Genshin3_7
{
    public class Venti : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,7),
            new CharacterEffectE(7,2,new Effect_Venti(),false),
            new CharacterSingleSummonQ(7,2,new Summon_Venti())
        };

        public override ElementCategory CharacterElement => ElementCategory.Anemo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;
    }
    public class Effect_Venti : AbstractCardPersistent
    {
        public Effect_Venti(bool talent = false)
        {
            Variant = talent ? 1 : 0;
            TriggerDic = new()
            {
                { SenderTag.UseDiceFromSwitch,new PersistentDiceCostModifier<UseDiceFromSwitchSender>(
                    (me,p,s,v)=>me.TeamIndex==s.TeamID,0,1,true,(me,p,s)=>p.Data=114)
                },
                { SenderTag.AfterSwitch, (me, p, s, v) =>
                {
                    if (p.Data!=null)
                    {
                        if (talent)
                        {
                            me.AddPersistent(new Effect_Venti_T());
                        }
                        p.Data=null;
                    }
                }
                }
            };
        }
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic { get; }
    }
    public class Summon_Venti : AbstractCardPersistentSummon
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>
            {
                me.Enemy.Hurt(new(7,2),this);
                //TODO:染色
            } 
            }
        };
    }
    public class Effect_Venti_T : AbstractCardPersistent
    {
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.UseDiceFromSkill,new PersistentDiceCostModifier<UseDiceFromSkillSender>(
                (me,p,s,v)=>me.TeamIndex==s.TeamID && s.Skill.Category==SkillCategory.A,-1,1)
            },
            { SenderTag.RoundStep,(me,p,s,v)=>p.Active=false}
        };
    }

    public class Talent_Venti : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 1;

        public override string CharacterNameID => "venti";

        public override CostInit Cost => new CostCreate().Anemo(3).ToCostInit();
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.Hurt(new(7, 2), c.Card.Skills[1], () => me.AddPersistent(new Effect_Venti(true)));
        }
    }
}
