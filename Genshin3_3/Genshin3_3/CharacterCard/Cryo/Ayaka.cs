using TCGBase;


namespace Genshin3_3
{
    public class Ayaka : AbstractCardCharacter
    {
        public override int MaxMP => 3;
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[] {
            new CharacterSimpleA(0,2,1),
            new CharacterSimpleE(1,3),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Cryo(3).MP(3).ToCostInit(),
                (skill,me,c,args)=>me.AddSummon(new Summon_Ayaka()),new DamageVariable(1,4)),
            new Effect_Ayaka_Passive(),
        };

        public override ElementCategory CharacterElement => ElementCategory.Cryo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Sword;

        public override CharacterRegion CharacterRegion => CharacterRegion.INAZUMA;

        public override string NameID => "ayaka";
    }
    public class Effect_Ayaka_Passive : AbstractCardSkillPassive
    {
        public override bool TriggerOnce => false;
        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.AfterSwitch,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s is AfterSwitchSender ss &&  ss.Target==p.PersistentRegion)
                {
                    me.AddPersistent(new Effect_Ayaka(),p.PersistentRegion);
                }
            }
            }
        };

    }
    public class Summon_Ayaka : AbstractSimpleSummon
    {
        public Summon_Ayaka() : base(1, 2, 2)
        {
        }
    }

    public class Effect_Ayaka : AbstractCardPersistent
    {
        public Effect_Ayaka(bool talent = false)
        {
            Variant = talent ? 1 : 0;
            TriggerDic = new()
            {
                { SenderTag.ElementEnchant,new PersistentElementEnchant(1)},
            };
            if (talent)
            {
                TriggerDic.Add(SenderTag.DamageIncrease, (me, p, s, v) =>
                {
                    if (PersistentFunc.IsCurrCharacterDamage(me, p, s, v, out var dv))
                    {
                        if (dv.Element == 1)
                        {
                            dv.Damage++;
                        }
                    }
                }
                );
            }
        }
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic { get; }
    }

    public class Talent_Ayaka : AbstractCardEquipmentTalent
    {
        public override string CharacterNameID => "ayaka";
        public override CostInit Cost => new CostCreate().Cryo(2).ToCostInit();
        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes=1},
            { SenderTag.UseDiceFromSwitch,new PersistentDiceCostModifier<UseDiceFromSwitchSender>(
                (me,p,s,v)=>me.TeamIndex==s.TeamID && s.Target==p.PersistentRegion,0,1)},
            { SenderTag.AfterSwitch,(me,p,s,v)=>
            {
                if (s is AfterSwitchSender ss && s.TeamID == me.TeamIndex && ss.Target==p.PersistentRegion)
                {
                    //覆盖了
                    me.AddPersistent(new Effect_Ayaka(true), p.PersistentRegion);
                }
            }
            }
        };
    }
}
