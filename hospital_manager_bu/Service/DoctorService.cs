using hospital_manager_bl.Util;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using hospital_manager_exceptions.Exceptions;
using hospital_manager_models.Models;
using System.Collections.Generic;
using System.Linq;

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
            if (doctorData == null)
            {
                throw new NotFoundDoctor("Doctor with ID " + username + " does not exist.");
            }
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

        public DoctorResponse UpdateDoctorConsultations(string doctorUsername, List<ConsultationRequest> consultations)
        {
            var doctorData = _unitOfWork.Doctor.GetDoctor(doctorUsername);

            List<ConsultationData> consultationsData = modelConverter.EnvelopeOf(consultations);
            List<ConsultationData> consultationsDataFiltered = new List<ConsultationData>();

            for(int i = 0; i < consultationsData.Count; i++) {
                if (!HospitalExists(consultationsData[i].HospitalId) || !SpecialityExists(consultationsData[i].SpecialityId))
                {
                    consultationsData.Remove(consultationsData[i]);
                    i--;
                }
            }

            consultationsData.ForEach(consultation =>
            {
                if (doctorData.Consultations.SingleOrDefault(consultationDoctor =>
                consultationDoctor.HospitalId == consultation.HospitalId
                && consultationDoctor.Duration == consultation.Duration
                && consultationDoctor.SpecialityId == consultation.SpecialityId
                ) == null)
                {
                    consultationsDataFiltered.Add(consultation);
                }
            });

            doctorData.Consultations.AddRange(consultationsDataFiltered);

            _unitOfWork.Doctor.Update(doctorData);
            _unitOfWork.Save();

            var doctorResponse = modelConverter.ResponseOf(_unitOfWork.Doctor.GetDoctor(doctorData.Username));
            return doctorResponse;
        }

        public DoctorResponse RegisterDoctor(UserAccountRequest userAccountRequest, string token)
        {
            for (int i = 0; i < userAccountRequest.DoctorRequest.Consultations.Count; i++)
            {
                if (!HospitalExists(userAccountRequest.DoctorRequest.Consultations[i].HospitalId) || !SpecialityExists(userAccountRequest.DoctorRequest.Consultations[i].SpecialityId))
                {
                    userAccountRequest.DoctorRequest.Consultations.Remove(userAccountRequest.DoctorRequest.Consultations[i]);
                    i--;
                }
            }
            for (int i = 0; i < userAccountRequest.DoctorRequest.SpecialityIds.Count; i++)
            {
                if (!SpecialityExists(userAccountRequest.DoctorRequest.SpecialityIds[i]))
                {
                    userAccountRequest.DoctorRequest.SpecialityIds.Remove(userAccountRequest.DoctorRequest.SpecialityIds[i]);
                    i--;
                }
            }
            try
            {
                string username = oAuthService.RegisterUser(userAccountRequest, token);
                userAccountRequest.DoctorRequest.Username = username;
                userAccountRequest.DoctorRequest.Name = userAccountRequest.Name + " " + userAccountRequest.Surname;
            }
            catch (InvalidUserRequest e)
            {
                throw new InvalidUserRequest(e.Message);
            }

            var doctorData = modelConverter.EnvelopeOf(userAccountRequest.DoctorRequest);
            _unitOfWork.Doctor.Add(doctorData);
            _unitOfWork.Save();

            var doctorResponse = modelConverter.ResponseOf(_unitOfWork.Doctor.GetDoctor(doctorData.Username));
            return doctorResponse;
        }
        private bool HospitalExists(long id)
        {
            return _unitOfWork.Hospital.Get(id) != null;
        }
        private bool SpecialityExists(long id)
        {
            return _unitOfWork.Speciality.Get(id) != null;
        }
    }
}
