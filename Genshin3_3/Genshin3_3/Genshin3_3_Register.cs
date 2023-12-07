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
            consumer.Accept(new Partner_Ayuan());
            consumer.Accept(new Partner_ChangTheNinth());
            consumer.Accept(new Partner_ChefMao());
            consumer.Accept(new Partner_Ellin());
            consumer.Accept(new Partner_IronTongueTian());
            consumer.Accept(new Partner_Katheryne());
            consumer.Accept(new Partner_Liben());
            consumer.Accept(new Partner_LiuSu());
            consumer.Accept(new Partner_Paimon());
            consumer.Accept(new Partner_Timaeus());
            consumer.Accept(new Partner_Timmie());
            consumer.Accept(new Partner_Wagner());
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
            consumer.Accept(new Food_GodUseVPN());
            consumer.Accept(new Food_Jueyunguoba());
            consumer.Accept(new Food_LotusFlowerCrisp());
            consumer.Accept(new Food_MintyMeatRolls());
            consumer.Accept(new Food_MondstadtHashBrown());
            consumer.Accept(new Food_MushroomPizza());
            consumer.Accept(new Food_NorthernSmokedChicken());
            consumer.Accept(new Food_SweetChicken());
            #endregion
            #region evt.utils
            consumer.Accept(new Event_BestCompanion());
            consumer.Accept(new Event_Blessing());
            consumer.Accept(new Event_CalxArts());
            consumer.Accept(new Event_ChangingShifts());
            consumer.Accept(new Event_GuardianOath());
            consumer.Accept(new Event_IHaventLost());
            consumer.Accept(new Event_LeaveItToMe());
            consumer.Accept(new Event_MasterOfWeapon());
            consumer.Accept(new Event_QuickKnit());
            consumer.Accept(new Event_SendOff());
            consumer.Accept(new Event_Starsigns());
            consumer.Accept(new Event_Strategize());
            consumer.Accept(new Event_WhenTheCraneReturned());
            consumer.Accept(new Dice(1));
            consumer.Accept(new Dice(2));
            consumer.Accept(new Dice(3));
            consumer.Accept(new Dice(4));
            consumer.Accept(new Dice(5));
            consumer.Accept(new Dice(6));
            consumer.Accept(new Dice(7));
            consumer.Accept(new ResonanceCryo());
            consumer.Accept(new ResonanceHydro());
            consumer.Accept(new ResonancePyro());
            consumer.Accept(new ResonanceElectro());
            consumer.Accept(new ResonanceGeo());
            consumer.Accept(new ResonanceDendro());
            consumer.Accept(new ResonanceAnemo());
            #endregion
            RegisterTalentCard(consumer);
        }

        public override void RegisterCharacter(IRegistryConsumer<AbstractCardCharacter> consumer)
        {
            consumer.Accept(new Kaeya());
            consumer.Accept(new Diona());
            consumer.Accept(new Chongyun());
            consumer.Accept(new Ganyu());
            consumer.Accept(new Ayaka());

            consumer.Accept(new Mona());
            consumer.Accept(new Barbara());
            consumer.Accept(new Xingqiu());
            consumer.Accept(new 纯水());
            consumer.Accept(new Maiden());

            consumer.Accept(new Bennett());
            consumer.Accept(new Diluc());
            consumer.Accept(new Xiangling());
            consumer.Accept(new Yoimiya());
            consumer.Accept(new 讨债哥());

            consumer.Accept(new Fischl());
            consumer.Accept(new Razor());
            consumer.Accept(new Keqing());
            consumer.Accept(new Cyno());

            consumer.Accept(new Ningguang());
            consumer.Accept(new 丘丘岩盔王());
            consumer.Accept(new Noel());

            consumer.Accept(new Collei());
            consumer.Accept(new Kunkun());

            consumer.Accept(new Sucrose());
            consumer.Accept(new Jean());
            consumer.Accept(new MaguuKenki());
        }
        private static void RegisterTalentCard(IRegistryConsumer<AbstractCardAction> consumer)
        {
            consumer.Accept(new Talent_Kaeya());
            consumer.Accept(new Talent_Diona());
            consumer.Accept(new Talent_Chongyun());
            consumer.Accept(new Talent_Ganyu());
            consumer.Accept(new Talent_Ayaka());

            consumer.Accept(new Talent_Mona());
            consumer.Accept(new Talent_Barbara());
            consumer.Accept(new Talent_Xingqiu());
            consumer.Accept(new Talent_纯水());
            consumer.Accept(new Talent_Maiden());

            consumer.Accept(new Talent_Bennett());
            consumer.Accept(new Talent_Diluc());
            consumer.Accept(new Talent_XiangLing());
            consumer.Accept(new Talent_Yoimiya());
            consumer.Accept(new Talent_Debt());

            consumer.Accept(new Talent_Fischl());
            consumer.Accept(new Talent_Razor());
            consumer.Accept(new Talent_Keqing());
            consumer.Accept(new Talent_Keqing_Special());
            consumer.Accept(new Talent_Cyno());

            consumer.Accept(new Talent_Ningguang());
            consumer.Accept(new Talent_Noel());
            consumer.Accept(new Talent_QQ());

            consumer.Accept(new Talent_Collei());
            consumer.Accept(new Talent_Kunkun());

            consumer.Accept(new Talent_Sucrose());
            consumer.Accept(new Talent_Jean());
            consumer.Accept(new Talent_MaguuKenki());
        }
    }
}
