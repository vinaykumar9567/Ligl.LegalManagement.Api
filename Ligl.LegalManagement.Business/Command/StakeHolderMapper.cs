using Ligl.LegalManagement.Model.Query;
using Ligl.LegalManagement.Repository.Domain;
using System.Reflection;
using StakeHolder = Ligl.LegalManagement.Repository.Domain.StakeHolder;
using StakeHolderEntity = Ligl.LegalManagement.Model.Command.StakeHolderEntity;


namespace Ligl.LegalManagement.Business.Command
{
    /// <summary>
    /// Mapper to StakeHolder and Case StakeHolder Object Mapping
    /// </summary>
    public static class StakeHolderMapper
    {
        /// <summary>
        ///     To Map Properties from  DB StakeHolder to Current Request Object
        /// </summary>
        /// <param name="destinationStakeHolderModel"></param>
        /// <param name="sourceStakeHolderModel"></param>
        /// <param name="defaultSettings"></param>
        public static StakeHolderModel MapStakeHolderEntity(StakeHolderModel destinationStakeHolderModel,StakeHolderModel? sourceStakeHolderModel = null, bool defaultSettings = false)
        {
            var currentDateTime = DateTime.UtcNow;
            destinationStakeHolderModel.ModifiedOn = currentDateTime;


            if (defaultSettings)
            {
                destinationStakeHolderModel.ID = destinationStakeHolderModel.UUID = Guid.NewGuid();
                destinationStakeHolderModel.CreatedOn = currentDateTime;
                destinationStakeHolderModel.IsDeleted = false;
            }

            if (sourceStakeHolderModel == null)
                return destinationStakeHolderModel;

            if (destinationStakeHolderModel.StatusUniqueID != sourceStakeHolderModel.StatusUniqueID)
                destinationStakeHolderModel.StatusChangeReason = sourceStakeHolderModel.StatusChangeReason;
            destinationStakeHolderModel.DepartmentUniqueID = sourceStakeHolderModel.DepartmentUniqueID;
            destinationStakeHolderModel.StatusUniqueID = sourceStakeHolderModel.StatusUniqueID;
            destinationStakeHolderModel.EmailAddress = sourceStakeHolderModel.EmailAddress;
            destinationStakeHolderModel.FirstName = sourceStakeHolderModel.FirstName;
            destinationStakeHolderModel.MiddleName = sourceStakeHolderModel.MiddleName;
            destinationStakeHolderModel.LastName = sourceStakeHolderModel.LastName;
            destinationStakeHolderModel.CategoryUniqueID = sourceStakeHolderModel.CategoryUniqueID;
            return destinationStakeHolderModel;
        }
        public static StakeHolderModel Mapper(StakeHolderEntity sourceStakeHolderModel, StakeHolderModel destinationStakeHolderModel)
        {

            destinationStakeHolderModel.EmailAddress = sourceStakeHolderModel.EmailAddress;
            destinationStakeHolderModel.FirstName = sourceStakeHolderModel.FirstName;
            destinationStakeHolderModel.MiddleName = sourceStakeHolderModel.MiddleName;
            destinationStakeHolderModel.LastName = sourceStakeHolderModel.LastName;
            return destinationStakeHolderModel;
        }


        public static CaseStakeHolderEntity Map(CaseStakeHolderModel model)
        {
            if (model == null)
            {
               
                throw new ArgumentNullException($"Error in {model} - user context session not found");
            }

            return new CaseStakeHolderEntity
            {
                CaseStakeHolderID = 0, // Assume auto-generated or handled elsewhere
                UUID = model.EntityUniqueID.GetValueOrDefault(),
                CaseID = 0, // Set based on additional logic if necessary
                StakeHolderID = model.StakeHolderModel != null ? model.StakeHolderModel.StakeHolderID : 0,
                DpnStatusID = model.EntityStatusUniqueID.GetHashCode(), // Example mapping; adjust as needed
                Status = model.EntityStatusUniqueID != Guid.Empty ? (int?)model.EntityStatusUniqueID.GetHashCode() : null,

            };
        }



        /// <summary>
        ///     Added Assignment of Case StakeHolders
        /// </summary>
        /// <param name="caseStakeHolderEntity"></param>
        /// <param name="caseID"></param>
        public static CaseStakeHolderEntity MapCaseStakeHolderEntity(CaseStakeHolderModel caseStakeHolderEntity, int caseID)
        {

            caseStakeHolderEntity.ID = caseStakeHolderEntity.UUID = (caseStakeHolderEntity.ID != null && caseStakeHolderEntity.ID != Guid.Empty)
                ? caseStakeHolderEntity.ID.GetValueOrDefault()
                : Guid.NewGuid();
            caseStakeHolderEntity.CaseID = caseID;
            return new CaseStakeHolderEntity
            {
                CaseID = caseStakeHolderEntity.CaseID
            };
        }
             

        public static StakeHolderEntity EntityMapper(StakeHolderEntity sourceStakeHolderModel,StakeHolderModel destinationStakeHolderModel)
        {

            destinationStakeHolderModel.EmailAddress = sourceStakeHolderModel.EmailAddress;
            destinationStakeHolderModel.FirstName = sourceStakeHolderModel.FirstName;
            destinationStakeHolderModel.MiddleName = sourceStakeHolderModel.MiddleName;
            destinationStakeHolderModel.LastName = sourceStakeHolderModel.LastName;
            return destinationStakeHolderModel;
        }


        public static CaseStakeHolder MapCaseStakeHolderEntitymapper(CaseStakeHolderModel caseStakeHolderEntity, int caseID)
        {

            caseStakeHolderEntity.ID = caseStakeHolderEntity.UUID = (caseStakeHolderEntity.ID != null && caseStakeHolderEntity.ID != Guid.Empty)
                ? caseStakeHolderEntity.ID.GetValueOrDefault()
                : Guid.NewGuid();
            caseStakeHolderEntity.CaseID = caseID;
        
            return GenericMapper.Map<CaseStakeHolderModel, CaseStakeHolder>(caseStakeHolderEntity);
            //return new CaseStakeHolder
            //{
            //    CaseId = caseStakeHolderEntity.CaseID
            //};
        }


        public static StakeHolderModel AutoMapStakeHolderEntity(StakeHolder destinationStakeHolderModel,StakeHolderModel? sourceStakeHolderModel = null, bool defaultSettings = false)
        {
            var currentDateTime = DateTime.UtcNow;
            if (defaultSettings)
            {
                destinationStakeHolderModel.UUID = destinationStakeHolderModel.UUID = Guid.NewGuid();
                destinationStakeHolderModel.CreatedOn = currentDateTime;

                destinationStakeHolderModel.IsDeleted = false;
                destinationStakeHolderModel.ModifiedOn = currentDateTime;
            }

            sourceStakeHolderModel.StatusChangeReason = destinationStakeHolderModel.StatusChangeReason;
            sourceStakeHolderModel.EmailAddress = destinationStakeHolderModel.EmailAddress;
            sourceStakeHolderModel.FirstName = destinationStakeHolderModel.FirstName;
            sourceStakeHolderModel.MiddleName = destinationStakeHolderModel.MiddleName;
            sourceStakeHolderModel.LastName = destinationStakeHolderModel.LastName;
            return sourceStakeHolderModel;
        }

        public static StakeHolderEntity AutoMapStakeHolderEntities(StakeHolderModel destinationStakeHolderModel, StakeHolderEntity? sourceStakeHolderModel = null, bool defaultSettings = false)
        {
            var currentDateTime = DateTime.UtcNow;
            if (defaultSettings)
            {
                destinationStakeHolderModel.ID = destinationStakeHolderModel.UUID = Guid.NewGuid();
                destinationStakeHolderModel.CreatedOn = currentDateTime;

                destinationStakeHolderModel.IsDeleted = false;
                destinationStakeHolderModel.ModifiedOn = currentDateTime;
            }

            sourceStakeHolderModel.StatusChangeReason = destinationStakeHolderModel.StatusChangeReason;
            sourceStakeHolderModel.EmailAddress = destinationStakeHolderModel.EmailAddress;
            sourceStakeHolderModel.FirstName = destinationStakeHolderModel.FirstName;
            sourceStakeHolderModel.MiddleName = destinationStakeHolderModel.MiddleName;
            sourceStakeHolderModel.LastName = destinationStakeHolderModel.LastName;
            return sourceStakeHolderModel;
        }

    }
}
