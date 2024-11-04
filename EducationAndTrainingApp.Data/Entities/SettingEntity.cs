using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAndTrainingApp.Data.Entities
{
    //Projenin bakımda olup olmadığı bilgisini barındıran class

    public class SettingEntity : BaseEntity
    {
        public bool MaintenenceMode { get; set; }

    }
}
