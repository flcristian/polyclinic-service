using AutoMapper;
using polyclinic_service.Users.DTOs;
using polyclinic_service.Users.Models;

namespace polyclinic_service.System;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserRequest, User>();
        CreateMap<UpdateUserRequest, User>();
    }
}