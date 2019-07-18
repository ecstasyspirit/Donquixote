using Donquixote.Models.DataStructuresModels.EnumModels;

namespace Donquixote.Models.DataStructuresModels.DataModels
{
    public class AttackResultDataModel
    {
        public ModeEnumModel AttackMode { get; set; }
        public SpeedEnumModel AttackSpeed { get; set; }

        public int PhonesLoaded { get; set; }
        public int PhonesMessaged { get; set; }
        public int PhoneSkiped { get; set; }
    }
}