using Donquixote.Models.DataStructure.EnumModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donquixote.Models.DataStructures.DataModels
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
