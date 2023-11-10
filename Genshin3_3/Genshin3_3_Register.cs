using TCGBase;

namespace Genshin3_3
{
    public class Genshin3_3_Register : AbstractRegister
    {
        public override void RegisterActionCard(IRegistryConsumer<AbstractCardAction> consumer)
        {
            #region eqp.weapon
            consumer.Accept(new TrivalWeapon(WeaponCategory.Sword));
            consumer.Accept(new TrivalWeapon(WeaponCategory.Claymore));
            consumer.Accept(new TrivalWeapon(WeaponCategory.Longweapon));
            consumer.Accept(new TrivalWeapon(WeaponCategory.Catalyst));
            consumer.Accept(new TrivalWeapon(WeaponCategory.Bow));
            consumer.Accept(new SacrificialWeapon(WeaponCategory.Sword));
            consumer.Accept(new SacrificialWeapon(WeaponCategory.Claymore));
            consumer.Accept(new SacrificialWeapon(WeaponCategory.Catalyst));
            consumer.Accept(new SacrificialWeapon(WeaponCategory.Bow));
            //注：天空武器并不都在3.3版本加入，只是为了方便才统一加入
            consumer.Accept(new SkywardWeapon(WeaponCategory.Sword));
            consumer.Accept(new SkywardWeapon(WeaponCategory.Claymore));
            consumer.Accept(new SkywardWeapon(WeaponCategory.Longweapon));
            consumer.Accept(new SkywardWeapon(WeaponCategory.Catalyst));
            consumer.Accept(new SkywardWeapon(WeaponCategory.Bow));
            consumer.Accept(new WolfGravestoneWeapon(WeaponCategory.Claymore));
            consumer.Accept(new LithicWeapon(WeaponCategory.Longweapon));
            consumer.Accept(new AquilaFavoniaWeapon(WeaponCategory.Sword));
            #endregion

            #region eqp.artifact
            consumer.Accept(new AdventurerBandana());
            consumer.Accept(new LuckyDogSilverCirclet());
            consumer.Accept(new ExileCirclet());
            consumer.Accept(new TravelingDoctorHandkerchief());
            consumer.Accept(new InstructorCap());
            consumer.Accept(new GamblerEarrings());
            consumer.Accept(new ElementArtifact(1, false));
            consumer.Accept(new ElementArtifact(2, false));
            consumer.Accept(new ElementArtifact(3, false));
            consumer.Accept(new ElementArtifact(4, false));
            consumer.Accept(new ElementArtifact(5, false));
            consumer.Accept(new ElementArtifact(6, false));
            consumer.Accept(new ElementArtifact(7, false));
            consumer.Accept(new ElementArtifact(1, true));
            consumer.Accept(new ElementArtifact(2, true));
            consumer.Accept(new ElementArtifact(3, true));
            consumer.Accept(new ElementArtifact(4, true));
            consumer.Accept(new ElementArtifact(5, true));
            consumer.Accept(new ElementArtifact(6, true));
            consumer.Accept(new ElementArtifact(7, true));
            #endregion
            #region spt.partner
            consumer.Accept(new Paimon());
            consumer.Accept(new Timmie());

            consumer.Accept(new LiuSu());
            consumer.Accept(new Liben());
            consumer.Accept(new ChangTheNinth());
            #endregion
            #region spt.location
            consumer.Accept(new DawnWinery());
            consumer.Accept(new FavoniusCathedral());
            consumer.Accept(new KnightsOfFavoniusLibrary());

            consumer.Accept(new LiyueHarbor());
            consumer.Accept(new JadeChamber());
            consumer.Accept(new WangshuInn());
            #endregion
            #region spt.item
            consumer.Accept(new NRE());
            consumer.Accept(new ParametricTransfomer());
            #endregion

            #region evt.food
            consumer.Accept(new SweetChicken());
            consumer.Accept(new MondstadtHashBrown());
            #endregion
            #region evt.utils
            consumer.Accept(new BestCompanion());
            consumer.Accept(new ChangingShifts());
            consumer.Accept(new LeaveItToMe());
            consumer.Accept(new QuickKnit());
            consumer.Accept(new SendOff());
            consumer.Accept(new Starsigns());
            consumer.Accept(new Strategize());
            consumer.Accept(new WhenTheCraneReturned());
            consumer.Accept(new Dice(1));
            consumer.Accept(new Dice(2));
            consumer.Accept(new Dice(3));
            consumer.Accept(new Dice(4));
            consumer.Accept(new Dice(5));
            consumer.Accept(new Dice(6));
            consumer.Accept(new Dice(7));
            #endregion
            RegisterTalentCard(consumer);
        }

        public override void RegisterCharacter(IRegistryConsumer<AbstractCardCharacter> consumer)
        {
            consumer.Accept(new Kaeya());
            consumer.Accept(new Diona());
            consumer.Accept(new Chongyun());
            consumer.Accept(new Ganyu());
            consumer.Accept(new Qiqi());
            consumer.Accept(new Ayaka());

            consumer.Accept(new Mona());
            consumer.Accept(new 纯水());

            consumer.Accept(new Bennett());
            consumer.Accept(new XiangLing());
            consumer.Accept(new Yoimiya());
            consumer.Accept(new 讨债哥());

            consumer.Accept(new Fischl());
            consumer.Accept(new Keqing());
            consumer.Accept(new YaeMiko());

            consumer.Accept(new NingGuang());
            consumer.Accept(new 丘丘岩盔王());
            consumer.Accept(new Noel());

            consumer.Accept(new Collei());
            consumer.Accept(new Nahida());

            consumer.Accept(new Sucrose());
        }
        private static void RegisterTalentCard(IRegistryConsumer<AbstractCardAction> consumer)
        {
            consumer.Accept(new Talent_Kaeya());
            consumer.Accept(new Talent_Diona());
            consumer.Accept(new Talent_Chongyun());
            consumer.Accept(new Talent_Ganyu());
            consumer.Accept(new Talent_Ayaka());

            consumer.Accept(new Talent_纯水());

            consumer.Accept(new Talent_Bennett());
            consumer.Accept(new Talent_XiangLing());
            consumer.Accept(new Talent_Yoimiya());
            consumer.Accept(new Talent_讨债哥());

            consumer.Accept(new Talent_Fischl());

            consumer.Accept(new Talent_Ningguang());
            consumer.Accept(new Talent_Noel());
            consumer.Accept(new Talent_丘丘岩盔王());

            consumer.Accept(new Talent_Collei());
        }
    }
}
