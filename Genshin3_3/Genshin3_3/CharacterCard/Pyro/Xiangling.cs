using TCGBase;

namespace Genshin3_3
{
    public class Xiangling : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Pyro(1).ToCostInit(),new DamageVariable(0,2)),
            new CharacterSingleSummonE(new Summon_Xiangling(),3),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Pyro(4).MP(2).ToCostInit(),
                (skill,me,c,args)=>me.AddPersistent(new Effect_Xiangling()),new DamageVariable(3,3)),
        };

        public override string NameID => "xiangling";

        public override ElementCategory CharacterElement => ElementCategory.Pyro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Longweapon;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;
    }
    public class Summon_Xiangling : AbstractSimpleSummon
    {
        public Summon_Xiangling() : base(3, 2, 2)
        {
        }
    }

    public class Effect_Xiangling : AbstractCardPersistent
    {
        public override int MaxUseTimes => 2;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.AfterUseSkill.ToString(),(me, p, s, v) =>
                {
                    //自己不触发火轮
                    if(p.Data!=null&&me.TeamIndex==s.TeamID && s is AfterUseSkillSender ski )
                    {
                        me.Enemy.Hurt(new DamageVariable(3, 2, 0), this);
                        p.AvailableTimes --;
                    }
                    p.Data=1;
                }
            }
        };
    }

    public class Talent_XiangLing : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "xiangling";

        public override int Skill => 1;

        public override CostInit Cost => new CostCreate().Pyro(3).ToCostInit();
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.Hurt(new(3, 1), c.Card.Skills[1], () => base.TalentTriggerAction(me, c, targetArgs));
        }
    }
}
