using Microsoft.AspNetCore.Mvc;
using Okul_Proje.Models;
using Okul_Proje.Services;
using System;

namespace Okul_Proje.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public StudentsController(ApplicationDbContext context, IWebHostEnvironment environment) 
        {
            this.context = context; 
        }
        public IActionResult Index()
        {
            var Students = context.Students.ToList();
            return View(Students);
        }
        [HttpGet]
        [HttpPost]
        public IActionResult Create(StudentDto studentDto)
        {
            if (studentDto.ImageFile == null)
            {
                ModelState.AddModelError("Imagefile", "The image file is required");
            }
            if (!ModelState.IsValid)
            {
                return View(studentDto);
            }


            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(studentDto.ImageFile!.FileName);

            string imageFullPath = "\\Users\\Fatih Enes Savcılı\\source\\repos\\Okul_Proje\\Okul_Proje\\wwwroot\\students\\" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))

            {

                studentDto.ImageFile.CopyTo(stream);

            }
            Student student = new Student()
            {
                Name = studentDto.Name,
                Surname = studentDto.Surname,
                tcNumber = studentDto.tcNumber,
                Number = studentDto.Number,
                classroom = studentDto.classroom,
                ImageFileName = newFileName,
            };

            context.Students.Add(student);
            context.SaveChanges();

            return RedirectToAction("Index", "Students");

        }
        public IActionResult Edit(int id)
        {
            var student = context.Students.Find(id);
            if (student == null)
            {
                return RedirectToAction("Index", "Students");
            }
            var studentDto = new StudentDto()
            {
                Name = student.Name,
                Surname = student.Surname,
                tcNumber = student.tcNumber,
                Number = student.Number,
                classroom = student.classroom,


            };

            context.Students.Update(student);
            context.SaveChanges();
            ViewData["StudentId"] = student.Id;
            ViewData["ImageFileName"] = student.ImageFileName;
            return View(studentDto);
        }
        public IActionResult Delete(int id)
        {
            var student = context.Students.Find(id);
            if (student == null)
            {
                return RedirectToAction("Index", "Students");
            }

            string imageFullPath = "\\Users\\Fatih Enes Savcılı\\source\\repos\\Okul_Proje\\Okul_Proje\\wwwroot\\students\\" + student.ImageFileName;
            System.IO.File.Delete(imageFullPath);

            context.Students.Remove(student);
            context.SaveChanges(true);
            return RedirectToAction("Index", "Students");
        }

    }
}
