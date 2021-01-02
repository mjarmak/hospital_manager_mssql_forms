using hospital_manager_bl.Util;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using hospital_manager_exceptions.Exceptions;
using hospital_manager_models.Models;
using System.Collections.Generic;
using System.Linq;

namespace hospital_manager_bl.Service
{
    public class SpecialityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ModelConverter modelConverter;

        public SpecialityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            modelConverter = new ModelConverter(_unitOfWork);
        }

        public SpecialityResponse GetSpeciality(long id)
        {
            return modelConverter.ResponseOf(_unitOfWork.Speciality.Get(id));
        }

        public IEnumerable<SpecialityResponse> GetSpecialities()
        {
            return _unitOfWork.Speciality.All().ToList()?.Select(speciality => modelConverter.ResponseOf(speciality)).ToList();
        }

        public SpecialityData SaveSpeciality(SpecialityRequest specialityRequest)
        {

            if (SpecialityNameExists(specialityRequest.Name))
            {
                throw new InvalidSpeciality("A Speciality with name " + specialityRequest.Name + " already exists.");
            }

            var specialityData = modelConverter.EnvelopeOf(specialityRequest);
            _unitOfWork.Speciality.Add(specialityData);
            _unitOfWork.Save();

            return _unitOfWork.Speciality.Get(specialityData.Id);
        }

        private bool SpecialityNameExists(string name)
        {
            return _unitOfWork.Speciality.GetSpecialityByName(name) != null;
        }
    }
}
