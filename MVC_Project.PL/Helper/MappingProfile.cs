using AutoMapper;
using MVC_Project.DAL.Models;
using MVC_Project.PL.View_Models;

namespace MVC_Project.PL.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap().ForMember(d=>d.Name,o=>o.MapFrom(m=>m.Name));
        }
    }
}
