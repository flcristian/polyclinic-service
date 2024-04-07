using polyclinic_service.Users.DTOs;
using polyclinic_service.Users.Models;

namespace polyclinic_service_api_tests.Users.Helpers;

public static class TestsUsersHelper
{
    public static List<User> CreateTestUsers(int count)
    {
        List<User> users = new List<User>();

        for (int i = 1; i <= count; i++)
        {
            users.Add(i % 2 == 0 ? CreateTestDoctor(i) : CreateTestPatient(i));
        }

        return users;
    }
    
    public static List<User> CreateTestDoctors(int count)
    {
        List<User> users = new List<User>();

        for (int i = 1; i <= count; i++)
        {
            users.Add(CreateTestDoctor(i));
        }

        return users;
    }
    
    public static List<User> CreateTestPatients(int count)
    {
        List<User> users = new List<User>();

        for (int i = 1; i <= count; i++)
        {
            users.Add(CreateTestPatient(i));
        }

        return users;
    }
    
    public static User CreateTestPatient(int id)
    {
        return new User
        {
            Id = id,
            Age = 20,
            Gender = "Male",
            Email = $"user{id}@email.com",
            Name = $"Name {id}",
            Password = "password",
            Phone = "PHONE_NUMBER",
            Type = UserType.Patient
        };
    }
    
    public static User CreateTestDoctor(int id)
    {
        return new User
        {
            Id = id,
            Age = 20,
            Gender = "Male",
            Email = $"user{id}@email.com",
            Name = $"Name {id}",
            Password = "password",
            Phone = "PHONE_NUMBER",
            Type = UserType.Doctor
        };
    }

    public static CreateUserRequest CreateTestCreateUserRequest(int id)
    {
        return new CreateUserRequest
        {
            Age = 20,
            Gender = "Male",
            Email = $"user{id}@email.com",
            Name = "Name",
            Password = "password",
            Phone = "PHONE_NUMBER",
            Type = UserType.Doctor
        };
    }
    
    public static UpdateUserRequest CreateTestUpdateUserRequest(int id)
    {
        return new UpdateUserRequest
        {
            Id = id,
            Age = 20,
            Gender = "Male",
            Email = $"user{id}@email.com",
            Name = "Name",
            Password = "password",
            Phone = "PHONE_NUMBER",
            Type = UserType.Doctor
        };
    }

    public static User CreateTestUserFromCreateRequest(int id, CreateUserRequest request)
    {
        return new User
        {
            Id = id,
            Age = request.Age,
            Gender = request.Gender,
            Email = request.Email,
            Name = request.Name,
            Phone = request.Phone,
            Password = request.Password,
            Type = request.Type
        };
    }
    
    public static User CreateTestUserFromUpdateRequest(UpdateUserRequest request)
    {
        return new User
        {
            Id = request.Id,
            Age = request.Age,
            Gender = request.Gender,
            Email = request.Email,
            Name = request.Name,
            Phone = request.Phone,
            Password = request.Password,
            Type = request.Type
        };
    }
}