using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Helpers;

namespace Core.Dtos
{
    public class EditDoctor1Dto
    {
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }     

        public string Resume { get; set; }
        public IFormFile Picture { get; set; }
        
        
        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> SpecializationsIds { get; set; }
    }
}