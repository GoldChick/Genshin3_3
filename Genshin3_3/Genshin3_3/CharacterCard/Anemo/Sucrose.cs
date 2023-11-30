using TCGBase;
namespace Genshin3_3
{
    public class Sucrose : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[] {
        new CharacterSimpleA(7,1),
        new 风灵作成6308(),
        new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Anemo(3).MP(2).ToCostInit(),
            (skill,me,c,args)=>me.AddSummon(new Summon_Sucrose()),new DamageVariable(7,1))
        };

        public override string NameID => "sucrose";

        public override ElementCategory CharacterElement => ElementCategory.Anemo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Catalyst;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;

        private class 风灵作成6308 : AbstractCardSkill
        {
            public override CostInit Cost => new CostCreate().Anemo(3).ToCostInit();

            public override SkillCategory Category => SkillCategory.E;

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.Hurt(new DamageVariable(7, 3, 0), this, me.Enemy.SwitchToLast);
            }
        }
    }
    public class Summon_Sucrose : AbstractCardPersistentSummon
    {
        private readonly bool _talent;
        public Summon_Sucrose(bool talent = false)
        {
            _talent = talent;
            Variant = _talent ? 1 : 0;
        }

        public override int MaxUseTimes => 3;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>
            {
                me.Enemy.Hurt(new(p.Data==null? 7: (int)p.Data,2),this);
            }
            },
            { SenderTag.AfterHurt,(me,p,s,v)=>
            {
                if (p.Data==null && s is NoDamageHurtSender hs && hs.Reaction==ReactionTags.Swirl)
                {
                    p.Data=hs.InitialElement;
                    Variant+=10*hs.InitialElement;
                    //TODO:染色
                }
            }
            }
        };
        public override void Update<T>(PlayerTeam me, Persistent<T> persistent)
        {
            base.Update(me, persistent);
            Variant = _talent ? 1 : 0;
        }
    }
    public class Talent_Sucrose : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 2;

        public override string CharacterNameID => "sucrose";

        public override CostInit Cost => new CostCreate().Anemo(3).MP(2).ToCostInit();
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.Hurt(new(7, 1), c.Card.Skills[2], () => me.AddSummon(new Summon_Sucrose(true)));
        }
    }
}
