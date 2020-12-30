using hospital_manager_bl.Util;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using hospital_manager_exceptions.Exceptions;
using hospital_manager_models.Models;
using System.Collections.Generic;
using System.Linq;

namespace hospital_manager_bl.Service
{
    public class SpecialityLinkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ModelConverter modelConverter;

        public SpecialityLinkService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            modelConverter = new ModelConverter(_unitOfWork);
        }

        //public void SaveSpecialitiesForDoctor(string doctorUsername, List<long> specialityIds)
        //{
        //    specialityIds.ForEach(id => _unitOfWork.SpecialityToDoctor.Add(new SpecialityToDoctorData(doctorUsername, id)));
        //}

        //public void SaveSpecialitiesForRoom(long roomId, List<long> specialityIds)
        //{
        //    specialityIds.ForEach(id => _unitOfWork.SpecialityToRoom.Add(new SpecialityToRoomData(roomId, id)));
        //}

    }
}
