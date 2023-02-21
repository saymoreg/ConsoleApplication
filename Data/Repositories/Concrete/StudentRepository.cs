using System;
using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;

namespace Data.Repositories.Concrete
{
    public class StudentRepository : IStudentRepository
    {
        static int id; // to use id in Add method

        public List<Student> GetAll()
        {
            return DbContext.Students;
        }

        public Student Get(int id)
        {
            return DbContext.Students.FirstOrDefault(s => s.Id == id);
        }

        public void Add(Student student)
        {
            id++;
            student.Id = id;
            DbContext.Students.Add(student);
        }

        public void Update(Student student)
        {
            var dbStudent = DbContext.Students.FirstOrDefault(s => s.Id == student.Id);
            if (dbStudent is not null)
            {
                dbStudent.Name = student.Name;
                dbStudent.Surname = student.Surname;
                dbStudent.Email = student.Email;
                dbStudent.BirthDate = student.BirthDate;
            }
        }

        public void Delete(Student student)
        {
            throw new NotImplementedException();
        }

        public bool IsDublicateEmail(string email)
        {
            return DbContext.Students.Any(s => s.Email == email);
        }
    }
}

