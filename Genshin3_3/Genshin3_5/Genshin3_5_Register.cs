using TCGBase;

namespace Genshin3_5
{
    public class Genshin3_5_Register : AbstractRegister
    {
        public override void RegisterActionCard(IRegistryConsumer<AbstractCardAction> consumer)
        {
            consumer.Accept(new Artifact_OrnateKabuto());
            consumer.Accept(new Artifact_GeneralAncientHelm());

            consumer.Accept(new Talent_Eula());
            consumer.Accept(new Talent_Kokomi());
            consumer.Accept(new Talent_KojouSara());
        }

        public override void RegisterCharacter(IRegistryConsumer<AbstractCardCharacter> consumer)
        {
            consumer.Accept(new Eula());
            consumer.Accept(new Kokomi());
            consumer.Accept(new KojouSara());
        }
    }
}
