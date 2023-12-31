﻿using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Policlinic_EF.Model;


class Program
{
    static void Main()
    {
        using (var context = new ApplicationContext())
        {

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            var weekdays = new[]
            {
                new WeekDay { WeekDayName = "Monday" },
                new WeekDay { WeekDayName = "Tuesday" },
                new WeekDay { WeekDayName = "Wednesday" },
                new WeekDay { WeekDayName = "Thursday" },
                new WeekDay { WeekDayName = "Friday" }
            };

            context.WeekDayList.AddRange(weekdays);
            context.SaveChanges();


            var doctor = new Doctor
            {
                DoctorName = "Саня",
                CabinetNum = 101
            };


            context.Doctors.Add(doctor);
            context.SaveChanges();


            var doctorSchedule = new DoctorShedule
            {
                WeekDay = weekdays.First(),
                Doctor = doctor,
                WorkTime = new Pair(new TimeOnly(10, 00), new TimeOnly(16, 00))
            };

            context.DoctorShedules.Add(doctorSchedule);
            context.SaveChanges();


            var patient = new Patient
            {
                Name = "Андрей"
            };

            context.Patients.Add(patient);
            context.SaveChanges();




            var pairForAppointment = new Pair(new TimeOnly(11, 00), new TimeOnly(11, 30));
            context.Add(pairForAppointment);
            context.SaveChanges();



            var appointment = new Appointment
            {
                Patient = patient,
                Doctor = doctor,
                AppointmentDate = new DateOnly(2023, 1, 2),
                AppointmentTime = new Pair(new TimeOnly(11, 00), new TimeOnly(11, 30))
            };

            context.Appointments.Add(appointment);
            context.SaveChanges();

        }
    }
}