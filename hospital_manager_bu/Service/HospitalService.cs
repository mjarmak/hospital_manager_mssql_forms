using hospital_manager_bl.Util;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using hospital_manager_exceptions.Exceptions;
using hospital_manager_models.Models;
using System.Collections.Generic;

namespace hospital_manager_bl.Service
{
    public class HospitalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ModelConverter modelConverter;

        public HospitalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            modelConverter = new ModelConverter(_unitOfWork);
        }

        public HospitalData GetHospital(long id)
        {
            return _unitOfWork.Hospital.GetHospital(id);
        }

        public IEnumerable<HospitalData> GetHospitals()
        {
            return _unitOfWork.Hospital.GetHospitals();
        }

        public HospitalData SaveHospital(HospitalRequest hospital)
        {
            var hospitalData = modelConverter.EnvelopeOf(hospital);
            _unitOfWork.Hospital.Add(hospitalData);
            _unitOfWork.Save();

            return _unitOfWork.Hospital.Get(hospitalData.Id);
        }
    }
}
