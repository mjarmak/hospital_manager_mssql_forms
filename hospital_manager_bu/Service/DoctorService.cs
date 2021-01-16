using hospital_manager_bl.Util;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using hospital_manager_models.Models;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable All

namespace hospital_manager_bl.Service
{
    public class DoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ModelConverter modelConverter;
        private readonly OAuthService oAuthService;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            modelConverter = new ModelConverter(_unitOfWork);
            oAuthService = new OAuthService();
        }

        public DoctorResponse GetDoctor(string username)
        {
            DoctorData doctorData = _unitOfWork.Doctor.GetDoctor(username);
            return modelConverter.ResponseOf(doctorData);
        }

        public List<DoctorResponse> GetDoctors()
        {
            return _unitOfWork.Doctor.GetDoctors()?.Select(doctor => modelConverter.ResponseOf(doctor)).ToList();
        }
        public List<DoctorResponse> GetDoctorsByHospitalId(long hospitalId)
        {
            List<DoctorData> doctorData = _unitOfWork.Doctor.GetDoctorsByHospitalId(hospitalId);
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
        public DoctorResponse RegisterDoctor(UserAccountRequest userAccountRequest, string token)
        {
            string username = oAuthService.RegisterUser(userAccountRequest, token);
            var url = "https://localhost:44321/register/doctor";
            userAccountRequest.DoctorRequest.Username = username;
            userAccountRequest.DoctorRequest.Name = userAccountRequest.Name + " " + userAccountRequest.Surname;

            var doctorData = modelConverter.EnvelopeOf(userAccountRequest.DoctorRequest);
            _unitOfWork.Doctor.Add(doctorData);
            _unitOfWork.Save();

            var doctorResponse = modelConverter.ResponseOf(_unitOfWork.Doctor.GetDoctor(doctorData.Username));
            return doctorResponse;
        }
    }
}
