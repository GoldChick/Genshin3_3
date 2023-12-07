using TCGBase;

namespace Genshin4_0
{
    public class Genshin_4_0_Util : AbstractModUtil
    {
        public override string NameSpace => "genshin3_8";

        public override string Description => "just 3 character + 5 card for 3.8 update";

        public override string Author => "Gold_Chick";

        public override string[] GetDependencies() => Array.Empty<string>();

        public override AbstractRegister GetRegister() => new Genshin4_0_Register();
    }
}
