using TCGBase;

namespace Genshin3_3
{
    public class Xiangling : AbstractCardCharacter
    {
        public static readonly AbstractCardPersistentSummon Summon_Xiangling = new SimpleSummon("summon_xiangling", 3, 2, 2);
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
        new CharacterSimpleA(0,2,3),
        new CharacterSingleSummonE(Summon_Xiangling,3),
        new CharacterEffectQ(3,3,new Effect_Xiangling(),false,3,4)
        };

        public override string NameID => "xiangling";

        public override ElementCategory CharacterElement => ElementCategory.Pyro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Longweapon;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;
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

        public override int[] Costs => new int[] { 0, 0, 0, 3 };

        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.Hurt(new(3, 1), c.Card.Skills[1], () => base.TalentTriggerAction(me, c, targetArgs));
        }
    }
}
