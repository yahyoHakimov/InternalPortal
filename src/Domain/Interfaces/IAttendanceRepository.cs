using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetAllAttendances();
        Task AddAttendance(Attendance attendance);
        Task<Attendance> GetAttendanceById(int id);
    }

}
