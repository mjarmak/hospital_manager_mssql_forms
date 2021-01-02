using hospital_manager_bl.Util;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace hospital_manager_bl.Service
{
    public class DoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ModelConverter modelConverter;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            modelConverter = new ModelConverter(_unitOfWork);
        }

        public DoctorResponse GetDoctor(string username)
        {
            DoctorData doctorData = _unitOfWork.Doctor.GetDoctor(username);
            return modelConverter.ResponseOf(doctorData);
        }

        public List<DoctorResponse> GetDoctors()
        {
            List<DoctorData> doctorData = _unitOfWork.Doctor.GetDoctors();
            return doctorData?.Select(doctor => modelConverter.ResponseOf(doctor)).ToList();
        }

        public DoctorResponse SaveDoctor(DoctorRequest doctorRequest)
        {
            var doctorData = modelConverter.EnvelopeOf(doctorRequest);
            _unitOfWork.Doctor.Add(doctorData);
            _unitOfWork.Save();

            var doctorResponse = modelConverter.ResponseOf(_unitOfWork.Doctor.GetDoctor(doctorData.Username));
            return doctorResponse;
        }
    }
}
