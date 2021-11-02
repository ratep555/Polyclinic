using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class EditDoctor1Dto
    {
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }     

        public string Resume { get; set; }


        public List<int> SpecializationsIds { get; set; }
    }
}