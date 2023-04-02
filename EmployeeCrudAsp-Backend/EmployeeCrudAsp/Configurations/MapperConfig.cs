using AutoMapper;
using EmployeeCrudAsp.Data;
using EmployeeCrudAsp.Models.Department;
using EmployeeCrudAsp.Models.Employee;

namespace EmployeeCrudAsp.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<DepartmentCreateDto, Department>().ReverseMap();
            CreateMap<DepartmentUpdateDto, Department>().ReverseMap();
            CreateMap<DepartmentReadOnlyDto, Department>().ReverseMap();

            CreateMap<EmployeeCreateDto, Employee>().ReverseMap();
            CreateMap<EmployeeUpdateDto, Employee>().ReverseMap();
            CreateMap<Employee, EmployeeReadOnlyDto>()
                .ForMember(q => q.DepartmentName, d => d.MapFrom(map => $"{map.Department.DepartmentName}"))
                .ReverseMap();
            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(a => a.DepartmentName, b => b.MapFrom(map => $"{map.Department.DepartmentName}"))
                .ReverseMap();
        }
    }
}
