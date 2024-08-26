using Ligl.LegalManagement.Model.Query;
namespace Ligl.LegalManagement.Business.Query
{
    /// <summary>
    /// InterviewMapper Class
    /// </summary>
    public static class InterviewMapper
    {

        /// <summary>
        /// To Map InterviewModel to InterviewEntity
        /// </summary>
        /// <param name="interviewSource"></param>
        /// <param name="interviewDestination"></param>
        /// <param name="isAddMode"></param>
        /// <returns></returns>
        public static InterviewEntityViewModel InterviewEntityMapper(InterviewEntityViewModel interviewSource,
            InterviewEntityViewModel interviewDestination, bool isAddMode = false)
        {
            var entityID = interviewDestination.EntityID;
            var entityTypeID = interviewDestination.EntityTypeID;
            var currentDateTime = DateTime.UtcNow;
            interviewDestination.Interviewer = interviewSource.Interviewer;
            interviewDestination.InterviewDate = interviewSource.InterviewDate;
            interviewDestination.InterviewPlace = interviewSource.InterviewPlace;
            interviewDestination.Notes = interviewSource.Notes;
            interviewDestination.ModifiedOn = currentDateTime;
            interviewDestination.ModifiedBy = "";

            if (!isAddMode)
                return interviewDestination;

            interviewDestination.UUID = Guid.NewGuid();
            interviewDestination.EntityID = entityID;
            interviewDestination.EntityTypeID = entityTypeID;
            interviewDestination.CreatedBy = "";
            interviewDestination.CreatedOn = currentDateTime;
            //interviewDestination.Status = LookUpsBusinessLogic
            //    .GetlookUpInfo(
            //        lookUpId: interviewSource.StatusUniqueID == null || interviewSource.StatusUniqueID == Guid.Empty
            //            ? Status.Active.GetEnumValue()
            //            : interviewSource.StatusUniqueID).Id;
            interviewDestination.IsDeleted = false;

            return interviewDestination;
        }
    }
}
