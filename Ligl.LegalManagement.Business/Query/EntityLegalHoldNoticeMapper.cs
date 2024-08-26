using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Business.Query
{
    /// <summary>
    /// Mapper For Entity Legal Hold Notice Table Operations
    /// </summary>
    public class EntityLegalHoldNoticeMapper
    {

        const string _className = nameof(EntityLegalHoldNoticeMapper);
    
        //private readonly ILogger<UpdateCaseLHEscalationDetailQueryHandler> _logger;

        //public EntityLegalHoldNoticeMapper(ILogger<UpdateCaseLHEscalationDetailQueryHandler> logger)
        //{
        //    _logger = logger;
        //}

        /// <summary>
        /// Notification mapper for entity legal hold notice
        /// </summary>
        /// <param name="EntityID"></param>
        /// <param name="EntityTypeID"></param>
        /// <param name="caseLegaHoldID"></param>
        /// <param name="lhnStatusID"></param>
        /// <returns></returns>
        public static  EntityLegalHoldNotice EntityLegalHoldNoticeFieldMapper(int EntityID, int EntityTypeID, int caseLegaHoldID, int lhnStatusID)
        {
       
         
                var currentDateTime = DateTime.UtcNow;
                var entityLhnModel = new EntityLegalHoldNotice
                {
                    UUID = Guid.NewGuid(),
                    EntityID = EntityID,
                    EntityTypeID = EntityTypeID,
                    CaseLegalHoldID = caseLegaHoldID,
                    LHNStatusID = lhnStatusID,
                    CreatedOn = currentDateTime,                
                    ModifiedOn = currentDateTime,              
                    IsDeleted = false

                };
                return entityLhnModel;
  
        }

    }
}
