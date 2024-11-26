using System.Text.RegularExpressions;
using AutoMapper;
using WebAPI.Migrations;

namespace WebAPI.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Student -> StudentViewModel
        CreateMap<student, StudentViewModel>()
            .ForMember(dest => dest.full_name,
                opt => opt.MapFrom(src => $"{src.first_name} {src.last_name}"))
            .ForMember(dest => dest.group_id, opt => opt.MapFrom(src => src.group_id))
            .ReverseMap(); // Зворотній мапінг

        // Course -> CourseViewModel
        CreateMap<course, CourseViewModel>().ReverseMap();

        // Group -> GroupViewModel
        CreateMap<group, GroupViewModel>().ReverseMap();

        // Specialty -> SpecialtyViewModel
        CreateMap<specialty, SpecialtyViewModel>().ReverseMap();

        // Enrollment -> EnrollmentViewModel
        CreateMap<enrollment, EnrollmentViewModel>().ReverseMap();

        // SpecialtyCourse -> SpecialtyCourseViewModel
        CreateMap<specialty_course, SpecialtyCourseViewModel>().ReverseMap();

        // AcademicYearPlan -> AcademicYearPlanViewModel
        CreateMap<academic_year_plan, AcademicYearPlanViewModel>().ReverseMap();

        // QRSession -> QRSessionViewModel
        CreateMap<qr_session, QRSessionViewModel>().ReverseMap();
    }
}